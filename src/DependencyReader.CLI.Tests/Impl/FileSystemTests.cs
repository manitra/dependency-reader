using System.Collections.Generic;
using System.IO;
using System.Linq;
using DependencyReader.CLI.Impl;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Impl
{
    [TestFixture]
    public class FileSystemTests
    {
        private readonly string me;

        public FileSystemTests()
        {
            me = GetType().Assembly.GetName().Name + ".dll";
        }

        [Test]
        [TestCaseSource("All")]
        public void FileExists_ReturnsSameValueAsWrappedClass(string path)
        {
            var expected = File.Exists(path);
            var target = new FileSystem();

            var result = target.FileExists(path);

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCaseSource("All")]
        public void DirectoryExists_ReturnsSameValueAsWrappedClass(string path)
        {
            var expected = Directory.Exists(path);
            var target = new FileSystem();

            var result = target.DirectoryExists(path);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetEntries_ReturnsMySelfOnCurrentDirectory()
        {
            var target = new FileSystem();

            var result = target.GetEntries(".").ToArray();

            Assert.IsNotEmpty(result, "GetEntries() should at least return this assembly");
            Assert.That(result.Any(row => row.Contains(me)));
        }

        [Test]
        [TestCaseSource("All")]
        public void GetFullPath_ReturnsSameValueAsWrappedClass(string path)
        {
            var expected = Path.GetFullPath(path);
            var target = new FileSystem();

            var result = target.GetFullPath(path);

            Assert.AreEqual(expected, result);
        }

        private IEnumerable<object> All()
        {
            yield return me; //existing file
            yield return "."; //existing directory
            yield return "NonExistingFile.txt"; //non existing file
            yield return "NonExistingDirectory/"; //non existing directory
        }
    }
}
