using CasosUso.DTOs;
using ExepcionesPropias;
using LogicaAplicacion;
using LogicaAplicacion.CasosUso;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesDominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Obligatorio2025.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;


        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


      
        [HttpPost("login")]
        public ActionResult Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Ok(_loginService.Login(dto));

            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}
