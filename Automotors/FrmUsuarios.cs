using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmUsuarios : Form
    {
        private Panel panelContenedor;

        public FrmUsuarios(Panel panel)
        {
            InitializeComponent();
            panelContenedor = panel;
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = true;

            // 🔑 Aseguramos que las columnas se generen automáticamente
            dataGridView1.AutoGenerateColumns = true;
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
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IdUsuario"].Value);

                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();
                        SqliteCommand cmd = new SqliteCommand("DELETE FROM Usuarios WHERE IdUsuario = @id", connection);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("✅ Usuario eliminado correctamente.");
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Error al eliminar: {ex.Message}");
                }
            }
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

            frmAgregar.FormClosed += (s, args) =>
            {
                panelContenedor.Controls.Clear();
                FrmUsuarios frmUsuarios = new FrmUsuarios(panelContenedor);
                frmUsuarios.TopLevel = false;
                frmUsuarios.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(frmUsuarios);
                frmUsuarios.Show();
            };
        }

        private void BModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un usuario para modificar.");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IdUsuario"].Value);
            string nombre = dataGridView1.SelectedRows[0].Cells["Nombre"].Value.ToString();
            string apellido = dataGridView1.SelectedRows[0].Cells["Apellido"].Value.ToString();
            string dni = dataGridView1.SelectedRows[0].Cells["DNI"].Value?.ToString() ?? "";
            string email = dataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
            string rol = dataGridView1.SelectedRows[0].Cells["Rol"].Value.ToString();
            bool estado = dataGridView1.SelectedRows[0].Cells["Estado"].Value?.ToString() == "Activo";

            panelContenedor.Controls.Clear();
            FrmAgregarUsuario frmModificar = new FrmAgregarUsuario(this, panelContenedor)
            {
                Nombre = nombre,
                Apellido = apellido,
                DNI = dni,
                Email = email,
                Rol = rol,
                Estado = estado,
                ModificarEnCurso = true,
                UsuarioId = id
            };

            frmModificar.TopLevel = false;
            frmModificar.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(frmModificar);
            frmModificar.Show();

            frmModificar.FormClosed += (s, args) =>
            {
                panelContenedor.Controls.Clear();
                FrmUsuarios frmUsuarios = new FrmUsuarios(panelContenedor);
                frmUsuarios.TopLevel = false;
                frmUsuarios.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(frmUsuarios);
                frmUsuarios.Show();
            };
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // opcional
        }

        private void CargarUsuarios()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // 🔑 LEFT JOIN para que siempre aparezcan usuarios aunque falte el rol
                    string query = @"
                        SELECT u.IdUsuario, u.Nombre, u.Apellido, u.DNI, u.Email, 
                               COALESCE(r.Nombre, 'Sin rol') as Rol, 
                               CASE WHEN u.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado
                        FROM Usuarios u
                        LEFT JOIN Roles r ON u.IdRol = r.IdRol";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dt.Columns.Add(reader.GetName(i), typeof(string));
                        }

                        while (reader.Read())
                        {
                            DataRow row = dt.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = reader.IsDBNull(i) ? DBNull.Value : reader.GetValue(i).ToString();
                            }
                            dt.Rows.Add(row);
                        }

                        // 🔑 Forzar generación automática de columnas
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al cargar usuarios: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarDataGrid()
        {
            CargarUsuarios();
        }
    }
}
