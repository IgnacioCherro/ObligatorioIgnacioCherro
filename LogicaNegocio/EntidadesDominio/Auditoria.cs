using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string AccionRealizada { get; set; }
        public DateTime FechaAuditoria { get; set; }
        public Usuario UsuarioAuditoria { get; set; }

    }
}
