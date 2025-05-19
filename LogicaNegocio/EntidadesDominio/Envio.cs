using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace LogicaNegocio.EntidadesDominio
{
    public abstract class Envio
    {
        public int Id { get; set; }
        public int NumeroTracking { get; set; }
        public int EmpleadoId { get; set; }
        public int ClienteId { get; set; }
        public int Peso { get; set; }
        public EstadoEnvio EstadoEnvio { get; set; }
        public TipoEnvio TipoEnvio { get; set; }    
        public Comentario ComentarioSeguimiento { get; set; }
        public DateTime FechaDeSalida { get; set; }
    }
}
