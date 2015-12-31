using NUnit.Framework;
namespace DependencyReader.CLI
{
    [TestFixture]
    public class ParamReaderTests
    {
        [Test]
        public void Read_WithData_ReturnsSame()
        {
            var target = new ParamReader();

            const string input = "a-random-path";
            var result = target.Read(new[] { input });

            Assert.AreEqual(input, result.TargetPath);
        }

        [Test]
        public void Read_WithoutData_ReturnsCurrentFolder()
        {
            var target = new ParamReader();

            var result = target.Read(new string[0]);

            Assert.AreEqual(".", result.TargetPath);
        }

    }
}
