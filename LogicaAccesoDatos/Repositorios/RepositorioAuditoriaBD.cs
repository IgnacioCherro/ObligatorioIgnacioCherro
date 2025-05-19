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
    public class RepositorioAuditoriaBD : IRepositorioAuditoria
    {
        private readonly AgenciaContext _agenciaContext;

        public RepositorioAuditoriaBD(AgenciaContext usuarioContext)
        {
            _agenciaContext = usuarioContext;
        }

        public void Create(Auditoria auditoria)
        {
            _agenciaContext.Auditorias.Add(auditoria);
            _agenciaContext.SaveChanges();
        }



    }
}
