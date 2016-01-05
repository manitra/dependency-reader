using System.Collections.Generic;
using System.Linq;
using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI.Impl
{
    /// <summary>
    /// This filter actually adds dependencies and skip none of the given input.
    /// It generates all the indirect dependencies which are deduced using transitivity
    /// If A depends on B and B depends on C then this implementation will yield
    /// - A depends on B
    /// - B depends on C
    /// - A depends on C  (this is the newly created one)
    /// </summary>
    public class IndirectDependencyGenerator : IDependencyFilter
    {
        public IEnumerable<DependencyInfo> Filter(IEnumerable<DependencyInfo> input)
        {
            var directMap = new Dictionary<AssemblyInfo, Node>();
            foreach (var dep in input)
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

            return GetDescendantDeps(directMap.Values);
        }

        private IEnumerable<DependencyInfo> GetDescendantDeps(IEnumerable<Node> parents)
        {
            foreach (var parent in parents)
            {

                var visited = parent.Children.ToDictionary(
                    child => child.Key,
                    child => new TraversingInfo(child));

                var todo = new Queue<TraversingInfo>(visited.Values);

                while (todo.Count > 0)
                {
                    var current = todo.Dequeue();

                    yield return new DependencyInfo
                    {
                        Parent = parent.Key,
                        Child = current.Node.Key,
                        Distance = current.Distance,
                        Path = current.Path
                    };

                    foreach (var child in current.Node.Children)
                    {
                        if (!visited.ContainsKey(child.Key))
                        {
                            var item = new TraversingInfo(child).SetParent(current);
                            visited.Add(child.Key, item);
                            todo.Enqueue(item);
                        }
                    }
                }
            }
        }

        class TraversingInfo
        {
            public Node Node { get; private set; }
            public int Distance { get; private set; }
            public IList<AssemblyInfo> Path { get; private set; }

            public TraversingInfo(Node node)
            {
                this.Node = node;
                Distance = 1;
                Path = new List<AssemblyInfo>();
            }

            public TraversingInfo SetParent(TraversingInfo parent)
            {
                this.Distance = parent.Distance + 1;
                this.Path = parent.Path.ToList();
                this.Path.Add(Node.Key);

                return this;
            }
        }

        class Node
        {
            private readonly AssemblyInfo key;
            private readonly List<Node> children;

            public Node(AssemblyInfo key)
            {
                this.key = key;
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

            public void Add(Node child)
            {
                this.children.Add(child);
            }
        }
    }
}