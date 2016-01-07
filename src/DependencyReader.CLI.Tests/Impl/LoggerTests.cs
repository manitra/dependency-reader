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
                    Child = new AssemblyInfo{ Name="child1", Version = "2.0"},
                };

                var target = new Logger(writer, Mock.Of<IStyleManager>());
                target.Log(entity);
                var result = writer.ToString();

                Assert.IsTrue(result.Contains(entity.Parent.Name), "Parent.Name");
                Assert.IsTrue(result.Contains(entity.Parent.Version), "Parent.Version");
                Assert.IsTrue(result.Contains(entity.Child.Name), "Child.Name");
                Assert.IsTrue(result.Contains(entity.Child.Version), "Child.Version");
            }
        }
    }
}
