using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<ReporteCompra> compra(string fechainicio, string fechafin, int idproveerdor)
        {
            List<ReporteCompra> Lista = new List<ReporteCompra>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("SP_REPORTECOMPRA", conexion);
                    cmd.Parameters.AddWithValue("fechaInicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechaFin", fechafin);
                    cmd.Parameters.AddWithValue("idProverdor", idproveerdor);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new ReporteCompra()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                MetodoPago = dr["MetodoPago"].ToString(),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                ApellidoUsuario = dr["ApellidoUsuario"].ToString(),
                                RazonSocial = dr["RazonSocial"].ToString(),
                                RIF = dr["RIF"].ToString(),
                                NombreProv = dr["NombreProv"].ToString(),
                                ApellidoProv = dr["ApellidoProv"].ToString(),
                                CI = dr["CI"].ToString(),
                                CodigoFabrica = dr["CodigoFabrica"].ToString(),
                                CodigoAvila = dr["CodigoAvila"].ToString(),
                                MarcaProducto = dr["MarcaProducto"].ToString(),
                                DescripcionProducto = dr["DescripcionProducto"].ToString(),
                                PrecioCompra = dr["PrecioCompra"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString(),
                                Deuda = dr["Deuda"].ToString()


                            });
                        }
                    }
                }

                catch (Exception ex)
                {
                    
                    Console.Write("Error al realizar el respaldo: " + ex.Message);
                    Lista = new List<ReporteCompra>();
                }
            }

            return Lista;
        }

        public List<ReporteVenta> venta(string fechainicio, string fechafin)
        {
            List<ReporteVenta> Lista = new List<ReporteVenta>();

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("SP_REPORTEVENTAS", conexion);
                    cmd.Parameters.AddWithValue("fechaInicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechaFin", fechafin);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new ReporteVenta()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                ApellidoUsuario = dr["ApellidoUsuario"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                ApellidoCliente = dr["ApellidoCliente"].ToString(),
                                CI = dr["CI"].ToString(),
                                CodigoFabrica = dr["CodigoFabrica"].ToString(),
                                CodigoAvila = dr["CodigoAvila"].ToString(),
                                MarcaProducto = dr["MarcaProducto"].ToString(),
                                DescripcionProducto = dr["DescripcionProducto"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString(),
                                Deuda = dr["Deuda"].ToString(),
                                MetodoPago = dr["MetodoPago"].ToString()
                            });
                        }
                    }
                }

                catch (Exception ex)
                {
                    Lista = new List<ReporteVenta>();
                }
            }

            return Lista;
        }


    }
}
