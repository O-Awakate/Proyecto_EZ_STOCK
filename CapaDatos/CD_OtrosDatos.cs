using CapaEntidad;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaDatos
{
    public class CD_OtrosDatos
    {
        //Obteniendo Datos (Dolar) 

        public Otros_Datos obtenerOtrosDatos()
        {
            Otros_Datos objotroDatos = new Otros_Datos();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    string query = "select IdOtrosDatos, ValorDolar, FechaCreacion from OTROSDATOS where IdOtrosDatos = 1";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objotroDatos = new Otros_Datos()
                            {
                                IdOtrosDatos = int.Parse(dr["IdOtrosDatos"].ToString()),
                                ValorDolar = decimal.Parse(dr["ValorDolar"].ToString()),
                                FechaRegistro = dr["FechaCreacion"].ToString()
                            };
                        }
                    }

                }
            }
            catch
            {
                objotroDatos = new Otros_Datos();
            }

            return objotroDatos;
        }

        //Guardar Datos (Dolar)

        public bool GuardarOtrosDatos(Otros_Datos objeto, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update OTROSDATOS SET ValorDolar = @ValorDolar, FechaCreacion = GETDATE()");
                    query.AppendLine("where IdOtrosDatos = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@ValorDolar", objeto.ValorDolar);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se puedo actualizar el valor del dolar";
                        respuesta = false;
                    }

                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }

            return respuesta;

        }

        //Obtener datos del negocio

        public Negocio obtenerDatos()
        {
            Negocio obj = new Negocio();

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    string query = "select IdNegocio,NombreNegocio, RIF,Direccion from NEGOCIO where IdNegocio = 1";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Negocio()
                            {
                                IdNegocio = int.Parse(dr["IdNegocio"].ToString()),
                                Nombre = dr["NombreNegocio"].ToString(),
                                RIF = dr["RIF"].ToString(),
                                Direccion = dr["Direccion"].ToString()
                            };
                        }
                    }

                }
            }
            catch
            {
                obj = new Negocio();
            }

            return obj;
        }

        //Guardar datos del negocio

        public bool GuardarDatos(Negocio objeto, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO Set NombreNegocio = @nombre,");
                    query.AppendLine("RIF = @rif,");
                    query.AppendLine("Direccion = @direccion");
                    query.AppendLine("where IdNegocio = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("@rif", objeto.RIF);
                    cmd.Parameters.AddWithValue("@Direccion", objeto.Direccion);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo guardar los datos";
                        respuesta = false;
                    }

                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }

            return respuesta;

        }

        //Obtiene el logo del negocio

        public byte[] obtenerLogo(out bool obtenido)
        {
            obtenido = true;
            byte[] logoByte = new byte[0];

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "select Logo from NEGOCIO where IdNegocio = 1";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            logoByte = (byte[])dr["Logo"];
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                obtenido = false;
                logoByte = new byte[0];
            }
            return logoByte;
        }

        //Actualiza el logo del negocio

        public bool ActualizarLogo(byte[] image, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update NEGOCIO Set Logo = @imagen");
                    query.AppendLine("where IdNegocio = 1;");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@imagen", image);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo";
                        respuesta = false;
                    }

                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                respuesta = false;
            }

            return respuesta;
        }

        public bool Respaldo(string rutaBackup, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    
                    string date = DateTime.Now.ToString("yyyy-MM-dd-HH-Mmm-ss");

                    string databese = conexion.Database.ToString();
                    string cmd = "BACKUP DATABASE["+databese+ "]TO DISK = '" + rutaBackup + "\\" + "EZSTOCK" + "-" + date+".bak'";
                    conexion.Open();

                    SqlCommand command = new SqlCommand(cmd, conexion);
                    

                    if (command.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo crear el backup de la base de datos";
                        respuesta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al realizar el respaldo: " + ex.Message;
                respuesta = false;
                Console.Write(mensaje);
            }

            return respuesta;
        }

        public bool RecuperarInformacion(string rutaRestore, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = true;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string databese = conexion.Database.ToString();
                    // Poner la base de datos en modo de usuario único antes de la restauración
                    string str1 = string.Format("ALTER DATABASE["+ databese + "]SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                    SqlCommand cmd1 = new SqlCommand(str1, conexion);
                    cmd1.ExecuteNonQuery();

                    // Restaurar la base de datos desde el archivo de respaldo
                    string str2 = "USE MASTER RESTORE DATABASE[" + databese + "]FROM DISK='" + rutaRestore+ "'WITH REPLACE;";
                    SqlCommand cmd2 = new SqlCommand(str2, conexion);
                    cmd2.ExecuteNonQuery();

                    // Poner la base de datos en modo de usuario múltiple después de la restauración
                    string str3 = string.Format("ALTER DATABASE[" + databese + "]SET MULTI_USER");
                    SqlCommand cmd3 = new SqlCommand(str3, conexion);
                    cmd3.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al realizar el respaldo: " + ex.Message;
                respuesta = false;
                Console.Write(mensaje);
            }

            return respuesta;
        }
    }
}
