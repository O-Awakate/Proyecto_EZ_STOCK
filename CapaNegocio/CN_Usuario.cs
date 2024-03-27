﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objCD_Usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return objCD_Usuario.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.oDatosPersona.Nombre == "")
            {
                Mensaje += "Por favor, ingresa el nombre del usuario.\n";
            }

            if (obj.oDatosPersona.Apellido == "")
            {
                Mensaje += "Por favor, ingresa el apellido del usuario.\n";
            }

            if (obj.oDatosPersona.CI == "")
            {
                Mensaje += "Por favor, ingresa la cédula del usuario.\n";
            }

            if (obj.oDatosPersona.oCorreo.UsuarioCorreo == "")
            {
                Mensaje += "Por favor, ingresa el correo del usuario.\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Por favor, ingresa la clave del usuario.\n";
            }
            
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCD_Usuario.Registrar(obj, out Mensaje);

            }
             
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.oDatosPersona.Nombre == "")
            {
                Mensaje += "Por favor, ingresa el nombre del usuario.\n";
            }

            if (obj.oDatosPersona.Apellido == "")
            {
                Mensaje += "Por favor, ingresa el apellido del usuario.\n";
            }

            if (obj.oDatosPersona.CI == "")
            {
                Mensaje += "Por favor, ingresa la cédula del usuario.\n";
            }

            if (obj.oDatosPersona.oCorreo.UsuarioCorreo == "")
            {
                Mensaje += "Por favor, ingresa el correo del usuario.\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Por favor, ingresa la clave del usuario.\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCD_Usuario.Editar(obj, out Mensaje); 

            }
            
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return objCD_Usuario.Eliminar(obj, out Mensaje);
        }

        public bool ActualizarContraseña(Usuario obj, out string Mensaje)
        {
            return objCD_Usuario.ActualizarContraseña(obj, out Mensaje);
        }
    }
}
