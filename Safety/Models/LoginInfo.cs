using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Safety.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// Cuenta del logeo.
        /// </summary>
        public Account Account { get; set; }
        /// <summary>
        /// Referencia al miembro.
        /// </summary>
        public Member Member { get; set; }
        /// <summary>
        /// Aplicacion a la que el miembro desea tener acceso.
        /// </summary>
        public Application Application { get; set; }
        /// <summary>
        /// Relacion entre cuenta aplicacion y roles.
        /// </summary>
        public AccountApplicationRole AccountApplicationRole { get; set; }
        /// <summary>
        /// Role de una cuenta para el logeo actual.
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// Areas a las que pertenece el miembro.
        /// </summary>
        public List<MemberArea> Areas { get; set; }
        /// <summary>
        ///
        /// </summary>
        public bool HasAccess { get; set; }

        /// <summary>
        /// Constructor para el metodo LoginInfo
        /// </summary>
        public LoginInfo()
        {
            Account = null;
            Member = null;
            Application = null;
            AccountApplicationRole = null;
            Role = null;
            Areas = null;

            HasAccess = false;
        }
    }
}
