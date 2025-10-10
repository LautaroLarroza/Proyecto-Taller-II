using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

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
            CargarServicios();
        }

        private void InicializarFormulario()
        {
            dtpFecha.Value = DateTime.Now;

            // Cuando no hay producto seleccionado, bloqueo la cantidad
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

                    // Clientes
                    string queryClientes = "SELECT idCliente, nombre + ' ' + apellido as NombreCompleto FROM Clientes ORDER BY apellido, nombre";
                    using (var cmd = new SqlCommand(queryClientes, connection))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        DataTable dtClientes = new DataTable();
                        da.Fill(dtClientes);
                        cboCliente.DisplayMember = "NombreCompleto";
                        cboCliente.ValueMember = "idCliente";
                        cboCliente.DataSource = dtClientes;
                        cboCliente.SelectedIndex = -1;
                    }

                    // Productos (cargo todos, el control de stock lo hago al seleccionar)
                    string queryProductos = @"SELECT p.idProducto, 
                                                     m.nombre + ' ' + p.Modelo + ' (' + CAST(p.Anio AS VARCHAR) + ')' as Descripcion,
                                                     p.precio, p.stock
                                              FROM Productos p
                                              INNER JOIN Marcas m ON p.idMarca = m.idMarca
                                              WHERE p.Estado = 1
                                              ORDER BY m.nombre, p.Modelo";
                    using (var cmd = new SqlCommand(queryProductos, connection))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        DataTable dtProductos = new DataTable();
                        da.Fill(dtProductos);
                        cboProducto.DisplayMember = "Descripcion";
                        cboProducto.ValueMember = "idProducto";
                        cboProducto.DataSource = dtProductos;
                        cboProducto.SelectedIndex = -1;
                    }

                    // Vendedores
                    string queryVendedores = @"SELECT u.idUsuario, u.nombre + ' ' + u.Apellido as NombreCompleto 
                          FROM Usuarios u
                          INNER JOIN Roles r ON u.idRol = r.idRol
                          WHERE u.Estado = 1 
                          AND (r.nombre LIKE '%Vendedor%')
                          ORDER BY u.Apellido, u.nombre";
                    using (var cmd = new SqlCommand(queryVendedores, connection))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        DataTable dtVendedores = new DataTable();
                        da.Fill(dtVendedores);
                        cboVendedor.DisplayMember = "NombreCompleto";
                        cboVendedor.ValueMember = "idUsuario";
                        cboVendedor.DataSource = dtVendedores;
                        cboVendedor.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarServicios()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT IdServicio, Nombre, Precio FROM Servicios WHERE Estado = 1 ORDER BY Nombre";
                    using (var cmd = new SqlCommand(query, connection))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cboServicio.DisplayMember = "Nombre";
                        cboServicio.ValueMember = "IdServicio";
                        cboServicio.DataSource = dt;
                        cboServicio.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cuando seleccionan un producto
        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Desmarco servicio si había
                cboServicio.SelectedIndex = -1;

                if (cboProducto.SelectedValue == null)
                {
                    // Sin producto seleccionado, bloqueo cantidad y precio
                    txtPrecioUnitario.Text = "0";
                    lblStockDisp.Text = "Stock disponible: 0";
                    nudCantidad.Enabled = false;
                    nudCantidad.Minimum = 0;
                    nudCantidad.Value = 0;
                    nudCantidad.Maximum = 0;
                    RecalcularTotal(null, null);
                    return;
                }

                int idProducto = Convert.ToInt32(cboProducto.SelectedValue);

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("SELECT precio, stock FROM Productos WHERE idProducto = @id", connection))
                    {
                        cmd.Parameters.AddWithValue("@id", idProducto);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal precio = reader.IsDBNull(0) ? 0m : reader.GetDecimal(0);
                                int stock = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);

                                txtPrecioUnitario.Text = precio.ToString("F2");
                                lblStockDisp.Text = $"Stock disponible: {stock}";

                                if (stock <= 0)
                                {
                                    // Bloqueo cantidad y aviso al usuario
                                    nudCantidad.Enabled = false;
                                    nudCantidad.Minimum = 0;
                                    nudCantidad.Value = 0;
                                    nudCantidad.Maximum = 0;
                                    MessageBox.Show("Este producto no tiene stock disponible.", "Sin stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    nudCantidad.Enabled = true;
                                    nudCantidad.Minimum = 1;
                                    // Limito el máximo al stock actual para evitar vender más de lo que hay
                                    nudCantidad.Maximum = stock;
                                    nudCantidad.Value = 1;
                                }
                            }
                        }
                    }
                }

                RecalcularTotal(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cuando seleccionan un servicio
        private void cboServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Desmarco producto si había
                cboProducto.SelectedIndex = -1;

                if (cboServicio.SelectedValue == null)
                {
                    txtPrecioUnitario.Text = "0";
                    lblStockDisp.Text = "Servicio";
                    nudCantidad.Enabled = false;
                    nudCantidad.Minimum = 0;
                    nudCantidad.Value = 0;
                    nudCantidad.Maximum = 0;
                    RecalcularTotal(null, null);
                    return;
                }

                int idServicio = Convert.ToInt32(cboServicio.SelectedValue);
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("SELECT Precio FROM Servicios WHERE IdServicio = @id", connection))
                    {
                        cmd.Parameters.AddWithValue("@id", idServicio);
                        object result = cmd.ExecuteScalar();
                        decimal precio = result == null ? 0m : Convert.ToDecimal(result);

                        txtPrecioUnitario.Text = precio.ToString("F2");
                        lblStockDisp.Text = "Servicio (sin stock)";

                        // Para servicios dejo la cantidad en 1 (no aplica stock)
                        nudCantidad.Enabled = true;
                        nudCantidad.Minimum = 1;
                        nudCantidad.Maximum = 1;
                        nudCantidad.Value = 1;
                    }
                }

                RecalcularTotal(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar servicio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RecalcularTotal(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtPrecioUnitario.Text, out decimal precio))
                {
                    txtTotal.Text = "0";
                    return;
                }

                int cantidad = (int)nudCantidad.Value;
                decimal subtotal = precio * cantidad;
                txtTotal.Text = subtotal.ToString("F2");
            }
            catch
            {
                txtTotal.Text = "0";
            }
        }

        private bool ValidarVenta()
        {
            if (cboCliente.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un cliente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCliente.Focus();
                return false;
            }

            if (cboVendedor.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un vendedor", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboVendedor.Focus();
                return false;
            }

            if (cboProducto.SelectedValue == null && cboServicio.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un producto o un servicio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboProducto.Focus();
                return false;
            }

            if (nudCantidad.Value < 1)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudCantidad.Focus();
                return false;
            }

            // Verifico stock si aplicable
            if (cboProducto.SelectedValue != null)
            {
                int stockActual = 0;
                try
                {
                    using (var connection = Conexion.GetConnection())
                    {
                        connection.Open();
                        using (var cmd = new SqlCommand("SELECT stock FROM Productos WHERE idProducto = @id", connection))
                        {
                            cmd.Parameters.AddWithValue("@id", cboProducto.SelectedValue);
                            object o = cmd.ExecuteScalar();
                            stockActual = o == null ? 0 : Convert.ToInt32(o);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if ((int)nudCantidad.Value > stockActual)
                {
                    MessageBox.Show($"Stock insuficiente. Solo hay {stockActual} unidades disponibles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarVenta()) return;

            try
            {
                // Mostrar previsualización antes de guardar
                var result = MessageBox.Show("¿Desea ver la previsualización de la factura antes de guardar?",
                    "Previsualización", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel) return;

                if (result == DialogResult.Yes)
                {
                    MostrarPrevisualizacionFactura();

                    // Preguntar si desea guardar después de ver la previsualización
                    var confirmar = MessageBox.Show("¿Desea guardar esta venta?",
                        "Confirmar Guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmar != DialogResult.Yes) return;
                }

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();
                    using (var trans = connection.BeginTransaction())
                    {
                        try
                        {
                            // Insert venta
                            string queryVenta = @"INSERT INTO Ventas (fecha, idCliente, idUsuario, FormaPago)
                                          VALUES (@Fecha, @IdCliente, @IdUsuario, @Forma);
                                          SELECT SCOPE_IDENTITY();";
                            using (var cmd = new SqlCommand(queryVenta, connection, trans))
                            {
                                cmd.Parameters.AddWithValue("@Fecha", dtpFecha.Value);
                                cmd.Parameters.AddWithValue("@IdCliente", cboCliente.SelectedValue);
                                cmd.Parameters.AddWithValue("@IdUsuario", cboVendedor.SelectedValue);
                                cmd.Parameters.AddWithValue("@Forma", cboFormaPago.SelectedItem?.ToString() ?? "");
                                int idVenta = Convert.ToInt32(cmd.ExecuteScalar());

                                // Detalles de productos
                                if (cboProducto.SelectedValue != null)
                                {
                                    using (var det = new SqlCommand(@"INSERT INTO DetalleVentas (idVenta, idProducto, cantidad, precioUnitario)
                                                            VALUES (@v,@p,@c,@pu)", connection, trans))
                                    {
                                        det.Parameters.AddWithValue("@v", idVenta);
                                        det.Parameters.AddWithValue("@p", cboProducto.SelectedValue);
                                        det.Parameters.AddWithValue("@c", (int)nudCantidad.Value);
                                        det.Parameters.AddWithValue("@pu", decimal.Parse(txtPrecioUnitario.Text));
                                        det.ExecuteNonQuery();
                                    }

                                    // Actualizo stock
                                    using (var upd = new SqlCommand("UPDATE Productos SET stock = stock - @c WHERE idProducto = @id", connection, trans))
                                    {
                                        upd.Parameters.AddWithValue("@c", (int)nudCantidad.Value);
                                        upd.Parameters.AddWithValue("@id", cboProducto.SelectedValue);
                                        upd.ExecuteNonQuery();
                                    }
                                }
                                // Detalles de servicios
                                else if (cboServicio.SelectedValue != null)
                                {
                                    using (var detS = new SqlCommand(@"INSERT INTO DetalleServicios (IdVenta, IdServicio, Precio)
                                                              VALUES (@v,@s,@p)", connection, trans))
                                    {
                                        detS.Parameters.AddWithValue("@v", idVenta);
                                        detS.Parameters.AddWithValue("@s", cboServicio.SelectedValue);
                                        detS.Parameters.AddWithValue("@p", decimal.Parse(txtPrecioUnitario.Text));
                                        detS.ExecuteNonQuery();
                                    }
                                }
                            }

                            trans.Commit();

                            MessageBox.Show("✅ Venta registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Preguntar si desea imprimir
                            var imprimir = MessageBox.Show("¿Desea imprimir la factura?",
                                "Imprimir Factura", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (imprimir == DialogResult.Yes)
                            {
                                ImprimirFactura();
                            }

                            LimpiarFormulario();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            // CORRECCIÓN: Pasar la excepción original en lugar de throw vacío
                            throw new Exception("Error al guardar la venta en la base de datos: " + ex.Message, ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarPrevisualizacionFactura()
        {
            try
            {
                Form previsualizacion = new Form();
                previsualizacion.Text = "Previsualización de Factura";
                previsualizacion.Size = new Size(500, 600);
                previsualizacion.StartPosition = FormStartPosition.CenterScreen;
                previsualizacion.FormBorderStyle = FormBorderStyle.FixedDialog;
                previsualizacion.MaximizeBox = false;
                previsualizacion.MinimizeBox = false;

                Panel panel = new Panel();
                panel.Dock = DockStyle.Fill;
                panel.AutoScroll = true;
                panel.BackColor = Color.White;
                panel.Padding = new Padding(20);

                // Contenido de la factura
                string item = cboProducto.SelectedValue != null ? cboProducto.Text : cboServicio.Text;
                string tipoItem = cboProducto.SelectedValue != null ? "Producto" : "Servicio";

                string facturaContent = $@"
FACTURA DE VENTA

Cliente: {cboCliente.Text}
Vendedor: {cboVendedor.Text}
Fecha: {dtpFecha.Value:dd/MM/yyyy}
Forma de pago: {cboFormaPago.Text}

Detalle:
{tipoItem}: {item}
Cantidad: {nudCantidad.Value}
Precio unitario: ${decimal.Parse(txtPrecioUnitario.Text):F2}
Total: ${decimal.Parse(txtTotal.Text):F2}

Gracias por su compra.
";

                TextBox txtFactura = new TextBox();
                txtFactura.Multiline = true;
                txtFactura.ReadOnly = true;
                txtFactura.Dock = DockStyle.Fill;
                txtFactura.Text = facturaContent;
                txtFactura.Font = new Font("Consolas", 10);
                txtFactura.BackColor = Color.White;
                txtFactura.BorderStyle = BorderStyle.None;
                txtFactura.ScrollBars = ScrollBars.Vertical;

                panel.Controls.Add(txtFactura);
                previsualizacion.Controls.Add(panel);

                previsualizacion.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar previsualización: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirFactura()
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += (s, ev) =>
                {
                    float y = 20;
                    Font title = new Font("Arial", 14, FontStyle.Bold);
                    Font f = new Font("Arial", 10);

                    ev.Graphics.DrawString("FACTURA DE VENTA", title, Brushes.Black, 200, y);
                    y += 40;

                    ev.Graphics.DrawString($"Cliente: {cboCliente.Text}", f, Brushes.Black, 50, y); y += 20;
                    ev.Graphics.DrawString($"Vendedor: {cboVendedor.Text}", f, Brushes.Black, 50, y); y += 20;
                    ev.Graphics.DrawString($"Fecha: {dtpFecha.Value:dd/MM/yyyy}", f, Brushes.Black, 50, y); y += 20;
                    ev.Graphics.DrawString($"Forma de pago: {cboFormaPago.Text}", f, Brushes.Black, 50, y); y += 30;

                    string item = cboProducto.SelectedValue != null ? cboProducto.Text : cboServicio.Text;
                    string tipoItem = cboProducto.SelectedValue != null ? "Producto" : "Servicio";

                    ev.Graphics.DrawString($"{tipoItem}: {item}", f, Brushes.Black, 50, y); y += 20;
                    ev.Graphics.DrawString($"Cantidad: {nudCantidad.Value}", f, Brushes.Black, 50, y); y += 20;
                    ev.Graphics.DrawString($"Precio unitario: ${decimal.Parse(txtPrecioUnitario.Text):F2}", f, Brushes.Black, 50, y); y += 20;
                    ev.Graphics.DrawString($"Total: ${decimal.Parse(txtTotal.Text):F2}", new Font("Arial", 11, FontStyle.Bold), Brushes.Black, 50, y); y += 30;

                    ev.Graphics.DrawString("Gracias por su compra.", f, Brushes.Black, 50, y);
                };

                PrintPreviewDialog ppd = new PrintPreviewDialog();
                ppd.Document = pd;
                ppd.Width = 800;
                ppd.Height = 600;
                ppd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar la factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGestionarServicios_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir formulario de servicios (debe existir en el proyecto)
                FrmServicios frm = new FrmServicios();
                frm.ShowDialog();
                // Recargo los servicios por si se agregaron/editaron
                CargarServicios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir gestión de servicios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimirFactura_Click(object sender, EventArgs e)
        {
            if (ValidarVenta())
            {
                MostrarPrevisualizacionFactura();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("¿Cancelar la venta actual?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
                LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            cboCliente.SelectedIndex = -1;
            cboVendedor.SelectedIndex = -1;
            cboProducto.SelectedIndex = -1;
            cboServicio.SelectedIndex = -1;
            txtPrecioUnitario.Text = "0";
            txtTotal.Text = "0";
            nudCantidad.Value = 0;
            nudCantidad.Minimum = 0;
            nudCantidad.Maximum = 0;
            lblStockDisp.Text = "Stock disponible: —";
            cboFormaPago.SelectedIndex = 0;
        }
    }
}