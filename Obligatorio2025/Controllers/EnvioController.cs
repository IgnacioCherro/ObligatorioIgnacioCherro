using CasosUso.DTOs;
using ExepcionesPropias;
using Humanizer;
using LogicaAplicacion.CasosUso;
using LogicaNegocio.EntidadesDominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Obligatorio2025.Controllers
{
    public class EnvioController : Controller
    {

        string apiUrl = "https://localhost:7170/api/envio";

        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public EnvioController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult AltaEnvio()
        {
            return View();
        }
  
        [HttpPost]
        public IActionResult AltaEnvio(AltaEnvioDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.PostAsync( $"{apiUrl}/alta", new StringContent(JsonConvert.SerializeObject(dto),Encoding.UTF8,"application/json")).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var objetoComoTexto = respuesta.Content.ReadAsStringAsync().Result;
                    ViewBag.Exito = "Envio creado satisfactoriamente";
                    return View();
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
        public IActionResult FinalizarEnvio()
        {
            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.GetAsync($"{apiUrl}/envios").Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var envios = JsonConvert.DeserializeObject<IEnumerable<Envio>>(respuesta.Content.ReadAsStringAsync().Result);
                    return View(envios);
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
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

   
        [HttpPost]
        public IActionResult FinalizarEnvio(int id)
        {

            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.PostAsync($"{apiUrl}/{id}/finalizar", null).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var envios = JsonConvert.DeserializeObject<IEnumerable<Envio>>(respuesta.Content.ReadAsStringAsync().Result);
                    ViewBag.Exito = "Envío finalizado.";
                    return RedirectToAction("FinalizarEnvio", "Envio");
                }
                else if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Unauthorized();
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ProblemDetails>(respuesta.Content.ReadAsStringAsync().Result);
                    ViewBag.Error = error.Detail;
                    return RedirectToAction("FinalizarEnvio", "Envio");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("FinalizarEnvio", "Envio");
            }
        }


        [HttpGet]
        public IActionResult AgregarComentario()
        {
            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.GetAsync($"{apiUrl}/comentarios").Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var comentarios = JsonConvert.DeserializeObject<IEnumerable<Envio>>(respuesta.Content.ReadAsStringAsync().Result);
                    return View(comentarios);
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



        [HttpPost]
        public IActionResult AgregarComentario(int idEnvio, string comentario)
        {

            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.PostAsync($"{apiUrl}/{idEnvio}/comentarios/agregar", new StringContent(comentario, Encoding.UTF8, "application/json")).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var objetoComoTexto = respuesta.Content.ReadAsStringAsync().Result;
                    ViewBag.Exito = "Envío finalizado.";
                    return RedirectToAction("AgregarComentario");
                }
                else if (respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Unauthorized();
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ProblemDetails>(respuesta.Content.ReadAsStringAsync().Result);
                    ViewBag.Error = error.Detail;
                    return RedirectToAction("AgregarComentario");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("AgregarComentario");
            }
        }
    }
}
