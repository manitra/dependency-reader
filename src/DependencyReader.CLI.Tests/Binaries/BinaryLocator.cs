namespace DependencyReader.CLI.Tests.Binaries
{
    public class BinaryLocator
    {
        public static string Location
        {
            get { return typeof(BinaryLocator).Namespace; }
        }
    }
}
