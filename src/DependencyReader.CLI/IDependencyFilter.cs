using System.Collections.Generic;
using DependencyReader.CLI.Entities;

namespace DependencyReader.CLI
{
    /// <summary>
    /// Filter a list of <see cref="DependencyInfo"/>.
    /// The interface also allow filter to actually add rows.
    /// </summary>
    public interface IDependencyFilter
    {
        /// <summary>
        /// Filters the input given some arbitrary criteria.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IEnumerable<DependencyInfo> Filter(IEnumerable<DependencyInfo> input);
    }
}