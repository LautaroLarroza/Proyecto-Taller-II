using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmProductos : Form
    {
        private readonly ProductoRepository _repo = new ProductoRepository(new Conexion());
        private BindingSource _bs = new BindingSource();

        public FrmProductos()
        {
            InitializeComponent();
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
                Width = 50
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Marca",
                HeaderText = "Marca",
                Width = 120
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Modelo",
                HeaderText = "Modelo",
                Width = 120
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Anio",
                HeaderText = "Año",
                Width = 60
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Precio",
                HeaderText = "Precio",
                Width = 80,
                DefaultCellStyle = { Format = "C2" }
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "CantidadStock",
                HeaderText = "Stock",
                Width = 60
            });

            dgvProductos.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                DataPropertyName = "Estado",
                HeaderText = "Activo",
                Width = 60
            });
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            using (FrmEditarProducto frm = new FrmEditarProducto())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var nuevo = frm.ProductoEditado;
                        int nuevoId = _repo.Insertar(nuevo);
                        nuevo.Id = nuevoId;

                        var lista = (List<Producto>)_bs.DataSource;
                        lista.Add(nuevo);
                        _bs.ResetBindings(false);
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

            var copia = new Producto(seleccionado.Id, seleccionado.Marca, seleccionado.Modelo,
                                     seleccionado.Anio, seleccionado.Precio,
                                     seleccionado.CantidadStock, seleccionado.Estado);

            using (FrmEditarProducto frm = new FrmEditarProducto(copia))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var editado = frm.ProductoEditado;
                        _repo.Actualizar(editado);

                        var lista = (List<Producto>)_bs.DataSource;
                        int idx = lista.FindIndex(p => p.Id == editado.Id);
                        if (idx >= 0) lista[idx] = editado;
                        _bs.ResetBindings(false);
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
                MessageBox.Show("Seleccione un producto para eliminar.", "Advertencia",
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
                    _repo.Eliminar(seleccionado.Id);
                    var lista = (List<Producto>)_bs.DataSource;
                    lista.Remove(seleccionado);
                    _bs.ResetBindings(false);
                    MessageBox.Show("Producto eliminado correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo eliminar: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
