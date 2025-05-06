using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAccesoDatos.Repositorios
{
    public class RepositorioEnvio:IRepositorioEnvio
    {
        private List<Envio> _envios = new List<Envio>();
        private int ultimoId = 1;

        public void Crear(Envio envio)
        {
            envio.Id = ultimoId++;
            _envios.Add(envio);

        }

        public Envio FindById(int id)
        {
            return _envios.FirstOrDefault(e => e.Id == id);
        }

        public void Update(Envio envio)
        {
            var envioBase = _envios.FindIndex(e => e.Id == envio.Id);
            if (envioBase != -1)
            {
                _envios[envioBase] = envio;
            }
        }
    }
}
