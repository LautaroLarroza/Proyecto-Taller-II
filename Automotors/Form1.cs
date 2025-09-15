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
            BUsuarios.Enabled = false;
            BBackUp.Enabled = false;
            BVentas.Enabled = false;
            BClientes.Enabled = false;
            BProductos.Enabled = false;
            button6.Enabled = false;

            switch (FrmLogin.TipoUsuario)
            {
                case "Administrador":
                    BUsuarios.Enabled = true;
                    BBackUp.Enabled = true;
                    BClientes.Enabled = true;
                    BProductos.Enabled = true;
                    break;

                case "Gerente":
                    BClientes.Enabled = true;
                    BVentas.Enabled = true;
                    button6.Enabled = true;
                    break;

                case "Vendedor":
                    BClientes.Enabled = true;
                    BProductos.Enabled = true;
                    break;
            }

            this.Text = $"Sistema Automotors - Usuario: {FrmLogin.UsuarioLogueado} ({FrmLogin.TipoUsuario})";
        }

        private void Form1_Load(object sender, EventArgs e) { }

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

        private void BBackUp_Click(object sender, EventArgs e) { if (!BBackUp.Enabled) return; panelContenedor.Controls.Clear(); }
        private void BVentas_Click(object sender, EventArgs e) { if (!BVentas.Enabled) return; panelContenedor.Controls.Clear(); }
        private void BClientes_Click(object sender, EventArgs e) { if (!BClientes.Enabled) return; panelContenedor.Controls.Clear(); }
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
        private void button6_Click(object sender, EventArgs e) { if (!button6.Enabled) return; panelContenedor.Controls.Clear(); }

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
    }
}
