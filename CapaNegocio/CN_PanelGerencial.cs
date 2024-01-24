using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_PanelGerencial
    {
        private CD_Panel_de_Gestion objCD_Panel = new CD_Panel_de_Gestion();

        public PaneldeGestion ObtenerContadores(DateTime fechaInicio, DateTime fechaFin)
        {
            return objCD_Panel.ObtenerContadores(fechaInicio, fechaFin);
        }
        public PaneldeGestion RedimientoVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            int NumeroDias = (fechaFin - fechaInicio).Days;

            return objCD_Panel.RedimientoVentas(fechaInicio, fechaFin, NumeroDias);
        }

        public PaneldeGestion RendimientoProductos(DateTime fechaInicio, DateTime fechaFin)
        {
            return objCD_Panel.RendimientoProductos(fechaInicio, fechaFin);
        }
    }
}
