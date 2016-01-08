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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        [Test]
        public void Set_Primary_ChangesConsoleColors()
        {
            var previousBack = Console.BackgroundColor;
            var previsousFore = Console.ForegroundColor;
            var target = new StyleManager();

            target.Set(Style.Primary, StyleGroup.Normal);

            Assert.That(
                previousBack != Console.BackgroundColor || previsousFore != Console.ForegroundColor,
                "Neither the background nore the foreground color changed");
        }
    }
}
