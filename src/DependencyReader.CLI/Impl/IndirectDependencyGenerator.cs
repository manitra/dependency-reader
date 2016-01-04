using System;
using System.Collections.Generic;
using System.Linq;
using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI.Impl
{
    public class IndirectDependencyGenerator : IReader
    {
        private readonly IReader innerReader;

        public IndirectDependencyGenerator(IReader innerReader)
        {
            this.innerReader = innerReader;
        }

        public IEnumerable<DependencyInfo> Read(string assemblyFilePath)
        {
            var direct = innerReader.Read(assemblyFilePath);
            var directMap = new Dictionary<AssemblyInfo, Node>();
            foreach (var dep in direct)
            {
                if (!directMap.ContainsKey(dep.Parent))
                {
                    directMap[dep.Parent] = new Node(dep.Parent);
                }

                if (!directMap.ContainsKey(dep.Child))
                {
                    directMap[dep.Child] = new Node(dep.Child);
                }

                directMap[dep.Parent].Add(directMap[dep.Child]);
            }

            return GetBottomUpDeps(directMap.Values.Where(n => n.Children.Count == 0));
        }

        private IEnumerable<DependencyInfo> GetBottomUpDeps(IEnumerable<Node> leaves)
        {
            var todo = new Queue<Node>(leaves);

            while (todo.Count > 0)
            {
                var current = todo.Dequeue();
                foreach (var dep in GetDescendantDeps(current))
                {
                    yield return dep;
                }

                foreach (var parent in current.Parents)
                    todo.Enqueue(parent);
            }
        }

        private IEnumerable<DependencyInfo> GetDescendantDeps(Node ancestor)
        {
            var todo = new Queue<Node>(ancestor.Children);

            while (todo.Count > 0)
            {
                var current = todo.Dequeue();
                yield return new DependencyInfo
                {
                    Parent = ancestor.Key,
                    Child = current.Key,
                    Distance = CalculateDistance(ancestor, current)
                };

                foreach (var child in current.Children)
                    todo.Enqueue(child);
            }
        }

        private int CalculateDistance(Node parent, Node child)
        {
            var todo = new Queue<Tuple<Node, int>>();
            todo.Enqueue(Tuple.Create(parent, 0));
            
            while (todo.Count > 0)
            {
                var current = todo.Dequeue();

                if (current.Item1 == child)
                {
                    return current.Item2;
                }

                foreach (var c in current.Item1.Children)
                {
                    todo.Enqueue(Tuple.Create(c, current.Item2 + 1));
                }
            }

            return -1;
        }
    }

    internal class Node
    {
        private readonly List<Node> parents;
        private readonly AssemblyInfo key;
        private readonly List<Node> children;

        public Node(AssemblyInfo key)
        {
            this.key = key;
            parents = new List<Node>();
            children = new List<Node>();
        }

        public AssemblyInfo Key
        {
            get { return this.key; }
        }

        public IList<Node> Children
        {
            get { return children.AsReadOnly(); }
        }

        public IList<Node> Parents
        {
            get { return parents.AsReadOnly(); }
        }

        public void Add(Node child)
        {
            child.parents.Add(this);
            this.children.Add(child);
        }
    }
}