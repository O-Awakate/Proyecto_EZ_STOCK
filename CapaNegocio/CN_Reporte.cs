using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objcd_reporte = new CD_Reporte();

        public List<ReporteCompra> compra(string fechainicio, string fechafin, int idproveerdor)
        {
            return objcd_reporte.compra(fechainicio, fechafin, idproveerdor);
        }

        public List<ReporteVenta> venta(string fechainicio, string fechafin)
        {
            return objcd_reporte.venta(fechainicio, fechafin);
        }
    }
}
