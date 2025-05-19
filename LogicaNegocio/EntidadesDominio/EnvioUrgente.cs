using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace LogicaNegocio.EntidadesDominio
{
    public class EnvioUrgente : Envio
    {
        public int DireccionPostal { get; set; }
        public bool EnvioEficiente { get; set; }
    }
}
