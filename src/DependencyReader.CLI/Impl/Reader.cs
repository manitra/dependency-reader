using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI.Impl
{
    /// <summary>
    /// This component is the actually dependency reader.
    /// </summary>
    public class Reader : IReader
    {
        private readonly ISet<string> loadedAssemblies;
        private readonly IPathUtility pathUtility;

        /// <summary>
        /// Construct a <see cref="Reader"/> object
        /// </summary>
        public Reader(IPathUtility pathUtility)
        {
            this.pathUtility = pathUtility;

            loadedAssemblies = new HashSet<string>(
                AppDomain.CurrentDomain
                    .ReflectionOnlyGetAssemblies()
                    .Select(a => a.GetName().Name)
            );
        }

        /// <summary>
        /// Loads the assembly located at the given <paramref name="assemblyFilePath"/>
        /// and returns a list of direct dependency which are kind of tuple
        /// </summary>
        /// <param name="assemblyFilePath"></param>
        /// <returns></returns>
        public IEnumerable<DependencyInfo> Read(string assemblyFilePath)
        {
            var name = pathUtility.GetFileNameWithoutExtension(assemblyFilePath);
            if (loadedAssemblies.Contains(name))
            {
                yield break;
            }
            loadedAssemblies.Add(name);

            var info = Load(assemblyFilePath);
            foreach (var child in info.Children)
            {
                yield return new DependencyInfo
                {
                    Parent = info.Parent,
                    Child = child,
                    Distance = 1,
                    Path = new List<AssemblyInfo>()
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
            catch (BadImageFormatException)
            {
                return new AssemblyLoadingInfo
                {
                    Parent = new AssemblyInfo
                    {
                        Name = pathUtility.GetFileNameWithoutExtension(assemblyFile) + "(native)",
                        Version = "0.0.0.0"
                    },
                    Children = new []
                    {
                        new AssemblyInfo
                        {
                            Name = "(unknown)",
                            Version = "0.0.0.0"
                        }
                    }
                };
            }

            return result;
        }

    }
}