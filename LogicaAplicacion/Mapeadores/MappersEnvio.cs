using CasosUso.DTOs;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace LogicaAplicacion.Mapeadores
{
    public static class MappersEnvio
    {
        public static EnvioDTO ToEnvioDTO(Envio envio)
        {
            return new EnvioDTO()
            {
                Cliente = null,
                Id = envio.Id,
                Empleado = null,
                EstadoEnvio = envio.EstadoEnvio,
                NumeroTracking = envio.NumeroTracking,
                FechaDeSalida = envio.FechaDeSalida,
                Peso = envio.Peso,
                TipoEnvio = envio.TipoEnvio,
                ComentarioSeguimiento = new ComentarioDTO()
                {
                    UsuarioAutor = envio.ComentarioSeguimiento.UsuarioAutor,
                    Fecha = envio.ComentarioSeguimiento.Fecha,
                    Id = envio.ComentarioSeguimiento.Id,
                    Texto = envio.ComentarioSeguimiento.Texto
                }
            };
        }

        public static int GenerarNumeroTracking()
        {
            return new Random().Next(123456, 999999);
        }
    }

}
