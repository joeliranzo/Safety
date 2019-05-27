using System;
using System.Collections.Generic;

namespace Safety.Models 
{
    public partial class Roles
    {
        public Roles()
        {
            AccountsApplicationsRoles = new HashSet<AccountsApplicationsRoles>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public int Range { get; set; }
        public int IdApp { get; set; }

        public Applications IdAppNavigation { get; set; }
        public ICollection<AccountsApplicationsRoles> AccountsApplicationsRoles { get; set; }
    }
}
