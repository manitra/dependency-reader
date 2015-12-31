using System.Collections.Generic;

namespace DependencyReader.CLI
{
    public class AssemblyLoadingInfo
    {
        public AssemblyInfo Parent { get; set; }
        public ICollection<AssemblyInfo> Children { get; set; }
    }
}