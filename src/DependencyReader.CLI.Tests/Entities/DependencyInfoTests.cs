using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyReader.CLI.Entities;
using NUnit.Framework;
namespace DependencyReader.CLI.Entities.Tests
{
    [TestFixture]
    public class DependencyInfoTests
    {
        [Test]
        public void Construction_Works()
        {
            Assert.DoesNotThrow(() => { new DependencyInfo(); });
        }

        [Test]
        [TestCaseSource("All")]
        public void Equals_WithTwoInstances_Works(DependencyInfo a, DependencyInfo b, bool areEqual)
        {
            Assert.AreEqual(areEqual, Equals(a, b));
        }

        [Test]
        public void Equals_WithEdgeCases_Works()
        {
            var target = new DependencyInfo();

            Assert.AreEqual(target, target);
            Assert.IsFalse(target.Equals(null));
            Assert.IsFalse(target.Equals(this));
        }

        [Test]
        [TestCaseSource("All")]
        public void GetHashCode_Works(DependencyInfo a, DependencyInfo b, bool areSame)
        {
            // in case instances are not equals, hashcodes still can collide
            // so we don't Assert that they are different
            if (areSame)
            {
                Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            }
        }

        [Test]
        public void ToString_Contains()
        {
            var target = new DependencyInfo
            {
                Parent = new AssemblyInfo { Name = "p", Version = "1" },
                Child = new AssemblyInfo { Name = "c", Version = "1" },
            };
            var result = target.ToString();

            Assert.That(result.Contains(target.Parent.ToString()));
            Assert.That(result.Contains(target.Child.ToString()));
        }

        private IEnumerable<object[]> All()
        {
            var a1 = new AssemblyInfo { Name = "a", Version = "1" };
            var a2 = new AssemblyInfo { Name = "a", Version = "2" };
            var b1 = new AssemblyInfo { Name = "b", Version = "1" };

            //equivalent objects
            yield return new object[]
            {
                new DependencyInfo
                {
                    Parent = a1,
                    Child = a1,
                    Distance = 2,
                    Path = new List<AssemblyInfo>{ b1 }
                },
                new DependencyInfo
                {
                    Parent = a1,
                    Child = a1,
                    Distance = 2,
                    Path = new List<AssemblyInfo>{ b1 }
                },
                true
            };

            //name mismatch
            yield return new object[]
            {
                new DependencyInfo
                {
                    Parent = a1,
                    Child = a1,
                },
                new DependencyInfo
                {
                    Parent = a1,
                    Child = b1,
                },
                false
            };

            //version mismatch
            yield return new object[]
            {
                new DependencyInfo
                {
                    Parent = a1,
                    Child = a1,
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = a1,
                    Child = a2,
                    Distance = 1
                },
                false
            };

            //version mismatch
            yield return new object[]
            {
                new DependencyInfo
                {
                    Parent = a1,
                    Child = a1,
                    Distance = 1
                },
                new DependencyInfo
                {
                    Parent = a1,
                    Child = a1,
                    Distance = 2
                },
                false
            };

            //path mismatch 
            yield return new object[]
            {
                new DependencyInfo
                {
                    Parent = a1,
                    Child = a1,
                    Path = new List<AssemblyInfo>{ a1 }
                },
                new DependencyInfo
                {
                    Parent = a1,
                    Child = a1,
                    Path = new List<AssemblyInfo> { a2 }
                },
                false
            };

        }
    }
}
