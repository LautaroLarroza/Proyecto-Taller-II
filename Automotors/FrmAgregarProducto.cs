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

        // Método para cargar datos cuando se usa para editar
        public void CargarDatosProducto(Producto producto)
        {
            if (producto != null)
            {
                cmbMarca.Text = producto.Marca;
                txtModelo.Text = producto.Modelo;
                nudAnio.Value = producto.Anio;
                nudPrecio.Value = producto.Precio;
                nudStock.Value = producto.CantidadStock;
                txtDescripcion.Text = producto.Descripcion;
                this.Text = "Modificar Producto";
            }
        }

        private void FrmAgregarProducto_Load(object sender, EventArgs e)
        {
            CargarMarcas();
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

        private void btnGuardar_Click(object sender, EventArgs e)
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
                Estado = true
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
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