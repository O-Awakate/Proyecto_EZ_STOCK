using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public Datos_Persona oDatosPersona { get; set; }
        public Rol oRol { get; set; }
        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
