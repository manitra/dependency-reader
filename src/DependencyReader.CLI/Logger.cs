using System;

namespace DependencyReader.CLI
{
    public class Logger
    {
        public virtual void Log(DependencyInfo dep)
        {
            Console.WriteLine(
                "{0}-{1} {2}-{3}",
                dep.Parent.Name,
                dep.Parent.Version,
                dep.Child.Name,
                dep.Child.Version
            );
        }
    }
}