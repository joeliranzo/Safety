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
    public class MemberAreaController : BaseController
    {
        ///GET api/application
        /// <summary>
        /// Con este metodo se puede obtener todas las areas de los miembros.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<MemberArea> Get()
        {
            return context.MemberArea.ToList();
        }

        // GET api/GetMemberAreaForMember/747
        /// <summary>
        /// Devuelve todas las areas de un miembro.
        /// </summary>
        /// <returns></returns>
        [Route("[action]/{idMember}")]
        [HttpGet]
        public IEnumerable<MemberArea> GetMemberAreaForMember(int? idMember)
        {
            return context.MemberArea
                .Where(w => w.Idmember == idMember)
                .ToList();
        }

        // POST api/MemberArea
        /// <summary>
        /// Registra una nueva area para un miembro.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void Post([FromBody]MemberArea memberArea)
        {
            try
            {
                context.MemberArea.Add(memberArea);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // PUT api/MemberArea/2
        /// <summary>
        /// Con este método se puede modificar una area para un miembro.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedMemberArea"></param>
        [HttpPut("{id}")]
        public void Put(int? id, [FromBody]MemberArea updatedMemberArea)
        {
            MemberArea memberArea = context.MemberArea
                .Find(id);

            if (memberArea == null) return;

            memberArea.Idarea = updatedMemberArea.Idarea;
            memberArea.Idmember = updatedMemberArea.Idmember;
            memberArea.IsManager = updatedMemberArea.IsManager;

            try
            {
                context.MemberArea.Update(memberArea);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        // DELETE api/MemberArea/5
        /// <summary>
        /// Con este método se puede eliminar una area para un miembro.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int? id)
        {
            MemberArea memberArea = context.MemberArea
                .Find(id);

            if (memberArea == null) return;

            try
            {
                context.MemberArea.Remove(memberArea);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }
    }
}