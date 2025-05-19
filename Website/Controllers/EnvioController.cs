using CasosUso.DTOs;
using ExepcionesPropias;
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
        string apiUrlAgencias = "https://localhost:7170/api/agencia";

        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public EnvioController(IHttpClientFactory httpClientFactory)
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

                var respuesta = client.GetAsync($"{apiUrl}/envios").Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var envios = JsonConvert.DeserializeObject<IEnumerable<EnvioDTO>>(respuesta.Content.ReadAsStringAsync().Result);
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
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }


        public IActionResult Create()
        {
            return View();
        }
  
        [HttpPost]
        public IActionResult Create(AltaEnvioDTO dto)
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
                    var envios = JsonConvert.DeserializeObject<IEnumerable<EnvioDTO>>(respuesta.Content.ReadAsStringAsync().Result);
                    ViewBag.Exito = "Envío finalizado.";
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


        [HttpPost]
        public IActionResult AgregarComentario(int idEnvio, string comentario)
        {

            try
            {
                var token = HttpContext.Session.GetString("jwt");
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var respuesta = client.PostAsync($"{apiUrl}/{idEnvio}/comentarios/agregar", new StringContent(JsonConvert.SerializeObject(comentario), Encoding.UTF8, "application/json")).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var objetoComoTexto = respuesta.Content.ReadAsStringAsync().Result;
                    ViewBag.Exito = "Envío finalizado.";
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

        [HttpGet]
        public IActionResult Agencias()
        {
            var token = HttpContext.Session.GetString("jwt");
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = client.GetAsync($"{apiUrlAgencias}/agencias").Result;
            if (!resp.IsSuccessStatusCode) return StatusCode((int)resp.StatusCode);

            var agencias = JsonConvert.DeserializeObject<IEnumerable<Agencia>>(resp.Content.ReadAsStringAsync().Result);
            return Json(agencias);
        }

    }
}
