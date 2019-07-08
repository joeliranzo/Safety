using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class Role
    {
        public Role()
        {
            AccountApplicationRole = new HashSet<AccountApplicationRole>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public int Range { get; set; }
        public int IdApp { get; set; }

        public Application IdAppNavigation { get; set; }
        public ICollection<AccountApplicationRole> AccountApplicationRole { get; set; }
    }
}
