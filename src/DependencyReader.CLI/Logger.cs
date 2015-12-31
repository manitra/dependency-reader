using System;

namespace DependencyReader.CLI
{
    public class Logger
    {
        public virtual void Log(string name, string version)
        {
            Console.WriteLine(name + " " + version);
        }
    }
}