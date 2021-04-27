using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurismoNet.Models;

namespace TurismoNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : Controller
    {
        private readonly ApplicationDBContext context;

        public ServiciosController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Servicio>> Get()
        {
            return await context.Servicios.ToListAsync();
        }


        [HttpGet("{id}", Name = "ServicioCreado")]
        public IActionResult GetById(int id)
        {
            var servicio = context.Servicios.FirstOrDefault(c => c.Id == id);

            if (servicio == null)
            {
                return NotFound();
            }
            return Ok(servicio);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Servicio serv)
        {
            if (ModelState.IsValid)
            {
                context.Servicios.Add(serv);
                context.SaveChanges();
                return new CreatedAtRouteResult("ServicioCreado", new { id = serv.Id }, serv);
            }
            return BadRequest();
        }

    }
}
