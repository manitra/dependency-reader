﻿using System.Linq;
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
                new IndirectDependencyGenerator(Mock.Of<IReader>());
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
            var target = new IndirectDependencyGenerator(Mock.Of<IReader>(o => o.Read(It.IsAny<string>()) == new[] { expected }));

            var result = target.Read("any").ToArray();

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
                    Child = new AssemblyInfo { Name = "chid", Version = "1.0" },
                    Distance = 1
                }
            };
            var target = new IndirectDependencyGenerator(Mock.Of<IReader>(o => o.Read(It.IsAny<string>()) == expected));

            var result = target.Read("any").ToArray();

            Assert.AreEqual(3, result.Length);
            Assert.Contains(expected[0], result);
            Assert.Contains(expected[1], result);
            Assert.AreEqual(
                new DependencyInfo
                {
                    Parent = expected[0].Parent,
                    Child = expected[1].Child,
                    Distance = 2
                } , 
                result[0]
            );
        }
    }
}