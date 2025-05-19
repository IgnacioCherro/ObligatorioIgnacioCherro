using LogicaNegocio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasosUso.DTOs;

namespace LogicaNegocio.InterfacesDominio
{
    public interface IUsuarioService
    {
        List<UsuarioDTO> ObtenerTodos();
        UsuarioDTO ObtenerPorId(int id);
        void Crear(CrearEmpleadoDTO dto);
        void Editar(UsuarioDTO empleado);
        void Eliminar(int id);
        List<UsuarioDTO> ObtenerTodosLosFuncionarios();
    }
}
