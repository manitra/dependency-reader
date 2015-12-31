using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyReader.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            new Runner(
                new ParamReader())
                .Execute();
        }
    }

    public class Runner
    {
        private readonly ParamReader paramReader;

        public Runner(ParamReader paramReader)
        {
            this.paramReader = paramReader;
        }

        public Runner Execute()
        {
            // Code here

            return this;
        }
    }

    public class ParamReader
    {
        public Parameters Read(string[] args)
        {
            return new Parameters
            {
                TargetPath = args[1]
            };
        }
    }

    public class Parameters
    {
        public string TargetPath { get; set; }
    }
}
