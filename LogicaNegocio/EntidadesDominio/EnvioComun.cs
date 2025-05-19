using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace LogicaNegocio.EntidadesDominio
{
    public class EnvioComun : Envio
    {
        public Agencia Agencia { get; set; }

    }
}
