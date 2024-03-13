using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Cliente
    {
        // Método para listar clientes desde la base de datos.
        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Construir y ejecutar consulta SQL para obtener información de clientes.
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT cl.IdCliente, dp.IdDatosPersona, dp.Nacionalidad, dp.CI, dp.Nombre, dp.Apellido, cl.Estado AS EstadoActual, t.IdTelefono, t.Numero, d.IdDireccion, d.Estado, d.Ciudad, d.Sector, d.Calle, d.Casa FROM CLIENTE cl");
                    query.AppendLine("INNER JOIN DATOS_PERSONA dp ON cl.IdDatosPersona = dp.IdDatosPersona");
                    query.AppendLine("INNER JOIN TELEFONO t ON dp.IdTelefono = t.IdTelefono");
                    query.AppendLine("INNER JOIN DIRECCION d ON dp.IdDireccion = d.IdDireccion");
                    

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    // Procesar resultados y agregar a la lista de clientes.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente()
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                oDatosPersona = new Datos_Persona
                                {
                                    // Construir objeto Datos_Persona con información del cliente.
                                    IdDatosPersona = Convert.ToInt32(dr["IdDatosPersona"]),
                                    Nacionalidad = dr["Nacionalidad"].ToString(),
                                    CI = dr["CI"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    oDireccion = new Direccion
                                    {
                                        // Construir objeto Direccion con información de la dirección del cliente.
                                        IdDireccion = Convert.ToInt32(dr["IdDireccion"]),
                                        Estado = dr["Estado"].ToString(),
                                        Ciudad = dr["Ciudad"].ToString(),
                                        Sector = dr["Sector"].ToString(),
                                        Calle = dr["Calle"].ToString(),
                                        Casa = dr["Casa"].ToString()
                                    },
                                    oTelefono = new Telefono
                                    {
                                        // Construir objeto Telefono con información del teléfono del cliente.
                                        IdTelefono = Convert.ToInt32(dr["IdTelefono"]),
                                        Numero = dr["Numero"].ToString()
                                    },
                                },
                                Estado = Convert.ToBoolean(dr["EstadoActual"]),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // En caso de error, reinicializar la lista de clientes.
                    lista = new List<Cliente>();
                }
            }

            return lista;
        }
        // Método para registrar un nuevo cliente en la base de datos.

        public int Registrar(Cliente obj, out string Mensaje)
        {

            int IdClienteGenrado = 0;
            Mensaje = string.Empty;

            try
            {
                // Llamar al procedimiento almacenado para registrar un cliente.
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCLIENTE", oconexion);
                    // Asignar parámetros del cliente al comando SQL.
                    cmd.Parameters.AddWithValue("Nacionalidad", obj.oDatosPersona.Nacionalidad);
                    cmd.Parameters.AddWithValue("CI", obj.oDatosPersona.CI);
                    cmd.Parameters.AddWithValue("Nombre", obj.oDatosPersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.oDatosPersona.Apellido);
                    cmd.Parameters.AddWithValue("Numero", obj.oDatosPersona.oTelefono.Numero);
                    cmd.Parameters.AddWithValue("Estado", obj.oDatosPersona.oDireccion.Estado);
                    cmd.Parameters.AddWithValue("Ciudad", obj.oDatosPersona.oDireccion.Ciudad);
                    cmd.Parameters.AddWithValue("Sector", obj.oDatosPersona.oDireccion.Sector);
                    cmd.Parameters.AddWithValue("Calle", obj.oDatosPersona.oDireccion.Calle);
                    cmd.Parameters.AddWithValue("Casa", obj.oDatosPersona.oDireccion.Casa);
                    cmd.Parameters.AddWithValue("EstadoActual", obj.Estado);
                    // Definir parámetros de salida.
                    cmd.Parameters.Add("IdDireccion", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("IdTelefono", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    // Ejecutar el procedimiento almacenado y obtener resultados.
                    cmd.ExecuteNonQuery();

                    IdClienteGenrado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                // En caso de error, asignar valores por defecto y capturar mensaje de error.
                IdClienteGenrado = 0;
                Mensaje = ex.Message;

            }

            return IdClienteGenrado;

        }
        // Método para editar la información de un cliente en la base de datos.
        public bool Editar(Cliente obj, out string Mensaje)
        {

            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                // Llamar al procedimiento almacenado para editar un cliente.

                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Asignar parámetros del cliente al comando SQL.
                    SqlCommand cmd = new SqlCommand("SP_MODIFICARCLIENTE", oconexion);
                    cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                    cmd.Parameters.AddWithValue("IdDatosPersona", obj.oDatosPersona.IdDatosPersona);
                    cmd.Parameters.AddWithValue("IdDireccion", obj.oDatosPersona.oDireccion.IdDireccion);
                    cmd.Parameters.AddWithValue("IdTelefono", obj.oDatosPersona.oTelefono.IdTelefono);
                    cmd.Parameters.AddWithValue("Nacionalidad", obj.oDatosPersona.Nacionalidad);
                    cmd.Parameters.AddWithValue("CI", obj.oDatosPersona.CI);
                    cmd.Parameters.AddWithValue("Nombre", obj.oDatosPersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.oDatosPersona.Apellido);
                    cmd.Parameters.AddWithValue("Numero", obj.oDatosPersona.oTelefono.Numero);
                    cmd.Parameters.AddWithValue("Estado", obj.oDatosPersona.oDireccion.Estado);
                    cmd.Parameters.AddWithValue("Ciudad", obj.oDatosPersona.oDireccion.Ciudad);
                    cmd.Parameters.AddWithValue("Sector", obj.oDatosPersona.oDireccion.Sector);
                    cmd.Parameters.AddWithValue("Calle", obj.oDatosPersona.oDireccion.Calle);
                    cmd.Parameters.AddWithValue("Casa", obj.oDatosPersona.oDireccion.Casa);
                    cmd.Parameters.AddWithValue("EstadoActual", obj.Estado);

                    // Definir parámetros de salida.
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    // Ejecutar el procedimiento almacenado y obtener resultados.
                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
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

        // Método para eliminar un cliente de la base de datos.
        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Llamar al procedimiento almacenado para eliminar un cliente.
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARCLIENTE", oconexion);
                    // Asignar parámetros del cliente al comando SQL.
                    cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    // Ejecutar el procedimiento almacenado y obtener resultados.
                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                // En caso de error, asignar valores por defecto y capturar mensaje de error.
                Respuesta = false;
                Mensaje = ex.Message;
            }

            return Respuesta;
        }
    }

}
