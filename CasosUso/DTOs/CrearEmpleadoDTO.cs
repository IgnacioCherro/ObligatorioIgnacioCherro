
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace CasosUso.DTOs
{
    public class CrearEmpleadoDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public RolUsuario Rol { get; set; } = RolUsuario.Funcionario;
    }
}
