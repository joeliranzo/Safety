﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safety.Models;

namespace Safety.Controllers
{
    /// <summary>
    /// La ruta por defecto para los usuarios es api/accounts
    /// Este es el controlador de las cuentas.
    /// Aquí se gestionan todos las cuentas.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountsController : BaseController
    {
        /// <summary>
        /// Con este metodo se puede verificar si un usuario y contraseña son correctos.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //[Route("[action]/{username}/{password}")]
        //[HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public Account Login(string username, string password)
        {
            Account account =
                context.Account.Where(w=>w.UserName == username).FirstOrDefault();

            if (account != null)
            {
                if (account.UserName.ToLower().Trim() == username.ToLower().Trim() &&
                    account.Password == MD5Hash.EncryptPassword(password))
                {
                    return account;
                }
            }
            return null;
        }

        // GET api/accounts
        /// <summary>
        /// Con este método se puede obtener un listado de todas las cuentas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return context.Account.ToList();
        }

        // GET api/accounts/GetById/2
        /// <summary>
        /// Con este método se puede obtener una cuenta.
        /// cuando se le proporciona su id de la base de datos.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[action]/{id}")]
        [HttpGet]
        //[HttpGet("{id}")]
        public Account GetById(int? id)
        {
            return context.Account.Find(id);
        }

        // GET api/accounts/GetByUserName/joliranzo
        /// <summary>
        /// Con este método se puede obtener una cuenta.
        /// cuando se le proporciona su nombre usuario.
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [Route("[action]/{UserName}")]
        [HttpGet]
        public Account GetByUserName(string UserName)
        {
            return context.Account
                .Where(w => w.UserName == UserName)
                .FirstOrDefault();
        }

        // POST api/accounts
        /// <summary>
        /// Con este método se puede crear una nueva cuenta.
        /// </summary>
        /// <param name="account"></param>
        [HttpPost]
        public void Post([FromBody]Account account)
        {
            Member user = context.Member.Find(account.Idmember);
            Account pAccount = context.Account
                .Where(w => w.Idmember == account.Idmember)
                .FirstOrDefault();

            //Initial and Fundamental validation
            if(user == null || pAccount != null)
            {
                //Return error, the user have an account
                //Or the account to create don't have an user created.
                return;
            }

            //La cuenta se envía por referencia.
            //Ya que se le puede reajustar varios parametros.
            //Se envía a preparar la cuenta.
            PrepareAccount(ref account);

            //Encrypt password with MD5.
            account.Password = MD5Hash.EncryptPassword(account.Password);

            //Setting Creation Date.
            account.CreationDate = DateTime.Now;

            try
            {
                context.Account.Add(account);
                context.SaveChanges();
            }
            catch(Exception e)
            {

            }
        }

        // PUT api/accounts/5
        /// <summary>
        /// Con este método se puede modificar una cuenta.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newaccount"></param>
        [HttpPut("{id}")]
        public void Put(int? id, [FromBody]Account newaccount)
        {
            Account account = context.Account
                .Find(id);
            
            if(account == null)
            {
                //Return error, account not found.
                return;
            }

            //Setting new values of the account.
            account.UserName = newaccount.UserName;
            account.Status = newaccount.Status;
            account.Idmember = newaccount.Idmember;
            account.CreationDate = newaccount.CreationDate;
            account.ExpirationDate = newaccount.ExpirationDate;

            //Encrypt password with MD5.
            account.Password = MD5Hash.EncryptPassword(newaccount.Password);

            try
            {
                //Updating the Account
                context.Account.Update(account);
                context.SaveChanges();
            }
            catch(Exception e)
            {

            }
        }

        // DELETE api/accounts/5
        /// <summary>
        /// Con este método se puede eliminar una cuenta.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int? id)
        {
            Account account = context.Account
                .Find(id);

            if (account == null)
            {
                //Return error, account not found.
                return;
            }

            try
            {
                context.Account.Remove(account);
                context.SaveChanges();
            }
            catch(Exception e)
            {
                
            }
        }


        // DELETE api/accounts/DeleteByUserName/username
        /// <summary>
        /// Con este método se puede eliminar una cuenta.
        /// </summary>
        /// <param name="username"></param>
        [Route("[action]/{UserName}")]
        [HttpDelete]
        public void DeleteByUserName(string username)
        {
            Account account = (from data in context.Account
                               where data.UserName == username
                               select data).FirstOrDefault();

            if (account == null)
            {
                //Return error, account not found.
                return;
            }

            try
            {
                context.Account.Remove(account);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Este método solo se puede invocar desde el controlador
        /// Y es útil para validar los campos de un usuario
        /// Y también es útil para realizarle  cualquier preparación
        /// extra que se necesite antes de ingresar a la base de datos
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private bool PrepareAccount(ref Account account)
        {
            Dictionary<int, string> validations = new Dictionary<int, string>();

            //If the auto-increment ID is not null; then set it to null for the DB.
            if (account.Id != null) account.Id = null;

            //Remove extra spaces from UserName.
            account.UserName = account.UserName.Trim();

            //Put all character from username to lower-case.
            account.UserName = account.UserName.ToLower();

            return true;
        }
    }
}
