using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace CasosUso.DTOs
{
    public class UsuarioLogueadoDTO
    {
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public RolUsuario Rol { get; set; }
        public string Token { get; set; }
    }
}
