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
                Mensaje += "Por favor, ingresa el número de cédula del Proveedor.\n";
            }
            if (obj.oDatosPersona.Nombre == "")
            {
                Mensaje += "Por favor, ingresa el nombre del Proveedor.\n";
            }
            if (obj.oDatosPersona.Apellido == "")
            {
                Mensaje += "Por favor, ingresa el apellido del Proveedor.\n";
            }
            if (obj.oDatosPersona.oTelefono.Numero == "")
            {
                Mensaje += "Por favor, ingresa el número telefónico del Proveedor.\n";
            }
            if (obj.oCasaProveedora.RazonSocial == "")
            {
                Mensaje += "Por favor, ingresa la razón social de la casa proveedora.\n";
            }
            if (obj.oCasaProveedora.RIF == "")
            {
                Mensaje += "Por favor, ingresa el RIF de la casa proveedora.\n";
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
                Mensaje += "Por favor, ingresa el número de cédula del Proveedor.\n";
            }
            if (obj.oDatosPersona.Nombre == "")
            {
                Mensaje += "Por favor, ingresa el nombre del Proveedor.\n";
            }
            if (obj.oDatosPersona.Apellido == "")
            {
                Mensaje += "Por favor, ingresa el apellido del Proveedor.\n";
            }
            if (obj.oDatosPersona.oTelefono.Numero == "")
            {
                Mensaje += "Por favor, ingresa el número telefónico del Proveedor.r\n";
            }
            if (obj.oCasaProveedora.RazonSocial == "")
            {
                Mensaje += "Por favor, ingresa la razón social de la casa proveedora.\n";
            }
            if (obj.oCasaProveedora.RIF == "")
            {
                Mensaje += "Por favor, ingresa el RIF de la casa proveedora.\n";
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
