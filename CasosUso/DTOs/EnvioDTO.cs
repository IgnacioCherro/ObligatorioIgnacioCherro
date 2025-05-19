using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace CasosUso.DTOs
{
    public class EnvioDTO
    {
        public int Id { get; set; }
        public int NumeroTracking { get; set; }
        public UsuarioDTO Empleado { get; set; }
        public UsuarioDTO Cliente { get; set; }
        public int Peso { get; set; }
        public EstadoEnvio EstadoEnvio { get; set; }
        public TipoEnvio TipoEnvio { get; set; }
        public ComentarioDTO ComentarioSeguimiento { get; set; }
        public DateTime FechaDeSalida { get; set; }
    }
}
