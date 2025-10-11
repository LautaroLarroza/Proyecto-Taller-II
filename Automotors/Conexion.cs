using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public static class Conexion
    {
        // Conexión a SQL Server con autenticación de Windows
        private static string connectionString =
    "Server=DESKTOP-CGS0MGN\\SQLEXPRESS;Database=bd_automotors;Trusted_Connection=True;Connection Timeout=30;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión (SQL Server): {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool ExecuteNonQuery(string query)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar consulta: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static SqlDataReader ExecuteReader(string query)
        {
            try
            {
                var connection = GetConnection();
                connection.Open();
                var command = new SqlCommand(query, connection);
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar consulta: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
