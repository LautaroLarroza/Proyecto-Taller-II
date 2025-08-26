using System;
using System.Windows.Forms;

namespace Automotors
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Código que quieras ejecutar al cargar el formulario principal
        }

        private void BUsuarios_Click(object sender, EventArgs e)
        {
            // Limpiar panel antes de abrir un nuevo formulario
            panelContenedor.Controls.Clear();

            // Crear y mostrar FrmUsuarios dentro del panel
            FrmUsuarios frm = new FrmUsuarios();
            frm.TopLevel = false; // importante para que sea hijo del panel
            frm.Dock = DockStyle.Fill; // ocupa todo el panel
            panelContenedor.Controls.Add(frm);
            frm.Show();
        }

        private void BBackUp_Click(object sender, EventArgs e)
        {
            // Limpiar panel
            panelContenedor.Controls.Clear();

            // Aquí podrías abrir tu formulario de BackUp de la misma forma
        }

        private void BVentas_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();
            // Aquí podrías abrir tu formulario de Ventas
        }

        private void BClientes_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();
            // Aquí podrías abrir tu formulario de Clientes
        }

        private void BProductos_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();
            // Aquí podrías abrir tu formulario de Productos
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();
            // Aquí podrías abrir tu formulario de Reportes
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Salir de la aplicación
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Opcional
        }
    }
}
