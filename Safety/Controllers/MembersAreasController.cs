using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Safety.Models;

namespace Safety.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MembersAreasController : BaseController
    {
        // GET api/users
        /// <summary>
        /// Con este método se pueude obtener un listado de todos los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<MemberArea> Get()
        {
            return context.MemberArea.ToList();
        }



    }
}