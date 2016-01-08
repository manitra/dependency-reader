using System.IO;
using DependencyReader.CLI.Entities;
using DependencyReader.CLI.Impl;
using Moq;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Impl
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public void Log_ContainsAllFields()
        {
            using(var writer = new StringWriter())
            {
                var entity = new DependencyInfo
                {
                    Parent = new AssemblyInfo{ Name="parent1", Version="1.0" },
                    Child = new AssemblyInfo{ Name="child1", Version = "3.0"},
                    Distance = 2,
                    Path = new []
                    {
                        new AssemblyInfo { Name = "intermediate1", Version = "4.0" },
                        new AssemblyInfo { Name = "intermediate2", Version = "5.0" },
                    }
                };

                var target = new Logger(writer, Mock.Of<IStyleManager>());
                target.Log(entity);
                var result = writer.ToString();

                Assert.That(result.Contains(entity.Parent.Name), "Missing Parent.Name");
                Assert.That(result.Contains(entity.Parent.Version), "Missing Parent.Version");
                Assert.That(result.Contains(entity.Child.Name), "Missing Child.Name");
                Assert.That(result.Contains(entity.Child.Version), "Missing Child.Version");
                Assert.That(result.Contains(">"), "Missing visual distance");
                Assert.That(result.Contains(string.Format("{0}", entity.Distance)), "Missing distance");
            }
        }
    }
}
