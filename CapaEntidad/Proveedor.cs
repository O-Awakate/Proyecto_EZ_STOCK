using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public Datos_Persona oDatosPersona { get; set; }
        public Casa_Proveedora oCasaProveedora { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
