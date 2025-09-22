using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using Automotors.Data;

namespace Automotors
{
    public partial class FrmProductos : Form
    {
        private readonly ProductoRepository _repo;
        private BindingSource _bs = new BindingSource();

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

            // NUEVA COLUMNA DESCRIPCIÓN
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Descripcion",
                HeaderText = "Descripción",
                Name = "colDescripcion",
                Width = 200,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvProductos.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                DataPropertyName = "Estado",
                HeaderText = "Activo",
                Name = "colEstado",
                Width = 50
            });
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
                            nuevo.Id = nuevoId;
                            var lista = _bs.DataSource as List<Producto> ?? new List<Producto>();
                            lista.Add(nuevo);
                            _bs.DataSource = null;
                            _bs.DataSource = lista;
                            _bs.ResetBindings(false);
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

                        var lista = (List<Producto>)_bs.DataSource;
                        int idx = lista.FindIndex(p => p.Id == editado.Id);
                        if (idx >= 0) lista[idx] = editado;
                        _bs.ResetBindings(false);

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
                $"¿Está seguro que desea eliminar el producto {seleccionado.Marca} {seleccionado.Modelo}?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bool eliminado = _repo.Eliminar(seleccionado.Id);
                    if (eliminado)
                    {
                        var lista = (List<Producto>)_bs.DataSource;
                        lista.Remove(seleccionado);
                        _bs.ResetBindings(false);
                        MessageBox.Show("Producto eliminado correctamente.", "Éxito",
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