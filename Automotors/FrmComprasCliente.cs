using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmComprasCliente : Form
    {
        private int idCliente;
        private string nombreCliente;

        public FrmComprasCliente(int idCliente, string nombreCliente)
        {
            InitializeComponent();
            this.idCliente = idCliente;
            this.nombreCliente = nombreCliente;
            this.Text = $"Compras de {nombreCliente}";
        }

        private void FrmComprasCliente_Load(object sender, EventArgs e)
        {
            lblCliente.Text = $"Compras del cliente: {nombreCliente}";
            CargarCompras();
        }

        private void CargarCompras()
        {
            try
            {
                string query = @"SELECT v.idVenta as IdVenta, v.fecha as Fecha, 
                (SELECT SUM(dv.cantidad * dv.precioUnitario) 
                 FROM DetalleVentas dv 
                 WHERE dv.idVenta = v.idVenta) as Total,
                u.Nombre + ' ' + u.Apellido as Vendedor
         FROM Ventas v
         INNER JOIN Usuarios u ON v.idUsuario = u.IdUsuario
         WHERE v.idCliente = @IdCliente
         ORDER BY v.fecha DESC";

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            dgvCompras.DataSource = dt;
                            ConfigurarGridCompras();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar compras: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGridCompras()
        {
            dgvCompras.AutoGenerateColumns = false;
            dgvCompras.Columns.Clear();

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "IdVenta",
                HeaderText = "N° Venta",
                Name = "IdVenta",
                ReadOnly = true,
                Width = 80
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                Name = "Fecha",
                Width = 120
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Total",
                HeaderText = "Total",
                Name = "Total",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "C2" }
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Vendedor",
                HeaderText = "Vendedor",
                Name = "Vendedor",
                Width = 150
            });
        }

        private void dgvCompras_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCompras.SelectedRows.Count > 0 && dgvCompras.SelectedRows[0].Cells["IdVenta"].Value != DBNull.Value)
            {
                int idVenta = Convert.ToInt32(dgvCompras.SelectedRows[0].Cells["IdVenta"].Value);
                CargarDetalleVenta(idVenta);
            }
        }

        private void CargarDetalleVenta(int idVenta)
        {
            try
            {
                string query = @"SELECT p.Modelo as Producto, p.descripcion as Descripcion, p.Anio as Anio,
                                dv.cantidad as Cantidad, dv.precioUnitario as PrecioUnit, 
                                (dv.cantidad * dv.precioUnitario) as Subtotal
                         FROM DetalleVentas dv
                         INNER JOIN Productos p ON dv.idProducto = p.idProducto
                         WHERE dv.idVenta = @IdVenta";

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@idVenta", idVenta);
                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            dgvDetalle.DataSource = dt;
                            ConfigurarGridDetalle();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalle de venta: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGridDetalle()
        {
            dgvDetalle.AutoGenerateColumns = false;
            dgvDetalle.Columns.Clear();

            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Producto",
                HeaderText = "Producto",
                Name = "Producto",
                ReadOnly = true,
                Width = 150
            });

            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Descripcion",
                HeaderText = "Descripción",
                Name = "Descripcion",
                ReadOnly = true,
                Width = 200
            });

            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Anio",
                HeaderText = "Año",
                Name = "Anio",
                ReadOnly = true,
                Width = 60
            });

            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Cantidad",
                HeaderText = "Cant.",
                Name = "Cantidad",
                ReadOnly = true,
                Width = 50
            });

            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "PrecioUnit",
                HeaderText = "Precio Unit.",
                Name = "PrecioUnit",
                ReadOnly = true,
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "C2" }
            });

            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Subtotal",
                HeaderText = "Subtotal",
                Name = "Subtotal",
                ReadOnly = true,
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "C2" }
            });
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}