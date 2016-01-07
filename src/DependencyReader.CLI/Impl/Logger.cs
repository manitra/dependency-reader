using System;
using System.IO;
using System.Text;
using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI.Impl
{
    /// <summary>
    /// Formats entities for the end user
    /// </summary>
    public class Logger : ILogger
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
        public void Log(DependencyInfo dep)
        {
            output.Write(
                "{0} {1} {2} {3} {4} {5}",
                dep.Parent.Name,
                dep.Parent.Version,
                VisualDistance(dep.Distance),
                dep.Child.Name,
                dep.Child.Version,
                dep.Distance
            );

            if (dep.Path.Count > 0)
            {
                output.Write(" ( ");
            }

            foreach (var part in dep.Path)
            {
                output.Write(" {0} {1}", part.Name, part.Version);
            }

            if (dep.Path.Count > 0)
            {
                output.Write(" )");
            }

            output.Write(Environment.NewLine);
        }

        private string VisualDistance(int distance)
        {
            var result = new StringBuilder(distance);
            for (int i = 0; i < distance; i++)
            {
                result.Append(">");
            }
            return result.ToString();
        }
    }
}