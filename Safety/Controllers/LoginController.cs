using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Safety.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        /// <summary>
        /// Metodo para loguearse.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="idApp"></param>
        /// <returns></returns>
        [Route("[action]/{username}/{password}/{idApp}")]
        [HttpGet]
        public bool Login(string username, string password, int idApp)
        {
            return false;
        }
    }
}