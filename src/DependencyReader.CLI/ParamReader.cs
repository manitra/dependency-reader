namespace DependencyReader.CLI
{
    /// <summary>
    /// A utility parser which reads the raw command line arguments
    /// and produces a <see cref="Parameters"/> object as output
    /// </summary>
    public class ParamReader
    {
        /// <summary>
        /// Parses the raw command line parameters
        /// </summary>
        /// <param name="args"></param>
        /// <returns>A strongly typed <see cref="Parameters"/> object</returns>
        public virtual Parameters Read(string[] args)
        {
            return new Parameters
            {
                TargetPath = args.Length > 0 ? args[0] : "."
            };
        }
    }
}