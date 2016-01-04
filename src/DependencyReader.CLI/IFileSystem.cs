using System.Collections.Generic;
using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI
{
    /// <summary>
    /// Class which wraps file system static methods to ease unit tests
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// returns true if the given path points to an existing element and that element is a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool FileExists(string path);

        /// <summary>
        /// returns true if the given path points to an existing element and that element is a directory
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool DirectoryExists(string path);

        /// <summary>
        /// Returns all the file system which are direct children of the given path including files and directories
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        IEnumerable<string> GetEntries(string folderPath);
        
        /// <summary>
        /// Resolve a relative url into its absolute form
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        string GetFullPath(string relativePath);
    }
}