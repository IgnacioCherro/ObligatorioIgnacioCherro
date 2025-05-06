using ExepcionesPropias;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio
{
   

    public class Usuario : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }

        public RolUsuario Rol { get; set; }


        public Usuario() { }

        public Usuario(string nombre) {
            Nombre = nombre;
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new DatosInvalidosExepction("El nombre no puede estar vacio");
            }

            if (string.IsNullOrEmpty(Apellido))
            {
                throw new DatosInvalidosExepction("El apellido no puede estar vacio");
            }

            if (string.IsNullOrEmpty(Email))
            {
                throw new DatosInvalidosExepction("El Email no puede estar vacio");
            }

            if (string.IsNullOrEmpty(Contrasenia))
            {
                throw new DatosInvalidosExepction("La contraseña no puede estar vacia");
            }

        }
    }
}
