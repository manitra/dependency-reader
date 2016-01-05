using System;
using System.IO;
using System.Linq;

namespace DependencyReader.CLI.Impl
{
    /// <summary>
    /// Orchestrates the dependency finding for a given path
    /// </summary>
    public class Runner : IRunner
    {
        private readonly IParamReader paramReader;
        private readonly IFileEnumerator fileEnumerator;
        private readonly IReader reader;
        private readonly IDependencyFilter filter;
        private readonly ILogger logger;
        private readonly TextWriter stdOutput;

        /// <summary>
        /// Constructs a <see cref="Runner"/> object with all its dependencies
        /// </summary>
        /// <param name="paramReader"></param>
        /// <param name="fileEnumerator"></param>
        /// <param name="reader"></param>
        /// <param name="filter"></param>
        /// <param name="logger"></param>
        /// <param name="stdOutput"></param>
        public Runner(IParamReader paramReader, IFileEnumerator fileEnumerator, IReader reader, IDependencyFilter filter, ILogger logger, TextWriter stdOutput)
        {
            this.paramReader = paramReader;
            this.fileEnumerator = fileEnumerator;
            this.reader = reader;
            this.filter = filter;
            this.logger = logger;
            this.stdOutput = stdOutput;
        }

        /// <summary>
        /// Execute the tool
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Execute(string[] args)
        {
            try
            {
                var param = paramReader.Read(args);

                var rows = fileEnumerator
                    .Find("(exe|dll)$", param.TargetPath)
                    .Select(reader.Read)
                    .SelectMany(list => list);

                foreach (var dependency in filter.Filter(rows))
                {
                    logger.Log(dependency);
                }
            }
            catch (Exception ex)
            {
                stdOutput.WriteLine(ex);
                return 256;
            }

            return 0;
        }
    }
}