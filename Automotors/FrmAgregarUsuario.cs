using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmAgregarUsuario : Form
    {
        private FrmUsuarios formPadre;
        private Panel panelContenedor;
        private bool cambiarContraseña = false;
        private bool _modificarEnCurso = false;

        public FrmAgregarUsuario(FrmUsuarios padre, Panel panel)
        {
            InitializeComponent();
            formPadre = padre;
            panelContenedor = panel;

            CheckContraseña.CheckedChanged += CheckContraseña_CheckedChanged;
            BGuardar.Click += BGuardar_Click;
            BCancelar.Click += BCancelar_Click;
            btnCambiarContraseña.Click += BtnCambiarContraseña_Click;

            TContraseña.PasswordChar = '•';
            TContraseña.Enabled = false;
            CargarRolesDesdeBD();
            ConfigurarEstado();

            // Agregar validación de email en tiempo real
            TUsuario.TextChanged += TUsuario_TextChanged;
        }

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

        public string Usuario
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

        public bool ModificarEnCurso
        {
            get => _modificarEnCurso;
            set
            {
                _modificarEnCurso = value;
                ConfigurarEstado();
            }
        }

        public int UsuarioId { get; set; }

        private void CheckContraseña_CheckedChanged(object? sender, EventArgs e)
        {
            TContraseña.PasswordChar = CheckContraseña.Checked ? '\0' : '•';
        }

        private void BtnCambiarContraseña_Click(object? sender, EventArgs e)
        {
            cambiarContraseña = !cambiarContraseña;
            TContraseña.Enabled = cambiarContraseña;
            TContraseña.Text = "";

            if (cambiarContraseña)
            {
                btnCambiarContraseña.Text = "Cancelar Cambio";
                btnCambiarContraseña.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
                TContraseña.Focus();
            }
            else
            {
                btnCambiarContraseña.Text = "Cambiar Contraseña";
                btnCambiarContraseña.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
                TContraseña.Text = "********";
                TContraseña.Enabled = false;
            }
        }

        private void BCancelar_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        // Validación de email en tiempo real
        private void TUsuario_TextChanged(object sender, EventArgs e)
        {
            string email = TUsuario.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                // Resetear el color cuando está vacío
                TUsuario.ForeColor = System.Drawing.Color.Black;
                return;
            }

            if (EsEmailValido(email))
            {
                TUsuario.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                TUsuario.ForeColor = System.Drawing.Color.Red;
            }
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
                        // Verificar si el email ya existe
                        if (EmailExiste(Usuario))
                        {
                            MessageBox.Show("❌ Este email ya está registrado. Use otro email.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TUsuario.Focus();
                            return;
                        }

                        // PARA USUARIOS NUEVOS: Siempre se crean activos
                        SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Usuarios (nombre, Apellido, DNI, usuario, PasswordHash, PasswordSalt, idRol, Estado) " +
                            "VALUES (@nombre, @Apellido, @DNI, @usuario, @PasswordHash, @PasswordSalt, @idRol, 1)", // Estado siempre 1 (activo)
                            connection);

                        AgregarParametros(cmd);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("✅ Usuario agregado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // PARA MODIFICACIÓN: Verificar si el email ya existe (excluyendo el usuario actual)
                        if (EmailExiste(Usuario, UsuarioId))
                        {
                            MessageBox.Show("❌ Este email ya está registrado. Use otro email.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TUsuario.Focus();
                            return;
                        }

                        // PARA MODIFICACIÓN: NO se actualiza el Estado
                        string query = "UPDATE Usuarios SET nombre=@nombre, Apellido=@Apellido, " +
                                       "DNI=@DNI, usuario=@usuario, idRol=@idRol "; // Eliminado Estado=@Estado

                        if (cambiarContraseña && !string.IsNullOrEmpty(Contraseña))
                            query += ", PasswordHash=@PasswordHash, PasswordSalt=@PasswordSalt ";

                        query += "WHERE idUsuario=@Id";

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@nombre", Nombre);
                            cmd.Parameters.AddWithValue("@Apellido", Apellido);
                            cmd.Parameters.AddWithValue("@DNI", DNI);
                            cmd.Parameters.AddWithValue("@usuario", Usuario);
                            cmd.Parameters.AddWithValue("@idRol", GetRolId(Rol));
                            cmd.Parameters.AddWithValue("@Id", UsuarioId);

                            if (cambiarContraseña && !string.IsNullOrEmpty(Contraseña))
                            {
                                byte[] salt = GenerateSalt();
                                byte[] passwordHash = HashPassword(Contraseña, salt);
                                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                                cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            }

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("✅ Usuario modificado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarParametros(SqlCommand cmd)
        {
            byte[] salt = GenerateSalt();
            byte[] passwordHash = HashPassword(Contraseña, salt);

            cmd.Parameters.AddWithValue("@nombre", Nombre);
            cmd.Parameters.AddWithValue("@Apellido", Apellido);
            cmd.Parameters.AddWithValue("@DNI", DNI);
            cmd.Parameters.AddWithValue("@usuario", Usuario);
            cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
            cmd.Parameters.AddWithValue("@idRol", GetRolId(Rol));
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

            if (string.IsNullOrWhiteSpace(Usuario))
            {
                MessageBox.Show("El email es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TUsuario.Focus();
                return false;
            }

            if (!EsEmailValido(Usuario))
            {
                MessageBox.Show("Ingrese un email válido (debe contener @ y dominio).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TUsuario.Focus();
                TUsuario.SelectAll();
                return false;
            }

            if (!ModificarEnCurso || cambiarContraseña)
            {
                if (string.IsNullOrWhiteSpace(Contraseña) || Contraseña.Length < 6)
                {
                    MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TContraseña.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(Rol))
            {
                MessageBox.Show("Debe seleccionar un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CRol.Focus();
                return false;
            }

            return true;
        }

        private void ConfigurarEstado()
        {
            if (ModificarEnCurso)
            {
                label6.Text = "Modificar Usuario";
                btnCambiarContraseña.Visible = true;
                TContraseña.Enabled = false;
                TContraseña.Text = "********";
                cambiarContraseña = false;
                btnCambiarContraseña.Text = "Cambiar Contraseña";
                btnCambiarContraseña.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            }
            else
            {
                label6.Text = "Agregar Nuevo Usuario";
                btnCambiarContraseña.Visible = false;
                TContraseña.Enabled = true;
                TContraseña.Text = "";
                cambiarContraseña = false;
            }
        }

        private bool EsDniValido(string dni)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8,10}$");
        }

        private bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Validación básica de email
                return System.Text.RegularExpressions.Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        // Verificar si el email ya existe en la base de datos
        private bool EmailExiste(string email, int? excluirUsuarioId = null)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE usuario = @email";

                    if (excluirUsuarioId.HasValue)
                    {
                        query += " AND idUsuario != @excluirId";
                    }

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        if (excluirUsuarioId.HasValue)
                        {
                            command.Parameters.AddWithValue("@excluirId", excluirUsuarioId.Value);
                        }

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verificando email: {ex.Message}");
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
                    string query = "SELECT idRol FROM Roles WHERE LOWER(Nombre) = LOWER(@Nombre)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", rolNombre);
                        var result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
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
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            InsertarRolesPorDefecto(connection);
                            CargarRolesDesdeBD();
                            return;
                        }

                        while (reader.Read())
                            CRol.Items.Add(reader["Nombre"].ToString());
                    }
                }

                if (CRol.Items.Count > 0)
                    CRol.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando roles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertarRolesPorDefecto(SqlConnection connection)
        {
            try
            {
                string insertQuery = @"
                    INSERT INTO Roles (Nombre) VALUES 
                    ('Administrador'),
                    ('Gerente'),
                    ('Vendedor')";
                using (var command = new SqlCommand(insertQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error insertando roles: {ex.Message}");
            }
        }
    }
}