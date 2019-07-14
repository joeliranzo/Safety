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
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private AccountsRolesController accountsRolesController;
        private AccountsController accountsController;
        private RolesController rolesController;
        private MembersController membersController;
        private ApplicationsController applicationsController;
        private LoginInfo login;

        /// <summary>
        /// Metodo contstructor para el logeo.
        /// </summary>
        public AuthenticationController()
        {
            login = new LoginInfo();
            accountsRolesController = new AccountsRolesController();
            accountsController = new AccountsController();
            rolesController = new RolesController();
            membersController = new MembersController();
            applicationsController = new ApplicationsController();
        }
        
        /// <summary>
        /// Metodo para loguearse.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="idApp"></param>
        /// <returns></returns>
        [Route("[action]/{username}/{password}/{idApp}")]
        [HttpGet]
        public LoginInfo Login(string username, string password, int? idApp)
        {
            login.Account = accountsController.Login(username, password);
            login.Member = membersController.GetById(login.Account.Idmember);
            login.Application = applicationsController.GetById(idApp);
            login.AccountApplicationRole = accountsRolesController
                    .GetRoleByAccApp(login.Account.Id, login.Application.Id);

            //Se verifica que exista una cuenta
            if (login.Account != null)
            {
                //En caso de que la cuenta exista se verifica que tenga acceso
                //A la aplicacion requerida
                if (login.AccountApplicationRole != null)
                {
                    login.Role = rolesController.GetById(login.AccountApplicationRole.Idrole);
                    login.HasAccess = true;
                }
            }

            return this.login;
        }
    }
}