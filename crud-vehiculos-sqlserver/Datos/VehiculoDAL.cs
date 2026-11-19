using crud_vehiculos_sqlserver.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace crud_vehiculos_sqlserver.Datos
{
    public class VehiculoDAL
    {
        // Leemos la cadena de conexión del archivo web.config
        private readonly string _cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;

        // Leer todos los vehiculos
        public List<Vehiculo> Listar()
        {
            var lista = new List<Vehiculo>();

            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                string query = "SELECT Id, Matricula, Modelo, Anio, Color FROM Vehiculo ORDER BY Id ASC";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open(); // Abrir la conexión a SQL Server

                // Usamos SqlDatReader para leer fila por fila
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Vehiculo
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Matricula = dr["Matricula"].ToString(),
                            Modelo = dr["Modelo"].ToString(),
                            Anio = Convert.ToInt32(dr["Anio"]),
                            Color = dr["Color"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        // Obtener un vehiculo por Id
        public Vehiculo ObtenerPorId(int id)
        {
            Vehiculo vehiculo = null;

            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                string query = "SELECT Id, Matricula, Modelo, Anio, Color FROM Vehiculo WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open(); // Abrir la conexión

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehiculo = new Vehiculo
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Matricula = dr["Matricula"].ToString(),
                            Modelo = dr["Modelo"].ToString(),
                            Anio = Convert.ToInt32(dr["Anio"]),
                            Color = dr["Color"].ToString()
                        };
                    }
                }
            }
            return vehiculo;
        }

        // Crear un nuevo vehiculo
        public bool Registrar(Vehiculo v)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                string query = @"INSERT INTO Vehiculo (Matricula, Modelo, Anio, Color)
                    VALUES (@Matricula, @Modelo, @Anio, @Color)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Matricula", v.Matricula);
                cmd.Parameters.AddWithValue("@Modelo", v.Modelo);
                cmd.Parameters.AddWithValue("Anio", v.Anio);
                cmd.Parameters.AddWithValue("@Color", v.Color);

                con.Open(); // Abre la conexión

                // Devuelve el número de filas afectadas
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Editar el vehiculo
        public bool Editar(Vehiculo v)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                string query = @"UPDATE Vehiculo 
                                 SET Matricula = @Matricula, Modelo = @Modelo, Anio = @Anio, Color = @Color 
                                 WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", v.Id);
                cmd.Parameters.AddWithValue("@Matricula", v.Matricula);
                cmd.Parameters.AddWithValue("@Modelo", v.Modelo);
                cmd.Parameters.AddWithValue("@Anio", v.Anio);
                cmd.Parameters.AddWithValue("@Color", v.Color);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 5. ELIMINAR UN VEHÍCULO (DELETE)
        public bool Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                string query = "DELETE FROM Vehiculo WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}