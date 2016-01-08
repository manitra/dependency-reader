using System;
using DependencyReader.CLI.Entities;
using DependencyReader.CLI.Impl;
using NUnit.Framework;

namespace DependencyReader.CLI.Tests.Impl
{
    [TestFixture]
    public class StyleManagerTests
    {
        [SetUp]
        public void BeforeEach()
        {
            Console.ResetColor();
        }

        [Test]
        [TestCase(Style.Primary)]
        [TestCase(Style.Warning)]
        [TestCase(Style.Error)]
        public void Set_ChangesConsoleColors(Style style)
        {
            var previsousFore = Console.ForegroundColor;
            var target = new StyleManager();

            target.Set(style, StyleGroup.Normal);

            if (!Console.IsOutputRedirected)
            {
                Assert.AreNotEqual(previsousFore, Console.ForegroundColor);
            }
            else
            {
                Assert.Inconclusive();
            }
        }
    }
}
