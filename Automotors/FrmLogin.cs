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
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            // Credenciales estáticas para admin
            if (usuario == "admin" && password == "admin123")
            {
                UsuarioLogueado = "Administrador";
                TipoUsuario = "Administrador";
                UsuarioId = 1; // ✅ ASIGNAR ID PARA ADMIN

                Form1 formPrincipal = new Form1();
                formPrincipal.Show();
                this.Hide();
                return;
            }

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verificar credenciales en la base de datos
            if (ValidarCredenciales(usuario, password))
            {
                Form1 formPrincipal = new Form1();
                formPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Error",
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
            SELECT u.IdUsuario, u.Nombre, u.Apellido, 
                   u.Usuario, u.PasswordHash, u.PasswordSalt,
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
                                    UsuarioId = reader.GetInt32(reader.GetOrdinal("IdUsuario")); // ✅ GUARDAR ID
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

        private void CrearUsuarioAdministradorPorDefecto()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    string checkUserQuery = "SELECT COUNT(*) FROM Usuarios WHERE Email = 'admin@automotors.com'";
                    using (SqlCommand checkUserCmd = new SqlCommand(checkUserQuery, connection))
                    {
                        int userCount = (int)checkUserCmd.ExecuteScalar();
                        if (userCount > 0) return;
                    }

                    string checkRolQuery = "SELECT COUNT(*) FROM Roles WHERE IdRol = 1";
                    using (SqlCommand checkRolCmd = new SqlCommand(checkRolQuery, connection))
                    {
                        int rolCount = (int)checkRolCmd.ExecuteScalar();
                        if (rolCount == 0)
                        {
                            MessageBox.Show("Error: No existe el rol Administrador en la base de datos", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    byte[] salt = GenerateSalt();
                    byte[] passwordHash = HashPassword("admin123", salt);

                    string insertUserQuery = @"
                        INSERT INTO Usuarios (Nombre, Apellido, DNI, Email, PasswordHash, PasswordSalt, IdRol, Estado)
                        VALUES ('Administrador', 'Sistema', '00000000', 'admin@automotors.com', 
                                @PasswordHash, @PasswordSalt, 1, 1)";

                    using (SqlCommand insertUserCmd = new SqlCommand(insertUserQuery, connection))
                    {
                        insertUserCmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        insertUserCmd.Parameters.AddWithValue("@PasswordSalt", salt);
                        insertUserCmd.ExecuteNonQuery();

                        MessageBox.Show("Usuario admin creado exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creando usuario admin: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            CrearUsuarioAdministradorPorDefecto();
            txtUsuario.Text = "admin";
            txtPassword.Text = "admin123";
        }
    }
}
