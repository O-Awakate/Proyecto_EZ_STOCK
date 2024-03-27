using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objcd_Producto = new CD_Producto();

        public List<Producto> listar()
        {
            return objcd_Producto.listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.CodigoAvila == "")
            {
                Mensaje += "Por favor, ingrese el Código Ávila del producto.\n";
            }
            if (obj.DescripcionProducto == "")
            {
                Mensaje += "Por favor, ingrese la descripción del producto.\n";
            }

            if (obj.AplicaParaCarro == "")
            {
                Mensaje += "Por favor, ingrese los carros para los cuales aplica el producto.\n";
            }

            if (obj.MarcaProducto == "")
            {
                Mensaje += "Por favor, ingrese la marca del producto.";
            }

            if (Mensaje != string.Empty)
            {

                return 0;
            }

            else
            {
                return objcd_Producto.Registrar(obj, out Mensaje);
            }


        }

        public bool Editar(Producto obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.CodigoAvila == "")
            {
                Mensaje += "Por favor, ingrese el Código Ávila del producto.\n";
            }
            if (obj.DescripcionProducto == "")
            {
                Mensaje += "Por favor, ingrese la descripción del producto.\n";
            }

            if (obj.AplicaParaCarro == "")
            {
                Mensaje += "Por favor, ingrese los carros para los cuales aplica el producto.\n";
            }

            if (obj.MarcaProducto == "")
            {
                Mensaje += "Por favor, ingrese la marca del producto.\n";
            }

            if (Mensaje != string.Empty)
            {

                return false;
            }

            else
            {
                return objcd_Producto.Editar(obj, out Mensaje);
            }

        }

        public bool Eliminar(Producto obj, out string Mensaje)
        {
            return objcd_Producto.Eliminar(obj, out Mensaje);
        }
    }
}
