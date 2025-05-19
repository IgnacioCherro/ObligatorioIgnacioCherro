using CasosUso.DTOs;
using ExepcionesPropias;
using LogicaAplicacion.Mapeadores;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.InterfacesDominio;
using LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace LogicaAplicacion.CasosUso
{
    public class EnvioService : IEnvioService
    {
        private readonly IRepositorioEnvio _repoEnvio;
        private readonly IRepositorioUsuario _repoUsuario;
        private readonly IRepositorioAgencia _repoAgencia;
        private readonly ICurrentUser _user;

        public EnvioService(IRepositorioEnvio repoEnvio, IRepositorioUsuario repoUsuario, IRepositorioAgencia repoAgencia, ICurrentUser user)
        {
            _repoEnvio = repoEnvio;
            _repoUsuario = repoUsuario;
            _repoAgencia = repoAgencia;
            _user = user;
        }


        public List<EnvioDTO> ObtenerEnviosEnProceso()
        {
            return _repoEnvio.FindAll().Where(e => e.EstadoEnvio == EstadoEnvio.EN_PROCESO).Select(x => {
                var nuevoEnvio = MappersEnvio.ToEnvioDTO(x);
                nuevoEnvio.Cliente =  MappersUsuario.ToUsuarioDTO(_repoUsuario.FindById(x.ClienteId));
                nuevoEnvio.Empleado = MappersUsuario.ToUsuarioDTO(_repoUsuario.FindById(x.EmpleadoId));
                return nuevoEnvio;
            }).ToList();
        }


        public void AgregarComentario(int idEnvio, string texto)
        {
            var envio = _repoEnvio.FindById(idEnvio);
            if (envio == null)
                throw new InvalidOperationException("No se encontró el envío.");

            var comentario = new Comentario
            {
                Texto = texto,
                UsuarioAutor = _user.Nombre,
                Fecha = DateTime.Now
            };

            envio.ComentarioSeguimiento = comentario;

            _repoEnvio.Update(envio);
        }

        public void Crear(AltaEnvioDTO dto)
        {
            var cliente = _repoUsuario.ObtenerPorEmail(dto.EmailCliente);
            if (cliente == null || cliente.Rol != RolUsuario.Cliente)
                throw new DatosInvalidosExepction("Cliente no válido");

            if (dto.Peso <= 0)
                throw new DatosInvalidosExepction("El peso debe ser mayor que 0");


            if (dto.TipoEnvio == TipoEnvio.URGENTE)
            {

                var envio = new EnvioUrgente()
                {
                    ClienteId = cliente.Id,
                    DireccionPostal = dto.DireccionPostal.Value,
                    FechaDeSalida = DateTime.UtcNow,
                    EmpleadoId = _user.Id.Value,
                    EnvioEficiente = false,
                    EstadoEnvio = EstadoEnvio.EN_PROCESO,
                    Peso = dto.Peso,
                    TipoEnvio = dto.TipoEnvio,
                    ComentarioSeguimiento = new Comentario()
                    {
                        Fecha = DateTime.UtcNow,
                        Texto = "El paquete esta siendo procesado",
                        UsuarioAutor = _user.Nombre
                    },
                    NumeroTracking = MappersEnvio.GenerarNumeroTracking()
                };

                _repoEnvio.Add(envio);

            }
            else
            {
                Agencia agenciaDestino = null;
                if (dto.AgenciaId.HasValue)
                {
                    agenciaDestino = _repoAgencia.ObtenerPorId(dto.AgenciaId.Value);
                    if (agenciaDestino == null)
                        throw new DatosInvalidosExepction("Agencia de destino no encontrada");
                }

                var envio = new EnvioComun()
                {
                    ClienteId = cliente.Id,
                    FechaDeSalida = DateTime.UtcNow,
                    EmpleadoId = _user.Id.Value,
                    EstadoEnvio = EstadoEnvio.EN_PROCESO,
                    Peso = dto.Peso,
                    TipoEnvio = dto.TipoEnvio,
                    ComentarioSeguimiento = new Comentario()
                    {
                        Fecha = DateTime.UtcNow,
                        Texto = "El paquete esta siendo procesado",
                        UsuarioAutor = _user.Nombre
                    },
                    NumeroTracking = MappersEnvio.GenerarNumeroTracking(),
                    Agencia = agenciaDestino,
                };

                _repoEnvio.Add(envio);

            }

        }

        public void Finalizar(int id)
        {
            var envio = _repoEnvio.FindById(id);
            if (envio == null)
                throw new Exception("No se encuentra este envio.");


            envio.EstadoEnvio = EstadoEnvio.FINALIZADO;

            envio.ComentarioSeguimiento = new Comentario()
            {
                Fecha = DateTime.UtcNow,
                Texto = $"El pedido se a entregado correctamente la fecha : {DateTime.UtcNow.ToShortDateString()}",
                UsuarioAutor = _user.Nombre
            };

            if (envio.TipoEnvio == TipoEnvio.URGENTE)
            {
                var envioUrgente = envio as EnvioUrgente;
                envioUrgente.EnvioEficiente = (envioUrgente.FechaDeSalida - DateTime.UtcNow).TotalHours < 24;
                envio = envioUrgente;
            }

            _repoEnvio.Update(envio);
        }

        public EnvioDTO ObtenerEnviosPorTrackingNumber(int number)
        {
            return MappersEnvio.ToEnvioDTO(_repoEnvio.ObtenerEnviosPorTrackingNumber(number));
        }
    }

}
