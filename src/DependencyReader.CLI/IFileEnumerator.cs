using System.Collections.Generic;

namespace DependencyReader.CLI
{
    public interface IFileEnumerator
    {
        /// <summary>
        /// Find all files in the target path whose name matches the given name filter.
        /// If the path points to a file, the method will return that file only.
        /// If the path points to a folder, the method will return all files inside that
        /// folder and its subfolders.
        /// </summary>
        /// <param name="nameFilterRegex">A Regular expression against which file names are checked (eg.  \.exe$ )</param>
        /// <param name="targetPath">The relative or absolute path to a file or a directory.  
        /// </param>
        /// <returns>ALl files within the target path whose name matches the <paramref name="nameFilterRegex"/></returns>
        IEnumerable<string> Find(string nameFilterRegex, string targetPath);
    }
}