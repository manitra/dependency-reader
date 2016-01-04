using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DependencyReader.CLI.Tests.Fakes
{
    public class FileSystemItem : IFileSystem
    {
        private string name;
        private FileSystemItem parent;
        private readonly IDictionary<string, FileSystemItem> children;

        public FileSystemItem()
            : this(null, "/")
        {
        }

        private FileSystemItem(FileSystemItem parent, string name)
        {
            this.parent = parent;
            this.name = name;
            this.children = new Dictionary<string, FileSystemItem>();
        }

        public FileSystemItem SetName(string name)
        {
            this.name = name;
            return this;
        }

        public FileSystemItem SetChildren(params string[] children)
        {
            return SetChildren(children.Select(child => new FileSystemItem(this, child)));
        }

        public FileSystemItem SetChildren(IEnumerable<FileSystemItem> children)
        {
            this.children.Clear();
            foreach (var child in children)
            {
                child.parent = this;
                this.children.Add(child.name, child);
            }

            return this;
        }

        public bool FileExists(string path)
        {
            var pointer = NavigateTo(path);
            return pointer != null && pointer.children.Count == 0;
        }

        public bool DirectoryExists(string path)
        {
            var pointer = NavigateTo(path);
            return pointer != null && pointer.children.Count > 0;
        }


        public IEnumerable<string> GetEntries(string folderPath)
        {
            var pointer = NavigateTo(folderPath);

            if (pointer == null)
            {
                return new string[0];
            }

            return pointer.children.Select(c => pointer.GetFullPath(c.Value.name));
        }

        public string GetFullPath(string relativePath)
        {
            var result = new StringBuilder();
            var pointer = NavigateTo(relativePath);
            var parts = new Stack<string>();

            while (pointer != null)
            {
                parts.Push(pointer.name);
                pointer = pointer.parent;
            }

            var putSlash = false;
            while (parts.Count > 0)
            {
                if (putSlash)
                {
                    result.Append("/");
                }

                var part = parts.Pop();
                result.Append(part);
                if (part != "/")
                {
                    putSlash = true;
                }
            }

            return result.ToString();
        }

        public override string ToString()
        {
            return name + "(" + children.Count + ")";
        }

        private FileSystemItem NavigateTo(string path)
        {
            var parts = new Queue<string>(path.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries));
            var pointer = this;

            if (parts.Count > 0 && parts.Peek() == "/")
            {
                while (pointer.parent != null)
                    pointer = pointer.parent;
            }

            while (parts.Count > 0)
            {
                if (pointer == null)
                {
                    continue;
                }

                var part = parts.Dequeue();

                if (part.Equals("."))
                {
                    continue;
                }

                if (part.Equals(".."))
                {
                    pointer = pointer.parent;
                    continue;
                }

                pointer.children.TryGetValue(part, out pointer);
            }

            return pointer;
        }
    }
}