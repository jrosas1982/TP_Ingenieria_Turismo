using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurismoNet.Models
{
    public class ApplicationDBContext  :  IdentityDbContext<ApplicationUser>
    {
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Atraccion> Atracciones { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<DestinosMasVIsitados> DestinosVisitados { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }
    }
}
