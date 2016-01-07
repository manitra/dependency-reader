using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DependencyReader.CLI.Impl;
using DependencyReader.CLI.Tests.Binaries;
using Moq;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Impl
{
    [TestFixture]
    public class ReaderTests
    {
        private readonly Dictionary<string, string> temps = new Dictionary<string, string>();

        public ReaderTests()
        {
            StartUp();
        }

        [Test]
        public void Reader_Construction_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                new Reader(Mock.Of<IPathUtility>());
            });
        }

        [Test]
        public void Read_Dll_ReturnsNonEmptyList()
        {
            var target = new Reader(new PathUtility());

            var result = target.Read(temps[BinaryLocator.Location + ".lib1.dll"]).ToArray();

            Assert.AreEqual(1, result.Length);
            Assert.IsTrue(result.Any(dep => dep.Child.Name == "mscorlib"));
        }

        [Test]
        public void Read_Exe_ReturnsNonEmptyList()
        {
            var target = new Reader(new PathUtility());

            var result = target.Read(temps[BinaryLocator.Location + ".console1.exe"]).ToArray();

            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(result.Any(dep => dep.Child.Name == "mscorlib"));
            Assert.IsTrue(result.Any(dep => dep.Child.Name == "lib1"));
        }


        // For some reason, the attribute is ignored when using Resharper 8.1
        // FixtureSetup is obsolete and ignored as well
        // TODO(manitra): find out why Resharper 8.1 ignore OneTimeSetUp and FixtureSetup attributes
        // [OneTimeSetUp]
        public void StartUp()
        {
            var assembly = GetType().Assembly;
            foreach (var resource in assembly.GetManifestResourceNames())
            {
                var target = Path.Combine(BaseFolder, resource);
                temps.Add(resource, target);
                using (var targetStream = File.OpenWrite(target))
                {
                    assembly.GetManifestResourceStream(resource).CopyTo(targetStream);
                }
            }
        }

        private string BaseFolder
        {
            get { return Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath); }
        }

    }
}
