using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Casa_Proveedora
    {
        public int IdCasaProveedora { get; set; }
        public string RIF { get; set; }
        public string RazonSocial { get; set; }
        public string SitioWeb { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
