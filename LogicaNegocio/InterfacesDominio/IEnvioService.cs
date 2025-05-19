using CasosUso.DTOs;
using LogicaNegocio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesDominio
{
    public interface IEnvioService
    {
        void Crear(AltaEnvioDTO dto);
        void Finalizar(int id);
        List<EnvioDTO> ObtenerEnviosEnProceso();
        void AgregarComentario(int id, string texto);
        EnvioDTO ObtenerEnviosPorTrackingNumber(int number);
    }
}
