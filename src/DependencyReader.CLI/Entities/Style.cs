namespace DependencyReader.CLI.Entities
{
    /// <summary>
    /// Kind of output style for the <see cref="ILogger"/> implementations
    /// </summary>
    public enum Style
    {
        /// <summary>
        /// Default console style
        /// </summary>
        Normal,
        
        /// <summary>
        /// Important info
        /// </summary>
        Primary,

        /// <summary>
        /// Weird thing, need to be fixed but not critical
        /// </summary>
        Warning,

        /// <summary>
        /// A critical error, can't continue unless it's fixed
        /// </summary>
        Error
    }
}