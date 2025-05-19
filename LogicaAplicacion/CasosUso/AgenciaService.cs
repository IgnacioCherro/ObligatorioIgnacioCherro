using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class AgenciaService : IAgenciaService
    {

        private readonly IRepositorioAgencia _repositorioAgencia;


        public AgenciaService(IRepositorioAgencia repositorioAgencia)
        {
            _repositorioAgencia = repositorioAgencia;
        }

        public List<Agencia> ObtenerAgencias()
        {
            return _repositorioAgencia.ObtenerAgencias();
        }

        public Agencia ObtenerPorId(int id)
        {
            return _repositorioAgencia.ObtenerPorId(id);
        }
    }
}
