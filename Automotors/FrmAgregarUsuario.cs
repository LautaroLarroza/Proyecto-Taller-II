using System;
using System.Data.SqlClient;
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

            CRol.Items.Clear();
            CRol.Items.Add("Vendedor");
            CRol.Items.Add("Administrador");
            CRol.Items.Add("Gerente");
            CRol.SelectedIndex = 0;
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
                        // INSERT con nuevas columnas
                        SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Usuarios (Nombre, Apellido, DNI, Email, PasswordHash, PasswordSalt, IdRol) " +
                            "VALUES (@Nombre, @Apellido, @DNI, @Email, @PasswordHash, @PasswordSalt, @IdRol)", connection);

                        AgregarParametros(cmd);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("✅ Usuario agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // UPDATE con nuevas columnas
                        SqlCommand cmd = new SqlCommand(
                            "UPDATE Usuarios SET Nombre=@Nombre, Apellido=@Apellido, DNI=@DNI, Email=@Email, " +
                            "PasswordHash=@PasswordHash, PasswordSalt=@PasswordSalt, IdRol=@IdRol " +
                            "WHERE IdUsuario=@Id", connection);

                        AgregarParametros(cmd);
                        cmd.Parameters.AddWithValue("@Id", UsuarioId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("✅ Usuario modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Volver al formulario de usuarios
                VolverAUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error en la operación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void AgregarParametros(SqlCommand cmd)
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

        private void VolverAUsuarios()
        {
            panelContenedor.Controls.Clear();
            FrmUsuarios frmUsuarios = new FrmUsuarios(panelContenedor);
            frmUsuarios.TopLevel = false;
            frmUsuarios.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(frmUsuarios);
            frmUsuarios.Show();
            this.Close();
        }

        // ====================
        // Métodos de utilidad
        // ====================

        private bool EsDniValido(string dni)
        {
            // Validar que el DNI contenga solo números y tenga entre 8 y 10 dígitos
            return System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8,10}$");
        }

        private bool EsEmailValido(string email)
        {
            // Validación básica de email
            return System.Text.RegularExpressions.Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
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

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[32]; // 256 bits
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private int GetRolId(string rolNombre)
        {
            switch (rolNombre.ToLower())
            {
                case "administrador": return 1;
                case "gerente": return 2;
                case "vendedor": return 3;
                default: return 3;
            }
        }

        // Eventos del formulario
        private void FrmAgregarUsuario_Load(object? sender, EventArgs e) { }
        private void panel1_Paint(object? sender, PaintEventArgs e) { }
    }
}