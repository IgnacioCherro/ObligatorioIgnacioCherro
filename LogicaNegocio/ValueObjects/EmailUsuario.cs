using ExepcionesPropias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public class EmailUsuario
    {
        public string Valor { get; private set; }

        public EmailUsuario(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Valor) || !Valor.Contains("@"))
                throw new DatosInvalidosExepction("Email inválido o vacio");

        }
    }
}
