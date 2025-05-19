using CasosUso.DTOs;
using LogicaAplicacion.CasosUso;
using LogicaNegocio.InterfacesDominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgenciaController : Controller
    {

        private readonly IAgenciaService _agenciaService;

        public AgenciaController(IAgenciaService agenciaService)
        {
            _agenciaService = agenciaService;
        }

        [HttpGet("{id:int}")]
        public ActionResult Agencia(int id)
        {
            try
            {
                return Ok(_agenciaService.ObtenerPorId(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [HttpGet("agencias")]
        public ActionResult Agencias()
        {
            try
            {
                return Ok(_agenciaService.ObtenerAgencias());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}

