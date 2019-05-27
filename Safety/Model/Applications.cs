using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class Applications
    {
        public Applications()
        {
            AccountsApplicationsRoles = new HashSet<AccountsApplicationsRoles>();
            Roles = new HashSet<Roles>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<AccountsApplicationsRoles> AccountsApplicationsRoles { get; set; }
        public ICollection<Roles> Roles { get; set; }
    }
}
