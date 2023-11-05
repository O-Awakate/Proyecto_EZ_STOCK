using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Apartado
    {
        public int IdApartado { get; set; }
        public Producto oProducto { get; set; }
        public string FechaVencimiento { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
