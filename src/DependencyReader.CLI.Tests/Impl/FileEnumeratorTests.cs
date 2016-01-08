using System.Linq;
using DependencyReader.CLI.Impl;
using Moq;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Impl
{
    [TestFixture]
    public class FileEnumeratorTests
    {
        [Test]
        public void Constructor_DoesNotThrow()
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
            Assert.AreEqual("/ring-tone", result.First());
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
            Assert.AreEqual("/ring-tone", result.First());
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
            Assert.AreEqual("/phones/iphone", result[0]);
            Assert.AreEqual("/phones/android", result[1]);
        }

        [Test]
        public void ToString_ContainsImportantInfo()
        {
            var target = new FileSystemItem().SetName("name");

            var result = target.ToString();

            Assert.That(result.Contains(result));
        }
    }
}
