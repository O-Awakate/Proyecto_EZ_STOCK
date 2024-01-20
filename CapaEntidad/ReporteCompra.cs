using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReporteCompra
    {
        public string FechaRegistro { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string MontoTotal { get; set; }
        public string MetodoPago { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string RazonSocial { get; set; }
        public string RIF { get; set; }
        public string NombreProv { get; set; }
        public string ApellidoProv { get; set; }
        public string CI { get; set; }
        public string CodigoFabrica { get; set; }
        public string CodigoAvila { get; set; }
        public string MarcaProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public string PrecioCompra { get; set; }
        public string PrecioVenta { get; set; }
        public string Cantidad { get; set; }
        public string SubTotal { get; set; }
        public string Deuda { get; set; }
    }
}

