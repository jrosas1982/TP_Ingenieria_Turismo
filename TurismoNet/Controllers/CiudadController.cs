using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicios;
using TurismoNet.DTO;
using TurismoNet.DTOs;
using TurismoNet.Helper;
using TurismoNet.Models;

namespace TurismoNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, agencia") ]
    public class CiudadController : ControllerBase
    {
        public readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public CiudadController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //[HttpGet]
        [HttpGet("ciudades")]
        // [Authorize(Roles = "Administrador")]
        //public IEnumerable<Ciudad> Get()
        public IEnumerable<CiudadesDTO> Get()
        {
            //return context.Ciudades.ToList();
            return mapper.Map<List<Ciudad>, List<CiudadesDTO>>(context.Ciudades.ToList());
        }

        //[HttpGet]
        [HttpGet("ciudadesMasVisitadas")]
        public IEnumerable<ReporteCiudadesMasVisitadasDTO> GetMasVisitadas()
        {
            return context.DestinosVisitados.OrderBy(x => x.CiudadId)
                          .Where(x => x.FechaBusqueda >= DateTime.Now.AddDays(-30))
                         .GroupBy(x => new { x.CiudadId })
                         .Select(x => new ReporteCiudadesMasVisitadasDTO { Cantidad = x.Count(), CiudadID = x.Key.CiudadId });
        }

        [HttpGet("{id}", Name = "CiudadCreada")]
        public IActionResult GetById(int id)
        {
            var ciudad = context.Ciudades
                .Include(x => x.Atracciones)
                .Include(y => y.Servicios)
                .FirstOrDefault(c => c.Id == id);

            var sumarVisita = new DestinosMasVIsitados()
            {
                FechaBusqueda = DateTime.Now,
                CiudadId = id
            };
            context.Add(sumarVisita);
            context.SaveChanges();

            if (ciudad == null)
            {
                return NotFound();
            }
            return Ok(ciudad);
        }

        //[HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, agencia")]
        [HttpPost("Crear")]
        //[Route("crear")]
        public IActionResult Post([FromBody] Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                context.Ciudades.Add(ciudad);
                context.SaveChanges();
                return new CreatedAtRouteResult("CiudadCreada", new { id = ciudad.Id }, ciudad);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, agencia")]
        public IActionResult Put([FromBody] Ciudad ciudad, int id)
        {
            if (ciudad.Id != id)
            {
                return BadRequest();
            }
            context.Entry(ciudad).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin, agencia")]
        public IActionResult Delete(int id)
        {
            var ciudad = context.Ciudades.FirstOrDefault(c => c.Id == id);

            if (ciudad != null)
            {
                context.Ciudades.Remove(ciudad);
                context.SaveChanges();
                return Ok(ciudad);
            }
            return NotFound();
        }

    }
}
