using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Devolucion_Venta
    {
        public int IdDevolucionCompra { get; set; }
        public Detalle_Venta oDetalleVenta { get; set; }
        public string Motivo { get; set; }
        public decimal MontoReembolso { get; set; }
        public string FechaRegistro { get; set; }
    }
}
