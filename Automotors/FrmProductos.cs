using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
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
                CargarProductos();
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
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
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}