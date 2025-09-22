using System;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Automotors
{
    public partial class FrmEditarProducto : Form
    {
        public Producto ProductoEditado { get; private set; }

        public FrmEditarProducto() : this(null) { }

        public FrmEditarProducto(Producto producto)
        {
            InitializeComponent();
            CargarMarcas();

            if (producto != null)
            {
                cmbMarca.Text = producto.Marca;
                txtModelo.Text = producto.Modelo;
                nudAnio.Value = producto.Anio;
                nudPrecio.Value = producto.Precio;
                nudStock.Value = producto.CantidadStock;
                txtDescripcion.Text = producto.Descripcion;
                chkEstado.Checked = producto.Estado;
                this.Text = "Modificar Producto";
            }
            else
            {
                this.Text = "Agregar Producto";
            }
        }

        private void CargarMarcas()
        {
            try
            {
                cmbMarca.Items.Clear();

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

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbMarca.Text))
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

            ProductoEditado = new Producto
            {
                Marca = cmbMarca.Text.Trim(),
                Modelo = txtModelo.Text.Trim(),
                Anio = (int)nudAnio.Value,
                Precio = nudPrecio.Value,
                CantidadStock = (int)nudStock.Value,
                Descripcion = txtDescripcion.Text.Trim(),
                Estado = chkEstado.Checked
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            string nuevaMarca = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre de la marca:", "Agregar Marca");

            if (!string.IsNullOrWhiteSpace(nuevaMarca))
            {
                try
                {
                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();

                        // Verificar si la marca ya existe
                        string checkQuery = "SELECT COUNT(*) FROM Marcas WHERE Nombre = @Nombre";
                        using (var checkCommand = new SqliteCommand(checkQuery, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@Nombre", nuevaMarca.Trim());
                            long existe = (long)checkCommand.ExecuteScalar();

                            if (existe > 0)
                            {
                                MessageBox.Show("La marca ya existe.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Insertar nueva marca
                        string insertQuery = "INSERT INTO Marcas (Nombre) VALUES (@Nombre)";
                        using (var command = new SqliteCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Nombre", nuevaMarca.Trim());
                            command.ExecuteNonQuery();

                            MessageBox.Show("Marca agregada correctamente", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Recargar marcas y seleccionar la nueva
                            CargarMarcas();
                            cmbMarca.Text = nuevaMarca.Trim();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar marca: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}