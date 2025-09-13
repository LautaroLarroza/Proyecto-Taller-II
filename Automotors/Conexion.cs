using System.Data;
using System.Data.SqlClient;


namespace Automotors
{
    public class Conexion
    {
        private readonly string connectionString =
            "Server=localhost\\SQLEXPRESS;Database=Automotors;Trusted_Connection=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
