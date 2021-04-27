using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurismoNet.DTOs;
using TurismoNet.Models;
using System.IO;
using Servicios;
using TurismoNet.Helper;

namespace Turismo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ApplicationDBContext context;


        public ReportesController(ApplicationDBContext context)
        {
            this.context = context;
        }


        [HttpGet]
        [HttpGet("ciudadesMasVisitadas")]
        public IEnumerable<ReporteCiudadesMasVisitadasDTO> GetMasVisitadas()
        {
            return context.DestinosVisitados.OrderBy(x => x.CiudadId)
                          .Where(x => x.FechaBusqueda >= DateTime.Now.AddDays(-30))
                         .GroupBy(x => new { x.CiudadId })
                         .Select(x => new ReporteCiudadesMasVisitadasDTO { Cantidad = x.Count(), CiudadID = x.Key.CiudadId });
        }
        //[HttpGet]
        [HttpGet("reporteVisitas")]
        public ActionResult ReporteEnCSV()
        {
            var x = DataCreate.CrearDocumento().ToString();
            List<string> listaArchivos = new List<string>();
            listaArchivos.Add(x);
            MailManager mail = new MailManager("noReply@dev.com", "jrosas1982@gmail.com", "Adjunto reporte", "Reporte de visitas", listaArchivos);
           // bool a = mail.enviarEmailReporte();
            if (mail.enviarEmailReporte())
                return Ok();
            else 
                return BadRequest();
        }

    }
}
