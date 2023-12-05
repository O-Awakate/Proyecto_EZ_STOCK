using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Credito
    {
        public int IdCompraCredito { get; set; }
        public decimal Intereses { get; set; }
        public decimal Deuda { get; set; }
        public string FechaRegistro { get; set; }
    }
}
