using LogicaAccesoDatos.EF;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using static CasosUso.DTOs.Enums;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioEnvioBD : IRepositorioEnvio
    {
        private readonly AgenciaContext _agenciaContext;

        public RepositorioEnvioBD(AgenciaContext envioContext)
        {
            _agenciaContext = envioContext;
        }

        public void Add(Envio obj)
        {
            _agenciaContext.Add(obj);
            _agenciaContext.SaveChanges();

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Envio> FindAll()
        {
            return _agenciaContext.Envios.Include(e => e.ComentarioSeguimiento).ToList();
        }

        public Envio FindById(int id)
        {
            return _agenciaContext.Envios.Where(x => x.Id == id).Include(e => e.ComentarioSeguimiento).SingleOrDefault();
        }

        public Envio ObtenerEnviosPorTrackingNumber(int number)
        {
            return _agenciaContext.Envios.Where(x => x.NumeroTracking == number).Include(e => e.ComentarioSeguimiento).SingleOrDefault();
        }

        public void Update(Envio obj)
        {
            _agenciaContext.Envios.Update(obj);
            _agenciaContext.SaveChanges();
        }
    }
}
