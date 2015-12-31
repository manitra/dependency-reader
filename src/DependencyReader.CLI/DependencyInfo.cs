namespace DependencyReader.CLI
{
    /// <summary>
    /// An entity which represent a direct dependency
    /// </summary>
    public class DependencyInfo
    {
        /// <summary>
        /// The assembly which has a dependency
        /// </summary>
        public AssemblyInfo Parent { get; set; }
        
        /// <summary>
        /// The assembly depended upon
        /// </summary>
        public AssemblyInfo Child { get; set; }
    }
}