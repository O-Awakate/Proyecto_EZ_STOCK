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
    public class CD_Categoria
    {
        // Método para listar categorías desde la base de datos.
        public List<Categoria> listar() 
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Construir y ejecutar consulta SQL para obtener categorías.
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCategoria,NombreCategoria,Estado from CATEGORIA ");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    // Procesar resultados y agregar a la lista.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                NombreCategoria = dr["NombreCategoria"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // En caso de error, reinicializar la lista.
                    lista = new List<Categoria>();
                }
            }

            return lista;
        }

        // Método para registrar una nueva categoría en la base de datos.
        public int Registrar(Categoria obj, out string Mensaje) 
        {

            int IdCategoriaGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Llamar al procedimiento almacenado para registrar una categoría.
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCATEGORIA", oconexion);
                    cmd.Parameters.AddWithValue("NombreCategoria", obj.NombreCategoria);
                    cmd.Parameters.AddWithValue("estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    // Ejecutar el procedimiento almacenado y obtener resultados.
                    cmd.ExecuteNonQuery();

                    IdCategoriaGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                // En caso de error, asignar valores por defecto y capturar mensaje de error.
                IdCategoriaGenerado = 0;
                Mensaje = ex.Message;

            }

            return IdCategoriaGenerado;

        }

        // Método para editar una categoría existente en la base de datos.
        public bool Editar(Categoria obj, out string Mensaje) 
        {

            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Llamar al procedimiento almacenado para editar una categoría.
                    SqlCommand cmd = new SqlCommand("SP_EditarCategoria", oconexion);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.IdCategoria);
                    cmd.Parameters.AddWithValue("NombreCategoria", obj.NombreCategoria);
                    cmd.Parameters.AddWithValue("estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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
                // En caso de error, asignar valores por defecto y capturar mensaje de error.
                Respuesta = false;
                Mensaje = ex.Message;

            }

            return Respuesta;

        }

        // Método para eliminar una categoría de la base de datos.
        public bool Eliminar(Categoria obj, out string Mensaje)
        {

            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    // Llamar al procedimiento almacenado para eliminar una categoría.
                    SqlCommand cmd = new SqlCommand("SP_EliminarCategoria", oconexion);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.IdCategoria);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
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
                // En caso de error, asignar valores por defecto y capturar mensaje de error.
                Respuesta = false;
                Mensaje = ex.Message;

            }

            return Respuesta;

        }


    }
}
