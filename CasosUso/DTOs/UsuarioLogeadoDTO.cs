using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosUso.DTOs
{
    public class UsuarioLogueadoDTO
    {
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
