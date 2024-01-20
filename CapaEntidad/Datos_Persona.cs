using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Datos_Persona
    {
        public int IdDatosPersona { get; set; }
        public Direccion oDireccion { get; set; }
        public Telefono oTelefono { get; set; }
        public Correo oCorreo { get; set; }
        public string CI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaRegistro { get; set; }
    }
}
