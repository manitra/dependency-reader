using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Contains an ordered list of all the assembly between <see cref="Parent"/> and <see cref="Child"/>
        /// </summary>
        public IList<AssemblyInfo> Path { get; set; }

        public DependencyInfo()
        {
            Path = new List<AssemblyInfo>();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DependencyInfo)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Parent != null ? Parent.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Child != null ? Child.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Distance;
                foreach (var part in Path)
                {
                    hashCode = (hashCode * 397) ^ (part.GetHashCode());
                }

                return hashCode;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} > {1} {2}", Parent, Child, Distance);
        }

        protected bool Equals(DependencyInfo other)
        {
            return
                Equals(Parent, other.Parent)
                && Equals(Child, other.Child)
                && Distance == other.Distance
                && Path
                    .Zip(other.Path, (a, b) => new[] { a, b })
                    .All(i => Equals(i[0], i[1]));
        }
    }
}