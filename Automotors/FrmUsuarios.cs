using System;
using System.Data;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmUsuarios : Form
    {
        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Id");
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Apellido");
            tabla.Columns.Add("Usuario");
            tabla.Columns.Add("Cargo");

            tabla.Rows.Add(1, "Juan", "Pérez", "jperez", "Admin");
            tabla.Rows.Add(2, "María", "Gómez", "mgomez", "Vendedor");

            dataGridView1.DataSource = tabla;
        }
        private void BAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarUsuario frm = new FrmAgregarUsuario();
            frm.ShowDialog(); // Se abre un modal para ingresar datos
            CargarUsuarios(); // Método que recarga la DataGridView
        }

        private void BEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                // Aquí borrás el usuario de la base de datos
                CargarUsuarios();
            }
        }
        private void CargarUsuarios()
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Id");
            tabla.Columns.Add("Nombre");
            tabla.Columns.Add("Apellido");
            tabla.Columns.Add("Usuario");
            tabla.Columns.Add("Cargo");

            // Ejemplo de datos
            tabla.Rows.Add(1, "Juan", "Pérez", "jperez", "Admin");
            tabla.Rows.Add(2, "María", "Gómez", "mgomez", "Vendedor");

            dataGridView1.DataSource = tabla;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Opcional
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
