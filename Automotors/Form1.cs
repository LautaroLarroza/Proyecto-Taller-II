using System;
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
            // 🔒 Deshabilitar todo por defecto
            BUsuarios.Enabled = false;
            BBackUp.Enabled = false;
            BVentas.Enabled = false;
            BClientes.Enabled = false;
            BProductos.Enabled = false;
            BReportes.Enabled = false;

            switch (FrmLogin.TipoUsuario)
            {
                case "Administrador":
                    BUsuarios.Enabled = true;
                    BBackUp.Enabled = true;
                    BProductos.Enabled = true;
                    break;

                case "Gerente":
                    BReportes.Enabled = true;
                    break;

                case "Vendedor":
                    BClientes.Enabled = true;
                    BVentas.Enabled = true;
                    break;
            }

            this.Text = $"Sistema Automotors - Usuario: {FrmLogin.UsuarioLogueado} ({FrmLogin.TipoUsuario})";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Probar conexión al iniciar la aplicación
            ProbarConexion();
        }

        private void BUsuarios_Click(object sender, EventArgs e)
        {
            if (!BUsuarios.Enabled) return;

            panelContenedor.Controls.Clear();
            FrmUsuarios frm = new FrmUsuarios(panelContenedor);
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(frm);
            frm.Show();
        }

        private void BBackUp_Click(object sender, EventArgs e)
        {
            if (!BBackUp.Enabled) return;
            panelContenedor.Controls.Clear();
        }

        private void BVentas_Click(object sender, EventArgs e)
        {
            if (!BVentas.Enabled) return;
            panelContenedor.Controls.Clear();
        }

        private void BClientes_Click(object sender, EventArgs e)
        {
            if (!BClientes.Enabled) return;
            panelContenedor.Controls.Clear();
        }

        private void BProductos_Click(object sender, EventArgs e)
        {
            if (!BProductos.Enabled) return;

            panelContenedor.Controls.Clear();
            FrmProductos frm = new FrmProductos();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(frm);
            frm.Show();
        }

        private void BReportes_Click(object sender, EventArgs e)
        {
            if (!BReportes.Enabled) return;
            panelContenedor.Controls.Clear();
            // Acá podés cargar tu formulario de reportes (FrmReportes por ej.)
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                FrmLogin login = new FrmLogin();
                login.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea salir de la aplicación?",
                "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void ProbarConexion()
        {
            if (Conexion.TestConnection())
            {
                // Opcional: mostrar mensaje de éxito (puedes comentar esto después de probar)
                MessageBox.Show("Conexión a la base de datos exitosa!", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo conectar a la base de datos. La aplicación puede no funcionar correctamente.",
                               "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
