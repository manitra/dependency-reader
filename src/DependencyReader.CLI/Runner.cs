using System;
using System.IO;

namespace DependencyReader.CLI
{
    /// <summary>
    /// The orchestrator component
    /// </summary>
    public class Runner
    {
        private readonly ParamReader paramReader;
        private readonly FileEnumerator fileEnumerator;
        private readonly Reader reader;
        private readonly Logger logger;
        private readonly TextWriter stdOutput;

        /// <summary>
        /// Constructs a <see cref="Runner"/> object with all its dependencies
        /// </summary>
        /// <param name="paramReader"></param>
        /// <param name="fileEnumerator"></param>
        /// <param name="reader"></param>
        /// <param name="logger"></param>
        /// <param name="stdOutput"></param>
        public Runner(ParamReader paramReader, FileEnumerator fileEnumerator, Reader reader, Logger logger, TextWriter stdOutput)
        {
            this.paramReader = paramReader;
            this.fileEnumerator = fileEnumerator;
            this.reader = reader;
            this.logger = logger;
            this.stdOutput = stdOutput;
        }

        /// <summary>
        /// Execute the tool
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual int Execute(string[] args)
        {
            try
            {
                var param = paramReader.Read(args);

                foreach (var assemblyFile in fileEnumerator.Find("(exe|dll)$", param.TargetPath))
                {
                    foreach (var dependency in reader.Read(assemblyFile))
                    {
                        logger.Log(dependency);
                    }
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