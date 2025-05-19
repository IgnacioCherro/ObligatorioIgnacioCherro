using ExepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace LogicaNegocio.EntidadesDominio
{
   

    public class Usuario : IValidable
    {
        public int Id { get; set; }
        public NombreUsuario Nombre { get; set; }
        public string Apellido { get; set; }
        public EmailUsuario Email { get; set; }
        public string Contrasenia { get; set; }
        public RolUsuario Rol { get; set; }


        public Usuario() { }

        public Usuario(NombreUsuario nombre) {
            Nombre = nombre;
            Validar();
        }

        public void Validar()
        {
            if (Nombre == null) throw new DatosInvalidosExepction("El nombre no puede ser null");

           

        }
    }
}
