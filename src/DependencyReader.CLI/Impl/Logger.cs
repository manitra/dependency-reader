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
        private readonly IStyleManager style;

        /// <summary>
        /// Construct a <see cref="Logger"/> with the given output text writer
        /// </summary>
        /// <param name="output"></param>
        /// <param name="style"></param>
        public Logger(TextWriter output, IStyleManager style)
        {
            this.output = output;
            this.style = style;
        }

        /// <summary>
        /// Formats a direct dependency for the end user
        /// </summary>
        /// <param name="dep"></param>
        public void Log(DependencyInfo dep)
        {
            Assembly(dep.Parent);
            Separator();
            VisualDistance(dep.Distance);
            Separator();
            Assembly(dep.Child);
            Separator();

            using (The(Style.Primary, StyleGroup.Alternative))
            {
                output.Write(dep.Distance);
            }

            if (dep.Path.Count > 0)
            {
                Separator();
                using (The(Style.Primary))
                {
                    output.Write("(");
                }
            }

            foreach (var part in dep.Path)
            {
                Separator();
                Assembly(part);
            }

            if (dep.Path.Count > 0)
            {
                Separator();
                using (The(Style.Primary))
                {
                    output.Write(")");
                }
            }

            NewLine();
        }


        private void Assembly(AssemblyInfo assembly)
        {
            using (The(Style.Primary))
            {
                output.Write(assembly.Name);
            }

            Separator();

            output.Write(assembly.Version);
        }

        private void VisualDistance(int distance)
        {
            using (The(Style.Primary, StyleGroup.Alternative))
            {
                for (int i = 0; i < distance; i++)
                {
                    output.Write(">");
                }
            }
        }

        private void NewLine()
        {
            output.Write(Environment.NewLine);
        }

        private void Separator()
        {
            output.Write(" ");
        }

        /// <summary>
        /// Small syntaxic sugar for using the <see cref="StyleReset"/> class.
        /// you can write 
        /// <code>
        /// using (The(Style.Primary))
        ///     output.Write("..");
        /// </code>
        /// </summary>
        /// <param name="style"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        private StyleReset The(Style style, StyleGroup group = StyleGroup.Normal)
        {
            return new StyleReset(style, group, this.style);
        }

        /// <summary>
        /// small utility class to ensure that style is always reset to default
        /// </summary>
        class StyleReset : IDisposable
        {
            private readonly IStyleManager style;

            public StyleReset(Style style, StyleGroup group, IStyleManager styleMgr)
            {
                this.style = styleMgr;
                this.style.Set(style, group);
            }

            public void Dispose()
            {
                style.Set(Style.Normal, StyleGroup.Normal);
            }
        }
    }
}