using System;
using System.IO;
using DependencyReader.CLI.Entities;
using DependencyReader.CLI.Impl;
using Moq;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Impl
{
    [TestFixture]
    public class RunnerTests
    {
        [Test]
        public void Execute_WithSingleDep_LogsOnce()
        {
            var deps = new[] { new DependencyInfo() };
            var logger = new Mock<ILogger>();
            var target = new Runner(
                Mock.Of<IParamReader>(o => o.Read(It.IsAny<string[]>()) == new CliParameters { TargetPath = "" }),
                Mock.Of<IFileEnumerator>(o => o.Find(It.IsAny<string>(), It.IsAny<string>()) == new[] { "" }),
                Mock.Of<IReader>(o => o.Read(It.IsAny<string>()) == deps),
                Mock.Of<IDependencyFilter>(o => o.Filter(deps) == deps),
                logger.Object,
                Mock.Of<TextWriter>()
            );

            var result = target.Execute(new string[0]);

            logger.Verify(
                o => o.Log(It.IsAny<DependencyInfo>()),
                Times.Exactly(deps.Length),
                "Log should be called once per dependency");
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Execute_WithAnInnerException_ReturnsNonZero()
        {
            var buggyParamReader = new Mock<IParamReader>(); buggyParamReader
                .Setup(o => o.Read(It.IsAny<string[]>()))
                .Throws(new Exception("Bim"));
            var target = new Runner(
                buggyParamReader.Object,
                Mock.Of<IFileEnumerator>(),
                Mock.Of<IReader>(),
                Mock.Of<IDependencyFilter>(),
                Mock.Of<ILogger>(),
                Mock.Of<TextWriter>()
            );

            var result = target.Execute(null);

            Assert.AreNotEqual(0, result);
        }
    }
}
