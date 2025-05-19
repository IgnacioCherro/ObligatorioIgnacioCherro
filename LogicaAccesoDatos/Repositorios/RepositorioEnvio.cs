using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioEnvio : IRepositorioEnvio
    {
        private List<Envio> _envios = new List<Envio>();
        private int ultimoId = 1;

        public void Crear(Envio envio)
        {
            envio.Id = ultimoId++;
            _envios.Add(envio);
        }

        public RepositorioEnvio()
        {
            PrecargaEnvio();
        }

        public Envio FindById(int id)
        {
            return _envios.FirstOrDefault(e => e.Id == id);
        }

        public void Update(Envio envio)
        {
            var existente = _envios.FirstOrDefault(e => e.Id == envio.Id);
            if (existente != null)
            {
                existente.EstadoEnvio = envio.EstadoEnvio;
            }
        }

        List<Envio> FindAll() { 
        
            return _envios; 
        }


        public void PrecargaEnvio()
        {
            //var envio1 = new Envio
            //{
            //    Id = 1,
            //    NumeroTracking = 1,
            //    DireccionPostal = 1,
            //    PesoPaquete = 1,
            //    TipoEnvio = TipoEnvio.COMUN,
            //    FechaAlta = DateTime.Now,
            //    EstadoEnvio = EstadoEnvio.EN_PROCESO,
            //    FechaEntrega = null


            //};
            _envios.Add(new EnvioUrgente());
        }

        public void Add(Envio obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        List<Envio> IRepositorio<Envio>.FindAll()
        {
            throw new NotImplementedException();
        }

        public Envio ObtenerEnviosPorTrackingNumber(int number)
        {
            throw new NotImplementedException();
        }
    }
}
