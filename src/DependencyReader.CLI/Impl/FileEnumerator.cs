using System.Collections.Generic;
using System.Text.RegularExpressions;
using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI.Impl
{
    /// <summary>
    /// A class which enumerates all the files matching a name filter inside a target path
    /// </summary>
    public class FileEnumerator : IFileEnumerator
    {
        private readonly IFileSystem fileSystem;
        private readonly IPathUtility pathUtility;

        /// <summary>
        /// Constructs a <see cref="FileEnumerator"/>
        /// </summary>
        /// <param name="fileSystem"></param>
        public FileEnumerator(IFileSystem fileSystem, IPathUtility pathUtility)
        {
            this.fileSystem = fileSystem;
            this.pathUtility = pathUtility;
        }

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
        public virtual IEnumerable<string> Find(string nameFilterRegex, string targetPath)
        {
            var matcher = new Regex(nameFilterRegex);
            var items = new Queue<string>();
            items.Enqueue(fileSystem.GetFullPath(targetPath));

            while (items.Count > 0)
            {
                var item = items.Dequeue();
                if (fileSystem.FileExists(item))
                {
                    if (matcher.IsMatch(pathUtility.GetFileName(item)))
                    {
                        yield return item;
                    }
                }
                else if (fileSystem.DirectoryExists(item))
                {
                    foreach (var child in fileSystem.GetEntries(item))
                    {
                        items.Enqueue(child);
                    }
                }
            }
        }
    }
}