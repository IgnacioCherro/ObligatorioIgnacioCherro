using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExepcionesPropias;
using LogicaNegocio.ValueObjects;
using static CasosUso.DTOs.Enums;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuarioMemoria : IRepositorioUsuario
    {

        private static List<Usuario> usuarios = new List<Usuario>();
        private static int ultimoId = 1;



        public RepositorioUsuarioMemoria()
        {
            CargarDatosPrueba();
        }

        public void Add(Usuario usuario)
        {
            usuario.Id = ultimoId++;
            usuarios.Add(usuario);
        }


        public List<Usuario> FindAll()
        {
            return usuarios;
        }

        public Usuario ObtenerPorEmail(string email)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario.Email.Valor == email)
                {
                    return usuario;
                }
            }
            return null;
        }

        public void Delete(int id)
        {
            usuarios.RemoveAll(x => x.Id == id);
        }

        public void Update(Usuario usuario)
        {
            var usuarioViejo = FindById(usuario.Id);
            usuarioViejo = usuario;

        }

        public Usuario FindById(int id)
        {

            return usuarios.Where(x => x.Id == id).FirstOrDefault();

        }

        public void CargarDatosPrueba()
        {
       
            var nombre = new NombreUsuario("Juan");
            var email = new EmailUsuario("juan@correo.com");
            var rol = RolUsuario.Administrador;
            var usuario = new Usuario(nombre)
            {
                Nombre= nombre,
                Apellido = "Pérez",
                Email = email,
                Contrasenia = "1234",
                Rol = rol
            };
            usuarios.Add(usuario);
            var nombre1 = new NombreUsuario("Juan1");
            var email1 = new EmailUsuario("cliente@correo.com");
            var rol1 = RolUsuario.Cliente;
            var usuario1 = new Usuario(nombre)
            {
                Nombre = nombre1,
                Apellido = "Pérez",
                Email = email1,
                Contrasenia = "1234",
                Rol = rol1
            };
            usuarios.Add(usuario1);
            var nombre2 = new NombreUsuario("Juan1");
            var email2 = new EmailUsuario("funcionario@correo.com");
            var rol2 = RolUsuario.Funcionario;
            var usuario2 = new Usuario(nombre)
            {
                Nombre = nombre2,
                Apellido = "Pérez",
                Email = email2,
                Contrasenia = "1234",
                Rol = rol2
            };
            usuarios.Add(usuario2);
        }
        public Usuario Login(EmailUsuario email, string contrasenia)
        {
            var usuario = ObtenerPorEmail(email.Valor);

            if (usuario != null && usuario.Contrasenia == contrasenia)
            {
                return usuario;

            }
                throw new Exception("Email o contraseña incorrectos.");

        }
        public void Crear(Usuario usuario)
        {
            usuarios.Add(usuario);
        }
        public List<Usuario> ObtenerTodos()
        {
            return usuarios;
        }

    }
}
