using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaNegocio
{
    public class CN_Proveedor
    {
        private CD_Proveedor objcd_Proveedor = new CD_Proveedor();

        public List<Proveedor> listar()
        {
            return objcd_Proveedor.listar();
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.oDatosPersona.CI == "")
            {
                Mensaje += "Es necesario la cedula del Proveedor\n";
            }
            if (obj.oDatosPersona.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del Proveedor\n";
            }

            if (obj.oCasaProveedora.RazonSocial == "")
            {
                Mensaje += "Es necesario la razon social del Proveedor\n";
            }

            if (Mensaje != string.Empty)
            {

                return 0;
            }

            else
            {
                return objcd_Proveedor.Registrar(obj, out Mensaje);
            }


        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.oDatosPersona.CI == "")
            {
                Mensaje += "Es necesario la cedula del Proveedor\n";
            }
            if (obj.oDatosPersona.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del Proveedor\n";
            }

            if (obj.oCasaProveedora.RazonSocial == "")
            {
                Mensaje += "Es necesario la razon social del Proveedor\n";
            }

            if (Mensaje != string.Empty)
            {

                return false;
            }

            else
            {
                return objcd_Proveedor.Editar(obj, out Mensaje);
            }

        }

        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            return objcd_Proveedor.Eliminar(obj, out Mensaje);
        }
    }
}
