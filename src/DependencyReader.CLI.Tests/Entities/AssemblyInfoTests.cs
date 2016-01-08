using System.Collections.Generic;
using DependencyReader.CLI.Entities;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Entities
{
    [TestFixture]
    public class AssemblyInfoTests
    {
        [Test]
        public void Construction_Works()
        {
            Assert.DoesNotThrow(() => { new AssemblyInfo(); });
        }

        [Test]
        [TestCaseSource("All")]
        public void Equals_WithTwoInstances_Works(AssemblyInfo a, AssemblyInfo b, bool areEqual)
        {
            Assert.AreEqual(areEqual, Equals(a, b));
        }

        [Test]
        public void Equals_WithEdgeCases_Works()
        {
            var target = new AssemblyInfo();

            Assert.AreEqual(target, target);
            Assert.IsFalse(target.Equals(null));
            Assert.IsFalse(target.Equals(this));
        }

        [Test]
        [TestCaseSource("All")]
        public void GetHashCode_Works(AssemblyInfo a, AssemblyInfo b, bool areSame)
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
            var target = new AssemblyInfo { Name = "p", Version = "1" };
            var result = target.ToString();

            Assert.That(result.Contains(target.Name.ToString()));
            Assert.That(result.Contains(target.Version.ToString()));
        }

        private IEnumerable<object[]> All()
        {
            //equivalent objects
            yield return new object[]
            {
                new AssemblyInfo { Name = "a", Version = "1" },
                new AssemblyInfo { Name = "a", Version = "1" },
                true
            };

            //name mismatch
            yield return new object[]
            {
                new AssemblyInfo { Name = "a", Version = "1" },
                new AssemblyInfo { Name = "b", Version = "1" },
                false
            };

            //version mismatch
            yield return new object[]
            {
                new AssemblyInfo { Name = "a", Version = "1" },
                new AssemblyInfo { Name = "a", Version = "2" },
                false
            };
        }
    }
}
