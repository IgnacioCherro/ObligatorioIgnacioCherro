
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CasosUso.DTOs.Enums;

namespace CasosUso.DTOs
{
    public class AltaEnvioDTO
    {
        [Required, EmailAddress]
        public string EmailCliente { get; set; }

        [Required]
        public TipoEnvio TipoEnvio { get; set; }

        [Range(1, int.MaxValue)]
        public int Peso { get; set; }
        public int? DireccionPostal { get; set; }
        public int? AgenciaId { get; set; }
    }
}
