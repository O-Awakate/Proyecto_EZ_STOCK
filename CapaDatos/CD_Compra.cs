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
    public class CD_Compra
    {
        public int ObtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT COUNT(*)+ 1 FROM COMPRA");
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

        public bool Registra(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;
            int idCredito = 0;


            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCOMPRA", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.OUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.OProvedor.IdProveedor);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("MontoBs", obj.MontoBs);
                    cmd.Parameters.AddWithValue("Abono", obj.Abono);
                    cmd.Parameters.AddWithValue("DetalleCompra", DetalleCompra);
                    cmd.Parameters.AddWithValue("MetodoPago", obj.MetodoPago);
                    cmd.Parameters.AddWithValue("TieneDeuda", obj.TieneDeuda);
                    cmd.Parameters.AddWithValue("Deuda", obj.oCredito.Deuda);
                    
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
        

        public Compra ObtenerCompra(string numero)
        {
            Compra obj = new Compra();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select c.IdCompra,c.IdUsuario,c.IdProveedor,");
                    query.AppendLine("dpu.Nombre, dpu.Apellido,");
                    query.AppendLine("dppr.CI, dppr.Nombre as NombreProveedor, dppr.Apellido as ApellidoProveedor, cp.RazonSocial, cp.RIF,");
                    query.AppendLine("c.TipoDocumento, c.NumeroDocumento, c.MontoTotal, c.MontoBs, c.MetodoPago, CONVERT(char(10), c.FechaRegistro, 103)[FechaRegistro],");
                    query.AppendLine("crd.IdCredito, crd.Deuda");
                    query.AppendLine("from COMPRA c");
                    query.AppendLine("left join CREDITO crd ON crd.IdCredito = c.IdCredito");
                    query.AppendLine("inner join USUARIO u on u.IdUsuario = c.IdUsuario");
                    query.AppendLine("inner join DATOS_PERSONA dpu ON dpu.IdDatosPersona = u.IdDatosPersona");
                    query.AppendLine("inner join PROVEEDOR pr on pr.IdProveedor = c.IdProveedor");
                    query.AppendLine("inner join DATOS_PERSONA dppr ON dppr.IdDatosPersona = pr.IdDatosPersona");
                    query.AppendLine("inner join CASA_PROVEEDORA cp on cp.IdCasaProveedora = pr.IdCasaProveedora");
                    query.AppendLine("where c.NumeroDocumento = @numero");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) 
                        {
                            obj = new Compra()
                            {
                                IdCompra = Convert.ToInt32(dr["IdCompra"]),
                                OUsuario = new Usuario()
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
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
                                OProvedor = new Proveedor()
                                {
                                    IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                    oDatosPersona = new Datos_Persona()
                                    {
                                        CI = dr["CI"].ToString(),
                                        Nombre = dr["NombreProveedor"].ToString(),
                                        Apellido = dr["ApellidoProveedor"].ToString()
                                    },
                                    oCasaProveedora = new Casa_Proveedora()
                                    {
                                        RazonSocial = dr["RazonSocial"].ToString(),
                                        RIF = dr["RIF"].ToString(),
                                    }
                                },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MetodoPago = dr["MetodoPago"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                MontoBs = Convert.ToDecimal(dr["MontoBs"].ToString()),
                                //Estado = Convert.ToBoolean(dr["Estado"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };

                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Compra();
                }
            }

            return obj;
        }

        public List<Detalle_Compra> ObtenerDetalleCompra(int idcompra)
        {
            List<Detalle_Compra> oLista = new List<Detalle_Compra>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();

                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select p.IdProducto, p.DescripcionProducto,p.MarcaProducto,dc.PrecioCompra,dc.PrecioVenta,dc.Cantidad,dc.MontoTotal from DETALLE_COMPRA dc");
                    query.AppendLine("inner join PRODUCTO p on p.IdProducto = dc.IdProducto");
                    query.AppendLine("where dc.IdCompra = @idcompra");


                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@idcompra", idcompra);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Detalle_Compra()
                            {
                                OProducto = new Producto() {
                                    IdProducto = int.Parse(dr["IdProducto"].ToString()),
                                    DescripcionProducto = dr["DescripcionProducto"].ToString(),
                                    MarcaProducto = dr["MarcaProducto"].ToString()
                                },
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),

                            });
                        }
                    }
                }

                catch (Exception ex)
                {
                    oLista = new List<Detalle_Compra>();
                }
            }

            return oLista;
        }

        public bool Abono (Abono_Credito obj, out string Mensaje)
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

        public bool DevolucionCompra(Compra obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DEVOlUCIONCOMPRA", conexion);
                    cmd.Parameters.AddWithValue("@IdCompra", obj.IdCompra);
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

        public bool DevolverProducto(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARCOMPRA", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("@IdCompra", obj.IdCompra); 
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.OUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@IdProveedor", obj.OProvedor.IdProveedor);
                    cmd.Parameters.AddWithValue("@TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("@MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("@MontoBs", obj.MontoBs);
                    cmd.Parameters.AddWithValue("@DetalleCompra", DetalleCompra);
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

    }
}
