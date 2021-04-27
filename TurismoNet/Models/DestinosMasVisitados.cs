using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoNet.Models
{
    public class DestinosMasVIsitados
    {
        public int Id { get; set; }
        public DateTime FechaBusqueda { get; set; }
        [ForeignKey("Ciudad")]
        public int CiudadId { get; set; }
        [JsonIgnore]
        public Ciudad ciudad { get; set; }



    }
}
