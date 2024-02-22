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
    public  class CD_Venta
    {
        public int ObtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*)+ 1 FROM VENTA");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    idCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());
                }

                catch (Exception ex)
                {
                    idCorrelativo = 0;
                }
            }

            return idCorrelativo;
        }

        public bool RestarStock(int idproducto, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRODUCTO set Stock = Stock - @cantidad where IdProducto = @idproducto");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public bool SumarStock(int idproducto, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRODUCTO set Stock = Stock + @cantidad where IdProducto = @idproducto");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public bool Registra(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARVENTA", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("IdCliente", obj.oCliente.IdCliente);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("MontoBs", obj.MontoBs);
                    cmd.Parameters.AddWithValue("MontoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("MontoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.AddWithValue("MetodoPago", obj.MetodoPago);
                    cmd.Parameters.AddWithValue("TieneDeuda", obj.TieneDeuda);
                    cmd.Parameters.AddWithValue("Deuda", obj.oCredito.Deuda);

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

        public Venta ObtenerVenta(string numero)
        {
            Venta obj = new Venta();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.IdVenta,");
                    query.AppendLine("dpu.Nombre, dpu.Apellido, u.IdUsuario,");
                    query.AppendLine("cl.IdCliente, dpcl.CI, dpcl.Nombre as NombreCliente, dpcl.Apellido as ApellidoCliente,");
                    query.AppendLine("v.TipoDocumento, v.NumeroDocumento, v.MontoTotal, v.MontoBs, v.MontoPago, v.MontoCambio, v.MetodoPago, CONVERT(char(10), v.FechaRegistro, 103)[FechaRegistro],");
                    query.AppendLine("crd.Deuda, crd.IdCredito");
                    query.AppendLine("from VENTA v");
                    query.AppendLine("left join CREDITO crd ON crd.IdCredito = v.IdCredito");
                    query.AppendLine("inner join USUARIO u on u.IdUsuario = v.IdUsuario");
                    query.AppendLine("inner join DATOS_PERSONA dpu ON dpu.IdDatosPersona = u.IdDatosPersona");
                    query.AppendLine("inner join CLIENTE cl on cl.IdCliente = v.IdCliente");
                    query.AppendLine("inner join DATOS_PERSONA dpcl ON dpcl.IdDatosPersona = cl.IdDatosPersona");
                    query.AppendLine("where v.NumeroDocumento = @numero");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;
                    
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Venta()
                            {
                                IdVenta = int.Parse(dr["IdVenta"].ToString()),
                                oUsuario = new Usuario()
                                {
                                    IdUsuario = int.Parse(dr["IdUsuario"].ToString()),
                                    oDatosPersona = new Datos_Persona()
                                    {
                                        Nombre = dr["Nombre"].ToString(),
                                        Apellido = dr["Apellido"].ToString()
                                    }
                                },
                                oCredito = new Credito()
                                {
                                    IdCredito = dr["IdCredito"] != DBNull.Value ? Convert.ToInt32(dr["IdCredito"]) : 0,
                                    Deuda = dr["Deuda"] != DBNull.Value ? Convert.ToDecimal(dr["Deuda"]) : 0
                                },
                                oCliente = new Cliente()
                                {
                                    IdCliente = int.Parse(dr["IdCliente"].ToString()),
                                    oDatosPersona = new Datos_Persona()
                                    {
                                        CI = dr["CI"].ToString(),
                                        Nombre = dr["NombreCliente"].ToString(),
                                        Apellido = dr["ApellidoCliente"].ToString()
                                    },
                                },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                MontoBs = Convert.ToDecimal(dr["MontoBs"].ToString()),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"].ToString()),
                                MetodoPago = dr["MetodoPago"].ToString(),
                                //Estado = Convert.ToBoolean(dr["Estado"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Venta();
                }
            }

            return obj;
        }

        public List<Detalle_Venta> ObtenerDetalleVenta(int IdVenta)
        {
            List<Detalle_Venta> oLista = new List<Detalle_Venta>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    Console.WriteLine("Conexión abierta correctamente");

                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select p.IdProducto, p.DescripcionProducto, p.MarcaProducto,dv.PrecioVenta, dv.Cantidad, dv.SubTotal from DETALLE_VENTA dv");
                    query.AppendLine("inner join PRODUCTO p on p.IdProducto = dv.IdProducto");
                    query.AppendLine("where dv.Idventa = @idVenta");
                    


                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@idVenta", IdVenta);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Detalle_Venta()
                            {
                                OProducto = new Producto()
                                {
                                    IdProducto = int.Parse(dr["IdProducto"].ToString()),
                                    DescripcionProducto = dr["DescripcionProducto"].ToString(),
                                    MarcaProducto = dr["MarcaProducto"].ToString()
                                },
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                SubTotal = Convert.ToDecimal(dr["SubTotal"].ToString()),

                            });
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Error al abrir la conexión: {ex.Message}");
                    oLista = new List<Detalle_Venta>();
                }
            }
            return oLista;
        }

        public bool Abono(Abono_Credito obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;


            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARABONOCREDITO", oconexion);
                    cmd.Parameters.AddWithValue("@IdCredito", obj.oCredito.IdCredito);
                    cmd.Parameters.AddWithValue("@MontoAbono", obj.Monto);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }

                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
            }
            return Respuesta;
        }

        public bool DevolucionVenta(Venta obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DEVOlUCIONVENTA", conexion);
                    cmd.Parameters.AddWithValue("@IdVenta", obj.IdVenta);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Mensaje = ex.Message;
                }
            }

            return respuesta;
        }

        public bool DevolverProducto(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARVENTA", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("@IdVenta", obj.IdVenta); // Asegúrate de tener el IdVenta en tu objeto Venta
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@IdCliente", obj.oCliente.IdCliente);
                    cmd.Parameters.AddWithValue("@TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("@MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("@MontoBs", obj.MontoBs);
                    cmd.Parameters.AddWithValue("@MontoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("@MontoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("@DetalleVenta", DetalleVenta);
                    cmd.Parameters.AddWithValue("@MetodoPago", obj.MetodoPago);
                    cmd.Parameters.AddWithValue("@TieneDeuda", obj.TieneDeuda);
                    cmd.Parameters.AddWithValue("@Deuda", obj.oCredito.Deuda);

                    // Parámetros de salida
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Respuesta = false;
                Mensaje = ex.Message;
            }

            return Respuesta;
        }

        public List<Venta> listar()
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Construir y ejecutar consulta SQL para obtener información de clientes.
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT v.IdVenta,");
                    query.AppendLine("dpu.Nombre, dpu.Apellido, u.IdUsuario,");
                    query.AppendLine("cl.IdCliente, dpcl.CI, dpcl.Nombre as NombreCliente, dpcl.Apellido as ApellidoCliente,");
                    query.AppendLine("v.TipoDocumento, v.NumeroDocumento, v.MontoTotal, v.MontoBs, v.MontoPago, v.MontoCambio, v.MetodoPago, CONVERT(char(10), v.FechaRegistro, 103)[FechaRegistro],");
                    query.AppendLine("crd.Deuda, crd.IdCredito");
                    query.AppendLine("FROM VENTA v");
                    query.AppendLine("LEFT JOIN CREDITO crd ON crd.IdCredito = v.IdCredito");
                    query.AppendLine("INNER JOIN USUARIO u ON u.IdUsuario = v.IdUsuario");
                    query.AppendLine("INNER JOIN DATOS_PERSONA dpu ON dpu.IdDatosPersona = u.IdDatosPersona");
                    query.AppendLine("INNER JOIN CLIENTE cl ON cl.IdCliente = v.IdCliente");
                    query.AppendLine("INNER JOIN DATOS_PERSONA dpcl ON dpcl.IdDatosPersona = cl.IdDatosPersona");
                    query.AppendLine("WHERE crd.Deuda IS NOT NULL");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    // Procesar resultados y agregar a la lista de clientes.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Venta()
                            {
                                IdVenta = int.Parse(dr["IdVenta"].ToString()),
                                oUsuario = new Usuario()
                                {
                                    IdUsuario = int.Parse(dr["IdUsuario"].ToString()),
                                    oDatosPersona = new Datos_Persona()
                                    {
                                        Nombre = dr["Nombre"].ToString(),
                                        Apellido = dr["Apellido"].ToString()
                                    }
                                },
                                oCredito = new Credito()
                                {
                                    IdCredito = dr["IdCredito"] != DBNull.Value ? Convert.ToInt32(dr["IdCredito"]) : 0,
                                    Deuda = dr["Deuda"] != DBNull.Value ? Convert.ToDecimal(dr["Deuda"]) : 0
                                },
                                oCliente = new Cliente()
                                {
                                    IdCliente = int.Parse(dr["IdCliente"].ToString()),
                                    oDatosPersona = new Datos_Persona()
                                    {
                                        CI = dr["CI"].ToString(),
                                        Nombre = dr["NombreCliente"].ToString(),
                                        Apellido = dr["ApellidoCliente"].ToString()
                                    },
                                },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                MontoBs = Convert.ToDecimal(dr["MontoBs"].ToString()),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"].ToString()),
                                MetodoPago = dr["MetodoPago"].ToString(),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // En caso de error, reinicializar la lista de Venta.
                    lista = new List<Venta>();
                }
            }

            return lista;
        }

    }
}
