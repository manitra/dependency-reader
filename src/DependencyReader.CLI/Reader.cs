using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DependencyReader.CLI
{
    public class Reader
    {
        private readonly ISet<string> loadedAssemblies;

        public Reader()
        {
            loadedAssemblies = new HashSet<string>(
                AppDomain.CurrentDomain
                    .ReflectionOnlyGetAssemblies()
                    .Select(a => a.GetName().Name)
            );
        }

        public virtual IEnumerable<DependencyInfo> Read(string assemblyFile)
        {
            var name = Path.GetFileNameWithoutExtension(assemblyFile);
            if (loadedAssemblies.Contains(name))
            {
                yield break;
            }
            loadedAssemblies.Add(name);

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