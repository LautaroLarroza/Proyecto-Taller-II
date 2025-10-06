using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Automotors.Data
{
    public class ProductoRepository
    {
        public List<Producto> Listar()
        {
            var lista = new List<Producto>();
            using (var connection = Conexion.GetConnection())
            {
                connection.Open();
                string query = @"SELECT p.IdProducto, m.Nombre AS Marca, p.Modelo, p.Anio, 
                        p.Precio, p.Stock, p.Descripcion, 
                        CASE WHEN p.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END as EstadoDisplay,
                        p.Estado as Estado
                 FROM Productos p
                 INNER JOIN Marcas m ON p.IdMarca = m.IdMarca";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Producto
                        {
                            Id = reader.GetInt32(0),
                            Marca = reader.GetString(1),
                            Modelo = reader.GetString(2),
                            Anio = reader.GetInt32(3),
                            Precio = reader.GetDecimal(4),
                            CantidadStock = reader.GetInt32(5),
                            Descripcion = reader.IsDBNull(6) ? "" : reader.GetString(6),
                            EstadoDisplay = reader.GetString(7),  // Nueva propiedad
                            Estado = reader.GetBoolean(8)         // Mantener el bool para operaciones
                        });
                    }
                }
            }
            return lista;
        }

        public int Insertar(Producto p)
        {
            using (var connection = Conexion.GetConnection())
            {
                connection.Open();

                // Primero obtener el IdMarca basado en el nombre de la marca
                int idMarca = ObtenerIdMarca(p.Marca);
                if (idMarca == -1)
                {
                    // Si la marca no existe, crearla
                    idMarca = CrearMarca(p.Marca);
                }

                string query = @"INSERT INTO Productos (IdMarca, Modelo, Anio, Precio, Stock, Descripcion, Estado)
                                 VALUES (@idMarca, @modelo, @anio, @precio, @stock, @descripcion, @estado);
                                 SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMarca", idMarca);
                    command.Parameters.AddWithValue("@modelo", p.Modelo);
                    command.Parameters.AddWithValue("@anio", p.Anio);
                    command.Parameters.AddWithValue("@precio", p.Precio);
                    command.Parameters.AddWithValue("@stock", p.CantidadStock);
                    command.Parameters.AddWithValue("@descripcion", p.Descripcion ?? "");
                    command.Parameters.AddWithValue("@estado", p.Estado);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Actualizar(Producto p)
        {
            using (var connection = Conexion.GetConnection())
            {
                connection.Open();

                // Primero obtener el IdMarca basado en el nombre de la marca
                int idMarca = ObtenerIdMarca(p.Marca);
                if (idMarca == -1)
                {
                    // Si la marca no existe, crearla
                    idMarca = CrearMarca(p.Marca);
                }

                string query = @"UPDATE Productos
                                 SET IdMarca = @idMarca,
                                     Modelo = @modelo,
                                     Anio = @anio,
                                     Precio = @precio,
                                     Stock = @stock,
                                     Descripcion = @descripcion,
                                     Estado = @estado
                                 WHERE IdProducto = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idMarca", idMarca);
                    command.Parameters.AddWithValue("@modelo", p.Modelo);
                    command.Parameters.AddWithValue("@anio", p.Anio);
                    command.Parameters.AddWithValue("@precio", p.Precio);
                    command.Parameters.AddWithValue("@stock", p.CantidadStock);
                    command.Parameters.AddWithValue("@descripcion", p.Descripcion ?? "");
                    command.Parameters.AddWithValue("@estado", p.Estado);
                    command.Parameters.AddWithValue("@id", p.Id);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Eliminar(int id)
        {
            using (var connection = Conexion.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM Productos WHERE IdProducto = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // Métodos auxiliares para manejar marcas
        private int ObtenerIdMarca(string nombreMarca)
        {
            using (var connection = Conexion.GetConnection())
            {
                connection.Open();
                string query = "SELECT IdMarca FROM Marcas WHERE Nombre = @Nombre";

                using (var command = new SqlCommand(query, connection))
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
                string query = "INSERT INTO Marcas (Nombre) VALUES (@Nombre); SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombreMarca);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
    }
}