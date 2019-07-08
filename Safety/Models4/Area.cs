using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class Area
    {
        public Area()
        {
            Member = new HashSet<Member>();
        }

        public int? Id { get; set; }
        public string Description { get; set; }
        public int? Iddependency { get; set; }

        public CentroCoste IddependencyNavigation { get; set; }
        public ICollection<Member> Member { get; set; }
    }
}
