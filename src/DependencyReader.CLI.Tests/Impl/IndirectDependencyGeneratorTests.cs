using System.Linq;
using DependencyReader.CLI.Entities;
using DependencyReader.CLI.Impl;
using Moq;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Impl
{
    [TestFixture]
    public class IndirectDependencyGeneratorTests
    {
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
                new IndirectDependencyGenerator();
            });
        }

        [Test]
        public void Read_Single_ReturnsSingle()
        {
            var expected = new DependencyInfo
            {
                Parent = new AssemblyInfo { Name = "parent", Version = "1.0" },
                Child = new AssemblyInfo { Name = "chid", Version = "1.0" },
                Distance = 1
            };
            var target = new IndirectDependencyGenerator();

            var result = target.Filter(new[] { expected }).ToArray();

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(expected, result[0]);
        }

        [Test]
        public void Read_TwoTransitive_ReturnsThree()
        {
            var expected = new[]{
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "grand-pa", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "parent", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "parent", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "child", Version = "1.0" },
                    Distance = 1
                }
            };
            var target = new IndirectDependencyGenerator();

            var result = target.Filter(expected).ToArray();

            Assert.AreEqual(3, result.Length);
            Assert.Contains(expected[0], result);
            Assert.Contains(expected[1], result);
            Assert.Contains(
                new DependencyInfo
                {
                    Parent = expected[0].Parent,
                    Child = expected[1].Child,
                    Distance = 2
                },
                result
            );
        }

        [Test]
        public void Read_DoubleParent_ReturnsTwo()
        {
            var expected = new[] {
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "dady", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "child", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "mummy", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "child", Version = "1.0" },
                    Distance = 1
                }
            };
            var target = new IndirectDependencyGenerator();

            var result = target.Filter(expected).ToArray();

            Assert.AreEqual(2, result.Length);
            foreach (var row in expected)
            {
                Assert.Contains(row, result);
            }
        }

        [Test]
        public void Read_DoubleChildrenAndTransitive_ReturnsFive()
        {
            var expected = new[] {
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "gran-pa", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "parent", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "parent", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "boule", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "parent", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "bill", Version = "1.0" },
                    Distance = 1
                }
            };
            var target = new IndirectDependencyGenerator();

            var result = target.Filter(expected).ToArray();

            Assert.AreEqual(5, result.Length);
            foreach (var row in expected)
            {
                Assert.Contains(row, result);
            }
            Assert.Contains(
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "gran-pa", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "boule", Version = "1.0" },
                    Distance = 2
                },
                result
            );
            Assert.Contains(
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "gran-pa", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "bill", Version = "1.0" },
                    Distance = 2
                },
                result
            );
        }

        [Test]
        public void Read_LongTransitiveChain_ReturnsCorrectNumberOfDeps()
        {
            var expected = new[] {
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "a1", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "a2", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "a2", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "a3", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "a3", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "a4", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "a4", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "a5", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "a5", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "a6", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "a6", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "a7", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "a7", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "a8", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "a8", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "a9", Version = "1.0" },
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "a9", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "a10", Version = "1.0" },
                    Distance = 1
                },
            };
            var target = new IndirectDependencyGenerator();

            var result = target.Filter(expected).ToArray();

            Assert.AreEqual((expected.Length * expected.Length) / 2.0 + expected.Length / 2.0, result.Length);
            foreach (var row in expected)
            {
                Assert.Contains(row, result);
            }
            Assert.Contains(
                new DependencyInfo
                {
                    Parent = new AssemblyInfo { Name = "a1", Version = "1.0" },
                    Child = new AssemblyInfo { Name = "a10", Version = "1.0" },
                    Distance = 9
                },
                result
            );
        }
    }
}
