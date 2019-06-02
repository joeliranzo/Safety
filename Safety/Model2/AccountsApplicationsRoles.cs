using System;
using System.Collections.Generic;

namespace Safety.Models 
{
    public partial class AccountsApplicationsRoles
    {
        public int Idacc { get; set; }
        public int Idapp { get; set; }
        public int Idrole { get; set; }

        public Accounts IdaccNavigation { get; set; }
        public Applications IdappNavigation { get; set; }
        public Roles IdroleNavigation { get; set; }
    }
}
