using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoNet.Models
{
    public class Servicio
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Direccion { get; set; }
        [ForeignKey("Ciudad")]
        public int CiudadId { get; set; }
        [JsonIgnore]
        public Ciudad ciudad { get; set; }
    }
}
