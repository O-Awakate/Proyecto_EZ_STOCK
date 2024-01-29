using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objcd_Venta = new CD_Venta();

        public bool RestarStock(int idproducto, int cantidad)
        {
            return objcd_Venta.RestarStock(idproducto, cantidad);
        }

        public bool SumarStock(int idproducto, int cantidad)
        {
            return objcd_Venta.SumarStock(idproducto, cantidad);
        }

        public int ObtenerCorrelativo()
        {
            return objcd_Venta.ObtenerCorrelativo();
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objcd_Venta.Registra(obj, DetalleVenta, out Mensaje);
        }

        public Venta obtenerVenta(string numero)
        {
            Venta oVenta = objcd_Venta.ObtenerVenta(numero);

            if (oVenta.IdVenta != 0)
            {
                List<Detalle_Venta> oDetalleventa = objcd_Venta.ObtenerDetalleVenta(oVenta.IdVenta);
                oVenta.oDetalleVenta = oDetalleventa;
            }
            return oVenta;
        }
        public bool Abono(Abono_Credito obj, out string Mensaje)
        {
            return objcd_Venta.Abono(obj, out Mensaje);
        }
        public bool DevolucionVenta(Venta obj, out string Mensaje)
        {
            return objcd_Venta.DevolucionVenta(obj, out Mensaje);
        }

        public bool DevolverProducto(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objcd_Venta.DevolverProducto(obj, DetalleVenta, out Mensaje);
        }
    }
}
