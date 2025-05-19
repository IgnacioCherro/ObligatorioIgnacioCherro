using LogicaAccesoDatos.EF;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuarioBD : IRepositorioUsuario
    {
        private readonly AgenciaContext _agenciaContext;

        public RepositorioUsuarioBD(AgenciaContext usuarioContext)
        {
            _agenciaContext = usuarioContext;
        }



        public void Add(Usuario usuario)
        {
            _agenciaContext.Usuarios.Add(usuario);
            _agenciaContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var usuarioABorrar = FindById(id);
            if(usuarioABorrar == null)
            {
                throw new InvalidOperationException("Usuario no existente");
            }

            _agenciaContext.Usuarios.Remove(usuarioABorrar);
            _agenciaContext.SaveChanges();

        }

        public List<Usuario> FindAll()
        {
            return _agenciaContext.Usuarios.ToList();
        }

        public Usuario FindById(int id)
        {
            return _agenciaContext.Usuarios.Where(x => x.Id == id).SingleOrDefault();
        }

        public Usuario ObtenerPorEmail(string email)
        {
            return FindAll().Where(x => x.Email.Valor == email).SingleOrDefault();
        }


        public void Update(Usuario obj)
        {
            var usuarioAEditar = FindById(obj.Id);
            if (usuarioAEditar  == null)
            {
                throw new InvalidOperationException("Usuario no existente");
            }


            usuarioAEditar.Nombre = obj.Nombre;
            usuarioAEditar.Apellido = obj.Apellido;
            usuarioAEditar.Email = obj.Email;
            usuarioAEditar.Contrasenia = obj.Contrasenia;

            _agenciaContext.SaveChanges();
        }
    }
}
