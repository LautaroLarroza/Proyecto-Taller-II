using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public static class Conexion
    {
        // ✅ USA EL NOMBRE CORRECTO DE TU INSTANCIA
        private static string connectionString = @"Data Source=GERBERFEDERICO\SQLEXPRESS01;Initial Catalog=bd_automotors;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool ExecuteNonQuery(string query)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static SqlDataReader ExecuteReader(string query)
        {
            try
            {
                SqlConnection connection = GetConnection();
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar consulta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}