using CasosUso.DTOs;
using ExepcionesPropias;
using LogicaNegocio.EntidadesDominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Obligatorio2025.Controllers
{
    public class LoginController : Controller
    {
        string apiUrl = "http://localhost:5064/api/login";
        private readonly HttpClient _httpClient;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();

        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult Index(LoginDTO dto)
        {
            try
            {
                var respuesta = _httpClient.PostAsync($"{apiUrl}/login", new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json")).Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var loginDto = JsonConvert.DeserializeObject<UsuarioLogueadoDTO>(respuesta.Content.ReadAsStringAsync().Result);
                    HttpContext.Session.SetString("rol", ((int)loginDto.Rol).ToString());
                    HttpContext.Session.SetString("jwt", loginDto.Token);

                    return RedirectToAction("Authenticated", "Home");
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
