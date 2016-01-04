using System.Collections.Generic;
using System.IO;
using System.Linq;
using DependencyReader.CLI.Impl;
using Moq;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Impl
{
    [TestFixture]
    public class ReaderTests
    {
        private readonly List<string> temps = new List<string>();

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

            var result = target.Read(GetType().Assembly.GetName().Name + ".Binaries.lib1.dll").ToArray();
            
            Assert.IsNotEmpty(result);
        }
        
        [Test]
        public void Read_Exe_ReturnsNonEmptyList()
        {
            var target = new Reader(new PathUtility());

            var result = target.Read(GetType().Assembly.GetName().Name + ".Binaries.console1.exe").ToArray();

            Assert.IsNotEmpty(result);
        }


        [TestFixtureSetUp]
        public void StartUp()
        {
            var assembly = GetType().Assembly;
            foreach (var resource in assembly.GetManifestResourceNames())
            {
                var target = Path.GetTempFileName();
                temps.Add(target);
                using (var targetStream = File.OpenWrite(target))
                {
                    assembly.GetManifestResourceStream(resource).CopyTo(targetStream);
                }
            }
        }

        [TestFixtureTearDown]
        public void CleanUp()
        {
            foreach (var file in temps)
            {
                File.Delete(file);
            }
        }

    }
}
