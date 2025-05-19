using CasosUso.DTOs;
using LogicaAplicacion.Mapeadores;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using static CasosUso.DTOs.Enums;

namespace LogicaAplicacion.CasosUso
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepositorioAuditoria _auditoriaRepo;
        private readonly IRepositorioUsuario _usuarioRepo;
        private readonly ICurrentUser _user;

        public UsuarioService(IRepositorioAuditoria audRepo, IRepositorioUsuario usuRepo, ICurrentUser currentUser)
        {
            _auditoriaRepo = audRepo;
            _usuarioRepo = usuRepo;
            _user = currentUser;
        }


        public void Crear(CrearEmpleadoDTO dto)
        {
            if ((RolUsuario)int.Parse(_user.Rol) != RolUsuario.Administrador)
                throw new InvalidOperationException("No tiene permisos para realizar esta acción.");

            var mailDuplicado = _usuarioRepo.ObtenerPorEmail(dto.Email);

            if (mailDuplicado != null)
            {
                throw new InvalidOperationException("Correo ya existente.");
            }

            var nuevoUsuario = MappersUsuario.ToUsuario(dto);

            nuevoUsuario.Validar();

            _usuarioRepo.Add(nuevoUsuario);

            var usuarioActual = _usuarioRepo.FindById(_user.Id.Value);

            _auditoriaRepo.Create(new Auditoria
            {
                UsuarioAuditoria = usuarioActual,
                FechaAuditoria = DateTime.Now,
                AccionRealizada = $"Alta de empleado: {dto.Nombre} ({dto.Email})"
            });
        }

        public void Editar(UsuarioDTO empleado)
        {
            if ((RolUsuario)int.Parse(_user.Rol) != RolUsuario.Administrador)
                throw new InvalidOperationException("No tiene permisos para realizar esta acción.");

            _usuarioRepo.Update(MappersUsuario.ToUsuario(empleado));

            var usuarioActual = _usuarioRepo.FindById(_user.Id.Value);

            _auditoriaRepo.Create(new Auditoria
            {
                UsuarioAuditoria = usuarioActual,
                FechaAuditoria = DateTime.Now,
                AccionRealizada = $"Empleado editado: {empleado.Email})"
            });
        }

        public void Eliminar(int id)
        {
            if ((RolUsuario)int.Parse(_user.Rol) != RolUsuario.Administrador)
                throw new InvalidOperationException("No tiene permisos para realizar esta acción.");

            var usuarioABorrar = _usuarioRepo.FindById(id);
            if (usuarioABorrar != null)
            {

                _usuarioRepo.Delete(id);
                var usuarioActual = _usuarioRepo.FindById(_user.Id.Value);

                _auditoriaRepo.Create(new Auditoria
                {
                    UsuarioAuditoria = usuarioActual,
                    FechaAuditoria = DateTime.Now,
                    AccionRealizada = $"Empleado eliminado: {usuarioABorrar.Email})"
                });
            }
        }

        public UsuarioDTO ObtenerPorId(int id)
        {
            var usuario = _usuarioRepo.FindById(id);
            if (usuario == null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }

            return MappersUsuario.ToUsuarioDTO(usuario);
        }

        public List<UsuarioDTO> ObtenerTodos()
        {
            return _usuarioRepo.FindAll().Select(y => MappersUsuario.ToUsuarioDTO(y)).ToList();
        }

        public List<UsuarioDTO> ObtenerTodosLosFuncionarios()
        {
            return ObtenerTodos().Where(x => x.Rol == RolUsuario.Funcionario).ToList();
        }

    }
}
