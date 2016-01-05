using System;
using DependencyReader.CLI.Impl;

namespace DependencyReader.CLI
{
    /// <summary>
    /// Entry point of the CLI
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the CLI
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // this should be the single point where we 'new' things.
            // idealy, we could use a simplified IoC container.
            new Runner(
                new ParamReader(),
                new FileEnumerator(
                    new FileSystem(),
                    new PathUtility()
                ),
                new Reader(
                    new PathUtility()
                ),
                new IndirectDependencyGenerator(),
                new Logger(Console.Out),
                Console.Out
            ).Execute(args);
        }
    }
}
