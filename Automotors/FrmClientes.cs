using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmClientes : Form
    {
        private DataTable dtClientes;

        public FrmClientes()
        {
            InitializeComponent();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                string query = "SELECT * FROM Clientes ORDER BY Apellido, Nombre";

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        dtClientes = new DataTable();
                        dtClientes.Load(reader);
                    }
                }

                dgvClientes.DataSource = dtClientes;
                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrid()
        {
            dgvClientes.AutoGenerateColumns = false;
            dgvClientes.Columns.Clear();

            // Configurar columnas del DataGridView
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "IdCliente",
                HeaderText = "ID",
                Name = "IdCliente",
                ReadOnly = true,
                Width = 50
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Name = "Nombre",
                Width = 120
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Apellido",
                HeaderText = "Apellido",
                Name = "Apellido",
                Width = 120
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DNI",
                HeaderText = "DNI",
                Name = "DNI",
                Width = 100
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "Email",
                Width = 180
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Telefono",
                HeaderText = "Teléfono",
                Name = "Telefono",
                Width = 120
            });

            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Direccion",
                HeaderText = "Dirección",
                Name = "Direccion",
                Width = 200
            });
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmClienteDetalle())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CargarClientes(); // Recargar la lista
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente para editar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCliente = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["IdCliente"].Value);

            using (var frm = new FrmClienteDetalle(idCliente))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CargarClientes(); // Recargar la lista
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente para eliminar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCliente = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["IdCliente"].Value);
            string nombre = dgvClientes.SelectedRows[0].Cells["Nombre"].Value.ToString();
            string apellido = dgvClientes.SelectedRows[0].Cells["Apellido"].Value.ToString();

            var result = MessageBox.Show($"¿Está seguro que desea eliminar al cliente {nombre} {apellido}?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM Clientes WHERE IdCliente = @IdCliente";
                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();
                        using (var cmd = new SqliteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Cliente eliminado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarClientes(); // Recargar la lista
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar cliente: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVerCompras_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente para ver sus compras", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCliente = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["IdCliente"].Value);
            string nombre = dgvClientes.SelectedRows[0].Cells["Nombre"].Value.ToString();
            string apellido = dgvClientes.SelectedRows[0].Cells["Apellido"].Value.ToString();

            using (var frm = new FrmComprasCliente(idCliente, $"{nombre} {apellido}"))
            {
                frm.ShowDialog();
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para editar datos, use el botón 'Editar' y modifique en el formulario de detalles.",
                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}