using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Safety.Models;

namespace Safety.Controllers
{
    /// <summary>
    /// La ruta por defecto para los usuarios es api/users
    /// Este es el controlador de los usuarios
    /// Aquí se gestionan todos los usuarios.
    /// </summary>
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        // GET api/users
        /// <summary>
        /// Con este método se pueude obtener un listado de todos los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return context.Users.ToList();
        }

        // GET api/users/GetById/2
        /// <summary>
        /// Con este método se puede obtener un usuario
        /// cuando se le proporciona su id de la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[action]/{id}")]
        [HttpGet]
        //[HttpGet("{id}")]
        public Users GetById(int id)
        {
            return context.Users.Find(id);
        }

        // GET api/users/GetByEmployNumber/50747
        /// <summary>
        /// Con este método se puede obtener un usuario
        /// cuando se le proporciona su numero de empleado
        /// </summary>
        /// <param name="EmployNumber"></param>
        /// <returns></returns>
        [Route("[action]/{EmployNumber}")]
        [HttpGet]
        public Users GetByEmployNumber(string EmployNumber)
        {
            return context.Users
                .Where(w => w.EmployNumber == EmployNumber)
                .FirstOrDefault();
        }

        // POST api/users
        /// <summary>
        /// Con este método se puede crear un nuevo usuario
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        public void Post([FromBody]Users user)
        {
            //El usuario se envía por referencia.
            //Ya que se le puede reajustar varios parametros.
            //Se envía a preparar el usuario.
            PrepareUser(ref user);

            context.Users.Add(user);
            context.SaveChanges();
        }

        // PUT api/users/5
        /// <summary>
        /// Con este método se puede modificar un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newuser"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Users newuser)
        {
            Users user = context.Users
                .Find(id);
            //El usuario se envía por referencia.
            //Ya que se le puede reajustar varios parametros.
            //Se envía a preparar el usuario.
            //PrepareUser(ref user);

            user.EmployNumber = newuser.EmployNumber;
            user.Ipphone = newuser.Ipphone;
            user.FirstName = newuser.FirstName;
            user.SurName = newuser.SurName;
            user.Email = newuser.Email;

            context.Users.Update(user);
            context.SaveChanges();
        }

        // DELETE api/users/5
        /// <summary>
        /// Con este método se puede eliminar un usuario
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Users user = context.Users
                .Find(id);

            context.Users.Remove(user);
            context.SaveChanges();
        }


        /// <summary>
        /// Este método solo se puede invocar desde el controlador
        /// Y es útil para validar los campos de un usuario
        /// Y también es útil para realizarle  cualquier preparación
        /// extra que se necesite antes de ingresar a la base de datos
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool PrepareUser(ref Users user)
        {
            //int employNumber;
            Dictionary<int, string> validations = new Dictionary<int, string>();

            //If the auto-increment ID is not null; then set it to null for the DB.
            if (user.Id != null) user.Id = null;

            //Remove extra spaces from Employ Number
            user.EmployNumber = user.EmployNumber.Trim();

            //
            
            //x = (int.TryParse(user.EmployNumber, out employNumber))
            //    ? ""
            //    : "";


            return true;
        }
    }
}
