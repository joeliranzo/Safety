﻿using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountApplicationRole = new HashSet<AccountApplicationRole>();
        }

        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public int Idmember { get; set; }
        public bool IsManager { get; set; }
        public bool IsSuperUser { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public Member IdmemberNavigation { get; set; }
        public ICollection<AccountApplicationRole> AccountApplicationRole { get; set; }
    }
}
