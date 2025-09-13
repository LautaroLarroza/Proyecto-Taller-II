using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                Conexion con = new Conexion();
                using (var connection = con.GetConnection())
                {
                    connection.Open();
                    MessageBox.Show("✅ Conectado a la base de datos con éxito.");

                    // Cargar usuarios reales desde SQL
                    SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Nombre, Apellido, Usuario, Rol FROM Usuarios", connection);
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
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                    Conexion con = new Conexion();
                    using (var connection = con.GetConnection())
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE Id = @id", connection);
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

        private void BModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un usuario para modificar.");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
            string nombre = dataGridView1.SelectedRows[0].Cells["Nombre"].Value.ToString();
            string apellido = dataGridView1.SelectedRows[0].Cells["Apellido"].Value.ToString();
            string usuario = dataGridView1.SelectedRows[0].Cells["Usuario"].Value.ToString();
            string rol = dataGridView1.SelectedRows[0].Cells["Rol"].Value.ToString();

            panelContenedor.Controls.Clear();
            FrmAgregarUsuario frmModificar = new FrmAgregarUsuario(this, panelContenedor)
            {
                Nombre = nombre,
                Apellido = apellido,
                Usuario = usuario,
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
