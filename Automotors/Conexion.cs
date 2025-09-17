using System.Configuration;
using System.Data.SqlClient;

namespace Automotors
{
    public class Conexion
    {
        private readonly string _cs;

        public Conexion()
        {
            _cs = ConfigurationManager.ConnectionStrings["AutomotorsDb"].ConnectionString;
        }

        public SqlConnection CrearConexion()
        {
            return new SqlConnection(_cs);
        }
    }
}
