namespace DependencyReader.CLI.Entities
{
    /// <summary>
    /// An entity which represent a direct dependency
    /// </summary>
    public class DependencyInfo
    {
        /// <summary>
        /// The assembly which has a dependency
        /// </summary>
        public AssemblyInfo Parent { get; set; }
        
        /// <summary>
        /// The assembly depended upon
        /// </summary>
        public AssemblyInfo Child { get; set; }

        /// <summary>
        /// Represent the dependency distance between the parent and the child.
        /// 1 means that Parent directly depend on Child.
        /// </summary>
        public int Distance { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as DependencyInfo;
            if (other == null) return false;

            return
                Equals(Parent, other.Parent)
                && Equals(Child, other.Child)
                && Equals(Distance, other.Distance);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Parent != null ? Parent.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Child != null ? Child.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Distance;
                return hashCode;
            }
        }
    }
}