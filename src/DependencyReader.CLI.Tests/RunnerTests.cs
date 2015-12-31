using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyReader.CLI;
using Moq;
using NUnit.Framework;
namespace DependencyReader.CLI
{
    [TestFixture]
    public class RunnerTests
    {
        [Test]
        public void Execute_WithSingleDep_LogsOnce()
        {
            var deps = new[] { new DependencyInfo() };
            var logger = new Mock<Logger>(null);
            var target = new Runner(
                Mock.Of<ParamReader>(o => o.Read(It.IsAny<string[]>()) == new Parameters { TargetPath = "" }),
                Mock.Of<FileEnumerator>(o => o.Find(It.IsAny<string>(), It.IsAny<string>()) == new[] { "" }),
                Mock.Of<Reader>(o => o.Read(It.IsAny<string>()) == deps),
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
            var buggyParamReader = new Mock<ParamReader>(); buggyParamReader
                .Setup(o => o.Read(It.IsAny<string[]>()))
                .Throws(new Exception("Bim"));
            var logger = new Mock<Logger>(null);
            var target = new Runner(
                buggyParamReader.Object,
                Mock.Of<FileEnumerator>(),
                Mock.Of<Reader>(),
                logger.Object,
                Mock.Of<TextWriter>()
            );

            var result = target.Execute(null);

            Assert.AreNotEqual(0, result);
        }
    }
}
