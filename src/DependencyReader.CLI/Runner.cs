﻿namespace DependencyReader.CLI
{
    public class Runner
    {
        private readonly ParamReader paramReader;
        private readonly FileEnumerator fileEnumerator;
        private readonly Reader reader;
        private readonly Logger logger;

        public Runner(ParamReader paramReader, FileEnumerator fileEnumerator, Reader reader, Logger logger)
        {
            this.paramReader = paramReader;
            this.fileEnumerator = fileEnumerator;
            this.reader = reader;
            this.logger = logger;
        }

        public virtual Runner Execute(string[] args)
        {
            var param = paramReader.Read(args);
            foreach (var assemblyFile in fileEnumerator.Find("(exe|dll)$", param.TargetPath))
            {
                foreach (var dependency in reader.Read(assemblyFile))
                {
                    logger.Log(dependency.Name, dependency.Version);
                }
            }

            return this;
        }
    }
}