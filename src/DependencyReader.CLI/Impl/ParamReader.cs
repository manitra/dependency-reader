using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI.Impl
{
    /// <summary>
    /// A utility parser which reads the raw command line arguments
    /// and produces a <see cref="CliParameters"/> object as output
    /// </summary>
    public class ParamReader : IParamReader
    {
        /// <summary>
        /// Parses the raw command line parameters
        /// </summary>
        /// <param name="args"></param>
        /// <returns>A strongly typed <see cref="CliParameters"/> object</returns>
        public virtual CliParameters Read(string[] args)
        {
            return new CliParameters
            {
                TargetPath = args.Length > 0 ? args[0] : "."
            };
        }
    }
}