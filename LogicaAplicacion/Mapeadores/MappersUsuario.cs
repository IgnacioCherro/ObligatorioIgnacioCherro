using CasosUso.DTOs;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.ValueObjects;
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
                NombreCompleto = $"{usuarioLogeado.Nombre.Valor} {usuarioLogeado.Apellido}",
                Email = usuarioLogeado.Email.Valor,
                Rol = usuarioLogeado.Rol
            };
        }

        public static UsuarioDTO ToUsuarioDTO(Usuario entidad)
        {
            return new UsuarioDTO
            {
                Nombre = entidad.Nombre.Valor,
                Apellido = entidad.Apellido,
                Email = entidad.Email.Valor,
                Contrasenia = entidad.Contrasenia,
                Rol = entidad.Rol,
                Id = entidad.Id

            };
        }

        public static Usuario ToUsuario(UsuarioDTO dto)
        {
            return new Usuario()
            {
                Apellido = dto.Apellido,
                Email = new EmailUsuario(dto.Email),
                Contrasenia = dto.Contrasenia,
                Rol = dto.Rol,
                Id= dto.Id,
                Nombre = new NombreUsuario(dto.Nombre)
            };
        }

        public static Usuario ToUsuario(CrearEmpleadoDTO dto)
        {
            return new Usuario(new NombreUsuario(dto.Nombre))
            {
                Apellido = dto.Apellido,
                Email = new EmailUsuario(dto.Email),
                Contrasenia = dto.Contrasenia,
                Rol = dto.Rol
            };

        }

    }
}
