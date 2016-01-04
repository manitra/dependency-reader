using System.IO;

namespace DependencyReader.CLI.Impl
{
    class PathUtility : IPathUtility
    {
        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public string Combine(params string[] parts)
        {
            return Path.Combine(parts);
        }
    }
}