using System;

namespace DependencyReader.CLI
{
    public class ParamReader
    {
        public virtual Parameters Read(string[] args)
        {
            if (args.Length < 1) throw new InvalidOperationException("Missing TargetPath");

            return new Parameters
            {
                TargetPath = args[0]
            };
        }
    }
}