using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{  
    public struct IngresosPorFecha
    {
        public string Fecha { get; set; }
        public decimal Montototal { get; set; }
    }
    public class PaneldeGestion
    {
        public int NumeroCliente { get; set; }
        public int NumeroProveerdo { get; set; }
        public int NumeroProductos { get; set; }
        public List<KeyValuePair<string, int>> ListaProductosTop { get; set; }
        public List<KeyValuePair<string,int>> ListaBajoStock { get; set; }
        public List<IngresosPorFecha> IngresosBrutos { get; set; }
        public int NumeroVentas { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal TotalGanancia { get; set; }


    }
}
