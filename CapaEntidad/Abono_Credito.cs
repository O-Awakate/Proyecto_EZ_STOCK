﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Abono_Credito
    {
        public int IdAbonoCompra { get; set; }
        public Credito oCredito { get; set; }
        public decimal Monto { get; set; }
        public string FechaAbono { get; set; }
    }
}
