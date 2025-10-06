using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Automotors
{
    public partial class FrmVentas : Form
    {
        private DataTable dtDetalleVenta;
        private decimal totalVenta = 0;

        public FrmVentas()
        {
            InitializeComponent();
            this.AutoScroll = true;
            this.MinimumSize = new System.Drawing.Size(1000, 540);
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            InicializarFormulario();
            CargarCombos();
        }

        private void InicializarFormulario()
        {
            dtpFecha.Value = DateTime.Now;

            nudCantidad.Minimum = 0; 
            nudCantidad.Value = 0;   
            nudCantidad.Maximum = 0; 

            if (cboFormaPago.Items.Count == 0)
            {
                cboFormaPago.Items.AddRange(new object[] { "Efectivo", "Transferencia", "Tarjeta", "Otro" });
                cboFormaPago.SelectedIndex = 0;
            }
        }

        private void CargarCombos()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // Cargar clientes
                    string queryClientes = "SELECT IdCliente, Nombre + ' ' + Apellido as NombreCompleto FROM Clientes ORDER BY Apellido, Nombre";
                    using (var cmd = new SqlCommand(queryClientes, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cboCliente.Items.Clear();
                        cboCliente.Items.Add(new { Id = 0, Nombre = "-- Seleccionar Cliente --" });

                        while (reader.Read())
                        {
                            cboCliente.Items.Add(new
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1)
                            });
                        }
                        cboCliente.DisplayMember = "Nombre";
                        cboCliente.ValueMember = "Id";
                        if (cboCliente.Items.Count > 0)
                            cboCliente.SelectedIndex = 0;
                    }

                    // Cargar productos activos
                    string queryProductos = @"SELECT p.IdProducto, 
                                                     m.Nombre + ' ' + p.Modelo + ' (' + CAST(p.Anio AS VARCHAR) + ')' as Descripcion,
                                                     p.Precio, p.Stock
                                              FROM Productos p
                                              INNER JOIN Marcas m ON p.IdMarca = m.IdMarca
                                              WHERE p.Estado = 1 AND p.Stock > 0
                                              ORDER BY m.Nombre, p.Modelo";

                    using (var cmd = new SqlCommand(queryProductos, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cboProducto.Items.Clear();
                        cboProducto.Items.Add(new { Id = 0, Descripcion = "-- Seleccionar Producto --", Precio = 0m, Stock = 0 });

                        while (reader.Read())
                        {
                            cboProducto.Items.Add(new
                            {
                                Id = reader.GetInt32(0),
                                Descripcion = reader.GetString(1),
                                Precio = reader.GetDecimal(2),
                                Stock = reader.GetInt32(3)
                            });
                        }
                        cboProducto.DisplayMember = "Descripcion";
                        cboProducto.ValueMember = "Id";
                        if (cboProducto.Items.Count > 0)
                            cboProducto.SelectedIndex = 0;
                    }

                    // Cargar vendedores (solo usuarios con rol de vendedor)
                    string queryVendedores = @"SELECT u.IdUsuario, u.Nombre + ' ' + u.Apellido as NombreCompleto 
                          FROM Usuarios u
                          INNER JOIN Roles r ON u.IdRol = r.IdRol
                          WHERE u.Estado = 1 
                          AND (r.Nombre = 'Vendedor' OR r.Nombre LIKE '%Vendedor%' OR r.Nombre = 'VENDEDOR')
                          ORDER BY u.Apellido, u.Nombre";

                    using (var cmd = new SqlCommand(queryVendedores, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cboVendedor.Items.Clear();
                        cboVendedor.Items.Add(new { Id = 0, Nombre = "-- Seleccionar Vendedor --" });

                        while (reader.Read())
                        {
                            cboVendedor.Items.Add(new
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1)
                            });
                        }
                        cboVendedor.DisplayMember = "Nombre";
                        cboVendedor.ValueMember = "Id";
                        if (cboVendedor.Items.Count > 0)
                            cboVendedor.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProducto.SelectedItem != null && cboProducto.SelectedIndex > 0)
            {
                dynamic producto = cboProducto.SelectedItem;
                txtPrecioUnitario.Text = producto.Precio.ToString("F2");
                lblStockDisp.Text = $"Stock disponible: {producto.Stock}";

                // ✅ CONFIGURAR NUD CORRECTAMENTE
                nudCantidad.Maximum = producto.Stock;
                nudCantidad.Minimum = 1; // Solo permitir mínimo 1 cuando hay producto seleccionado
                nudCantidad.Value = 1;   // Poner en 1 cuando se selecciona producto

                RecalcularTotal(sender, e);
            }
            else
            {
                txtPrecioUnitario.Text = "0";
                lblStockDisp.Text = "Stock disponible: 0";

                // ✅ CONFIGURAR NUD PARA ESTADO SIN PRODUCTO
                nudCantidad.Minimum = 0; // Permitir 0 cuando no hay producto
                nudCantidad.Value = 0;   // Poner en 0 cuando no hay producto seleccionado
                nudCantidad.Maximum = 0; // No permitir valores mayores a 0

                RecalcularTotal(sender, e);
            }
        }

        private void RecalcularTotal(object sender, EventArgs e)
        {
            try
            {
                if (cboProducto.SelectedIndex > 0 && nudCantidad.Value >= 1) // ✅ CAMBIADO A >= 1
                {
                    decimal precio = decimal.Parse(txtPrecioUnitario.Text);
                    decimal subtotal = precio * (int)nudCantidad.Value;
                    txtTotal.Text = subtotal.ToString("F2");
                }
                else
                {
                    txtTotal.Text = "0";
                }
            }
            catch
            {
                txtTotal.Text = "0";
            }
        }

        private void txtObs_TextChanged(object sender, EventArgs e)
        {
            // Opcional: Validaciones de observaciones si necesitas
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarVenta())
                return;

            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Insertar venta
                            string queryVenta = @"INSERT INTO Ventas (Fecha, IdCliente, IdUsuario) 
                                                 VALUES (@Fecha, @IdCliente, @IdUsuario);
                                                 SELECT SCOPE_IDENTITY();";

                            int idVenta;
                            using (var cmd = new SqlCommand(queryVenta, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Fecha", dtpFecha.Value);
                                cmd.Parameters.AddWithValue("@IdCliente", ((dynamic)cboCliente.SelectedItem).Id);
                                cmd.Parameters.AddWithValue("@IdUsuario", ((dynamic)cboVendedor.SelectedItem).Id);
                                idVenta = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // Insertar detalle de venta y actualizar stock
                            string queryDetalle = @"INSERT INTO DetalleVentas (IdVenta, IdProducto, Cantidad, PrecioUnitario) 
                                                   VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario)";

                            string queryActualizarStock = "UPDATE Productos SET Stock = Stock - @Cantidad WHERE IdProducto = @IdProducto";

                            // Insertar el producto actual como detalle
                            if (cboProducto.SelectedIndex > 0)
                            {
                                dynamic producto = cboProducto.SelectedItem;
                                int cantidad = (int)nudCantidad.Value;
                                decimal precio = decimal.Parse(txtPrecioUnitario.Text);

                                // Insertar detalle
                                using (var cmd = new SqlCommand(queryDetalle, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@IdVenta", idVenta);
                                    cmd.Parameters.AddWithValue("@IdProducto", producto.Id);
                                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                                    cmd.Parameters.AddWithValue("@PrecioUnitario", precio);
                                    cmd.ExecuteNonQuery();
                                }

                                // Actualizar stock
                                using (var cmd = new SqlCommand(queryActualizarStock, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                                    cmd.Parameters.AddWithValue("@IdProducto", producto.Id);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();

                            MessageBox.Show($"✅ Venta registrada correctamente\nN° de Venta: {idVenta}\nTotal: {txtTotal.Text}",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LimpiarFormulario();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception($"Error al procesar la venta: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar venta: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarVenta()
        {
            if (cboCliente.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un cliente", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCliente.Focus();
                return false;
            }

            if (cboVendedor.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un vendedor", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboVendedor.Focus();
                return false;
            }

            if (cboProducto.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un producto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboProducto.Focus();
                return false;
            }

            // ✅ VALIDACIÓN CORREGIDA - Ahora Minimum es 1 cuando hay producto
            if (nudCantidad.Value < 1) // Cambiado de <= 0 a < 1
            {
                MessageBox.Show("La cantidad debe ser mayor a 0", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudCantidad.Focus();
                return false;
            }

            // Validar stock
            if (cboProducto.SelectedIndex > 0)
            {
                dynamic producto = cboProducto.SelectedItem;
                if (nudCantidad.Value > producto.Stock)
                {
                    MessageBox.Show($"Stock insuficiente. Solo hay {producto.Stock} unidades disponibles",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void LimpiarFormulario()
        {
            cboCliente.SelectedIndex = 0;
            cboVendedor.SelectedIndex = 0;
            cboProducto.SelectedIndex = 0;
            txtPrecioUnitario.Text = "0";
            txtTotal.Text = "0";

            // ✅ CONFIGURAR NUD AL LIMPIAR
            nudCantidad.Minimum = 0;
            nudCantidad.Value = 0;
            nudCantidad.Maximum = 0;

            lblStockDisp.Text = "Stock disponible: —";
            txtObs.Text = "";
            dtpFecha.Value = DateTime.Now;
            cboFormaPago.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("¿Cancelar la venta actual?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                LimpiarFormulario();
            }
        }

        private void tlp_Paint(object sender, PaintEventArgs e)
        {
            // Opcional: Personalizar el dibujo del TableLayoutPanel si lo necesitas
        }

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            RecalcularTotal(sender, e);
        }
    }
}