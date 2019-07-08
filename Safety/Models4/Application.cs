using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class Application
    {
        public Application()
        {
            AccountApplicationRole = new HashSet<AccountApplicationRole>();
            Role = new HashSet<Role>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<AccountApplicationRole> AccountApplicationRole { get; set; }
        public ICollection<Role> Role { get; set; }
    }
}
