using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Safety.Models;
using Safety.Options;

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

        //private SecurityToken token;

        private readonly IConfiguration _configuration;
        private readonly JwtSettings jwtSettings;

        /// <summary>
        /// Metodo contstructor para la Autenticacion.
        /// </summary>
        //public AuthenticationController()
        //{
        //    login = new LoginInfo();
        //    accountsRolesController = new AccountsRolesController();
        //    accountsController = new AccountsController();
        //    rolesController = new RolesController();
        //    membersController = new MembersController();
        //    applicationsController = new ApplicationsController();
        //}

        /// <summary>
        /// Metodo contstructor para la Autenticacion.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="jwtSettings"></param>
        public AuthenticationController(IConfiguration configuration, JwtSettings jwtSettings)
        {
            _configuration = configuration;
            this.jwtSettings = jwtSettings;

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
        [HttpPost]
        public IActionResult Login(string username, string password, int? idApp)
        {
            login.Account = accountsController.Login(username, password);
            login.Member = membersController.GetById(login.Account.Idmember);
            login.Application = applicationsController.GetById(idApp);
            login.AccountApplicationRole = accountsRolesController
                    .GetRoleByAccApp(login.Account.Id, login.Application.Id);

            //Se verifica que exista una cuenta
            if (login.Account != null)
            {
                //Se verifica que la cuenta este activa.
                if (login.Account.Status > 0 && (login.Account.ExpirationDate ?? DateTime.MaxValue) >= DateTime.Now)
                {
                    //En caso de que la cuenta exista se verifica que tenga acceso
                    //A la aplicacion requerida
                    if (login.Account.IsSuperUser)
                    {
                        //Se le asigna el rol de mayor rango, para la aplicación que
                        //Se esta intentando acceder.
                        login.Role = rolesController.GetMaxRoleForApp(login.Application.Id);
                        login.HasAccess = true;
                    }
                    else if (login.AccountApplicationRole != null)
                    {
                        login.Role = rolesController.GetById(login.AccountApplicationRole.Idrole);
                        login.HasAccess = true;
                    }
                }
            }

            //En caso de que tenga acceso se crea el token
            if(login.HasAccess)
            {
                return BuildToken(this.login);
            }

            return Unauthorized(); //BadRequest(ModelState);
        }

        private IActionResult BuildToken(LoginInfo login)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, login.Account.UserName),
                new Claim("IdMember", login.Member.Id.ToString()),
                new Claim("EmployNumber", login.Member.EmployNumber.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "egehid.com.do",
                audience:"egehid.com.do",
                claims : claims,
                expires: expiration,
                signingCredentials: creds
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            });
        }

        //private SecurityToken GenerarToken()
        //{
        //    // Leemos el secret_key desde nuestro appseting
        //    var secretKey = _configuration.GetValue<string>("SecretKey");
        //    var key = Encoding.ASCII.GetBytes(secretKey);

        //    // Creamos los claims (pertenencias, características) del usuario
        //    var claims = new[]
        //    {
        //        //new Claim(ClaimTypes.NameIdentifier, login.Account.Id),
        //        new Claim(ClaimTypes.Email, login.Member.Email)
        //    };

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = claims,
        //        // Nuestro token va a durar un día
        //        Expires = DateTime.UtcNow.AddDays(1),
        //        // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var createdToken = tokenHandler.CreateToken(tokenDescriptor);

        //    return createdToken;

        //}
    }
}