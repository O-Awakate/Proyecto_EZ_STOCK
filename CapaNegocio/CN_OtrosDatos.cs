using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_OtrosDatos
    {
        private CD_OtrosDatos objcd_Negocio = new CD_OtrosDatos();
        private CD_OtrosDatos objcd_OtrosDatos = new CD_OtrosDatos();

        public Negocio obtenerDatos()
        {
            return objcd_Negocio.obtenerDatos();
        }
        public Otros_Datos obtenerOtrosDatos()
        {
            return objcd_OtrosDatos.obtenerOtrosDatos();
        }

        public bool GuardarOtrosDatos(Otros_Datos obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.ValorDolar <= 0)
            {
                Mensaje += "Por favor, introduce un valor válido.\n";
            }

            if (Mensaje != string.Empty)
            {

                return false;
            }

            else
            {
                return objcd_OtrosDatos.GuardarOtrosDatos(obj, out Mensaje);
            }


        }

        public bool GuardarDatos(Negocio obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.Nombre == "")
            {
                Mensaje += "Por favor, ingresa el nombre del Negocio.\n";
            }
            if (obj.RIF == "")
            {
                Mensaje += "Por favor, ingresa el RIF del Negocio.\n";
            }

            if (obj.Direccion == "")
            {
                Mensaje += "Por favor, ingresa la dirección del Negocio.\n";
            }

            if (Mensaje != string.Empty)
            {

                return false;
            }

            else
            {
                return objcd_Negocio.GuardarDatos(obj, out Mensaje);
            }


        }
        public byte[] obtenerLogo(out bool obtenido)
        {
            return objcd_Negocio.obtenerLogo(out obtenido);
        }
        public bool actualizarLogo(byte[] image, out string mensaje)
        {
            return objcd_Negocio.ActualizarLogo(image, out mensaje);
        }
        public bool Respaldo(string rutaBackup, out string mensaje)
        {
            return objcd_Negocio.Respaldo(rutaBackup, out mensaje);
        }
        public bool RecuperarInformacion(string rutaRestore, out string mensaje)
        {
            return objcd_Negocio.RecuperarInformacion(rutaRestore, out mensaje);
        }
    }
}
