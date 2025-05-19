using CasosUso.DTOs;
using ExepcionesPropias;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CasosUso.DTOs.Enums; 

namespace Obligatorio2025.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _empleadoService;

        public UsuarioController(IUsuarioService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [Authorize]
        [HttpPost("alta")]
        public IActionResult Create(CrearEmpleadoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _empleadoService.Crear(dto);
                return Ok();
            }
            catch(DatosInvalidosExepction ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }


        [Authorize]
        [HttpGet("empleados")]
        public IActionResult ListaEmpleado()
        {
            try
            {
                return Ok(_empleadoService.ObtenerTodosLosFuncionarios());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                return Ok(_empleadoService.ObtenerPorId(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [Authorize]
        [HttpPost("{id:int}/editar")]
        public IActionResult Edit(int id, UsuarioDTO emp)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _empleadoService.Editar(emp);
                return Ok();
            }
            catch (DatosInvalidosExepction ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [Authorize]
        [HttpPost("{id:int}/borrar")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _empleadoService.Eliminar(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}
