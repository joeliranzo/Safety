using System;
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
    /// Controlador para Areas
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AreasController : BaseController
    {
        ///GET api/area
        /// <summary>
        /// Con este metodo se pueden obtener todas las areas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Area> Get()
        {
            return context.Area.ToList();
        }

        // GET api/area/GetById/2
        /// <summary>
        /// Con este metodo se puede obtener un area, dado su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public Area GetById(int? id)
        {
            return context.Area.Find(id);
        }

        // GET api/area/GetAllAreasOfDependency/89
        /// <summary>
        /// Con este metodo se puede obtener todas las areas de una dependencia.
        /// </summary>
        /// <param name="idDependency"></param>
        /// <returns></returns>
        [Route("[action]/{idDependency}")]
        [HttpGet]
        public IEnumerable<Area> GetAllAreasOfDependency(int idDependency)
        {
            return context.Area.Where(w=>w.Iddependency == idDependency).ToList();
        }

        // GET api/area/GetAreaByName/Tecnologia
        /// <summary>
        /// Con este metodo se puede obtener todas las areas de una dependencia.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("[action]/{name}")]
        [HttpGet]
        public Area GetAreaByName(string name)
        {
            return context.Area.Where(w => w.Description == name).FirstOrDefault();
        }

        // POST api/area
        /// <summary>
        /// Con este método se puede registrar una nueva area.
        /// </summary>
        [HttpPost]
        public void Post([FromBody]Area area)
        {
            PrepareArea(ref area);

            try
            {
                context.Area.Add(area);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // PUT api/area/3
        /// <summary>
        /// Con este método se puede modificar una area.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedArea"></param>
        [HttpPut("{id}")]
        public void Put(int? id, [FromBody]Area updatedArea)
        {
            Area area = context.Area
                .Find(id);

            if (area == null) return;

            area.Description = updatedArea.Description;
            area.Iddependency = updatedArea.Iddependency;

            try
            {
                context.Area.Update(area);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // DELETE api/area/3
        /// <summary>
        /// Con este método se puede eliminar una aplicacion.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int? id)
        {
            Area area = context.Area
                .Find(id);

            if (area == null) return;

            try
            {
                context.Area.Remove(area);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        private bool PrepareArea(ref Area area)
        {
            //This method need be more extensible.

            //If the auto-increment ID is not null; then set it to null for the DB.
            if (area.Id != null) area.Id = null;

            return true;
        }

    }
}