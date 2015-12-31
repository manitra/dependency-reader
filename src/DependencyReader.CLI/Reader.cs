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

            var info = Load(assemblyFile);
            foreach (var child in info.Item2)
            {
                yield return new DependencyInfo
                {
                    Parent = new AssemblyInfo
                    {
                        Name = info.Item1.Name,
                        Version = info.Item1.Version.ToString()
                    },
                    Child = new AssemblyInfo
                    {
                        Name = child.Name,
                        Version = child.Version.ToString()
                    }
                };
            }
        }

        private Tuple<AssemblyName, AssemblyName[]> Load(string assemblyFile)
        {
            try
            {
                var parent = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
                return Tuple.Create(
                    parent.GetName(),
                    parent.GetReferencedAssemblies()
                );
            }
            catch (Exception ex)
            {
                return Tuple.Create(
                    new AssemblyName(Path.GetFileNameWithoutExtension(assemblyFile) + "-(native)") { Version = new Version(1, 0) },
                    new[]
                    {
                        new AssemblyName("(unknown)") { Version = new Version(1, 0) },
                    }
                );
            }
        }
    }
}