using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Panel_de_Gestion
    {
        public PaneldeGestion ObtenerContadores(DateTime fechaInicio, DateTime fechaFin)
        {
            PaneldeGestion obj = new PaneldeGestion();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_OBTENERCONTADORES", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FechaInicio", System.Data.SqlDbType.DateTime).Value = fechaInicio; ;
                    cmd.Parameters.Add("@FechaFin", System.Data.SqlDbType.DateTime).Value = fechaFin;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new PaneldeGestion
                            {
                                NumeroCliente = Convert.ToInt32(dr["ContadorClientes"]),
                                NumeroProveerdo = Convert.ToInt32(dr["ContadorProveedores"]),
                                NumeroProductos = Convert.ToInt32(dr["ContadorProductos"]),
                                NumeroVentas = Convert.ToInt32(dr["ContadorVentas"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new PaneldeGestion();
                }
            }

            return obj;
        }

        public PaneldeGestion RedimientoVentas(DateTime fechaInicio, DateTime fechaFin, int NumeroDias)
        {
            PaneldeGestion obj = new PaneldeGestion();
            obj.IngresosBrutos = new List<IngresosPorFecha>();
            obj.TotalGanancia = 0;
            obj.TotalIngresos = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                oconexion.Open();
                using (var cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = oconexion;
                        cmd.CommandText = @"SELECT v.FechaRegistro,SUM(v.MontoTotal), SUM(dv.SubTotal - (dv.Cantidad * p.PrecioCompra)) FROM VENTA v
                                                INNER JOIN DETALLE_VENTA dv ON v.IdVenta = dv.IdVenta
                                                INNER JOIN PRODUCTO p ON dv.IdProducto = p.IdProducto
                                                WHERE v.FechaRegistro BETWEEN @FechaInicio AND @FechaFin GROUP BY v.FechaRegistro";
                        cmd.Parameters.Add("@FechaInicio", System.Data.SqlDbType.DateTime).Value = fechaInicio;
                        cmd.Parameters.Add("@FechaFin", System.Data.SqlDbType.DateTime).Value = fechaFin;
                        var reader = cmd.ExecuteReader();
                        var resultado = new List<KeyValuePair<DateTime, decimal>>();
                        while (reader.Read())
                        {
                            resultado.Add(new KeyValuePair<DateTime, decimal>((DateTime)reader[0], (decimal)reader[1]));
                            
                            obj.TotalIngresos += (decimal)reader[1];
                            obj.TotalGanancia += (decimal)reader[2];
                        }

                        reader.Close();
                        // agrupar por dias
                        if(NumeroDias <= 30)
                        {
                            foreach(var item in resultado)
                            {
                                obj.IngresosBrutos.Add(new IngresosPorFecha()
                                {
                                    Fecha = item.Key.ToString("dd MM"),
                                    Montototal = item.Value
                                });
                            }
                        }
                        // agrupar por semanas
                        else if (NumeroDias <= 92)
                        {
                            obj.IngresosBrutos = (from ListaVentas in resultado
                                                  group ListaVentas by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                                      ListaVentas.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                                  into venta
                                                  select new IngresosPorFecha
                                                  {
                                                      Fecha = "Semana " + venta.Key.ToString(),
                                                      Montototal = venta.Sum(amount => amount.Value)
                                                  }).ToList();
                        }
                        // agrupar por meses
                        else if (NumeroDias <= (365 * 2))
                        {
                            bool isYear = NumeroDias <= 365 ? true : false;
                            obj.IngresosBrutos = (from ListaVentas in resultado
                                                  group ListaVentas by ListaVentas.Key.ToString("MMM yyyy")
                                                  into venta
                                                  select new IngresosPorFecha
                                                  {
                                                      Fecha = isYear ? venta.Key.Substring(0, venta.Key.IndexOf(" ")): venta.Key,
                                                      Montototal = venta.Sum(amount => amount.Value)
                                                  }).ToList();
                        }
                        // agrupar por año 
                        else 
                        {
                            obj.IngresosBrutos = (from ListaVentas in resultado
                                                  group ListaVentas by ListaVentas.Key.ToString("yyyy")
                                                  into venta
                                                  select new IngresosPorFecha
                                                  {
                                                      Fecha = venta.Key,
                                                      Montototal = venta.Sum(amount => amount.Value)
                                                  }).ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        obj = new PaneldeGestion();
                    }
                }
                
            }

            return obj;

            
        }

        public PaneldeGestion RendimientoProductos(DateTime fechaInicio, DateTime fechaFin)
        {
            PaneldeGestion obj = new PaneldeGestion();
            obj.ListaProductosTop = new List<KeyValuePair<string, int>>();
            obj.ListaBajoStock = new List<KeyValuePair<string, int>>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                oconexion.Open();
                using (var cmd = new SqlCommand())
                {
                    try
                    {
                        SqlDataReader dr;
                        cmd.Connection = oconexion;
                        cmd.CommandText = @"select top 5 p.DescripcionProducto,  SUM(dv.Cantidad) as Q from DETALLE_VENTA dv
                                            inner join PRODUCTO p on p.IdProducto = dv.IdProducto
                                            inner join VENTA v on v.IdVenta = dv.Idventa
                                            WHERE v.FechaRegistro BETWEEN @FechaInicio AND @FechaFin 
                                            GROUP BY p.DescripcionProducto order by Q desc";
                        cmd.Parameters.Add("@FechaInicio", System.Data.SqlDbType.DateTime).Value = fechaInicio;
                        cmd.Parameters.Add("@FechaFin", System.Data.SqlDbType.DateTime).Value = fechaFin;
                        dr = cmd.ExecuteReader();


                        while (dr.Read())
                        {
                            obj.ListaProductosTop.Add(new KeyValuePair<string, int>(dr[0].ToString(), (int)dr[1]));
                        }
                        dr.Close();

                        cmd.CommandText = @"select DescripcionProducto, Stock from PRODUCTO where Stock <= 6";

                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            obj.ListaBajoStock.Add(new KeyValuePair<string, int>(dr[0].ToString(), (int)dr[1]));
                        }
                        dr.Close();

                    }
                    catch (Exception ex)
                    {
                        obj = new PaneldeGestion();
                    }
                }

            }

            return obj;
        }
    }
}
