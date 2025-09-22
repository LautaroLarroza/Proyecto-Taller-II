using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Automotors
{
    public partial class FrmMarcas : Form
    {
        private List<Marca> marcasList;
        private BindingSource bindingSource;

        public FrmMarcas()
        {
            InitializeComponent();
            marcasList = new List<Marca>();
            bindingSource = new BindingSource();
            CargarMarcas();
        }

        private void CargarMarcas()
        {
            try
            {
                marcasList.Clear();

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT IdMarca, Nombre, Descripcion FROM Marcas ORDER BY Nombre";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marcasList.Add(new Marca
                            {
                                IdMarca = reader.GetInt32(reader.GetOrdinal("IdMarca")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ?
                                            string.Empty : reader.GetString(reader.GetOrdinal("Descripcion"))
                            });
                        }
                    }
                }

                bindingSource.DataSource = marcasList;
                dgvMarcas.DataSource = bindingSource;
                dgvMarcas.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar marcas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var form = new FrmEditarMarca())
            {
                if (form.ShowDialog() == DialogResult.OK && form.MarcaEditada != null)
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
                                checkCommand.Parameters.AddWithValue("@Nombre", form.MarcaEditada.Nombre.Trim());
                                long existe = (long)checkCommand.ExecuteScalar();

                                if (existe > 0)
                                {
                                    MessageBox.Show("La marca ya existe.", "Advertencia",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Insertar nueva marca
                            string insertQuery = "INSERT INTO Marcas (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)";
                            using (var command = new SqliteCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Nombre", form.MarcaEditada.Nombre.Trim());
                                command.Parameters.AddWithValue("@Descripcion",
                                    string.IsNullOrWhiteSpace(form.MarcaEditada.Descripcion) ?
                                    DBNull.Value : form.MarcaEditada.Descripcion.Trim());

                                command.ExecuteNonQuery();

                                MessageBox.Show("Marca agregada correctamente", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                CargarMarcas();
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una marca para modificar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var marcaSeleccionada = dgvMarcas.SelectedRows[0].DataBoundItem as Marca;
            if (marcaSeleccionada == null) return;

            using (var form = new FrmEditarMarca(marcaSeleccionada))
            {
                if (form.ShowDialog() == DialogResult.OK && form.MarcaEditada != null)
                {
                    try
                    {
                        using (var connection = Conexion.GetConnection())
                        {
                            connection.Open();

                            // Verificar si el nuevo nombre ya existe (excluyendo la marca actual)
                            string checkQuery = "SELECT COUNT(*) FROM Marcas WHERE Nombre = @Nombre AND IdMarca != @IdMarca";
                            using (var checkCommand = new SqliteCommand(checkQuery, connection))
                            {
                                checkCommand.Parameters.AddWithValue("@Nombre", form.MarcaEditada.Nombre.Trim());
                                checkCommand.Parameters.AddWithValue("@IdMarca", form.MarcaEditada.IdMarca);
                                long existe = (long)checkCommand.ExecuteScalar();

                                if (existe > 0)
                                {
                                    MessageBox.Show("Ya existe otra marca con ese nombre.", "Advertencia",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Actualizar marca
                            string updateQuery = "UPDATE Marcas SET Nombre = @Nombre, Descripcion = @Descripcion WHERE IdMarca = @IdMarca";
                            using (var command = new SqliteCommand(updateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@IdMarca", form.MarcaEditada.IdMarca);
                                command.Parameters.AddWithValue("@Nombre", form.MarcaEditada.Nombre.Trim());
                                command.Parameters.AddWithValue("@Descripcion",
                                    string.IsNullOrWhiteSpace(form.MarcaEditada.Descripcion) ?
                                    DBNull.Value : form.MarcaEditada.Descripcion.Trim());

                                int affected = command.ExecuteNonQuery();

                                if (affected > 0)
                                {
                                    MessageBox.Show("Marca modificada correctamente", "Éxito",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    CargarMarcas();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al modificar marca: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una marca para eliminar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var marcaSeleccionada = dgvMarcas.SelectedRows[0].DataBoundItem as Marca;
            if (marcaSeleccionada == null) return;

            // Verificar si la marca está siendo usada en productos
            if (MarcaTieneProductos(marcaSeleccionada.IdMarca))
            {
                MessageBox.Show("No se puede eliminar la marca porque tiene productos asociados.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmacion = MessageBox.Show(
                $"¿Está seguro que desea eliminar la marca '{marcaSeleccionada.Nombre}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();
                        string query = "DELETE FROM Marcas WHERE IdMarca = @IdMarca";

                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IdMarca", marcaSeleccionada.IdMarca);
                            int affected = command.ExecuteNonQuery();

                            if (affected > 0)
                            {
                                MessageBox.Show("Marca eliminada correctamente", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                CargarMarcas();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar marca: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool MarcaTieneProductos(int idMarca)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Productos WHERE IdMarca = @IdMarca";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdMarca", idMarca);
                        long count = (long)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception)
            {
                return true; // Por seguridad, asumir que tiene productos si hay error
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvMarcas_SelectionChanged(object sender, EventArgs e)
        {
            bool haySeleccion = dgvMarcas.SelectedRows.Count > 0;
            btnModificar.Enabled = haySeleccion;
            btnEliminar.Enabled = haySeleccion;
        }
    }

    public class Marca
    {
        public int IdMarca { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}