using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmUsuarios : Form
    {
        private DataTable tablaUsuarios = new DataTable();
        private Panel panelContenedor;

        public FrmUsuarios(Panel panel)
        {
            InitializeComponent();
            panelContenedor = panel;
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            InicializarTabla();
            dataGridView1.DataSource = tablaUsuarios;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = true;
        }

        private void InicializarTabla()
        {
            tablaUsuarios = new DataTable();
            tablaUsuarios.Columns.Add("Id", typeof(int));
            tablaUsuarios.Columns.Add("Nombre", typeof(string));
            tablaUsuarios.Columns.Add("Apellido", typeof(string));
            tablaUsuarios.Columns.Add("Usuario", typeof(string));
            tablaUsuarios.Columns.Add("Cargo", typeof(string));

            tablaUsuarios.Rows.Add(1, "Juan", "Pérez", "jperez", "Admin");
            tablaUsuarios.Rows.Add(2, "María", "Gómez", "mgomez", "Vendedor");
        }

        private void BAgregar_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();
            FrmAgregarUsuario frmAgregar = new FrmAgregarUsuario(this, panelContenedor);
            frmAgregar.ModificarEnCurso = false;
            frmAgregar.TopLevel = false;
            frmAgregar.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(frmAgregar);
            frmAgregar.Show();
        }

        private void BEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un usuario para eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "¿Seguro que desea eliminar el usuario seleccionado?",
                "Confirmar",
                MessageBoxButtons.YesNo
            );

            if (confirm == DialogResult.Yes)
            {
                for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[i];
                    if (!row.IsNewRow)
                    {
                        DataRow filaTabla = ((DataRowView)row.DataBoundItem).Row;
                        tablaUsuarios.Rows.Remove(filaTabla);
                    }
                }
            }
        }

        public void AgregarUsuarioANuevaFila(string nombre, string apellido, string usuario, string rol)
        {
            int nuevoId = tablaUsuarios.Rows.Count == 0
                ? 1
                : tablaUsuarios.AsEnumerable().Max(r => r.Field<int>("Id")) + 1;

            tablaUsuarios.Rows.Add(nuevoId, nombre, apellido, usuario, rol);
        }

        private void BModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un usuario para modificar.");
                return;
            }

            DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
            DataRow filaTabla = ((DataRowView)filaSeleccionada.DataBoundItem).Row;

            panelContenedor.Controls.Clear();
            FrmAgregarUsuario frmModificar = new FrmAgregarUsuario(this, panelContenedor)
            {
                Nombre = filaTabla["Nombre"].ToString() ?? "",
                Apellido = filaTabla["Apellido"].ToString() ?? "",
                Usuario = filaTabla["Usuario"].ToString() ?? "",
                Rol = filaTabla["Cargo"].ToString() ?? "",
                ModificarEnCurso = true
            };

            frmModificar.TopLevel = false;
            frmModificar.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(frmModificar);
            frmModificar.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // opcional
        }
    }
}
