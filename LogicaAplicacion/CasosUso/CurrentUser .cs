using LogicaNegocio.InterfacesDominio;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public sealed class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _ctx;

        public CurrentUser(IHttpContextAccessor ctx) => _ctx = ctx;

        private ClaimsPrincipal? Principal => _ctx.HttpContext?.User;

        public int? Id =>
            int.TryParse(Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id)
            ? id : null;

        public string? Nombre =>
            Principal?.FindFirst(ClaimTypes.Name)?.Value;

        public string? Rol =>
            Principal?.FindFirst(ClaimTypes.Role)?.Value;
    }
}
