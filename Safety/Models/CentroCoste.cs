using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class CentroCoste
    {
        public CentroCoste()
        {
            Area = new HashSet<Area>();
        }

        public int? Id { get; set; }
        public double? Mandt { get; set; }
        public string Spras { get; set; }
        public double? Kokrs { get; set; }
        public double? Dependencia { get; set; }
        public string Datbi { get; set; }
        public string ShortDescription { get; set; }
        public string Descripcion { get; set; }
        public string Mctxt { get; set; }

        public ICollection<Area> Area { get; set; }
    }
}
