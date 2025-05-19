using CasosUso.DTOs;
using ExepcionesPropias;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static CasosUso.DTOs.Enums;

namespace Obligatorio2025.Controllers
{
    public class UsuarioController : Controller
    {

        string apiUrl = "https://localhost:7170/api/usuario";
        private readonly IHttpClientFactory _httpClientFactory;

        public UsuarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.GetAsync($"{apiUrl}/empleados").Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var empleados = JsonConvert.DeserializeObject<IEnumerable<UsuarioDTO>>(respuesta.Content.ReadAsStringAsync().Result);
                    return View(empleados);
                }
                else if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Unauthorized();
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ProblemDetails>(respuesta.Content.ReadAsStringAsync().Result);
                    ViewBag.Error = error.Detail;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CrearEmpleadoDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.PostAsync($"{apiUrl}/alta", new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json")).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var objetoComoTexto = respuesta.Content.ReadAsStringAsync().Result;
                    ViewBag.Exito = "Empleado creado con éxito";
                    return RedirectToAction("Index");
                }
                else if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Unauthorized();
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ProblemDetails>(respuesta.Content.ReadAsStringAsync().Result);
                    ViewBag.Error = error.Detail;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }



        [HttpGet]
        public IActionResult Edit(int id )
        {
            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.GetAsync($"{apiUrl}/{id}").Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var empleados = JsonConvert.DeserializeObject<UsuarioDTO>(respuesta.Content.ReadAsStringAsync().Result);
                    return View(empleados);
                }
                else if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Unauthorized();
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ProblemDetails>(respuesta.Content.ReadAsStringAsync().Result);
                    ViewBag.Error = error.Detail;
                    return Problem();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return Problem(statusCode: 500, title: "Internal server error", detail: "A ocurrido un error inesperado, intente de nuevo mas tarde");
            }
        }

        [HttpPost]
        public IActionResult Edit(UsuarioDTO emp)
        {
            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.PostAsync($"{apiUrl}/{emp.Id}/editar", new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json")).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var objetoComoTexto = respuesta.Content.ReadAsStringAsync().Result;
                    ViewBag.Exito = "Empleado actualizado con éxito";
                    return RedirectToAction("Index");
                }
                else if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Unauthorized();
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ProblemDetails>(respuesta.Content.ReadAsStringAsync().Result);
                    ViewBag.Error = error.Detail;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
 
        public IActionResult Delete(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.PostAsync($"{apiUrl}/{id}/borrar", null).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var objetoComoTexto = respuesta.Content.ReadAsStringAsync().Result;
                    ViewBag.Exito = "Empleado eliminado con éxito";
                    return RedirectToAction("Index");
                }
                else if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Unauthorized();
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ProblemDetails>(respuesta.Content.ReadAsStringAsync().Result);
                    ViewBag.Error = error.Detail;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
