using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class AccountApplicationRole
    {
        public int Idacc { get; set; }
        public int Idapp { get; set; }
        public int Idrole { get; set; }

        public Account IdaccNavigation { get; set; }
        public Application IdappNavigation { get; set; }
        public Role IdroleNavigation { get; set; }
    }
}
