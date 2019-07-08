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
    public class AccountsRolesController : BaseController
    {
        // GET api/accountsroles
        /// <summary>
        /// Con este método se pueude obtener un listado de toda la relacion de las cuentas y los roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AccountApplicationRole> Get()
        {
            return context.AccountApplicationRole.ToList();
        }

        // GET api/AccountsRoles/GetRoleByAccApp/357/1
        /// <summary>
        /// Con este metodo se puede obtener el rol de una cuenta para una aplicacion
        /// </summary>
        /// <param name="idAcc"></param>
        /// <param name="idApp"></param>
        /// <returns></returns>
        [Route("[action]/{idAcc}/{idApp}")]
        [HttpGet]
        public AccountApplicationRole GetRoleByAccApp(int idAcc, int idApp)
        {
            return context.AccountApplicationRole
                .Where(w => w.Idacc == idAcc && w.Idapp == idApp)
                .FirstOrDefault();
        }

        // GET api/AccountsRoles/GetAllRolesForAcc/357
        /// <summary>
        /// Con este metodo se puede obtener el rol de una cuenta para una aplicacion
        /// </summary>
        /// <param name="idAcc"></param>
        /// <returns></returns>
        [Route("[action]/{idAcc}")]
        [HttpGet]
        public AccountApplicationRole GetAllRolesForAcc(int idAcc)
        {
            return context.AccountApplicationRole
                .Where(w => w.Idacc == idAcc)
                .FirstOrDefault();
        }


        // GET api/AccountsRoles/GetAllRolesForApp/1
        /// <summary>
        /// Con este metodo se puede obtener el rol de una cuenta para una aplicacion
        /// </summary>
        /// <param name="idApp"></param>
        /// <returns></returns>
        [Route("[action]/{idApp}")]
        [HttpGet]
        public AccountApplicationRole GetAllRolesForApp(int idApp)
        {
            return context.AccountApplicationRole
                .Where(w => w.Idapp == idApp)
                .FirstOrDefault();
        }

        // POST api/AccountsRoles
        /// <summary>
        /// Con este método se puede crear una nueva relacion
        /// de Cuentas, Aplicaciones y Roles.
        /// </summary>
        /// <param name="aar"></param>
        [HttpPost]
        public void Post([FromBody]AccountApplicationRole aar)
        {
            if (aar == null) return;

            try
            {
                context.AccountApplicationRole.Add(aar);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // PUT api/AccountsRoles/idAcc/idApp
        /// <summary>
        /// Con este método se puede crear una nueva relacion
        /// de Cuentas, Aplicaciones y Roles.
        /// </summary>
        /// <param name="idAcc"></param>
        /// <param name="idApp"></param>
        /// <param name="updatedAar"></param>
        [HttpPut("{idAcc}/{idApp}")]
        public void Put(int idAcc, int idApp, [FromBody]AccountApplicationRole updatedAar)
        {
            AccountApplicationRole aar = context.AccountApplicationRole
                .Where(w => w.Idacc == idAcc && w.Idapp == idApp)
                .FirstOrDefault();

            aar.Idrole = updatedAar.Idrole;

            if (aar == null) return;

            try
            {
                context.AccountApplicationRole.Update(aar);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // DELETE api/AccountsRoles/5
        /// <summary>
        /// Con este método se puede crear una nueva relacion
        /// de Cuentas, Aplicaciones y Roles.
        /// </summary>
        /// <param name="idAcc"></param>
        /// <param name="idApp"></param>
        [HttpDelete("{idAcc}/{idApp}")]
        public void Delete(int idAcc, int idApp)
        {
            AccountApplicationRole aar = context.AccountApplicationRole
                .Where(w => w.Idacc == idAcc && w.Idapp == idApp)
                .FirstOrDefault();

            if (aar == null) return;

            try
            {
                context.AccountApplicationRole.Remove(aar);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }
    }
}