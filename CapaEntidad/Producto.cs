using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public Categoria oCategoria { get; set; }
        public string CodigoFabrica { get; set; }
        public string CodigoAvila { get; set; }
        public string DescripcionProducto { get; set; }
        public string MarcaProducto { get; set; }
        public string MarcaCarro { get; set; }
        public string AplicaParaCarro { get; set; }
        public int Stock { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioAlMayor { get; set; }
        public decimal PrecioMercadoLibre { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
