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
                    query.AppendLine("SELECT u.IdUsuario, dp.IdDatosPersona, dp.TipoCI, dp.CI, dp.Nombre, dp.Apellido, u.Clave, u.ConfirmarClave, u.Estado AS EstadoActual, c.IdCorreo, c.ExtensionDominio, c.Dominio, c.Correo, t.IdTelefono, t.TipoTelefono, t.Numero, d.IdDireccion, d.Estado, d.Ciudad, d.Sector, d.Calle, d.Casa, r.IdRol, r.Descripcion FROM USUARIO u");
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
                                    TipoCI = dr["TipoCI"].ToString(),
                                    CI = dr["CI"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    oCorreo = new Correo
                                    {
                                        IdCorreo = Convert.ToInt32(dr["IdCorreo"]),
                                        UsuarioCorreo = dr["Correo"].ToString(),
                                        Dominio = dr["Dominio"].ToString(),
                                        ExtensionDominio = dr["ExtensionDominio"].ToString()
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
                                        TipoTelefono = dr["TipoTelefono"].ToString(),
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
    }
}
