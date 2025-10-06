using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmGestionRoles : Form
    {
        public FrmGestionRoles()
        {
            InitializeComponent();
        }

        private void FrmGestionRoles_Load(object sender, EventArgs e)
        {
            CargarRoles();
        }

        private void CargarRoles()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT IdRol, Nombre FROM Roles ORDER BY Nombre";

                    using (var cmd = new SqlCommand(query, connection))
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvRoles.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre del rol:", "Agregar Rol");

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                try
                {
                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();

                        // Verificar si el rol ya existe
                        string checkQuery = "SELECT COUNT(*) FROM Roles WHERE Nombre = @Nombre";
                        using (var checkCmd = new SqlCommand(checkQuery, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@Nombre", nombre.Trim());
                            int existe = (int)checkCmd.ExecuteScalar();

                            if (existe > 0)
                            {
                                MessageBox.Show("El rol ya existe", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Insertar nuevo rol
                        string insertQuery = "INSERT INTO Roles (Nombre) VALUES (@Nombre)";
                        using (var cmd = new SqlCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", nombre.Trim());
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Rol agregado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarRoles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar rol: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un rol para eliminar", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idRol = Convert.ToInt32(dgvRoles.SelectedRows[0].Cells["IdRol"].Value);
            string nombre = dgvRoles.SelectedRows[0].Cells["Nombre"].Value.ToString();

            // Verificar si el rol está siendo usado
            if (RolTieneUsuarios(idRol))
            {
                MessageBox.Show("No se puede eliminar el rol porque tiene usuarios asignados", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"¿Está seguro que desea eliminar el rol '{nombre}'?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();
                        string query = "DELETE FROM Roles WHERE IdRol = @IdRol";

                        using (var cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@IdRol", idRol);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Rol eliminado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarRoles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar rol: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool RolTieneUsuarios(int idRol)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE IdRol = @IdRol";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdRol", idRol);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception)
            {
                return true; // Por seguridad, asumir que tiene usuarios si hay error
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}