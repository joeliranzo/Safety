using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class Member
    {
        public int? Id { get; set; }
        public string EmployNumber { get; set; }
        public string Ipphone { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }

        public Account Account { get; set; }
    }
}
