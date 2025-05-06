using CasosUso.DTOs;
using LogicaAplicacion.Mapeadores;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using ExepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class LoginService
    {
        private readonly IRepositorioUsuario _repo;

        public LoginService(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public UsuarioLogueadoDTO Login(LoginDTO loginDto)
        {
            var usuario = _repo.ObtenerPorEmail(loginDto.Email);

            if (usuario == null)
                throw new DatosInvalidosExepction ("Usuario no encontrado");

            if (usuario.Contrasenia != loginDto.Contrasenia)
                throw new DatosInvalidosExepction("La contraseña no es correcta");

            if (usuario.Rol == RolUsuario.Cliente)
                throw new DatosInvalidosExepction("Los clientes no pueden iniciar sesión");

            return MappersUsuario.ToUsuarioLogueadoDTO(usuario);
        }
    }
}
