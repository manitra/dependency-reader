using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace DependencyReader.CLI
{
    public class FileEnumerator
    {
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