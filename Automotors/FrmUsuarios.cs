using System;
using System.Data;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmUsuarios : Form
    {
        private DataTable tablaUsuarios = new DataTable(); // tabla persistente

        public FrmUsuarios()
        {
            InitializeComponent();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            InicializarTabla();
            dataGridView1.DataSource = tablaUsuarios;

            // Configuración para selección completa de fila y multi-selección
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = true;
        }

        // Inicializa la tabla con columnas y datos de ejemplo
        private void InicializarTabla()
        {
            tablaUsuarios = new DataTable();
            tablaUsuarios.Columns.Add("Id", typeof(int));
            tablaUsuarios.Columns.Add("Nombre", typeof(string));
            tablaUsuarios.Columns.Add("Apellido", typeof(string));
            tablaUsuarios.Columns.Add("Usuario", typeof(string));
            tablaUsuarios.Columns.Add("Cargo", typeof(string));

            // Datos de ejemplo
            tablaUsuarios.Rows.Add(1, "Juan", "Pérez", "jperez", "Admin");
            tablaUsuarios.Rows.Add(2, "María", "Gómez", "mgomez", "Vendedor");
        }

        // Botón Agregar
        private void BAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarUsuario frmAgregar = new FrmAgregarUsuario(this);
            frmAgregar.ShowDialog();
        }

        // Botón Eliminar
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
                // Eliminamos las filas seleccionadas en orden inverso
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

        // Método público para agregar un usuario desde FrmAgregarUsuario
        public void AgregarUsuarioANuevaFila(string nombre, string apellido, string usuario, string rol)
        {
            int nuevoId = tablaUsuarios.Rows.Count + 1;
            tablaUsuarios.Rows.Add(nuevoId, nombre, apellido, usuario, rol);
        }

        // Botón Modificar
        private void BModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un usuario para modificar.");
                return;
            }

            DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
            DataRow filaTabla = ((DataRowView)filaSeleccionada.DataBoundItem).Row;

            // Obtener datos seguros
            string nombre = filaTabla["Nombre"]?.ToString() ?? "";
            string apellido = filaTabla["Apellido"]?.ToString() ?? "";
            string usuario = filaTabla["Usuario"]?.ToString() ?? "";
            string rol = filaTabla["Cargo"]?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(rol))
            {
                MessageBox.Show("Todos los campos del usuario deben estar completos para modificar.");
                return;
            }

            // Abrir FrmAgregarUsuario con datos prellenados
            FrmAgregarUsuario frmModificar = new FrmAgregarUsuario(this)
            {
                Nombre = nombre,
                Apellido = apellido,
                Usuario = usuario,
                Rol = rol
            };

            frmModificar.ShowDialog();

            // Actualizar la fila con los datos modificados
            filaTabla["Nombre"] = frmModificar.Nombre;
            filaTabla["Apellido"] = frmModificar.Apellido;
            filaTabla["Usuario"] = frmModificar.Usuario;
            filaTabla["Cargo"] = frmModificar.Rol;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Opcional
        }
    }
}
