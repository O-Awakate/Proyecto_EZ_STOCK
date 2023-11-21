using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public Usuario OUsuario { get; set; }
        public Proveedor OProvedor { get; set; }
        public Credito oCompraCredito { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoBs { get; set; }
        public decimal Abono { get; set; }
        public decimal Deuda { get; set; }
        public List<Detalle_Compra> ODetalleCompra { get; set; }
        public string FechaRegistro { get; set; }
    }
}
