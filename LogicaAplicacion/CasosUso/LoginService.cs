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
using static CasosUso.DTOs.Enums;
using LogicaNegocio.InterfacesDominio;

namespace LogicaAplicacion.CasosUso
{
    public class LoginService: ILoginService
    {
        private readonly IRepositorioUsuario _repo;
        private readonly JwtService _jwtService;

        public LoginService(IRepositorioUsuario repo, JwtService jwtService)
        {
            _jwtService = jwtService;
            _repo = repo;
        }
        
        public UsuarioLogueadoDTO Login(LoginDTO loginDto)
        {
            EmailUsuario emailVO;

            try
            {
                emailVO = new EmailUsuario(loginDto.Email);
            }
            catch
            {
                throw new DatosInvalidosExepction("Email inválido.");
            }

            var usuario = _repo.ObtenerPorEmail(emailVO.Valor);

            if (usuario == null)
                throw new DatosInvalidosExepction("Email o contraseña incorrectos.");

            if (usuario.Contrasenia != loginDto.Contrasenia)
                throw new DatosInvalidosExepction("Email o contraseña incorrectos.");

            if (usuario.Rol == RolUsuario.Cliente)
                throw new DatosInvalidosExepction("Acceso denegado para este tipo de usuario.");

            var token = _jwtService.GenerarToken(usuario);

            var usuarioLogeado = MappersUsuario.ToUsuarioLogueadoDTO(usuario);
            usuarioLogeado.Token = token;
            return usuarioLogeado;
        }




    }
}
