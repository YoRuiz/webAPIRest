using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webAPIRest_Ent;
using webAPIRest_BL;

namespace webAPIRest.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<clsPersona> Get()
        {
            return new clsListadosBL().listarBL();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            clsManejadoraPersonaBL maneja = new clsManejadoraPersonaBL();
            if (maneja.getPersonaBL(id) != null)
            {
                return new ObjectResult( maneja.getPersonaBL(id));
            }else
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]clsPersona p)
        {   
            new clsManejadoraPersonaBL().insertaPersonaBL(p);           
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]clsPersona p)
        {
            clsManejadoraPersonaBL lista = new clsManejadoraPersonaBL();
            p.id = id;
            //controlar si existe o no
            if (lista.getPersonaBL(id) != null)
            {
                new clsManejadoraPersonaBL().actualizaPersonaBL(p);
            }else
            {
                Response.StatusCode = 404;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            clsManejadoraPersonaBL lista = new clsManejadoraPersonaBL();
            if (lista.getPersonaBL(id) != null)
            {
                new clsManejadoraPersonaBL().borraPersonaBL(id);
            }else
            {
                Response.StatusCode = 204;
            }
        }
    }
}
