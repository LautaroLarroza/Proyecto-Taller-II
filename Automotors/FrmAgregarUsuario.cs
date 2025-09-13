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

            TContraseña.PasswordChar = '*';

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

        public bool ModificarEnCurso { get; set; } = false;

        // 👇 Aquí guardamos el ID del usuario cuando estemos modificando
        public int UsuarioId { get; set; }

        // ====================
        // Eventos
        // ====================

        private void CheckContraseña_CheckedChanged(object? sender, EventArgs e)
        {
            TContraseña.PasswordChar = CheckContraseña.Checked ? '\0' : '*';
        }

        private void BGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Apellido) ||
                string.IsNullOrWhiteSpace(Usuario) ||
                string.IsNullOrWhiteSpace(Contraseña))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            if (string.IsNullOrEmpty(Rol))
            {
                MessageBox.Show("Debe seleccionar un rol.");
                return;
            }

            try
            {
                Conexion con = new Conexion();
                using (var connection = con.GetConnection())
                {
                    connection.Open();

                    if (!ModificarEnCurso)
                    {
                        // INSERT
                        SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Usuarios (Nombre, Apellido, Usuario, Contraseña, Rol, Estado) " +
                            "VALUES (@Nombre, @Apellido, @Usuario, @Contraseña, @Rol, 1)", connection);

                        cmd.Parameters.AddWithValue("@Nombre", Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", Apellido);
                        cmd.Parameters.AddWithValue("@Usuario", Usuario);
                        cmd.Parameters.AddWithValue("@Contraseña", Contraseña);
                        cmd.Parameters.AddWithValue("@Rol", Rol);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("✅ Usuario agregado correctamente.");
                    }
                    else
                    {
                        // UPDATE
                        SqlCommand cmd = new SqlCommand(
                            "UPDATE Usuarios SET Nombre=@Nombre, Apellido=@Apellido, Usuario=@Usuario, Contraseña=@Contraseña, Rol=@Rol " +
                            "WHERE Id=@Id", connection);

                        cmd.Parameters.AddWithValue("@Nombre", Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", Apellido);
                        cmd.Parameters.AddWithValue("@Usuario", Usuario);
                        cmd.Parameters.AddWithValue("@Contraseña", Contraseña);
                        cmd.Parameters.AddWithValue("@Rol", Rol);
                        cmd.Parameters.AddWithValue("@Id", UsuarioId);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("✅ Usuario modificado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error en la operación: " + ex.Message);
            }

            // 🔥 Volver al formulario de usuarios
            panelContenedor.Controls.Clear();
            FrmUsuarios frmUsuarios = new FrmUsuarios(panelContenedor);
            frmUsuarios.TopLevel = false;
            frmUsuarios.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(frmUsuarios);
            frmUsuarios.Show();
        }

        // Eventos opcionales
        private void FrmAgregarUsuario_Load(object? sender, EventArgs e) { }
        private void panel1_Paint(object? sender, PaintEventArgs e) { }
        private void checkBox1_CheckedChanged(object? sender, EventArgs e) { }
    }
}
