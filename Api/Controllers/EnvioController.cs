using CasosUso.DTOs;
using ExepcionesPropias;
using LogicaAplicacion.CasosUso;
using LogicaNegocio.InterfacesDominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Obligatorio2025.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnvioController : ControllerBase
    {
        private readonly IEnvioService _envioService;

        public EnvioController(IEnvioService envioService)
        {
            _envioService = envioService;
        }

        [Authorize]
        [HttpPost("alta")]
        public IActionResult AltaEnvio(AltaEnvioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(dto);

            try
            {
                _envioService.Crear(dto);
                return Ok();
            }
            catch (DatosInvalidosExepction ex)
            {
                var error = new ProblemDetails()
                {
                    Title = "Datos invalidos",
                    Status = 400,
                    Detail = ex.Message
                };

                return BadRequest(error);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [Authorize]
        [HttpGet("envios")]
        public IActionResult ObtenerEnvios()
        {
            try
            {
                return Ok(_envioService.ObtenerEnviosEnProceso());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }


        [Authorize]
        [HttpPost("{id:int}/finalizar")]
        public IActionResult FinalizarEnvio(int id)
        {
            try
            {
                _envioService.Finalizar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [Authorize]
        [HttpGet("comentarios")]
        public IActionResult AgregarComentario()
        {
            try
            {
                return Ok(_envioService.ObtenerEnviosEnProceso());
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }


        [Authorize]
        [HttpPost("{id:int}/comentarios/agregar")]
        public IActionResult AgregarComentario(int id, [FromBody]string comentario)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _envioService.AgregarComentario(id, comentario);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [HttpGet("trackingNumber/{number:int}")]
        public IActionResult ObtenerEnvios(int number)
        {
            try
            {
                return Ok(_envioService.ObtenerEnviosPorTrackingNumber(number));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}
