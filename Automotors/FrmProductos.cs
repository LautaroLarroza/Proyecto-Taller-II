using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using Automotors.Data;
using System.Drawing;

namespace Automotors
{
    public partial class FrmProductos : Form
    {
        private readonly ProductoRepository _repo;
        private BindingSource _bs = new BindingSource();
        private bool eventoEnProgreso = false;

        public FrmProductos()
        {
            InitializeComponent();
            _repo = new ProductoRepository();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarDesdeDb();
        }

        private void CargarDesdeDb()
        {
            try
            {
                var datos = _repo.Listar();
                _bs.DataSource = datos;
                dgvProductos.AutoGenerateColumns = false;
                dgvProductos.DataSource = _bs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando productos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Name = "colId",
                Width = 50
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Marca",
                HeaderText = "Marca",
                Name = "colMarca",
                Width = 100
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Modelo",
                HeaderText = "Modelo",
                Name = "colModelo",
                Width = 100
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Anio",
                HeaderText = "Año",
                Name = "colAnio",
                Width = 60
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Precio",
                HeaderText = "Precio",
                Name = "colPrecio",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "CantidadStock",
                HeaderText = "Stock",
                Name = "colStock",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Descripcion",
                HeaderText = "Descripción",
                Name = "colDescripcion",
                Width = 200,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // COLUMNA ESTADO (Texto Activo/Inactivo)
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "EstadoDisplay",
                HeaderText = "Estado",
                Name = "colEstado",
                Width = 80,
                ReadOnly = true
            });

            // COLUMNA OCULTA para el valor booleano del estado
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Estado",
                HeaderText = "EstadoBool",
                Name = "colEstadoBool",
                Visible = false
            });

            // Configurar eventos
            dgvProductos.CellFormatting += DgvProductos_CellFormatting;
            dgvProductos.CellClick += DgvProductos_CellClick;
            dgvProductos.SelectionChanged += DgvProductos_SelectionChanged;
        }

        private void DgvProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvProductos.Columns[e.ColumnIndex].Name == "colEstado" && e.Value != null)
            {
                string estado = e.Value.ToString();
                if (estado.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(198, 239, 206); // Verde claro
                    e.CellStyle.ForeColor = Color.FromArgb(0, 97, 0);      // Verde oscuro
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 199, 206); // Rojo claro
                    e.CellStyle.ForeColor = Color.FromArgb(156, 0, 6);     // Rojo oscuro
                }

                e.CellStyle.Font = new Font(dgvProductos.Font, FontStyle.Bold);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void DgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Solo procesar clics en la columna Estado
            if (dgvProductos.Columns[e.ColumnIndex].Name != "colEstado") return;

            if (eventoEnProgreso) return;
            eventoEnProgreso = true;

            try
            {
                var producto = dgvProductos.Rows[e.RowIndex].DataBoundItem as Producto;
                if (producto == null) return;

                // Confirmar cambio de estado
                string accion = producto.Estado ? "DESACTIVAR" : "ACTIVAR";
                var confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea {accion} el producto {producto.Marca} {producto.Modelo}?",
                    "Confirmar cambio de estado",
                    MessageBoxButtons.YesNo,
                    producto.Estado ? MessageBoxIcon.Warning : MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    CambiarEstadoProducto(producto.Id, !producto.Estado, producto.Marca, producto.Modelo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar estado: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void CambiarEstadoProducto(int id, bool nuevoEstado, string marca, string modelo)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Productos SET Estado = @estado WHERE IdProducto = @id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@estado", nuevoEstado);
                        command.Parameters.AddWithValue("@id", id);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            CargarDesdeDb();

                            string mensaje = nuevoEstado
                                ? $"✅ Producto {marca} {modelo} activado correctamente."
                                : $"⛔ Producto {marca} {modelo} desactivado correctamente.";

                            MessageBox.Show(mensaje, "Actualización de Estado",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("❌ No se pudo actualizar el estado del producto.", "Error",
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

        private void DgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            bool haySeleccion = dgvProductos.SelectedRows.Count > 0;
            btnModificar.Enabled = haySeleccion;
            btnEliminar.Enabled = haySeleccion;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            using (FrmAgregarProducto formAgregar = new FrmAgregarProducto())
            {
                if (formAgregar.ShowDialog() == DialogResult.OK && formAgregar.ProductoEditado != null)
                {
                    try
                    {
                        var nuevo = formAgregar.ProductoEditado;
                        int nuevoId = _repo.Insertar(nuevo);

                        if (nuevoId > 0)
                        {
                            CargarDesdeDb();
                            MessageBox.Show("Producto agregado correctamente.", "Éxito",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo guardar el producto: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para modificar.", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var seleccionado = dgvProductos.SelectedRows[0].DataBoundItem as Producto;
            if (seleccionado == null) return;

            using (FrmAgregarProducto frm = new FrmAgregarProducto())
            {
                frm.CargarDatosProducto(seleccionado);

                if (frm.ShowDialog() == DialogResult.OK && frm.ProductoEditado != null)
                {
                    try
                    {
                        var editado = frm.ProductoEditado;
                        editado.Id = seleccionado.Id;
                        _repo.Actualizar(editado);

                        CargarDesdeDb();
                        MessageBox.Show("Producto actualizado correctamente.", "Éxito",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo actualizar: " + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var seleccionado = dgvProductos.SelectedRows[0].DataBoundItem as Producto;
            if (seleccionado == null) return;

            var confirm = MessageBox.Show(
                $"¿Está seguro que desea ELIMINAR PERMANENTEMENTE el producto {seleccionado.Marca} {seleccionado.Modelo}?\n\n⚠️ Esta acción no se puede deshacer.",
                "Confirmar eliminación permanente",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool eliminado = _repo.Eliminar(seleccionado.Id);
                    if (eliminado)
                    {
                        CargarDesdeDb();
                        MessageBox.Show("✅ Producto eliminado permanentemente.", "Éxito",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo eliminar: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGestionarMarcas_Click(object sender, EventArgs e)
        {
            using (FrmMarcas frmMarcas = new FrmMarcas())
            {
                frmMarcas.ShowDialog();
                CargarDesdeDb();
            }
        }
    }
}