namespace DependencyReader.CLI.Entities
{
    /// <summary>
    /// An entity which represents the name of assembly
    /// </summary>
    public class AssemblyInfo
    {
        /// <summary>
        /// The simple name of an assembly: 'System.Data'
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The string representation of the version of an assembly: '2.4.5.0'
        /// </summary>
        public string Version { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AssemblyInfo)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Version != null ? Version.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return Name + "-" + Version;
        }

        protected bool Equals(AssemblyInfo other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Version, other.Version);
        }

    }
}