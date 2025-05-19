using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.DTOs
{
    public class Enums
    {

        public enum RolUsuario
        {
            Funcionario,
            Cliente,
            Administrador
        }

        public enum TipoEnvio
        {
            URGENTE,
            COMUN
        }

        public enum EstadoEnvio
        {
            EN_PROCESO,
            FINALIZADO

        }
    }
}
