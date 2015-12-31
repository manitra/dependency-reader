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
            foreach (var child in info.Children)
            {
                yield return new DependencyInfo
                {
                    Parent = info.Parent,
                    Child = new AssemblyInfo
                    {
                        Name = child.Name,
                        Version = child.Version.ToString()
                    }
                };
            }
        }

        private AssemblyLoadingInfo Load(string assemblyFile)
        {
            var result = new AssemblyLoadingInfo();
            try
            {
                var parent = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
                var parentName = parent.GetName();
                result.Parent = new AssemblyInfo
                {
                    Name = parentName.Name,
                    Version = parentName.Version.ToString()
                };
                result.Children = parent.GetReferencedAssemblies()
                    .Select(a => new AssemblyInfo
                    {
                        Name = a.Name,
                        Version = a.Version.ToString()
                    })
                    .ToArray();
            }
            catch (Exception ex)
            {
                return new AssemblyLoadingInfo
                {
                    Parent = new AssemblyInfo
                    {
                        Name = Path.GetFileNameWithoutExtension(assemblyFile) + "(native)",
                        Version = "0.0.0.0"
                    },
                    Children = new []
                    {
                        new AssemblyInfo
                        {
                            Name = "(unknown)",
                            Version = "0.0.0.0"
                        },
                    }
                };
            }

            return result;
        }

    }
}