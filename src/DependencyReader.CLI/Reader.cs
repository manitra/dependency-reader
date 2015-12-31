﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DependencyReader.CLI
{
    /// <summary>
    /// This component is the actually dependency reader.
    /// </summary>
    public class Reader
    {
        private readonly ISet<string> loadedAssemblies;

        /// <summary>
        /// Construct a <see cref="Reader"/> object
        /// </summary>
        public Reader()
        {
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
        public virtual IEnumerable<DependencyInfo> Read(string assemblyFilePath)
        {
            var name = Path.GetFileNameWithoutExtension(assemblyFilePath);
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
                    Child = child
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