using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmUsuarios : Form
    {
        private Panel panelContenedor;
        private bool eventoEnProgreso = false;
        private DataTable dataTableUsuarios;

        public FrmUsuarios(Panel panel)
        {
            InitializeComponent();
            panelContenedor = panel;
            dataTableUsuarios = new DataTable();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarUsuarios();
        }

        private void ConfigurarDataGridView()
        {
            // Configuración básica del DataGridView
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Configurar eventos
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.DataError += dataGridView1_DataError;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;

            // Configurar columnas
            ConfigurarColumnasDataGrid();

            // Formato visual para “Activo / Inactivo”
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void ConfigurarColumnasDataGrid()
        {
            dataGridView1.Columns.Clear();

            // Columnas visibles
            var columnasVisibles = new[]
            {
                new { Name = "Nombre", Header = "Nombre", Width = 120 },
                new { Name = "Apellido", Header = "Apellido", Width = 120 },
                new { Name = "DNI", Header = "DNI", Width = 100 },
                new { Name = "Usuario", Header = "Usuario", Width = 150 },
                new { Name = "Rol", Header = "Rol", Width = 130 },
                new { Name = "EstadoDisplay", Header = "Estado", Width = 100 }
            };

            foreach (var col in columnasVisibles)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = col.Name,
                    DataPropertyName = col.Name,
                    HeaderText = col.Header,
                    Width = col.Width,
                    ReadOnly = true
                });
            }

            // Columnas ocultas
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "IdUsuario",
                DataPropertyName = "IdUsuario",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "EstadoBool",
                DataPropertyName = "EstadoBool",
                HeaderText = "EstadoBool",
                Visible = false
            });
        }

        private void CargarUsuarios()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            u.IdUsuario, 
                            u.Nombre, 
                            u.Apellido,
                            u.DNI,
                            u.Usuario, 
                            COALESCE(r.Nombre, 'Sin rol') as Rol,
                            CASE 
                                WHEN u.Estado = 1 THEN 'Activo' 
                                ELSE 'Inactivo' 
                            END as EstadoDisplay,
                            CAST(u.Estado AS BIT) as EstadoBool
                        FROM Usuarios u
                        LEFT JOIN Roles r ON u.idRol = r.idRol
                        ORDER BY u.Apellido, u.Nombre";

                    using (var command = new SqlCommand(query, connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        dataTableUsuarios.Clear();
                        adapter.Fill(dataTableUsuarios);
                        CargarDataGridViewSeguro(dataTableUsuarios);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al cargar usuarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDataGridViewSeguro(DataTable dt)
        {
            dataGridView1.SuspendLayout();

            try
            {
                dataGridView1.DataSource = null;

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.Refresh();
                    dataGridView1.ClearSelection();

                    Console.WriteLine($"✅ Datos cargados: {dt.Rows.Count} usuarios");
                }
                else
                {
                    Console.WriteLine("ℹ️ No hay usuarios para mostrar");
                }

                // Forzar repintado completo (mejora visual)
                dataGridView1.Invalidate();
                dataGridView1.Refresh();
                Application.DoEvents();
            }
            finally
            {
                dataGridView1.ResumeLayout();
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "EstadoDisplay" && e.Value != null)
            {
                string estado = e.Value.ToString();
                if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(198, 239, 206);
                    e.CellStyle.ForeColor = Color.FromArgb(0, 97, 0);
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 199, 206);
                    e.CellStyle.ForeColor = Color.FromArgb(156, 0, 6);
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
            Console.WriteLine($"Error en DataGridView: {e.Exception.Message}");
        }

        private void BEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un usuario.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var usuarioSeleccionado = dataGridView1.SelectedRows[0];
            string nombre = usuarioSeleccionado.Cells["Nombre"].Value?.ToString() ?? "";
            string apellido = usuarioSeleccionado.Cells["Apellido"].Value?.ToString() ?? "";

            DialogResult confirm = MessageBox.Show(
                $"¿Seguro que desea ELIMINAR PERMANENTEMENTE al usuario {nombre} {apellido}?\n\nEsta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(usuarioSeleccionado.Cells["IdUsuario"].Value);
                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE IdUsuario = @id", connection))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            int rows = cmd.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                MessageBox.Show("✅ Usuario eliminado permanentemente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarUsuarios();
                            }
                            else
                            {
                                MessageBox.Show("❌ No se pudo eliminar el usuario.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Error al eliminar: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BAgregar_Click(object sender, EventArgs e)
        {
            AbrirFormularioAgregarUsuario(false);
        }

        private void BModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un usuario para modificar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int id = Convert.ToInt32(selectedRow.Cells["IdUsuario"].Value);
                string nombre = selectedRow.Cells["Nombre"].Value?.ToString() ?? "";
                string apellido = selectedRow.Cells["Apellido"].Value?.ToString() ?? "";
                string usuario = selectedRow.Cells["Usuario"].Value?.ToString() ?? "";
                string dni = selectedRow.Cells["DNI"].Value?.ToString() ?? "";
                string rol = selectedRow.Cells["Rol"].Value?.ToString() ?? "";

                AbrirFormularioAgregarUsuario(true, id, nombre, apellido, usuario, dni, rol);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al cargar datos para modificar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirFormularioAgregarUsuario(bool esModificacion, int id = 0, string nombre = "",
            string apellido = "", string usuario = "", string dni = "", string rol = "")
        {
            panelContenedor.Controls.Clear();
            FrmAgregarUsuario frm = new FrmAgregarUsuario(this, panelContenedor)
            {
                ModificarEnCurso = esModificacion,
                UsuarioId = id,
                Nombre = nombre,
                Apellido = apellido,
                Usuario = usuario,
                DNI = dni,
                Rol = rol,
                TopLevel = false,
                Dock = DockStyle.Fill
            };

            panelContenedor.Controls.Add(frm);
            frm.Show();

            frm.FormClosed += (s, args) =>
            {
                CargarUsuarios();
            };
        }

        private void BRoles_Click(object sender, EventArgs e)
        {
            using (var frmRoles = new FrmGestionRoles())
            {
                frmRoles.ShowDialog();
                CargarUsuarios();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            if (dataGridView1.Columns[e.ColumnIndex].Name != "EstadoDisplay")
                return;

            if (eventoEnProgreso)
                return;

            eventoEnProgreso = true;

            try
            {
                var fila = dataGridView1.Rows[e.RowIndex];

                if (fila.Cells["IdUsuario"].Value == null || fila.Cells["EstadoBool"].Value == null)
                    return;

                int id = Convert.ToInt32(fila.Cells["IdUsuario"].Value);
                bool estadoActual = Convert.ToBoolean(fila.Cells["EstadoBool"].Value);
                bool nuevoEstado = !estadoActual;

                string nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";
                string apellido = fila.Cells["Apellido"].Value?.ToString() ?? "";

                DialogResult confirm = MessageBox.Show(
                    $"¿Está seguro que desea {(nuevoEstado ? "ACTIVAR" : "DESACTIVAR")} al usuario {nombre} {apellido}?",
                    "Confirmar cambio de estado",
                    MessageBoxButtons.YesNo,
                    nuevoEstado ? MessageBoxIcon.Question : MessageBoxIcon.Warning
                );

                if (confirm == DialogResult.Yes)
                {
                    ActualizarEstadoUsuario(id, nuevoEstado, nombre, apellido);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el estado: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 500;
                timer.Tick += (s, args) =>
                {
                    eventoEnProgreso = false;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }

        private void ActualizarEstadoUsuario(int id, bool nuevoEstado, string nombre, string apellido)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Usuarios SET Estado = @estado WHERE IdUsuario = @id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            CargarUsuarios();

                            string mensaje = nuevoEstado
                                ? $"✅ Usuario {nombre} {apellido} activado correctamente."
                                : $"⛔ Usuario {nombre} {apellido} desactivado correctamente.";

                            MessageBox.Show(mensaje, "Actualización de Estado",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("❌ No se pudo actualizar el estado del usuario.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al actualizar estado: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarDataGrid()
        {
            CargarUsuarios();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool haySeleccion = dataGridView1.SelectedRows.Count > 0;
            BModificar.Enabled = haySeleccion;
            BEliminar.Enabled = haySeleccion;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name != "EstadoDisplay")
                {
                    BModificar_Click(sender, e);
                }
            }
        }
    }
}
