using System;
using System.Collections.Generic;
using System.Reflection;

namespace DependencyReader.CLI
{
    public class Reader
    {
        public virtual IEnumerable<DependencyInfo> Read(string assemblyFile)
        {
            var parent = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
            var parentName = parent.GetName();
            var children = parent.GetReferencedAssemblies();

            foreach (var child in children)
            {
                yield return new DependencyInfo
                {
                    Parent = new AssemblyInfo
                    {
                        Name = parentName.Name,
                        Version = parentName.Version.ToString()
                    },
                    Child = new AssemblyInfo
                    {
                        Name = child.Name,
                        Version = child.Version.ToString()
                    }
                };
            }
        }
    }
}