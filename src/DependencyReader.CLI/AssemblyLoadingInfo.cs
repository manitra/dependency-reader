using System.Collections.Generic;

namespace DependencyReader.CLI
{
    /// <summary>
    /// An entity which contains an <see cref="AssemblyInfo"/> along with
    /// its like of dependencies which is a collection of <see cref="AssemblyInfo"/>
    /// as well
    /// </summary>
    public class AssemblyLoadingInfo
    {
        /// <summary>
        /// The main assembly
        /// </summary>
        public AssemblyInfo Parent { get; set; }
        
        /// <summary>
        /// The list of its dependencies
        /// </summary>
        public ICollection<AssemblyInfo> Children { get; set; }
    }
}