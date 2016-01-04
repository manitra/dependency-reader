using System;
using DependencyReader.CLI.Impl;

namespace DependencyReader.CLI
{
    /// <summary>
    /// This is the entry point of the tool
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // this should be the single point where we 'new' things.
            // idealy, we could use a simplified IoC container.
            var runner = new Runner(
                new ParamReader(),
                new FileEnumerator(
                    new FileSystem(),
                    new PathUtility()
                ),
                new Reader(
                    new PathUtility()
                ),
                new Logger(Console.Out),
                Console.Out
            );
            runner.Execute(args);
        }
    }
}
