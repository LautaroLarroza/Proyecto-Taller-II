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

        // =========================
        // Accesos por rol
        // =========================
        private void ConfigurarAccesos()
        {
            // Deshabilitar todo por defecto
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
                    // (si querés que admin también vea ventas/clientes/reportes, habilitalos acá)
                    break;

                case "Gerente":
                    BReportes.Enabled = true;
                    BProductos.Enabled = true;   // gerente puede ver/editar productos
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
            ProbarConexion();
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
            panelContenedor.Controls.Clear();
        }

        private void BClientes_Click(object sender, EventArgs e)
        {
            if (!BClientes.Enabled) return;
            panelContenedor.Controls.Clear();
            // Cuando tengas el form de clientes, abrilo con AbrirEnPanel(new FrmClientes());
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

        // (si además tenés BVentas_Click, lo redirigimos por si el Designer cambia)
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

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void ProbarConexion()
        {
            if (Conexion.TestConnection())
            {
                // Podés comentar este mensaje si te molesta
                MessageBox.Show("Conexión a la base de datos exitosa!", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo conectar a la base de datos. La aplicación puede no funcionar correctamente.",
                    "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
