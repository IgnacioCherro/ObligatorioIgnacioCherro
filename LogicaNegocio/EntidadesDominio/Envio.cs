using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio
{
    public class Envio
    {

        public int Id { get; set; }
        public int NumeroTracking { get; set; }
        public Usuario EntregaPedido { get; set; }
        public Usuario RecibePedido { get; set; }
        public Agencia AgenciaDestino { get; set; }
        public int? DireccionPostal { get; set; }
        public TipoEnvio TipoEnvio { get; set; }    
        public int PesoPaquete { get; set; }
        public EstadoEnvio EstadoEnvio { get; set; }
        public DateTime FechaAlta { get; set; }

        public DateTime? FechaEntrega { get; set; }




    }
}
