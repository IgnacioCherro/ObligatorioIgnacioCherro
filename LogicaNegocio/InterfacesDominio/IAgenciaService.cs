using LogicaNegocio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesDominio
{
    public interface IAgenciaService
    {
        List<Agencia> ObtenerAgencias();
        Agencia ObtenerPorId(int id);

    }
}
