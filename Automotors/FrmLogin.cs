using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmLogin : Form
    {
        public static string UsuarioLogueado { get; private set; }
        public static string TipoUsuario { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verificar credenciales en la base de datos
            if (ValidarCredenciales(usuario, password))
            {
                // Abrir formulario principal
                Form1 formPrincipal = new Form1();
                formPrincipal.Show();
                this.Hide(); // Ocultar login
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCredenciales(string email, string password)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // Buscar el usuario por email
                    string query = @"
                        SELECT u.IdUsuario, u.Nombre, u.Apellido, u.Email, u.PasswordHash, u.PasswordSalt, 
                               r.Nombre as Rol, u.Estado
                        FROM Usuarios u
                        INNER JOIN Roles r ON u.IdRol = r.IdRol
                        WHERE u.Email = @Email AND u.Estado = 1";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Obtener el hash y salt almacenados
                                byte[] storedHash = (byte[])reader["PasswordHash"];
                                byte[] storedSalt = (byte[])reader["PasswordSalt"];

                                // Verificar la contraseña
                                if (VerifyPassword(password, storedHash, storedSalt))
                                {
                                    UsuarioLogueado = $"{reader["Nombre"]} {reader["Apellido"]}";
                                    TipoUsuario = reader["Rol"].ToString();
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar credenciales: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            try
            {
                // Hashear la contraseña ingresada con el salt almacenado
                byte[] enteredHash = HashPassword(password, storedSalt);

                // Comparar los hashes
                if (enteredHash.Length != storedHash.Length)
                    return false;

                for (int i = 0; i < enteredHash.Length; i++)
                {
                    if (enteredHash[i] != storedHash[i])
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                // Combinar password + salt antes de hashear
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

                return sha256.ComputeHash(combinedBytes);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Método para crear un usuario administrador por defecto (solo para desarrollo)
        private void CrearUsuarioAdministradorPorDefecto()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // Verificar si ya existe el usuario admin
                    string checkQuery = "SELECT COUNT(*) FROM Usuarios WHERE Email = 'admin@automotors.com'";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            // Crear usuario administrador por defecto
                            byte[] salt = GenerateSalt();
                            byte[] passwordHash = HashPassword("admin123", salt);

                            string insertQuery = @"
                                INSERT INTO Usuarios (Nombre, Apellido, DNI, Email, PasswordHash, PasswordSalt, IdRol, Estado)
                                VALUES ('Administrador', 'Sistema', '00000000', 'admin@automotors.com', 
                                        @PasswordHash, @PasswordSalt, 1, 1)";

                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                            {
                                insertCmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                                insertCmd.Parameters.AddWithValue("@PasswordSalt", salt);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creando usuario por defecto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // Opcional: Crear usuario administrador por defecto al cargar el login
            // CrearUsuarioAdministradorPorDefecto();
        }
    }
}