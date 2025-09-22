using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Automotors
{
    public static class Conexion
    {
        // La BD debe estar en la misma carpeta que el .exe
        private static string connectionString = "Data Source=BD/bd_automotors.db;Cache=Shared;Mode=ReadWriteCreate;";



        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
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
                MessageBox.Show($"Error de conexión (SQLite): {ex.Message}", "Error",
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
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
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

        public static SqliteDataReader ExecuteReader(string query)
        {
            try
            {
                var connection = GetConnection();
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = query;
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
