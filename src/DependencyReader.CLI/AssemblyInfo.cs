namespace DependencyReader.CLI
{
    /// <summary>
    /// An entity which represents the name of assembly
    /// </summary>
    public class AssemblyInfo
    {
        /// <summary>
        /// The simple name of an assembly: 'System.Data'
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The string representation of the version of an assembly: '2.4.5.0'
        /// </summary>
        public string Version { get; set; }
    }
}