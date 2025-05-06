using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio
{
    public class EnvioUrgente:Envio 
    {
        public int DireccionPostal {  get; set; }
        public Boolean EnvioEficiente { get; set; }
    }
}
