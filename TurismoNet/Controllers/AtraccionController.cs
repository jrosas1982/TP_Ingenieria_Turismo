using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoNet.Models;

namespace TurismoNet.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Ciudad/{CiudadId}/Atraccion")]
    [ApiController]
    public class AtraccionController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public AtraccionController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Atraccion> GetAll(int CiudadId)
        {
            return _context.Atracciones.Where(x => x.CiudadId == CiudadId).ToList();
        }
        [HttpGet("{id}", Name = "AtraccionCreada")]
        public IActionResult GetById(int id)
        {
            var sitio = _context.Atracciones.FirstOrDefault(s => s.Id == id);

            if (sitio == null)
            {
                return NotFound();
            }
            return new ObjectResult(sitio);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Atraccion atraccion, int ciudadId)
        {
            atraccion.CiudadId = ciudadId;
            if (ModelState.IsValid)
            {
                _context.Atracciones.Add(atraccion);
                _context.SaveChanges();
                return new CreatedAtRouteResult("AtraccionCreada", new { id = atraccion.Id }, atraccion);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Atraccion atraccion, int id)
        {
            if (atraccion.Id != id)
            {
                return BadRequest();
            }
            _context.Entry(atraccion).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sitio = _context.Atracciones.FirstOrDefault(s => s.Id == id);

            if (sitio != null)
            {
                _context.Atracciones.Remove(sitio);
                _context.SaveChanges();
                return Ok(sitio);
            }
            return NotFound();
        }
    }
}
