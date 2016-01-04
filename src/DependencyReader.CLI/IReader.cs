using System.Collections.Generic;
using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI
{
    public interface IReader
    {
        /// <summary>
        /// Loads the assembly located at the given <paramref name="assemblyFilePath"/>
        /// and returns a list of direct dependency which are kind of tuple
        /// </summary>
        /// <param name="assemblyFilePath"></param>
        /// <returns></returns>
        IEnumerable<DependencyInfo> Read(string assemblyFilePath);
    }
}