using ExepcionesPropias;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicaNegocio.ValueObjects
{
    [Owned]
    public class NombreUsuario
    {
        public string Valor {  get; private set; }

        public NombreUsuario(string valor)
        {
            Valor = valor;
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Valor))
            {
                throw new DatosInvalidosExepction("El nombre no puede estar vacio");
            }
        }
    }
}
