using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DependencyReader.CLI
{
    /// <summary>
    /// A class which enumerates all the files matching a name filter inside a target path
    /// </summary>
    public class FileEnumerator
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
        public virtual IEnumerable<string> Find(string nameFilterRegex, string targetPath)
        {
            var matcher = new Regex(nameFilterRegex);
            var items = new Queue<string>();
            items.Enqueue(Path.GetFullPath(targetPath));

            while (items.Count > 0)
            {
                var item = items.Dequeue();
                if (File.Exists(item))
                {
                    if (matcher.IsMatch(Path.GetFileName(item)))
                    {
                        yield return item;
                    }
                }
                else if (Directory.Exists(item))
                {
                    foreach (var child in Directory.GetFileSystemEntries(item, "*", SearchOption.TopDirectoryOnly))
                    {
                        items.Enqueue(child);
                    }
                }
            }
        }
    }
}