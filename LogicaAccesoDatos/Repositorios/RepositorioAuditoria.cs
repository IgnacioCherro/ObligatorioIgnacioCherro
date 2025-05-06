using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioAuditoria : IRepositorioAuditoria
    {
        List<Auditoria> auditorias = new List<Auditoria>();
        public void Create(Auditoria auditoria)
        {
            auditorias.Add(auditoria);
        }
    }
}
