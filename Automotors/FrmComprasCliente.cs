using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
        }

        private void FrmComprasCliente_Load(object sender, EventArgs e)
        {
            this.Text = $"Compras de {nombreCliente}";
            lblCliente.Text = $"Compras del cliente: {nombreCliente}";
            CargarCompras();

            if (dgvCompras.Rows.Count > 0)
            {
                dgvCompras.Rows[0].Selected = true;
            }
        }

        private void CargarCompras()
        {
            try
            {
                string query = @"SELECT 
                    v.idVenta as IdVenta, 
                    CONVERT(VARCHAR, v.fecha, 103) + ' ' + CONVERT(VARCHAR, v.fecha, 108) as Fecha,
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
                string query = @"SELECT 
                    m.Nombre + ' ' + p.Modelo as Producto, 
                    p.Anio as Anio,
                    dv.cantidad as Cantidad, 
                    dv.precioUnitario as PrecioUnit, 
                    (dv.cantidad * dv.precioUnitario) as Subtotal
                 FROM DetalleVentas dv
                 INNER JOIN Productos p ON dv.idProducto = p.idProducto
                 INNER JOIN Marcas m ON p.idMarca = m.idMarca
                 WHERE dv.idVenta = @IdVenta";

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdVenta", idVenta);
                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            dgvDetalle.DataSource = dt;
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}