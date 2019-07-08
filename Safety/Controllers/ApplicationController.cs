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
    public class ApplicationController : BaseController
    {
        ///GET api/application
        /// <summary>
        /// Con este metodo se puede obtener todas las aplicaciones.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Application> Get()
        {
            return context.Application.ToList();
        }

        // GET api/application/GetById/2
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public Application GetById(int id)
        {
            return context.Application.Find(id);
        }

        // POST api/application
        /// <summary>
        /// Con este método se puede registrar una nueva aplicacion.
        /// </summary>
        [HttpPost]
        public void Post([FromBody]Application app)
        {
            PrepareApp(ref app);

            try
            {
                context.Application.Add(app);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // PUT api/application/3
        /// <summary>
        /// Con este método se puede modificar una aplicacion.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedApp"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Application updatedApp)
        {
            Application app = context.Application
                .Find(id);

            if (app == null) return;

            app.Name = updatedApp.Name;
            app.Description = updatedApp.Description;

            try
            {
                context.Application.Update(app);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // DELETE api/application/3
        /// <summary>
        /// Con este método se puede eliminar una aplicacion.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Application app = context.Application
                .Find(id);

            if (app == null) return;

            try
            {
                context.Application.Remove(app);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        private bool PrepareApp(ref Application app)
        {
            //This method need be more extensible.

            //If the auto-increment ID is not null; then set it to null for the DB.
            if (app.Id != null) app.Id = null;

            return true;
        }
    }
}