using System;

namespace DependencyReader.CLI
{
    public class ParamReader
    {
        public virtual Parameters Read(string[] args)
        {
            return new Parameters
            {
                TargetPath = args.Length > 0 ? args[0] : "."
            };
        }
    }
}