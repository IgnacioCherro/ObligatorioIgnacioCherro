using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.DTOs
{
    public class AltaEnvioDTO
    {
        public string EmailCliente {  get; set; }
        public string EmailEmpleado { get; set; }
        public TipoEnvio TipoEnvio { get; set; }
        public int Peso { get; set; }
        public int? DireccionPostal { get; set; }
        public int? IdAgenciaDestino { get; set; }

    }
}
