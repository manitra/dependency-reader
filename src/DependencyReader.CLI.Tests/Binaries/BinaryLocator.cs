using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyReader.CLI.Tests.Binaries
{
    public class BinaryLocator
    {
        public static string Location
        {
            get { return typeof(BinaryLocator).Namespace; }
        }
    }
}
