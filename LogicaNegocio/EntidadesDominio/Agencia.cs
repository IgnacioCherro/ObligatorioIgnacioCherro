using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio
{
    public class Agencia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string DireccionPostal { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
