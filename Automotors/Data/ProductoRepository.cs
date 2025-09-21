using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Automotors.Data
{
    public class ProductoRepository
    {
        public ProductoRepository()
        {
            // Constructor vacío
        }

        public List<Producto> Listar()
        {
            var productos = new List<Producto>();

            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT 
                           p.IdProducto as Id, 
                           m.Nombre as Marca, 
                           p.Modelo, 
                           p.Anio, 
                           p.Precio, 
                           p.Stock as CantidadStock, 
                           p.Descripcion,
                           p.Estado 
                       FROM Productos p
                       INNER JOIN Marcas m ON p.IdMarca = m.IdMarca
                       WHERE p.Estado = 1";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(new Producto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Marca = reader.GetString(reader.GetOrdinal("Marca")),
                                Modelo = reader.GetString(reader.GetOrdinal("Modelo")),
                                Anio = reader.GetInt32(reader.GetOrdinal("Anio")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                CantidadStock = reader.GetInt32(reader.GetOrdinal("CantidadStock")),
                                Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? "" : reader.GetString(reader.GetOrdinal("Descripcion")),
                                Estado = reader.GetBoolean(reader.GetOrdinal("Estado"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar productos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return productos;
        }

        public int Insertar(Producto producto)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // Primero obtener el IdMarca basado en el nombre
                    int idMarca = ObtenerIdMarca(producto.Marca);

                    if (idMarca == -1)
                    {
                        // Si la marca no existe, crearla
                        idMarca = CrearMarca(producto.Marca);
                    }

                    string query = @"INSERT INTO Productos (IdMarca, Modelo, Anio, Precio, Stock, Descripcion, Estado) 
                           VALUES (@IdMarca, @Modelo, @Anio, @Precio, @Stock, @Descripcion, @Estado);
                           SELECT last_insert_rowid();";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdMarca", idMarca);
                        command.Parameters.AddWithValue("@Modelo", producto.Modelo);
                        command.Parameters.AddWithValue("@Anio", producto.Anio);
                        command.Parameters.AddWithValue("@Precio", producto.Precio);
                        command.Parameters.AddWithValue("@Stock", producto.CantidadStock);
                        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        command.Parameters.AddWithValue("@Estado", producto.Estado ? 1 : 0);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar producto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        public bool Actualizar(Producto producto)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // Primero obtener el IdMarca basado en el nombre
                    int idMarca = ObtenerIdMarca(producto.Marca);
                    if (idMarca == -1)
                    {
                        idMarca = CrearMarca(producto.Marca);
                    }

                    string query = @"UPDATE Productos 
                                   SET IdMarca = @IdMarca, Modelo = @Modelo, Anio = @Anio, 
                                       Precio = @Precio, Stock = @Stock, Descripcion = @Descripcion, Estado = @Estado
                                   WHERE IdProducto = @Id";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", producto.Id);
                        command.Parameters.AddWithValue("@IdMarca", idMarca);
                        command.Parameters.AddWithValue("@Modelo", producto.Modelo);
                        command.Parameters.AddWithValue("@Anio", producto.Anio);
                        command.Parameters.AddWithValue("@Precio", producto.Precio);
                        command.Parameters.AddWithValue("@Stock", producto.CantidadStock);
                        command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        command.Parameters.AddWithValue("@Estado", producto.Estado ? 1 : 0);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar producto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    // Eliminación lógica (cambiar estado)
                    string query = "UPDATE Productos SET Estado = 0 WHERE IdProducto = @Id";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public Producto ObtenerPorId(int id)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT p.IdProducto as Id, m.Nombre as Marca, p.Modelo, p.Anio, 
                                   p.Precio, p.Stock as CantidadStock, p.Descripcion, p.Estado
                            FROM Productos p
                            INNER JOIN Marcas m ON p.IdMarca = m.IdMarca
                            WHERE p.IdProducto = @Id";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Producto
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Marca = reader.GetString(reader.GetOrdinal("Marca")),
                                    Modelo = reader.GetString(reader.GetOrdinal("Modelo")),
                                    Anio = reader.GetInt32(reader.GetOrdinal("Anio")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                    CantidadStock = reader.GetInt32(reader.GetOrdinal("CantidadStock")),
                                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? "" : reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener producto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        private int ObtenerIdMarca(string nombreMarca)
        {
            using (var connection = Conexion.GetConnection())
            {
                connection.Open();
                string query = "SELECT IdMarca FROM Marcas WHERE Nombre = @Nombre";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombreMarca);
                    var result = command.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        private int CrearMarca(string nombreMarca)
        {
            using (var connection = Conexion.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO Marcas (Nombre) VALUES (@Nombre); SELECT last_insert_rowid();";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombreMarca);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
    }
}