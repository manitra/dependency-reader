using System;
using System.IO;

namespace DependencyReader.CLI
{
    /// <summary>
    /// Formats entities for the end user
    /// </summary>
    public class Logger
    {
        private readonly TextWriter output;

        /// <summary>
        /// Construct a <see cref="Logger"/> with the given output text writer
        /// </summary>
        /// <param name="output"></param>
        public Logger(TextWriter output)
        {
            this.output = output;
        }

        /// <summary>
        /// Formats a direct dependency for the end user
        /// </summary>
        /// <param name="dep"></param>
        public virtual void Log(DependencyInfo dep)
        {
            output.WriteLine(
                "{0}-{1} {2}-{3}",
                dep.Parent.Name,
                dep.Parent.Version,
                dep.Child.Name,
                dep.Child.Version
            );
        }
    }
}