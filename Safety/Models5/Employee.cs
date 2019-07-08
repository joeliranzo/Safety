using System;
using System.Collections.Generic;

namespace Safety.Models
{
    public partial class Employee
    {
        public int? Id { get; set; }
        public string Identification { get; set; }
        public int? EmployNumber { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Segundoapellido { get; set; }
        public double? Salary { get; set; }
        public string Birthday { get; set; }
        public string DateEntry { get; set; }
        public DateTime? DateEntry2 { get; set; }
        public string Position { get; set; }
        public string OldFuncion { get; set; }
        public string Funcion { get; set; }
        public string SubdivisiónDePersonal { get; set; }
        public string Department { get; set; }
        public double? Dependency { get; set; }
        public string TextoCentroCoste { get; set; }
        public string Sexo { get; set; }
        public bool? EntryRegistration { get; set; }
        public short? Status { get; set; }
        public string Mail { get; set; }
        public string Mail2 { get; set; }
        public double? PositionId { get; set; }
        public string PositionIdold { get; set; }
        public DateTime? Modificacion { get; set; }
        public string Descripcion { get; set; }
        public string HorarioId { get; set; }
        public bool? Overtime { get; set; }
        public double? OldSalary { get; set; }
        public string OldPositionId { get; set; }
        public string OldDescriptionPosition { get; set; }
        public bool? Generate { get; set; }
        public double? CentroCoste { get; set; }
        public double? Area { get; set; }
        public double? UnitOrganization { get; set; }
        public double? FunctionId { get; set; }
        public string Movil { get; set; }
        public decimal? IdCentral { get; set; }
        public short? SapNv { get; set; }
        public short? IEmployeeNum { get; set; }
        public bool? StatusSap { get; set; }
    }
}
