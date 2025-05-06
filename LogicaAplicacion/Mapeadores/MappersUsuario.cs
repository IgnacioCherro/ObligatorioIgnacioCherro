using CasosUso.DTOs;
using LogicaNegocio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mapeadores
{
    public class MappersUsuario
    {

        public static UsuarioLogueadoDTO ToUsuarioLogueadoDTO(Usuario usuarioLogeado)
        {
            return new UsuarioLogueadoDTO
            {
                NombreCompleto = $"{usuarioLogeado.Nombre} {usuarioLogeado.Apellido}",
                Email = usuarioLogeado.Email,
                Rol = usuarioLogeado.Rol
            };
        }

        public static UsuarioDTO ToUsuarioDTO(Usuario entidad)
        {
            return new UsuarioDTO
            {
                Nombre = entidad.Nombre,
                Apellido = entidad.Apellido,
                Email = entidad.Email,
                Contrasenia = entidad.Contrasenia,
                Rol = entidad.Rol
            };
        }

        public static Usuario ToUsuario(UsuarioDTO dto)
        {
            return new Usuario(dto.Nombre)
            {
                Apellido = dto.Apellido,
                Email = dto.Email,
                Contrasenia = dto.Contrasenia,
                Rol = dto.Rol
            };
        }

        public static Usuario ToUsuario(CrearEmpleadoDTO dto)
        {
            return new Usuario(dto.Nombre)
            {
                Apellido = dto.Apellido,
                Email = dto.Email,
                Contrasenia = dto.Contrasenia,
                Rol = dto.Rol
            };

        }



    }
}
