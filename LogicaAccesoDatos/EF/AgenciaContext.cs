using LogicaNegocio.EntidadesDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace LogicaAccesoDatos.EF
{
    public class AgenciaContext : DbContext
    {
        public DbSet<Usuario> Usuarios{ get; set; }
        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

        public AgenciaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            ConfigurarHerenciaEnvio(mb);
        }

        void ConfigurarHerenciaEnvio(ModelBuilder mb)
        {
            mb.Entity<Envio>()
               .HasDiscriminator<TipoEnvio>(nameof(Envio.TipoEnvio))
               .HasValue<EnvioComun>(TipoEnvio.COMUN)
               .HasValue<EnvioUrgente>(TipoEnvio.URGENTE);

        }


    }
}
