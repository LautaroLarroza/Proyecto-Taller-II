using System;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmAgregarUsuario : Form
    {
        private FrmUsuarios formPadre;
        private Panel panelContenedor;

        public FrmAgregarUsuario(FrmUsuarios padre, Panel panel)
        {
            InitializeComponent();
            formPadre = padre;
            panelContenedor = panel;

            CheckContraseña.CheckedChanged += CheckContraseña_CheckedChanged;
            BGuardar.Click += BGuardar_Click;
            BCancelar.Click += BCancelar_Click;

            TContraseña.PasswordChar = '•';
            CargarRolesDesdeBD();
        }

        // ====================
        // Propiedades públicas
        // ====================
        public string Nombre
        {
            get => TNombre.Text;
            set => TNombre.Text = value;
        }

        public string Apellido
        {
            get => TApellido.Text;
            set => TApellido.Text = value;
        }

        public string DNI
        {
            get => TDNI.Text;
            set => TDNI.Text = value;
        }

        public string Email
        {
            get => TUsuario.Text;
            set => TUsuario.Text = value;
        }

        public string Contraseña
        {
            get => TContraseña.Text;
            set => TContraseña.Text = value;
        }

        public string Rol
        {
            get => CRol.Text;
            set => CRol.Text = value;
        }

        public bool ModificarEnCurso { get; set; } = false;
        public int UsuarioId { get; set; }

        // ====================
        // Eventos
        // ====================

        private void CheckContraseña_CheckedChanged(object? sender, EventArgs e)
        {
            TContraseña.PasswordChar = CheckContraseña.Checked ? '\0' : '•';
        }

        private void BCancelar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void BGuardar_Click(object? sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    if (!ModificarEnCurso)
                    {
                        // INSERT
                        SqliteCommand cmd = new SqliteCommand(
                            "INSERT INTO Usuarios (Nombre, Apellido, DNI, Email, PasswordHash, PasswordSalt, IdRol) " +
                            "VALUES (@Nombre, @Apellido, @DNI, @Email, @PasswordHash, @PasswordSalt, @IdRol)",
                            connection);

                        AgregarParametros(cmd);
                        cmd.ExecuteNonQuery();

                        // ✅ SOLO mensaje esencial
                        MessageBox.Show("✅ Usuario agregado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // UPDATE - Manejar contraseña correctamente
                        string query = "UPDATE Usuarios SET Nombre=@Nombre, Apellido=@Apellido, " +
                                      "DNI=@DNI, Email=@Email, IdRol=@IdRol ";

                        if (!string.IsNullOrEmpty(Contraseña))
                        {
                            query += ", PasswordHash=@PasswordHash, PasswordSalt=@PasswordSalt ";
                        }

                        query += "WHERE IdUsuario=@Id";

                        using (SqliteCommand cmd = new SqliteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", Nombre);
                            cmd.Parameters.AddWithValue("@Apellido", Apellido);
                            cmd.Parameters.AddWithValue("@DNI", DNI);
                            cmd.Parameters.AddWithValue("@Email", Email);
                            cmd.Parameters.AddWithValue("@IdRol", GetRolId(Rol));
                            cmd.Parameters.AddWithValue("@Id", UsuarioId);

                            if (!string.IsNullOrEmpty(Contraseña))
                            {
                                byte[] salt = GenerateSalt();
                                byte[] passwordHash = HashPassword(Contraseña, salt);
                                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                                cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            }

                            cmd.ExecuteNonQuery();
                        }

                        // ✅ SOLO mensaje esencial
                        MessageBox.Show("✅ Usuario modificado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // ✅ CERRAR AUTOMÁTICAMENTE
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarParametros(SqliteCommand cmd)
        {
            byte[] salt = GenerateSalt();
            byte[] passwordHash = HashPassword(Contraseña, salt);

            cmd.Parameters.AddWithValue("@Nombre", Nombre);
            cmd.Parameters.AddWithValue("@Apellido", Apellido);
            cmd.Parameters.AddWithValue("@DNI", DNI);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
            cmd.Parameters.AddWithValue("@IdRol", GetRolId(Rol));
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Apellido))
            {
                MessageBox.Show("El apellido es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(DNI) || !EsDniValido(DNI))
            {
                MessageBox.Show("Ingrese un DNI válido (8-10 dígitos).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TDNI.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email) || !EsEmailValido(Email))
            {
                MessageBox.Show("Ingrese un email válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(Contraseña) || Contraseña.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TContraseña.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(Rol))
            {
                MessageBox.Show("Debe seleccionar un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CRol.Focus();
                return false;
            }

            return true;
        }

        // ====================
        // Métodos de utilidad
        // ====================

        private bool EsDniValido(string dni)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8,10}$");
        }

        private bool EsEmailValido(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
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

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private int GetRolId(string rolNombre)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT IdRol FROM Roles WHERE LOWER(Nombre) = LOWER(@Nombre)";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", rolNombre);
                        var result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // ✅ SOLO mostrar errores graves
                Console.WriteLine($"Error obteniendo ID del rol: {ex.Message}");
            }

            switch (rolNombre.ToLower())
            {
                case "administrador": return 1;
                case "gerente": return 2;
                case "vendedor": return 3;
                default: return 3;
            }
        }

        private void CargarRolesDesdeBD()
        {
            try
            {
                CRol.Items.Clear();

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Nombre FROM Roles ORDER BY Nombre";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            // ✅ SIN MENSAJE - Insertar silenciosamente
                            InsertarRolesPorDefecto(connection);
                            CargarRolesDesdeBD();
                            return;
                        }

                        while (reader.Read())
                        {
                            CRol.Items.Add(reader["Nombre"].ToString());
                        }
                    }
                }

                if (CRol.Items.Count > 0)
                {
                    CRol.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                // ✅ SOLO ERRORES GRAVES
                MessageBox.Show($"Error cargando roles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertarRolesPorDefecto(SqliteConnection connection)
        {
            try
            {
                string insertQuery = @"
                    INSERT INTO Roles (Nombre, Descripcion) VALUES 
                    ('Administrador', 'Acceso completo al sistema'),
                    ('Gerente', 'Gestion de ventas y productos'),
                    ('Vendedor', 'Solo ventas y clientes')";

                using (var command = new SqliteCommand(insertQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // ✅ SOLO ERRORES GRAVES
                Console.WriteLine($"Error insertando roles: {ex.Message}");
            }
        }

        private void FrmAgregarUsuario_Load(object? sender, EventArgs e) { }
        private void panel1_Paint(object? sender, PaintEventArgs e) { }
    }
}