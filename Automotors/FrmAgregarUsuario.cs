using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmAgregarUsuario : Form
    {
        private FrmUsuarios? formPadre;
        private Panel? panelContenedor;
        private bool cambiarContraseña = false;
        private bool _modificarEnCurso = false;
        private bool _tituloForzado = false;


        // === Constructor vacío: necesario para ShowDialog (modal) ===
        public FrmAgregarUsuario()
        {
            InitializeComponent();

            // Estilo popup modal
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.AcceptButton = BGuardar;
            this.CancelButton = BCancelar;

            // Hooks (evitá duplicar estos enganches en el Designer)
            CheckContraseña.CheckedChanged += CheckContraseña_CheckedChanged;
            BGuardar.Click += BGuardar_Click;
            BCancelar.Click += BCancelar_Click;
            TUsuario.TextChanged += TUsuario_TextChanged;

            // Si tu diseño tiene este botón, lo enganchamos; si no, no pasa nada
            try { btnCambiarContraseña.Click += BtnCambiarContraseña_Click; } catch { }

            TContraseña.PasswordChar = '•';
            TContraseña.Enabled = false;

            CargarRolesDesdeBD();
            ConfigurarEstado();
        }

        // === Constructor por compatibilidad cuando se embebe en panel (si lo usás en otro lado) ===
        public FrmAgregarUsuario(FrmUsuarios padre, Panel panel) : this()
        {
            formPadre = padre;
            panelContenedor = panel;
        }

        // ---------- Helpers de UI ----------
        // Busca un Label llamado "label6" o "lblTitulo" y le pone el texto
        private void SetHeaderTitle(string texto)
        {
            try
            {
                // No busques: usá directamente el label del Designer
                if (lblTitulo != null)
                    lblTitulo.Text = texto;
            }
            catch { /* ignorar */ }
        }



        public void ConfigurarTitulo(string titulo)
        {
            _tituloForzado = true;      // marcamos que el padre fijó el título
            this.Text = titulo;
            SetHeaderTitle(titulo);     // tu helper que setea label6/lblTitulo si existe
        }


        // ---------------------- PROPIEDADES ----------------------
        public string Nombre { get => TNombre.Text; set => TNombre.Text = value; }
        public string Apellido { get => TApellido.Text; set => TApellido.Text = value; }
        public string DNI { get => TDNI.Text; set => TDNI.Text = value; }
        public string Usuario { get => TUsuario.Text; set => TUsuario.Text = value; }
        public string Contraseña { get => TContraseña.Text; set => TContraseña.Text = value; }
        public string Rol { get => CRol.Text; set => CRol.Text = value; }

        public bool ModificarEnCurso
        {
            get => _modificarEnCurso;
            set { _modificarEnCurso = value; ConfigurarEstado(); }
        }

        public int UsuarioId { get; set; }

        // ---------------------- EVENTOS ----------------------
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
                try { btnCambiarContraseña.Text = "Cancelar Cambio"; } catch { }
                try { btnCambiarContraseña.BackColor = System.Drawing.Color.FromArgb(192, 57, 43); } catch { }
                TContraseña.Focus();
            }
            else
            {
                try { btnCambiarContraseña.Text = "Cambiar Contraseña"; } catch { }
                try { btnCambiarContraseña.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); } catch { }
                TContraseña.Text = "********";
                TContraseña.Enabled = false;
            }
        }

        private void BCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Validación de email en tiempo real
        private void TUsuario_TextChanged(object sender, EventArgs e)
        {
            string email = TUsuario.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                TUsuario.ForeColor = System.Drawing.Color.Black;
                return;
            }
            TUsuario.ForeColor = EsEmailValido(email) ? System.Drawing.Color.Green : System.Drawing.Color.Red;
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
                        // Nuevo usuario
                        if (EmailExiste(Usuario))
                        {
                            MessageBox.Show("❌ Este email ya está registrado. Use otro email.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TUsuario.Focus();
                            return;
                        }

                        SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Usuarios (nombre, Apellido, DNI, usuario, PasswordHash, PasswordSalt, idRol, Estado) " +
                            "VALUES (@nombre, @Apellido, @DNI, @usuario, @PasswordHash, @PasswordSalt, @idRol, 1)",
                            connection);

                        AgregarParametros(cmd);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("✅ Usuario agregado correctamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Modificación
                        if (EmailExiste(Usuario, UsuarioId))
                        {
                            MessageBox.Show("❌ Este email ya está registrado. Use otro email.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TUsuario.Focus();
                            return;
                        }

                        string query = "UPDATE Usuarios SET nombre=@nombre, Apellido=@Apellido, " +
                                       "DNI=@DNI, usuario=@usuario, idRol=@idRol ";

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

                // Si vino en modo modal → devolvemos OK para refrescar grilla
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------------- AUXILIARES ----------------------
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
            { MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); TNombre.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(Apellido))
            { MessageBox.Show("El apellido es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); TApellido.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(DNI) || !EsDniValido(DNI))
            { MessageBox.Show("Ingrese un DNI válido (8-10 dígitos).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); TDNI.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(Usuario))
            { MessageBox.Show("El email es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); TUsuario.Focus(); return false; }

            if (!EsEmailValido(Usuario))
            { MessageBox.Show("Ingrese un email válido (debe contener @ y dominio).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); TUsuario.Focus(); TUsuario.SelectAll(); return false; }

            if (!ModificarEnCurso || cambiarContraseña)
            {
                if (string.IsNullOrWhiteSpace(Contraseña) || Contraseña.Length < 6)
                { MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); TContraseña.Focus(); return false; }
            }

            if (string.IsNullOrEmpty(Rol))
            { MessageBox.Show("Debe seleccionar un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); CRol.Focus(); return false; }

            return true;
        }

        private void ConfigurarEstado()
        {
            if (ModificarEnCurso)
            {
                // Forzar SIEMPRE el título de edición
                SetHeaderTitle("Modificar Usuario");
                this.Text = "Modificar Usuario";

                try { btnCambiarContraseña.Visible = true; } catch { }
                TContraseña.Enabled = false;
                TContraseña.Text = "********";
                cambiarContraseña = false;
                try { btnCambiarContraseña.Text = "Cambiar Contraseña"; } catch { }
                try { btnCambiarContraseña.BackColor = System.Drawing.Color.FromArgb(52, 152, 219); } catch { }
            }
            else
            {
                // Forzar SIEMPRE el título de alta
                SetHeaderTitle("Agregar Nuevo Usuario");
                this.Text = "Agregar Nuevo Usuario";

                try { btnCambiarContraseña.Visible = false; } catch { }
                TContraseña.Enabled = true;
                TContraseña.Text = "";
                cambiarContraseña = false;
            }
        }


        private bool EsDniValido(string dni) =>
            System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8,10}$");

        private bool EsEmailValido(string email) =>
            System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private bool EmailExiste(string email, int? excluirUsuarioId = null)
        {
            try
            {
                using var connection = Conexion.GetConnection();
                connection.Open();
                string query = "SELECT COUNT(*) FROM Usuarios WHERE usuario = @email";
                if (excluirUsuarioId.HasValue) query += " AND idUsuario != @excluirId";

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                if (excluirUsuarioId.HasValue)
                    command.Parameters.AddWithValue("@excluirId", excluirUsuarioId.Value);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verificando email: {ex.Message}");
                return false;
            }
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];
            Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);
            return sha256.ComputeHash(combinedBytes);
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        private int GetRolId(string rolNombre)
        {
            try
            {
                using var connection = Conexion.GetConnection();
                connection.Open();
                string query = "SELECT idRol FROM Roles WHERE LOWER(Nombre) = LOWER(@Nombre)";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", rolNombre);
                var result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo ID del rol: {ex.Message}");
            }

            return rolNombre.ToLower() switch
            {
                "administrador" => 1,
                "gerente" => 2,
                _ => 3
            };
        }

        private void CargarRolesDesdeBD()
        {
            try
            {
                CRol.Items.Clear();
                using var connection = Conexion.GetConnection();
                connection.Open();
                string query = "SELECT Nombre FROM Roles ORDER BY Nombre";
                using var command = new SqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close();
                    InsertarRolesPorDefecto(connection);
                    CargarRolesDesdeBD();
                    return;
                }

                while (reader.Read())
                    CRol.Items.Add(reader["Nombre"].ToString());

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
                using var command = new SqlCommand(insertQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error insertando roles: {ex.Message}");
            }
        }
    }
}
