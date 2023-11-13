using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objcd_Cliente = new CD_Cliente();

        public List<Cliente> listar()
        {
            return objcd_Cliente.listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.oDatosPersona.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del Cliente\n";
            }

            if (obj.oDatosPersona.Apellido == "")
            {
                Mensaje += "Es necesario el apellido del Cliente\n";
            }

            if (obj.oDatosPersona.CI == "")
            {
                Mensaje += "Es necesario la cedula del Cliente\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Cliente.Registrar(obj, out Mensaje);

            }

        }

        public bool Editar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.oDatosPersona.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del Cliente\n";
            }

            if (obj.oDatosPersona.Apellido == "")
            {
                Mensaje += "Es necesario el apellido del Cliente\n";
            }

            if (obj.oDatosPersona.CI == "")
            {
                Mensaje += "Es necesario la cedula del Cliente\n";
            }
            

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Cliente.Editar(obj, out Mensaje); ;

            }

        }

        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            return objcd_Cliente.Eliminar(obj, out Mensaje);
        }
    }
}
