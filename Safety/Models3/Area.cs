﻿using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class Area
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int? Iddependency { get; set; }
    }
}
