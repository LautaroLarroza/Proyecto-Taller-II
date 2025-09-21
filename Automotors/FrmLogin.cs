using System;
using Microsoft.Data.Sqlite;
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

            // Credenciales estáticas para admin
            if (usuario == "admin" && password == "admin123")
            {
                UsuarioLogueado = "Administrador";
                TipoUsuario = "Administrador";

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

                    using (SqliteCommand cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        using (SqliteDataReader reader = cmd.ExecuteReader())
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

        // Método para crear un usuario administrador por defecto en la base de datos
        private void CrearUsuarioAdministradorPorDefecto()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // 1. PRIMERO verificar si YA EXISTE el usuario admin
                    string checkUserQuery = "SELECT COUNT(*) FROM Usuarios WHERE Email = 'admin@automotors.com'";
                    using (SqliteCommand checkUserCmd = new SqliteCommand(checkUserQuery, connection))
                    {
                        long userCount = (long)checkUserCmd.ExecuteScalar();
                        if (userCount > 0)
                        {
                            return; // Ya existe, no hacer nada
                        }
                    }

                    // 2. VERIFICAR que existe el rol Administrador (IdRol = 1)
                    string checkRolQuery = "SELECT COUNT(*) FROM Roles WHERE IdRol = 1";
                    using (SqliteCommand checkRolCmd = new SqliteCommand(checkRolQuery, connection))
                    {
                        long rolCount = (long)checkRolCmd.ExecuteScalar();
                        if (rolCount == 0)
                        {
                            MessageBox.Show("Error: No existe el rol Administrador en la base de datos", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // 3. SOLO SI no existe el usuario Y sí existe el rol, crearlo
                    byte[] salt = GenerateSalt();
                    byte[] passwordHash = HashPassword("admin123", salt);

                    string insertUserQuery = @"
                INSERT INTO Usuarios (Nombre, Apellido, DNI, Email, PasswordHash, PasswordSalt, IdRol, Estado)
                VALUES ('Administrador', 'Sistema', '00000000', 'admin@automotors.com', 
                        @PasswordHash, @PasswordSalt, 1, 1)";

                    using (SqliteCommand insertUserCmd = new SqliteCommand(insertUserQuery, connection))
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
            DebugBaseDeDatos(); // <- Agregar para diagnosticar
            CrearUsuarioAdministradorPorDefecto();

            txtUsuario.Text = "admin";
            txtPassword.Text = "admin123";
        }

        private void DebugBaseDeDatos()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // Verificar roles
                    string rolesQuery = "SELECT * FROM Roles";
                    using (var cmd = new SqliteCommand(rolesQuery, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("=== ROLES EN BD ===");
                        while (reader.Read())
                        {
                            Console.WriteLine($"IdRol: {reader["IdRol"]}, Nombre: {reader["Nombre"]}");
                        }
                    }

                    // Verificar usuarios
                    string usersQuery = "SELECT * FROM Usuarios";
                    using (var cmd = new SqliteCommand(usersQuery, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("=== USUARIOS EN BD ===");
                        while (reader.Read())
                        {
                            Console.WriteLine($"Usuario: {reader["Email"]}, IdRol: {reader["IdRol"]}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en debug: {ex.Message}");
            }
        }
    }
}