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
            var direct = innerReader.Read(assemblyFilePath).ToArray();
            var directMap = new Dictionary<AssemblyInfo, Node>(direct.Length);
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

            return direct;
        }
    }

    internal class Node
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

        public void Add(Node child)
        {
            this.children.Add(child);
        }
    }
}