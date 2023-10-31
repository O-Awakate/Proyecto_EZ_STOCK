﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public Datos_Persona oDatosPersona { get; set; }
        public decimal Deuda { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
