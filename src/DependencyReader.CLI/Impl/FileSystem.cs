using System.IO;
using DependencyReader.CLI.Entities;

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

        public string[] GetEntries(string folderPath, string namePattern, SearchType option)
        {
            return Directory.GetFileSystemEntries(folderPath, namePattern, (SearchOption)option);
        }

        public string GetFullPath(string relativePath)
        {
            return Path.GetFullPath(relativePath);
        }
    }
}