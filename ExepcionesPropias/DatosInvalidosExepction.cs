using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExepcionesPropias
{
    public class DatosInvalidosExepction:Exception
    {
        public DatosInvalidosExepction()
        {
            
        }

        public DatosInvalidosExepction(string mensaje) : base(mensaje) 
        { 
        
        }


        public DatosInvalidosExepction(string mensaje, Exception interna) : base(mensaje, interna) 
        { 
        
        }
    }
}
