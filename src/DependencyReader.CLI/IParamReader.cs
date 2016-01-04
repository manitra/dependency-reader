using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI
{
    public interface IParamReader
    {
        /// <summary>
        /// Parses the raw command line parameters
        /// </summary>
        /// <param name="args"></param>
        /// <returns>A strongly typed <see cref="CliParameters"/> object</returns>
        CliParameters Read(string[] args);
    }
}