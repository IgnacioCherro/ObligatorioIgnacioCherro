using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Obligatorio2025.Models;
using System.Diagnostics;

namespace Obligatorio2025.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Menu()
        {
            var rol = HttpContext.Session.GetString("Rol");
            ViewBag.RolUsuario = rol;
            return View();
        }
    }
}
