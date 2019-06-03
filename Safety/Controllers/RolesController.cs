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
    public class RolesController : BaseController
    {
        ///GET api/roles
        /// <summary>
        /// Con este metodo se puede obtener todos los roles de todas las aplicaciones.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Role> Get()
        {
            return context.Role.ToList();
        }

        // GET api/roles/GetById/2
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public Role GetById(int id)
        {
            return context.Role.Find(id);
        }

        // GET api/roles/GetByIdApp/1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idApp"></param>
        /// <returns></returns>
        [Route("[action]/{idApp}")]
        [HttpGet]
        public IEnumerable<Role> GetByIdApp(int idApp)
        {
            return context.Role
                .Where(w=> w.IdApp == idApp).ToList();
        }

        // POST api/roles
        /// <summary>
        /// Con este método se puede crear un nuevo rol
        /// </summary>
        [HttpPost]
        public void Post([FromBody]Role role)
        {
            PrepareRole(ref role);

            try
            {
                context.Role.Add(role);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // PUT api/roles/3
        /// <summary>
        /// Con este método se puede modificar un rol
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedRole"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Role updatedRole)
        {
            Role role = context.Role
                .Find(id);

            if (role == null) return;

            role.Name = updatedRole.Name;
            role.Range = updatedRole.Range;
            role.IdApp = updatedRole.IdApp;

            try
            {
                context.Role.Update(role);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // DELETE api/roles/3
        /// <summary>
        /// Con este método se puede eliminar un rol
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Role role = context.Role
                .Find(id);

            if (role == null) return;

            try
            {
                context.Role.Remove(role);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // DELETE api/role/DeleteRolesByIdApp/3
        /// <summary>
        /// Con este método se puede eliminar todos los roles de una Aplicacion.
        /// </summary>
        /// <param name="idApp"></param>
        [Route("[action]/{idApp}")]
        [HttpDelete]
        public void DeleteRolesByIdApp(int idApp)
        {
            List<Role> role = (from data in context.Role
                           where data.IdApp == idApp
                           select data).ToList();

            if (role == null) return;

            try
            {
                foreach (var item in role)
                {
                    context.Role.Remove(item);
                }
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        private bool PrepareRole(ref Role role)
        {
            //This method need be more extensible.

            //If the auto-increment ID is not null; then set it to null for the DB.
            if (role.Id != null) role.Id = null;

            return true;
        }
    }
}