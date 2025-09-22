using System;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmClienteDetalle : Form
    {
        private int idCliente;
        private bool esNuevo;

        public FrmClienteDetalle()
        {
            InitializeComponent();
            esNuevo = true;
            this.Text = "Nuevo Cliente";
        }

        public FrmClienteDetalle(int idCliente)
        {
            InitializeComponent();
            this.idCliente = idCliente;
            esNuevo = false;
            this.Text = "Editar Cliente";
            CargarDatosCliente();
        }

        private void CargarDatosCliente()
        {
            try
            {
                string query = "SELECT * FROM Clientes WHERE IdCliente = @IdCliente";

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtNombre.Text = reader["Nombre"].ToString();
                                txtApellido.Text = reader["Apellido"].ToString();
                                txtDNI.Text = reader["DNI"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtTelefono.Text = reader["Telefono"].ToString();
                                txtDireccion.Text = reader["Direccion"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del cliente: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            try
            {
                if (esNuevo)
                {
                    string query = @"INSERT INTO Clientes (Nombre, Apellido, DNI, Email, Telefono, Direccion) 
                                     VALUES (@Nombre, @Apellido, @DNI, @Email, @Telefono, @Direccion)";

                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();
                        using (var cmd = new SqliteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                            cmd.Parameters.AddWithValue("@DNI", txtDNI.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                            cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Cliente creado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string query = @"UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, DNI = @DNI, 
                                    Email = @Email, Telefono = @Telefono, Direccion = @Direccion 
                                    WHERE IdCliente = @IdCliente";

                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();
                        using (var cmd = new SqliteCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                            cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                            cmd.Parameters.AddWithValue("@DNI", txtDNI.Text);
                            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                            cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                            cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Cliente actualizado correctamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cliente: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es obligatorio", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDNI.Text))
            {
                MessageBox.Show("El DNI es obligatorio", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDNI.Focus();
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