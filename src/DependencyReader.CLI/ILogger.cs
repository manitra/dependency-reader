using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI
{
    public interface ILogger
    {
        /// <summary>
        /// Formats a direct dependency for the end user
        /// </summary>
        /// <param name="dep"></param>
        void Log(DependencyInfo dep);
    }
}