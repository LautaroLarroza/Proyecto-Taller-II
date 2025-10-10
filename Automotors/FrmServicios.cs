using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmServicios : Form
    {
        private int? idServicioActual = null;

        public FrmServicios(int? idServicio = null)
        {
            InitializeComponent();
            idServicioActual = idServicio;
        }

        private void FrmServicios_Load(object sender, EventArgs e)
        {
            if (idServicioActual.HasValue)
                CargarServicio(idServicioActual.Value);
        }

        private void CargarServicio(int id)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Nombre, Descripcion, Precio, Estado FROM Servicios WHERE IdServicio = @id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtDescripcion.Text = reader["Descripcion"].ToString();
                            nudPrecio.Value = Convert.ToDecimal(reader["Precio"]);
                            chkActivo.Checked = Convert.ToBoolean(reader["Estado"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese un nombre para el servicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query;

                    if (idServicioActual.HasValue)
                    {
                        query = @"UPDATE Servicios
                                  SET Nombre=@n, Descripcion=@d, Precio=@p, Estado=@e
                                  WHERE IdServicio=@id";
                    }
                    else
                    {
                        query = @"INSERT INTO Servicios (Nombre, Descripcion, Precio, Estado)
                                  VALUES (@n, @d, @p, @e)";
                    }

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@n", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@d", txtDescripcion.Text.Trim());
                    cmd.Parameters.AddWithValue("@p", nudPrecio.Value);
                    cmd.Parameters.AddWithValue("@e", chkActivo.Checked);
                    if (idServicioActual.HasValue)
                        cmd.Parameters.AddWithValue("@id", idServicioActual.Value);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("✅ Servicio guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar servicio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
