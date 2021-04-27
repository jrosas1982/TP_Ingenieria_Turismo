using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoNet.Models
{
    public class Atraccion
    {
        public int Id { get; set; }
        public string DescCorta { get; set; }
        public string DescLarga { get; set; }
        [ForeignKey("Ciudad")]
        public int CiudadId { get; set; }
        [JsonIgnore]
        public Ciudad ciudad { get; set; }
        //public byte[] FotoSitio { get; set; }
    }
}
