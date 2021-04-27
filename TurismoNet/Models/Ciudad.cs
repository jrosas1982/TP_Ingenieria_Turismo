using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoNet.Models
{
    public class Ciudad
    {
        public Ciudad()
        {
            Atracciones = new List<Atraccion>();
            Servicios = new List<Servicio>();
            //    DestinosVisitados = new List<DestinosMasVIsitados>();
        }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<Atraccion> Atracciones { get; set; }
        public List<Servicio> Servicios { get; set; }
        // public List<DestinosMasVIsitados> DestinosVisitados { get; set; }  
        //public byte[] FotoCiudad { get; set; }
    }
}