namespace DependencyReader.CLI.Entities
{
    /// <summary>
    /// An entity which represent all the possible command line option of this tool
    /// </summary>
    public class CliParameters
    {
        /// <summary>
        /// The path where the tool will start to search for dll and exe files.
        /// If ommited, the tool will use the current folder
        /// </summary>
        public string TargetPath { get; set; }
    }
}