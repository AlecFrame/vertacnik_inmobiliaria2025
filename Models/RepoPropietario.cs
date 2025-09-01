using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using System.Linq;
using System.Threading.Tasks;

namespace vertacnik_inmobiliaria2025.Models
{
    public class RepoPropietario : RepositorioBase
    {


        public RepoPropietario(IConfiguration configuration) : base(configuration)
        {

        }

        public int Alta(Propietario p)
        {   
            Console.WriteLine("Ejecutando Alta de propietario: " + p.Nombre + " " + p.Apellido);
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "INSERT INTO Propietarios (Nombre, Apellido, Dni, Telefono, Email, Clave, Estado) " +
                             "VALUES (@nombre, @apellido, @dni, @telefono, @email, @clave, 1); " +
                             "SELECT LAST_INSERT_ID();";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@telefono", p.Telefono);
                    command.Parameters.AddWithValue("@email", p.Email);
                    command.Parameters.AddWithValue("@clave", p.Clave);

                    Console.WriteLine("sql final: "+sql);

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    p.IdPropietario = res;
                    connection.Close();
                }
            }
            return res;
        }

        public int Baja(int idPropietario)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "DELETE FROM Propietarios WHERE IdPropietario = @idPropietario";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPropietario", idPropietario);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int Inactivar(int idPropietario)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE Propietarios SET Estado = 0 WHERE IdPropietario = @idPropietario";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPropietario", idPropietario);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int Activar(int idPropietario)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE Propietarios SET Estado = 1 WHERE IdPropietario = @idPropietario";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPropietario", idPropietario);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int Modificacion(Propietario p)
        {
            int res = -1;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql;
                if (!string.IsNullOrEmpty(p.Clave))
                {
                    sql = "UPDATE Propietarios SET Nombre = @nombre, Apellido = @apellido, Dni = @dni, " +
                        "Telefono = @telefono, Email = @email, Clave = @clave, Estado = @estado WHERE IdPropietario = @idPropietario";
                }
                else
                {
                    sql = "UPDATE Propietarios SET Nombre = @nombre, Apellido = @apellido, Dni = @dni, " +
                        "Telefono = @telefono, Email = @email, Estado = @estado WHERE IdPropietario = @idPropietario";
                }
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", p.Nombre);
                    command.Parameters.AddWithValue("@apellido", p.Apellido);
                    command.Parameters.AddWithValue("@dni", p.Dni);
                    command.Parameters.AddWithValue("@telefono", p.Telefono);
                    command.Parameters.AddWithValue("@email", p.Email);
                    command.Parameters.AddWithValue("@estado", p.Estado);
                    command.Parameters.AddWithValue("@idPropietario", p.IdPropietario);
                    if (!string.IsNullOrEmpty(p.Clave))
                        command.Parameters.AddWithValue("@clave", p.Clave);

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public Propietario ObtenerPorId(int idPropietario)
        {
            Propietario p = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, Email, Clave, Estado " +
                             "FROM Propietarios WHERE IdPropietario = @idPropietario";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPropietario", idPropietario);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            p = new Propietario
                            {
                                IdPropietario = Convert.ToInt32(reader["IdPropietario"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Dni = reader["Dni"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Email = reader["Email"].ToString(),
                                Clave = reader["Clave"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                        }
                    }
                    connection.Close();
                }
            }
            return p;
        }
        
        public IList<Propietario> ObtenerTodos()
        {
            IList<Propietario> lista = new List<Propietario>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, Email, Clave, Estado FROM Propietarios";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Propietario p = new Propietario
                            {
                                IdPropietario = Convert.ToInt32(reader["IdPropietario"]),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Dni = reader["Dni"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Email = reader["Email"].ToString(),
                                Clave = reader["Clave"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            };
                            lista.Add(p);
                        }
                    }
                    connection.Close();
                }
            }
            return lista;
        }
    }
}
