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
    public  class CN_Compra
    {
        private CD_Compra objcd_Compra = new CD_Compra();

        public int ObtenerCorrelativo()
        {
            return objcd_Compra.ObtenerCorrelativo();
        }

        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            return objcd_Compra.Registra(obj, DetalleCompra, out Mensaje);
        }

        public Compra ObtenerCompra(string numero)
        {
            Compra oCompra = objcd_Compra.ObtenerCompra(numero);
            
                if (oCompra.IdCompra != 0)
                {
                    List<Detalle_Compra> oDetalleCompra = objcd_Compra.ObtenerDetalleCompra(oCompra.IdCompra);

                    oCompra.ODetalleCompra = oDetalleCompra;
                    
                }
            return oCompra;

        }
    }
}
