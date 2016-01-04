namespace DependencyReader.CLI
{
    /// <summary>
    /// Runs the dependency reading process on a given path
    /// </summary>
    public interface IRunner
    {
        /// <summary>
        /// Runs the dependency reading process on a given path
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        int Execute(string[] args);
    }
}