﻿using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.EntidadesDominio
{
    public class Administrador : Usuario
    {
        public Administrador(string nombre) : base(nombre)
        {
        }

        public Administrador()
        {
            Rol = RolUsuario.Administrador;
        }



    }
}
