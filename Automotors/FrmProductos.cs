using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmProductos : Form
    {
<<<<<<< HEAD
=======
        private readonly ProductoRepository _repo = new ProductoRepository(new Conexion());
        private BindingSource _bs = new BindingSource();

>>>>>>> 2589dee1a930e4bca7307439a0779e59ffa5b83f
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
=======
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
>>>>>>> 2589dee1a930e4bca7307439a0779e59ffa5b83f
            {
                string query = @"
SELECT p.IdProducto, m.Nombre as Marca, p.Modelo, p.Anio, 
       p.Precio, p.Stock as CantidadStock, p.Descripcion, p.Estado
FROM Productos p
INNER JOIN Marcas m ON p.IdMarca = m.IdMarca
WHERE p.Estado = 1
ORDER BY p.IdProducto";

                using (SqlDataReader reader = Conexion.ExecuteReader(query))
                {
                    if (reader != null)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvProductos.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarProducto formAgregar = new FrmAgregarProducto();
            if (formAgregar.ShowDialog() == DialogResult.OK)
            {
<<<<<<< HEAD
                CargarProductos();
=======
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
>>>>>>> 2589dee1a930e4bca7307439a0779e59ffa5b83f
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
<<<<<<< HEAD
                // Suponiendo que tienes una clase Producto con las siguientes propiedades:
                Producto producto = new Producto
                {
                    IdProducto = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProducto"].Value),
                    Marca = dgvProductos.SelectedRows[0].Cells["Marca"].Value.ToString(),
                    Modelo = dgvProductos.SelectedRows[0].Cells["Modelo"].Value.ToString(),
                    Anio = dgvProductos.SelectedRows[0].Cells["Anio"].Value != DBNull.Value ? Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["Anio"].Value) : (int?)null,
                    Precio = Convert.ToDecimal(dgvProductos.SelectedRows[0].Cells["Precio"].Value),
                    CantidadStock = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["CantidadStock"].Value),
                    Descripcion = dgvProductos.SelectedRows[0].Cells["Descripcion"].Value.ToString(),
                    Estado = dgvProductos.SelectedRows[0].Cells["Estado"].Value.ToString() == "Activo"
                };

                FrmEditarProducto formEditar = new FrmEditarProducto(producto);
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    CargarProductos();
=======
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
>>>>>>> 2589dee1a930e4bca7307439a0779e59ffa5b83f
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                int idProducto = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["IdProducto"].Value);
                string modelo = dgvProductos.SelectedRows[0].Cells["Modelo"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar el producto '{modelo}'?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Elimina o comenta la siguiente línea si la columna Estado no existe
                    // string query = $"UPDATE Productos SET Estado = 0 WHERE IdProducto = {idProducto}";
                    // Usa un DELETE si realmente quieres eliminar el registro:
                    string query = $"DELETE FROM Productos WHERE IdProducto = {idProducto}";
                    if (Conexion.ExecuteNonQuery(query))
                    {
                        MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarProductos();
                    }
                }
            }
<<<<<<< HEAD
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
=======

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
>>>>>>> 2589dee1a930e4bca7307439a0779e59ffa5b83f
            }
        }
    }
}
