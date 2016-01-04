using System.Linq;
using DependencyReader.CLI.Impl;
using DependencyReader.CLI.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Impl
{
    [TestFixture]
    public class FileEnumeratorTests
    {
        [Test]
        public void FileEnumerator_Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                new FileEnumerator(
                    Mock.Of<IFileSystem>(),
                    Mock.Of<IPathUtility>()
                );
            });
        }

        [Test]
        public void Find_WithMatchingRegex_ReturnsNonEmptyList()
        {
            var target = new FileEnumerator(
                new FileSystemItem().SetChildren("tone-ring", "ring-tone", "phone"),
                new PathUtility()
            );

            var result = target.Find("tone$", ".").ToArray();

            Assert.AreEqual(1, result.Length);
            Assert.IsTrue(result.First().EndsWith("/ring-tone"));
        }

        [Test]
        public void Find_WithNonMatchingRegex_ReturnsEmptyList()
        {
            var target = new FileEnumerator(
                new FileSystemItem().SetChildren("tone-ring", "ring-tone", "phone"),
                new PathUtility()
            );

            var result = target.Find("abc", ".").ToArray();

            Assert.IsEmpty(result);
        }

        [Test]
        public void Find_WithFilePath_ReturnsSingleEntry()
        {
            var target = new FileEnumerator(
                new FileSystemItem().SetChildren("tone-ring", "ring-tone", "phone"),
                new PathUtility()
            );

            var result = target.Find(".*", "ring-tone").ToArray();

            Assert.AreEqual(1, result.Length);
            Assert.IsTrue(result.First().EndsWith("/ring-tone"));
        }

        [Test]
        public void Find_WithFolderPath_ReturnsNestedFiles()
        {
            var target = new FileEnumerator(
                new FileSystemItem().SetChildren(new[] {
                    new FileSystemItem().SetName("phones").SetChildren("iphone", "android"),
                    new FileSystemItem().SetName("anything-else")
                }),
                new PathUtility()
            );

            var result = target.Find(".*", "phones").ToArray();

            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result[0].EndsWith("/iphone"));
            Assert.IsTrue(result[1].EndsWith("/android"));
        }
    }
}
