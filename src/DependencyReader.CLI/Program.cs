using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyReader.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new Runner(
                new ParamReader(),
                new FileEnumerator(),
                new Reader(),
                new Logger()
            );
            runner.Execute(args);
        }
    }
}
