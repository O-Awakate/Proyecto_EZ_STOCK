using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Datos_Sistema
    {
        private CD_Datos_Sistema objcd_DatosSistema = new CD_Datos_Sistema();

        public Datos_Sistema ObtenerDatos (int numero)
        {
            Datos_Sistema oDatosSistema = objcd_DatosSistema.ObtenerDatos(numero);
            
            return oDatosSistema;
        }
    }
}
