using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmAgregarProducto : Form // <- QUITAR "private"
    {
        public FrmAgregarProducto()
        {
            InitializeComponent();
            CargarMarcas();
        }

        private void CargarMarcas()
        {
            try
            {
                string query = "SELECT IdMarca, Nombre FROM Marcas WHERE Estado = 1 ORDER BY Nombre";
                using (SqlDataReader reader = Conexion.ExecuteReader(query))
                {
                    if (reader != null)
                    {
                        cmbMarca.Items.Clear();
                        while (reader.Read())
                        {
                            cmbMarca.Items.Add(new
                            {
                                Id = Convert.ToInt32(reader["IdMarca"]),
                                Name = reader["Nombre"].ToString()
                            });
                        }
                        cmbMarca.DisplayMember = "Name";
                        cmbMarca.ValueMember = "Id";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar marcas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                try
                {
                    dynamic selectedMarca = cmbMarca.SelectedItem;
                    int idMarca = selectedMarca.Id;

                    // Conversiones seguras con manejo de valores nulos
                    int? anio = null;
                    decimal precio = 0;
                    int stock = 0;

                    // Manejar año (puede ser nulo)
                    if (!string.IsNullOrEmpty(txtAnio.Text) && int.TryParse(txtAnio.Text, out int anioTemp))
                    {
                        anio = anioTemp;
                    }

                    // Conversiones para precio y stock
                    decimal.TryParse(txtPrecio.Text, out precio);
                    int.TryParse(txtStock.Text, out stock);

                    // Construir la consulta SQL manejando el valor nulo para año
                    string anioValue = anio.HasValue ? anio.Value.ToString() : "NULL";

                    string query = $@"
                        INSERT INTO Productos (IdMarca, Modelo, Anio, Precio, Stock, Descripcion)
                        VALUES ({idMarca}, '{txtModelo.Text}', {anioValue}, {precio.ToString().Replace(",", ".")}, 
                                {stock}, '{txtDescripcion.Text.Replace("'", "''")}')";

                    if (Conexion.ExecuteNonQuery(query))
                    {
                        MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarDatos()
        {
            if (cmbMarca.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una marca.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                MessageBox.Show("Ingrese el modelo del producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}