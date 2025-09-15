using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmProductos : Form
    {
        private List<Producto> productos = new List<Producto>();

        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarProductosEstaticos();
            ConfigurarDataGridView();
        }

        private void CargarProductosEstaticos()
        {
            productos.Clear();
            productos.Add(new Producto(1, "Toyota", "Corolla", 2024, 25000.00m, 5, true));
            productos.Add(new Producto(2, "Toyota", "Hilux", 2024, 35000.00m, 3, true));
            productos.Add(new Producto(3, "Ford", "F-150", 2024, 40000.00m, 2, true));
            productos.Add(new Producto(4, "Chevrolet", "Cruze", 2024, 22000.00m, 4, true));
            productos.Add(new Producto(5, "Honda", "Civic", 2024, 23000.00m, 6, true));
            productos.Add(new Producto(6, "Volkswagen", "Golf", 2024, 24000.00m, 3, true));

            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productos;
        }

        private void ConfigurarDataGridView()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.Columns.Clear();

            // Configurar columnas manualmente
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
                    Producto nuevoProducto = frm.ProductoEditado;
                    nuevoProducto.Id = productos.Count > 0 ? productos[productos.Count - 1].Id + 1 : 1;
                    productos.Add(nuevoProducto);
                    dgvProductos.DataSource = null;
                    dgvProductos.DataSource = productos;
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

            Producto productoSeleccionado = dgvProductos.SelectedRows[0].DataBoundItem as Producto;

            using (FrmEditarProducto frm = new FrmEditarProducto(productoSeleccionado))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    int index = productos.IndexOf(productoSeleccionado);
                    productos[index] = frm.ProductoEditado;
                    dgvProductos.DataSource = null;
                    dgvProductos.DataSource = productos;
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

            Producto productoSeleccionado = dgvProductos.SelectedRows[0].DataBoundItem as Producto;

            DialogResult result = MessageBox.Show(
                $"¿Está seguro que desea eliminar el producto {productoSeleccionado.Marca} {productoSeleccionado.Modelo}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                productos.Remove(productoSeleccionado);
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = productos;
                MessageBox.Show("Producto eliminado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}