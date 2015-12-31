using System;
using System.Collections.Generic;

namespace DependencyReader.CLI
{
    public class Reader
    {
        public virtual IEnumerable<AssemblyInfo> Read(string assemblyFile)
        {
            throw new NotImplementedException();
        }
    }
}