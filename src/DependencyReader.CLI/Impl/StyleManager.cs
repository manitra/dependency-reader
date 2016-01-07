using System;
using System.Collections.Generic;
using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI.Impl
{
    /// <summary>
    /// Provide a way set style using semantics
    /// </summary>
    public class StyleManager : IStyleManager
    {
        private static readonly IDictionary<Style, IDictionary<StyleGroup, ConsoleColor>> ColorsByStyles = new Dictionary
            <Style, IDictionary<StyleGroup, ConsoleColor>>
        {
            {
                Style.Normal,
                new Dictionary<StyleGroup, ConsoleColor>
                {
                    { StyleGroup.Normal, ConsoleColor.Gray },
                    { StyleGroup.Alternative, ConsoleColor.Gray }
                }
            },
            {
                Style.Primary,
                new Dictionary<StyleGroup, ConsoleColor>
                {
                    { StyleGroup.Normal, ConsoleColor.Cyan },
                    { StyleGroup.Alternative, ConsoleColor.Green }
                }
            },
            {
                Style.Warning,
                new Dictionary<StyleGroup, ConsoleColor>
                {
                    { StyleGroup.Normal, ConsoleColor.Yellow },
                    { StyleGroup.Alternative, ConsoleColor.Yellow }
                }
            },
            {
                Style.Error,
                new Dictionary<StyleGroup, ConsoleColor>
                {
                    { StyleGroup.Normal, ConsoleColor.Red },
                    { StyleGroup.Alternative, ConsoleColor.Red }
                }
            },

        };

        /// <summary>
        /// Set the style of the console
        /// </summary>
        /// <param name="style"></param>
        /// <param name="group"></param>
        public void Set(Style style, StyleGroup group)
        {
            Console.ForegroundColor = ColorsByStyles[style][group];
        }
    }
}