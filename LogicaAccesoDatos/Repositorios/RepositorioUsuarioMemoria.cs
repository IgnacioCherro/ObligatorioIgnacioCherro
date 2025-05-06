using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExepcionesPropias;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuarioMemoria : IRepositorioUsuario
    {

        private static List<Usuario> usuarios = new List<Usuario>();
        private static int ultimoId = 1;

       public void Add(Usuario usuario)
        {
            usuario.Id=ultimoId++;
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
                if (usuario.Email == email)
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
        
            return  usuarios.Where(x => x.Id == id).FirstOrDefault();
            
        }
    }
}
