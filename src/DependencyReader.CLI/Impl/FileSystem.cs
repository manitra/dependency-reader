using System.Collections.Generic;
using System.IO;

namespace DependencyReader.CLI.Impl
{
    public class FileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public IEnumerable<string> GetEntries(string folderPath)
        {
            return Directory.GetFileSystemEntries(folderPath, "*", SearchOption.TopDirectoryOnly);
        }

        public string GetFullPath(string relativePath)
        {
            return Path.GetFullPath(relativePath);
        }
    }
}