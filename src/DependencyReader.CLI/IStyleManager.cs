using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI
{
    /// <summary>
    /// Provides methods to set the Console style using semantics
    /// </summary>
    public interface IStyleManager
    {
        /// <summary>
        /// Set the style of the console
        /// </summary>
        /// <param name="style"></param>
        /// <param name="group"></param>
        void Set(Style style, StyleGroup group);
    }
}