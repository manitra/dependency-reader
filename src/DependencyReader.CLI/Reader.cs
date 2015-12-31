using System;
using System.Collections.Generic;

namespace DependencyReader.CLI
{
    public class Reader
    {
        public virtual IEnumerable<DependencyInfo> Read(string assemblyFile)
        {
            yield return new DependencyInfo
            {
                Parent = new AssemblyInfo
                {
                    Name = assemblyFile.Substring(assemblyFile.LastIndexOf('\\')),
                    Version = "1.0"
                },
                Child = new AssemblyInfo
                {
                    Name = "mscorlib",
                    Version = "1.0.0.0"
                }
            };
            yield return new DependencyInfo
            {
                Parent = new AssemblyInfo
                {
                    Name = assemblyFile.Substring(assemblyFile.LastIndexOf('\\')),
                    Version = "1.0"
                },
                Child = new AssemblyInfo
                {
                    Name = "System.Data",
                    Version = "1.0.1.0"
                }
            };
        }
    }
}