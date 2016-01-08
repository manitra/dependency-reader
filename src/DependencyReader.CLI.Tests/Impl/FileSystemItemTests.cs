using System.Linq;
using DependencyReader.CLI.Impl;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Impl
{
    [TestFixture]
    public class FileSystemItemTests
    {
        [Test]
        public void Construction_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                new FileSystemItem();
            });
        }

        [Test]
        public void SetName_IsReflectedInToString()
        {
            var target = new FileSystemItem();

            target.SetName("yay");

            Assert.That(target.ToString().Contains("yay"));
        }

        [Test]
        public void SetChildren_CanBeCheckedWithHasDirectoryAndHasFile()
        {
            var target = new FileSystemItem();

            target.SetChildren("existing");

            Assert.That(target.FileExists("existing"));
            Assert.IsFalse(target.FileExists("not-existing"));
        }

        [Test]
        public void FileExists_WithNestedFolder_Works()
        {
            var target = Sample();

            Assert.That(target.FileExists("/var/lib/file"));
            Assert.That(target.FileExists("var/lib/file"));
            Assert.That(target.FileExists("var/lib/../lib/file"));

            Assert.IsFalse(target.FileExists("var/non-existing"));
            Assert.IsFalse(target.FileExists("var"));
            Assert.IsFalse(target.FileExists("var/lib"));
        }

        [Test]
        public void DirectoryExists_WithNestedFolders_Works()
        {
            var target = Sample();

            Assert.That(target.DirectoryExists("/var/lib"));
            Assert.That(target.DirectoryExists("var/lib"));
            Assert.That(target.DirectoryExists("var/lib/"));
            Assert.That(target.DirectoryExists("var/lib/../lib"));

            Assert.IsFalse(target.DirectoryExists("var/non-existing"));
            Assert.IsFalse(target.DirectoryExists("var/lib/file"));
        }

        [Test]
        public void GetEntries_WithNestedFolders_Works()
        {
            var target = Sample();

            var result = target.GetEntries(".").ToArray();

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("/var", result.First());
        }

        [Test]
        public void GetEntries_WithNonExistingPath_ReturnsEmpty()
        {
            var target = Sample();

            var result = target.GetEntries("/var/lib/apache").ToArray();

            Assert.IsEmpty(result);
        }

        [Test]
        public void GetEntries_WithAbsolutePath_Works()
        {
            var target = new FileSystemItem().SetName("target");
            new FileSystemItem().SetChildren(new[]
            {
                target,
                new FileSystemItem().SetName("sibling").SetChildren(
                    "nephew1",
                    "nephew2"
                )
            });

            var result = target.GetEntries("/sibling").ToArray();

            Assert.AreEqual(2, result.Length);
            Assert.Contains("/sibling/nephew1", result);
            Assert.Contains("/sibling/nephew2", result);
        }

        [Test]
        public void GetFullPath_WithRelative_ReturnsAbsolute()
        {
            var target = Sample();

            var result = target.GetFullPath("var/lib/file/..");

            Assert.AreEqual("/var/lib", result);
        }

        private FileSystemItem Sample()
        {
            return new FileSystemItem().SetChildren(new[] {
                new FileSystemItem().SetName("var").SetChildren(new [] {
                    new FileSystemItem().SetName("lib").SetChildren(
                        "file"
                        )
                })
            });
        }
    }
}
