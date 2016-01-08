namespace DependencyReader.CLI
{
    /// <summary>
    /// Helper class to play with file system paths
    /// </summary>
    public interface IPathUtility
    {
        /// <summary>
        /// Get the filename part of the path without the extension
        /// eg.  /toto/myfile.json    => myfile
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetFileNameWithoutExtension(string path);

        /// <summary>
        /// Return the file name part including its extension
        /// eg. /toto/myfile.json       => myfile.json
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetFileName(string path);
    }
}