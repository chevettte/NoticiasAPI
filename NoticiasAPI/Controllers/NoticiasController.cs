using ConectarDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NoticiasAPI.Models;
using System.Data.Entity;

namespace NoticiasAPI.Controllers
{
    public class NoticiasController : ApiController
    {
        private NoticiasEntities dbNoticias = new NoticiasEntities();

        // Visualizamos los registros de nuestras noticias
        [HttpGet]
        public IEnumerable<dashboard> Get()
        {
            using (NoticiasEntities noticiasEntities = new NoticiasEntities())
            {
                return noticiasEntities.dashboards.ToList();
            }
        }

        // Visualiza solo un registro
        [HttpPost]
        public dashboard Get(int ID)
        {
            using (NoticiasEntities noticiasEntities = new NoticiasEntities())
            {
                return noticiasEntities.dashboards.FirstOrDefault(c => c.id == ID);
            }
        }

        // Guarda nuevas noticias
        public IHttpActionResult noticiaNueva([FromBody]dashboard not)
        {
            if(ModelState.IsValid)
            {
                dbNoticias.dashboards.Add(not);
                dbNoticias.SaveChanges();
                return Ok(not);
            }
            else
            {
                return BadRequest();
            }
        }

        //Modificación de registros
        [HttpPut]
        public IHttpActionResult actualizarUsuario(int ID, [FromBody]dashboard not)
        {
            if (ModelState.IsValid)
            {
                var usuarioExiste = dbNoticias.dashboards.Count(c => c.id == ID) > 0;

                if (usuarioExiste)
                {
                    dbNoticias.Entry(not).State = EntityState.Modified;
                    dbNoticias.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }


        //Borarr los registros de Noticias
        [HttpDelete]
        public IHttpActionResult eliminarNoticia(int ID)
        {
            var not = dbNoticias.dashboards.Find(ID);

            if(not != null)
            {
                dbNoticias.dashboards.Remove(not);
                dbNoticias.SaveChanges();

                return Ok(not);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
