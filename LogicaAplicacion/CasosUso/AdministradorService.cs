using CasosUso.DTOs;
using ExepcionesPropias;
using LogicaAplicacion.Mapeadores;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class AdministradorService
    {
        private readonly IRepositorioUsuario _repo;
        private readonly IRepositorioAuditoria _auditoria;


        public AdministradorService(IRepositorioUsuario repo, IRepositorioAuditoria auditoria)
        {
            _repo = repo;
            _auditoria=auditoria;
            
        }

        public void Crear(CrearEmpleadoDTO nuevoUsuario)
        {

            if (nuevoUsuario == null)
                throw new DatosInvalidosExepction("No se proporcionaron datos para el alta");

            var usuario= MappersUsuario.ToUsuario(nuevoUsuario);
            usuario.Validar();
            if (usuario.Rol == RolUsuario.Cliente)
            {
                throw new DatosInvalidosExepction("No puedes crear usuarios de tipo Cliente");
            }
            Usuario buscado = _repo.ObtenerPorEmail(usuario.Email);

            if (buscado != null)
                throw new DatosInvalidosExepction("El nombre de usuario ya existe");
            _repo.Add(usuario);

            var auditoria = new Auditoria()
            {
                AccionRealizada = $"Crear un nuevo empleado con el id {usuario.Id}",
                FechaAuditoria = DateTime.Now,
                UsuarioAuditoria = usuario
            };
            _auditoria.Create(auditoria);



        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            var auditoria = new Auditoria()
            {
                AccionRealizada = $"Eliminado el usuario con el ID {id}",
                FechaAuditoria = DateTime.Now

            };
            _auditoria.Create(auditoria);
        }


        public void Upload(Usuario usuarioEditado)
        {
            usuarioEditado.Validar();
            if(usuarioEditado.Rol== RolUsuario.Cliente)
            {
                throw new DatosInvalidosExepction("No se puede cambiar el rol a Cliente");
            }

            _repo.Update(usuarioEditado);
            var auditoria = new Auditoria()
            {
                AccionRealizada = $"Modificado del empleado con id {usuarioEditado.Id}",
                FechaAuditoria = DateTime.Now,
                UsuarioAuditoria = usuarioEditado
            };
            _auditoria.Create(auditoria);
           
        }

        public Usuario FindById(int id)
        {
            var usuario= _repo.FindById(id);
            if (usuario == null)
            {
                throw new Exception("No se encuentra un usuario con ese id");
            }
            return usuario;

        }



    }
}
