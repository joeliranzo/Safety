using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class MemberArea
    {
        public int Id { get; set; }
        public int Idmember { get; set; }
        public int Idarea { get; set; }

        public Area IdareaNavigation { get; set; }
        public Member IdmemberNavigation { get; set; }
    }
}
