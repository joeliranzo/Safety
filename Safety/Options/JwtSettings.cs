using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Safety.Options
{
    /// <summary>
    /// Configuración para el token.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Llave secreta del token JWT.
        /// </summary>
        public string Secret { get; set; }
    }
}
