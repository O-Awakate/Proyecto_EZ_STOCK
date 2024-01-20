using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Datos_Sistema
    {
        public Datos_Sistema ObtenerDatos(int numero)
        {
            Datos_Sistema obj = new Datos_Sistema();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT Identificador, Autentificador,Numero FROM DATOS_SISTEMA");
                    query.AppendLine("WHERE IdDatosSistema = @numero");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Datos_Sistema()
                            {
                                Identificador = dr["Identificador"].ToString(),
                                Autentificador = dr["Autentificador"].ToString(),
                                Numero = dr["Numero"].ToString()
                                
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Datos_Sistema();
                }
            }

            return obj;
        }


    }
}
