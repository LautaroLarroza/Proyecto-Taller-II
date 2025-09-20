using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmUsuarios : Form
    {
        private Panel panelContenedor;

        public FrmUsuarios(Panel panel)
        {
            InitializeComponent();
            panelContenedor = panel;
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    MessageBox.Show("✅ Conectado a la base de datos con éxito.");

                    // ✅ CONSULTA CORREGIDA según tu diseño de base de datos
                    // En FrmUsuarios_Load, actualiza la consulta:
                    SqlDataAdapter da = new SqlDataAdapter(@"
    SELECT u.IdUsuario, u.Nombre, u.Apellido, u.DNI, u.Email, 
           r.Nombre as Rol, 
           CASE WHEN u.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado
    FROM Usuarios u
    INNER JOIN Roles r ON u.IdRol = r.IdRol", connection);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al conectar: " + ex.Message);
            }       

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = true;
        }

        private void BEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un usuario para eliminar.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "¿Seguro que desea eliminar el usuario seleccionado?",
                "Confirmar",
                MessageBoxButtons.YesNo
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IdUsuario"].Value);

                    // ✅ CORREGIDO: Usar Conexion estática directamente
                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE IdUsuario = @id", connection);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Usuario eliminado correctamente.");

                    // Refrescar la tabla
                    FrmUsuarios frm = new FrmUsuarios(panelContenedor);
                    frm.TopLevel = false;
                    frm.Dock = DockStyle.Fill;
                    panelContenedor.Controls.Add(frm);
                    frm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error al eliminar: " + ex.Message);
                }
            }
        }

        // Los otros métodos deben mantenerse aquí también:
        private void BAgregar_Click(object sender, EventArgs e)
        {
            panelContenedor.Controls.Clear();
            FrmAgregarUsuario frmAgregar = new FrmAgregarUsuario(this, panelContenedor);
            frmAgregar.ModificarEnCurso = false;
            frmAgregar.TopLevel = false;
            frmAgregar.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(frmAgregar);
            frmAgregar.Show();
        }

        private void BModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un usuario para modificar.");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IdUsuario"].Value);
            string nombre = dataGridView1.SelectedRows[0].Cells["Nombre"].Value.ToString();
            string apellido = dataGridView1.SelectedRows[0].Cells["Apellido"].Value.ToString();
            string usuario = dataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
            string rol = dataGridView1.SelectedRows[0].Cells["Rol"].Value.ToString();

            panelContenedor.Controls.Clear();
            FrmAgregarUsuario frmModificar = new FrmAgregarUsuario(this, panelContenedor)
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = usuario,
                Rol = rol,
                ModificarEnCurso = true,
                UsuarioId = id
            };

            frmModificar.TopLevel = false;
            frmModificar.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(frmModificar);
            frmModificar.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // opcional
        }
    }
}