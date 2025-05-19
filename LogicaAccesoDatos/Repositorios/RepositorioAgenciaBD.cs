using LogicaAccesoDatos.EF;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioAgenciaBD : IRepositorioAgencia
    {

        private readonly AgenciaContext _agenciaContext;

        public RepositorioAgenciaBD(AgenciaContext usuarioContext)
        {
            _agenciaContext = usuarioContext;
        }

        public List<Agencia> ObtenerAgencias()
        {
            return _agenciaContext.Agencias.ToList();
        }

        public Agencia ObtenerPorId(int id)
        {
            return _agenciaContext.Agencias.Where(x => x.Id == id).SingleOrDefault();
        }
    }
}
