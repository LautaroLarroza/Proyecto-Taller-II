using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmLogin : Form
    {
        public static string UsuarioLogueado { get; private set; }
        public static string TipoUsuario { get; private set; }
        public static int UsuarioId { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
            EsUsuarioAdmin = false;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtUsuario.Text = "admin";
            txtPassword.Text = "admin123";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            // Credenciales estáticas para admin (acceso total)
            if (usuario == "admin" && password == "admin123")
            {
                UsuarioLogueado = "Administrador Sistema";
                TipoUsuario = "Administrador";
                UsuarioId = 1;
                EsUsuarioAdmin = true; // ← Nueva propiedad

                Form1 principal = new Form1();
                principal.Show();
                this.Hide();
                return;
            }

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verificar credenciales de usuarios registrados
            if (ValidarCredenciales(usuario, password))
            {
                Form1 principal = new Form1();
                principal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCredenciales(string usuario, string password)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT u.IdUsuario, u.Nombre, u.Apellido, u.Usuario,
                               u.PasswordHash, u.PasswordSalt,
                               r.Nombre AS Rol, u.Estado
                        FROM Usuarios u
                        INNER JOIN Roles r ON u.IdRol = r.IdRol
                        WHERE u.Usuario = @Usuario AND u.Estado = 1";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] storedHash = (byte[])reader["PasswordHash"];
                                byte[] storedSalt = (byte[])reader["PasswordSalt"];

                                if (VerifyPassword(password, storedHash, storedSalt))
                                {
                                    UsuarioId = reader.GetInt32(reader.GetOrdinal("IdUsuario"));
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
                byte[] enteredHash = HashPassword(password, storedSalt);
                if (enteredHash.Length != storedHash.Length) return false;

                for (int i = 0; i < enteredHash.Length; i++)
                    if (enteredHash[i] != storedHash[i]) return false;

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

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public static bool EsUsuarioAdmin { get; private set; }
    }
}
