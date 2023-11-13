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
    public class CD_Proveedor
    {
        public List<Proveedor> listar()
        {
            List<Proveedor> lista = new List<Proveedor>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT pr.IdProveedor, dp.IdDatosPersona, dp.CI, dp.Nombre, dp.Apellido, pr.Estado AS EstadoActual, t.IdTelefono, t.Numero, cpr.IdCasaProveedora, d.IdDireccion, d.Estado, d.Ciudad, d.Sector, d.Calle, d.Casa, cpr.RIF,cpr.RazonSocial,cpr.SitioWeb FROM PROVEEDOR pr");
                    query.AppendLine("INNER JOIN CASA_PROVEEDORA cpr ON pr.IdCasaProveedora = cpr.IdCasaProveedora");
                    query.AppendLine("INNER JOIN DATOS_PERSONA dp ON pr.IdDatosPersona = dp.IdDatosPersona");
                    query.AppendLine("INNER JOIN TELEFONO t ON dp.IdTelefono = t.IdTelefono");
                    query.AppendLine("INNER JOIN DIRECCION d ON dp.IdDireccion = d.IdDireccion");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Proveedor()
                            {
                                IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                oDatosPersona = new Datos_Persona
                                {
                                    IdDatosPersona = Convert.ToInt32(dr["IdDatosPersona"]),
                                    CI = dr["CI"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
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
                                oCasaProveedora = new Casa_Proveedora
                                {
                                    IdCasaProveedora = Convert.ToInt32(dr["IdCasaProveedora"]),
                                    RIF = dr["RIF"].ToString(),
                                    RazonSocial = dr["RazonSocial"].ToString(),
                                    SitioWeb = dr["SitioWeb"].ToString()
                                },
                                Estado = Convert.ToBoolean(dr["EstadoActual"]),
                            });
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Proveedor>();
                }
            }

            return lista;
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {

            int IdProveedorGenrado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPROVEEDOR", oconexion);
                    cmd.Parameters.AddWithValue("CI", obj.oDatosPersona.CI);
                    cmd.Parameters.AddWithValue("Nombre", obj.oDatosPersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.oDatosPersona.Apellido);
                    cmd.Parameters.AddWithValue("Numero", obj.oDatosPersona.oTelefono.Numero);
                    cmd.Parameters.AddWithValue("Estado", obj.oDatosPersona.oDireccion.Estado);
                    cmd.Parameters.AddWithValue("Ciudad", obj.oDatosPersona.oDireccion.Ciudad);
                    cmd.Parameters.AddWithValue("Sector", obj.oDatosPersona.oDireccion.Sector);
                    cmd.Parameters.AddWithValue("Calle", obj.oDatosPersona.oDireccion.Calle);
                    cmd.Parameters.AddWithValue("Casa", obj.oDatosPersona.oDireccion.Casa);
                    cmd.Parameters.AddWithValue("RIF", obj.oCasaProveedora.RIF);
                    cmd.Parameters.AddWithValue("RazonSocial", obj.oCasaProveedora.RazonSocial);
                    cmd.Parameters.AddWithValue("SitioWeb", obj.oCasaProveedora.SitioWeb);
                    cmd.Parameters.AddWithValue("EstadoActual", obj.Estado);
                    
                    cmd.Parameters.Add("IdDireccion", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("IdTelefono", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("IdCasaProveedor", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    IdProveedorGenrado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {

                IdProveedorGenrado = 0;
                Mensaje = ex.Message;

            }

            return IdProveedorGenrado;

        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {

            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_MODIFICARPROVEEDORES", oconexion);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.IdProveedor);
                    cmd.Parameters.AddWithValue("IdCasaProveedor", obj.oCasaProveedora.IdCasaProveedora);
                    cmd.Parameters.AddWithValue("IdDatosPersona", obj.oDatosPersona.IdDatosPersona);
                    cmd.Parameters.AddWithValue("IdDireccion", obj.oDatosPersona.oDireccion.IdDireccion);
                    cmd.Parameters.AddWithValue("IdTelefono", obj.oDatosPersona.oTelefono.IdTelefono);
                    cmd.Parameters.AddWithValue("CI", obj.oDatosPersona.CI);
                    cmd.Parameters.AddWithValue("Nombre", obj.oDatosPersona.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", obj.oDatosPersona.Apellido);
                    cmd.Parameters.AddWithValue("Numero", obj.oDatosPersona.oTelefono.Numero);
                    cmd.Parameters.AddWithValue("Estado", obj.oDatosPersona.oDireccion.Estado);
                    cmd.Parameters.AddWithValue("Ciudad", obj.oDatosPersona.oDireccion.Ciudad);
                    cmd.Parameters.AddWithValue("Sector", obj.oDatosPersona.oDireccion.Sector);
                    cmd.Parameters.AddWithValue("Calle", obj.oDatosPersona.oDireccion.Calle);
                    cmd.Parameters.AddWithValue("Casa", obj.oDatosPersona.oDireccion.Casa);
                    cmd.Parameters.AddWithValue("RIF", obj.oCasaProveedora.RIF);
                    cmd.Parameters.AddWithValue("RazonSocial", obj.oCasaProveedora.RazonSocial);
                    cmd.Parameters.AddWithValue("SitioWeb", obj.oCasaProveedora.SitioWeb);
                    cmd.Parameters.AddWithValue("EstadoActual", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

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

        public bool Eliminar(Proveedor obj, out string Mensaje)
        {

            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_ELIMINARPROVEEDOR", oconexion);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.IdProveedor);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

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
    }
}
