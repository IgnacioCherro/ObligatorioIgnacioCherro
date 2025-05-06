using CasosUso.DTOs;
using ExepcionesPropias;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosUso
{
    public class EnvioService
    {
        private readonly IRepositorioEnvio _repoEnvio;
        private readonly IRepositorioUsuario _repoUsuario;
        private readonly IRepositorioAgencia _repoAgencia;

        public EnvioService(IRepositorioEnvio repoEnvio, IRepositorioUsuario repoUsuario, IRepositorioAgencia repoAgencia)
        {
            _repoEnvio = repoEnvio;
            _repoUsuario = repoUsuario;
            _repoAgencia = repoAgencia;
        }

        public void AltaEnvio(AltaEnvioDTO dto)
        {
            var cliente = _repoUsuario.ObtenerPorEmail(dto.EmailCliente);
            if (cliente == null || cliente.Rol != RolUsuario.Cliente)
                throw new DatosInvalidosExepction("Cliente no válido");

            Agencia agenciaDestino = null;
            if (dto.IdAgenciaDestino.HasValue)
            {
                agenciaDestino = _repoAgencia.FindById(dto.IdAgenciaDestino.Value);
                if (agenciaDestino == null)
                    throw new Exception("Agencia de destino no encontrada");
            }

            var envio = new Envio
            {
                RecibePedido = cliente,
                AgenciaDestino = agenciaDestino,
                DireccionPostal = dto.DireccionPostal,
                PesoPaquete = dto.Peso,
                TipoEnvio = dto.TipoEnvio,
                FechaAlta = DateTime.Now,
                EstadoEnvio = EstadoEnvio.EN_PROCESO 
            };

            _repoEnvio.Crear(envio);
        }

        public void FinalizarEnvio(int idEnvio)
        {
            var envio = _repoEnvio.FindById(idEnvio);
            if (envio == null)
                throw new Exception("El envío no existe.");

            if (envio.EstadoEnvio != EstadoEnvio.EN_PROCESO)
                throw new Exception("El envío no está en proceso y no puede ser finalizado.");

            envio.EstadoEnvio = EstadoEnvio.FINALIZADO;
            envio.FechaEntrega = DateTime.Now;

            _repoEnvio.Update(envio);  
        }


    }

}
