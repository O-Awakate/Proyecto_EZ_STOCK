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
    public class CD_Usuario
    {
        public List<Usuario> Listar()
        {
            List<Usuario> Lista = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT u.IdUsuario, dp.IdDatosPersona, dp.CI, dp.Nombre, dp.Apellido, u.Clave, u.ConfirmarClave, u.Estado AS EstadoActual, c.IdCorreo, c.Correo, t.IdTelefono, t.Numero, d.IdDireccion, d.Estado, d.Ciudad, d.Sector, d.Calle, d.Casa, r.IdRol, r.Descripcion FROM USUARIO u");
                    query.AppendLine("INNER JOIN ROL r ON u.IdRol = r.IdRol");
                    query.AppendLine("INNER JOIN DATOS_PERSONA dp ON u.IdDatosPersona = dp.IdDatosPersona");
                    query.AppendLine("INNER JOIN CORREO c ON dp.IdCorreo = c.IdCorreo");
                    query.AppendLine("INNER JOIN TELEFONO t ON dp.IdTelefono = t.IdTelefono");
                    query.AppendLine("INNER JOIN DIRECCION d ON dp.IdDireccion = d.IdDireccion");
                    

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Usuario()
                            {
                                IdUsuario=Convert.ToInt32(dr["IdUsuario"]),
                                oDatosPersona = new Datos_Persona
                                {
                                    IdDatosPersona = Convert.ToInt32(dr["IdDatosPersona"]),
                                    CI = dr["CI"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    oCorreo = new Correo
                                    {
                                        IdCorreo = Convert.ToInt32(dr["IdCorreo"]),
                                        UsuarioCorreo = dr["Correo"].ToString()
                                    },
                                    oDireccion = new Direccion
                                    {
                                        IdDireccion = Convert.ToInt32(dr["IdDireccion"]),
                                        Estado = dr["Estado"].ToString(),
                                        Ciudad = dr["Ciudad"].ToString(),
                                        Sector = dr["Sector"].ToString(),
                                        Calle = dr["Calle"].ToString(),
                                        Casa = dr["Casa"].ToString()
                                    },
                                    oTelefono = new Telefono
                                    {
                                        IdTelefono = Convert.ToInt32(dr["IdTelefono"]),
                                        Numero = dr["Numero"].ToString()
                                    },


                                },
                                Clave = dr["Clave"].ToString(),
                                ConfirmarClave = dr["ConfirmarClave"].ToString(),
                                Estado = Convert.ToBoolean(dr["EstadoActual"]),
                                oRol = new Rol
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Descripcion = dr["Descripcion"].ToString()
                                },


                            });

                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<Usuario>();
                }
            }
            return Lista;
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {

            int IdUsuarioGenrado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIOS", conexion);
                    
                    cmd.Parameters.AddWithValue("CI", obj.oDatosPersona.CI);
                    cmd.Parameters.AddWithValue("Nombre", obj.oDatosPersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.oDatosPersona.Apellido);
                    cmd.Parameters.AddWithValue("Correo", obj.oDatosPersona.oCorreo.UsuarioCorreo);
                    cmd.Parameters.AddWithValue("Numero", obj.oDatosPersona.oTelefono.Numero);
                    cmd.Parameters.AddWithValue("Estado", obj.oDatosPersona.oDireccion.Estado);
                    cmd.Parameters.AddWithValue("Ciudad", obj.oDatosPersona.oDireccion.Ciudad);
                    cmd.Parameters.AddWithValue("Sector", obj.oDatosPersona.oDireccion.Sector);
                    cmd.Parameters.AddWithValue("Calle", obj.oDatosPersona.oDireccion.Calle);
                    cmd.Parameters.AddWithValue("Casa", obj.oDatosPersona.oDireccion.Casa);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("ConfiClave", obj.ConfirmarClave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("EstadoUsuario", obj.Estado);
                    
                    cmd.Parameters.Add("IdDireccion", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("IdTelefono", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("IdCorreo", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    
                    IdUsuarioGenrado = Convert.ToInt32(cmd.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {

                IdUsuarioGenrado = 0;
                Mensaje = ex.Message;

            }

            return IdUsuarioGenrado;

        }
        

        public bool Editar(Usuario obj, out string Mensaje)
        {

            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARUSUARIOS", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Parámetros de entrada
                    cmd.Parameters.AddWithValue("IdDatosPersona", obj.oDatosPersona.IdDatosPersona);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("IdDireccion", obj.oDatosPersona.oDireccion.IdDireccion);
                    cmd.Parameters.AddWithValue("IdTelefono", obj.oDatosPersona.oTelefono.IdTelefono);
                    cmd.Parameters.AddWithValue("IdCorreo", obj.oDatosPersona.oCorreo.IdCorreo);
                    cmd.Parameters.AddWithValue("CI", obj.oDatosPersona.CI);
                    cmd.Parameters.AddWithValue("Nombre", obj.oDatosPersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.oDatosPersona.Apellido);
                    cmd.Parameters.AddWithValue("Correo", obj.oDatosPersona.oCorreo.UsuarioCorreo);
                    cmd.Parameters.AddWithValue("Numero", obj.oDatosPersona.oTelefono.Numero);
                    cmd.Parameters.AddWithValue("Estado", obj.oDatosPersona.oDireccion.Estado);
                    cmd.Parameters.AddWithValue("Ciudad", obj.oDatosPersona.oDireccion.Ciudad);
                    cmd.Parameters.AddWithValue("Sector", obj.oDatosPersona.oDireccion.Sector);
                    cmd.Parameters.AddWithValue("Calle", obj.oDatosPersona.oDireccion.Calle);
                    cmd.Parameters.AddWithValue("Casa", obj.oDatosPersona.oDireccion.Casa);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("ConfiClave", obj.ConfirmarClave);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("EstadoUsuario", obj.Estado);

                   // Parámetros de salida
                    cmd.Parameters.Add("Respuesta", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    //Obtener los valores de los parámetros de salida
                    Respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }

            catch (Exception ex)
            {

                Respuesta = false;
                Mensaje = ex.Message;

            }

            return Respuesta;

        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {

            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIOS", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {

                Respuesta = false;
                Mensaje = ex.Message;

            }

            return Respuesta;

        }


    }
}
