using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta_Credito
    {
        public int IdVentaCredito { get; set; }
        public List<Abono_Credito> oAbonoVenta { get; set; }
        public string FechaVencimiento { get; set; }
        public decimal Intereses { get; set; }
        public decimal Monto { get; set; }
        public decimal Deuda { get; set; }
        public string FechaRegistro { get; set; }
    }
}
