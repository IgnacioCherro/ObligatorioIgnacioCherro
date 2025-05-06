using CasosUso.DTOs;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Mapeadores
{
    public static class MappersEnvio
    {
        public static Envio ToEnvio(AltaEnvioDTO dto, Usuario cliente, Usuario empleado, Agencia agenciaDestino = null)
        {
            return new Envio
            {
                RecibePedido = cliente,
                EntregaPedido = empleado,
                AgenciaDestino = agenciaDestino,
                DireccionPostal = dto.DireccionPostal,
                TipoEnvio = dto.TipoEnvio,
                PesoPaquete = dto.Peso,
                FechaAlta = DateTime.Now,
                EstadoEnvio = EstadoEnvio.EN_PROCESO,
                NumeroTracking = GenerarNumeroTracking()
            };
        }

        private static int GenerarNumeroTracking()
        {
            return new Random().Next(123456, 999999);
        }
    }
        
}
