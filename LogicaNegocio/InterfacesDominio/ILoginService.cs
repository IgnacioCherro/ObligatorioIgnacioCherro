using CasosUso.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesDominio
{
    public interface ILoginService
    {
        UsuarioLogueadoDTO Login(LoginDTO loginDto);
    }
}
