using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmAgregarProducto : Form
    {
        public Producto ProductoEditado { get; private set; }

        public FrmAgregarProducto()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbMarca.SelectedItem == null || string.IsNullOrWhiteSpace(cmbMarca.Text))
            {
                MessageBox.Show("La marca es obligatoria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMarca.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                MessageBox.Show("El modelo es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtModelo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAnio.Text) || !int.TryParse(txtAnio.Text, out int anio))
            {
                MessageBox.Show("El año debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAnio.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text) || !decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtStock.Text) || !int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("El stock debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStock.Focus();
                return;
            }

            try
            {
                ProductoEditado = new Producto
                {
                    Marca = cmbMarca.Text.Trim(),
                    Modelo = txtModelo.Text.Trim(),
                    Anio = anio,
                    Precio = precio,
                    CantidadStock = stock,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Estado = true
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmAgregarProducto_Load(object sender, EventArgs e)
        {
            CargarMarcas();
        }

        private void CargarMarcas()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Nombre FROM Marcas ORDER BY Nombre";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbMarca.Items.Add(reader.GetString(reader.GetOrdinal("Nombre")));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar marcas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}