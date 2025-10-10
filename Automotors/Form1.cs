using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConfigurarAccesos();
        }

        private void ConfigurarAccesos()
        {
            // Deshabilitar todo por defecto
            BUsuarios.Enabled = false;
            BBackUp.Enabled = false;
            BVentas.Enabled = false;
            BClientes.Enabled = false;
            BProductos.Enabled = false;
            BReportes.Enabled = false;

            // Acceso completo solo para el usuario admin
            if (FrmLogin.EsUsuarioAdmin)
            {
                BUsuarios.Enabled = true;
                BBackUp.Enabled = true;
                BVentas.Enabled = true;
                BClientes.Enabled = true;
                BProductos.Enabled = true;
                BReportes.Enabled = true;
            }
            else
            {
                // Permisos normales por rol para otros usuarios
                switch (FrmLogin.TipoUsuario)
                {
                    case "Administrador":
                        BUsuarios.Enabled = true;
                        BBackUp.Enabled = true;
                        BProductos.Enabled = true;
                        break;

                    case "Gerente":
                        BReportes.Enabled = true;
                        BProductos.Enabled = true;
                        break;

                    case "Vendedor":
                        BClientes.Enabled = true;
                        BVentas.Enabled = true;
                        break;
                }
            }

            this.Text = $"Sistema Automotors - Usuario: {FrmLogin.UsuarioLogueado} ({FrmLogin.TipoUsuario})";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProbarConexion();
            InsertarRolesPorDefecto(); // Asegurar que existan roles básicos
        }

        /// <summary>
        /// Inserta los roles por defecto si no existen
        /// </summary>
        private void InsertarRolesPorDefecto()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // Verificar si ya existen roles
                    string checkQuery = "SELECT COUNT(*) FROM Roles";
                    using (var checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            // Insertar roles por defecto
                            string insertQuery = @"
                                INSERT INTO Roles (Nombre) VALUES ('Administrador');
                                INSERT INTO Roles (Nombre) VALUES ('Gerente');
                                INSERT INTO Roles (Nombre) VALUES ('Vendedor');";

                            using (var insertCmd = new SqlCommand(insertQuery, connection))
                            {
                                insertCmd.ExecuteNonQuery();
                                Console.WriteLine("Roles por defecto insertados correctamente.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // No mostrar error para no molestar al usuario
                Console.WriteLine($"Error insertando roles por defecto: {ex.Message}");
            }
        }

        private void AbrirEnPanel(Form frm)
        {
            try
            {
                // limpiar y disponer lo anterior
                foreach (Control c in panelContenedor.Controls) c.Dispose();
                panelContenedor.Controls.Clear();

                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;

                panelContenedor.Controls.Add(frm);
                panelContenedor.Tag = frm;
                frm.BringToFront();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir la vista: " + ex.Message,
                    "Navegación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BUsuarios_Click(object sender, EventArgs e)
        {
            if (!BUsuarios.Enabled) return;
            AbrirEnPanel(new FrmUsuarios(panelContenedor));
        }

        private void BBackUp_Click(object sender, EventArgs e)
        {
            if (!BBackUp.Enabled) return;
            AbrirEnPanel(new FrmBackup());
        }

        private void BClientes_Click(object sender, EventArgs e)
        {
            if (!BClientes.Enabled) return;
            AbrirEnPanel(new FrmClientes());
        }

        private void BProductos_Click(object sender, EventArgs e)
        {
            if (!BProductos.Enabled) return;
            AbrirEnPanel(new FrmProductos());
        }

        private void BReportes_Click(object sender, EventArgs e)
        {
            if (!BReportes.Enabled) return;
            AbrirEnPanel(new FrmReportes());
        }

        private void BVentas_Click_1(object sender, EventArgs e)
        {
            if (!BVentas.Enabled) return;
            AbrirEnPanel(new FrmVentas());
        }

        private void BVentas_Click(object sender, EventArgs e)
        {
            BVentas_Click_1(sender, e);
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                new FrmLogin().Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea salir de la aplicación?",
                "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes) Application.Exit();
        }

        private void ProbarConexion()
        {
            if (Conexion.TestConnection())
            {
                // Podés comentar este mensaje si te molesta
                // MessageBox.Show("Conexión a la base de datos exitosa!", "Éxito",
                //     MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo conectar a la base de datos. La aplicación puede no funcionar correctamente.",
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {
            // Este método puede dejarse vacío si no necesitas lógica personalizada de pintado
            // O puedes agregar lógica de dibujo personalizado si es necesario
        }
    }
}