using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace Automotors
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FrmReportes_Load(object? sender, EventArgs e)
        {
            cboReporte.Items.Clear();
            cboReporte.Items.AddRange(new object[]
            {
                "Ventas por fecha (detalle)",
                "Ventas por producto (resumen)",
                "Ventas por cliente",
                "Ventas por vendedor",
                "Stock de productos",
                "Productos más vendidos",
                "Clientes con más compras",
                "Listado de clientes",
                "Productos sin stock",
                "Ventas de servicios"
            });
            cboReporte.SelectedIndex = 0;

            dtpHasta.Value = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddDays(-30);
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            try
            {
                DataTable dt = cboReporte.SelectedIndex switch
                {
                    0 => ReporteVentasDetalle(dtpDesde.Value, dtpHasta.Value),
                    1 => ReporteVentasPorProducto(dtpDesde.Value, dtpHasta.Value),
                    2 => ReporteVentasPorCliente(dtpDesde.Value, dtpHasta.Value),
                    3 => ReporteVentasPorVendedor(dtpDesde.Value, dtpHasta.Value),
                    4 => ReporteStockProductos(),
                    5 => ReporteProductosMasVendidos(dtpDesde.Value, dtpHasta.Value),
                    6 => ReporteClientesConMasCompras(dtpDesde.Value, dtpHasta.Value),
                    7 => ReporteClientes(),
                    8 => ReporteProductosSinStock(),
                    9 => ReporteVentasDeServicios(dtpDesde.Value, dtpHasta.Value),
                    _ => new DataTable()
                };

                dgv.DataSource = dt;
                ActualizarResumen(dt);
                dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo ejecutar el reporte.\n\n" + ex.Message,
                    "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarResumen(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                lblResumen.Text = "Sin resultados.";
                return;
            }

            decimal total = 0;
            bool encontrado = false;

            foreach (DataColumn c in dt.Columns)
            {
                if (string.Equals(c.ColumnName, "Total", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.ColumnName, "Importe", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.ColumnName, "ImporteTotal", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.ColumnName, "Precio", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r[c] != DBNull.Value &&
                            decimal.TryParse(Convert.ToString(r[c]), NumberStyles.Any, CultureInfo.InvariantCulture, out var v))
                        {
                            total += v;
                        }
                    }
                    lblResumen.Text = $"Filas: {dt.Rows.Count:N0}   |   Suma {c.ColumnName}: {total:C2}";
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                lblResumen.Text = $"Filas: {dt.Rows.Count:N0}";
            }
        }

        private static DataTable ExecDataTable(string sql, Action<SqlCommand>? addParams = null)
        {
            using var cn = Conexion.GetConnection();
            if (cn.State != ConnectionState.Open) cn.Open();

            using var cmd = new SqlCommand(sql, cn);
            addParams?.Invoke(cmd);

            using var rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            var dt = new DataTable();
            dt.Load(rd);
            return dt;
        }

        // === REPORTES ===

        private DataTable ReporteVentasDetalle(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                -- Productos
                SELECT
                    v.idVenta AS NroVenta,
                    v.fecha AS Fecha,
                    c.nombre + ' ' + c.apellido AS Cliente,
                    c.dni AS DNI_Cliente,
                    u.nombre + ' ' + u.apellido AS Vendedor,
                    u.dni AS DNI_Vendedor,
                    'Producto' AS Tipo,
                    m.nombre AS Marca,
                    p.Modelo AS Modelo,
                    p.Anio AS Año,
                    dv.cantidad AS Cantidad,
                    dv.precioUnitario AS PrecioUnitario,
                    (dv.cantidad * dv.precioUnitario) AS Total
                FROM Ventas v
                INNER JOIN DetalleVentas dv ON dv.idVenta = v.idVenta
                INNER JOIN Productos p ON p.idProducto = dv.idProducto
                INNER JOIN Marcas m ON m.idMarca = p.idMarca
                INNER JOIN Clientes c ON c.idCliente = v.idCliente
                INNER JOIN Usuarios u ON u.idUsuario = v.idUsuario
                WHERE v.fecha BETWEEN @d AND @h

                UNION ALL

                -- Servicios
                SELECT
                    v.idVenta AS NroVenta,
                    v.fecha AS Fecha,
                    c.nombre + ' ' + c.apellido AS Cliente,
                    c.dni AS DNI_Cliente,
                    u.nombre + ' ' + u.apellido AS Vendedor,
                    u.dni AS DNI_Vendedor,
                    'Servicio' AS Tipo,
                    s.Nombre AS Marca,
                    s.Descripcion AS Modelo,
                    NULL AS Año,
                    1 AS Cantidad,
                    s.Precio AS PrecioUnitario,
                    s.Precio AS Total
                FROM Ventas v
                INNER JOIN DetalleServicios ds ON ds.IdVenta = v.idVenta
                INNER JOIN Servicios s ON s.IdServicio = ds.IdServicio
                INNER JOIN Clientes c ON c.idCliente = v.idCliente
                INNER JOIN Usuarios u ON u.idUsuario = v.idUsuario
                WHERE v.fecha BETWEEN @d AND @h

                ORDER BY Fecha ASC";

            return ExecDataTable(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@d", desde.Date);
                cmd.Parameters.AddWithValue("@h", hasta.Date.AddDays(1).AddSeconds(-1));
            });
        }

        private DataTable ReporteVentasPorProducto(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT
                    p.Modelo AS Producto,
                    m.nombre AS Marca,
                    SUM(dv.cantidad) AS CantidadVendida,
                    SUM(dv.cantidad * dv.precioUnitario) AS Importe
                FROM Ventas v
                INNER JOIN DetalleVentas dv ON dv.idVenta = v.idVenta
                INNERJOIN Productos p ON p.idProducto = dv.idProducto
                INNERJOIN Marcas m ON m.idMarca = p.idMarca
                WHERE v.fecha BETWEEN @d AND @h
                GROUP BY p.idProducto, p.Modelo, m.nombre
                ORDER BY Importe DESC";
            return ExecDataTable(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@d", desde);
                cmd.Parameters.AddWithValue("@h", hasta);
            });
        }

        private DataTable ReporteVentasPorCliente(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT
                    c.nombre + ' ' + c.apellido AS Cliente,
                    c.dni AS DNI,
                    COUNT(DISTINCT v.idVenta) AS CantidadVentas,
                    SUM(dv.cantidad) AS TotalProductos,
                    SUM(dv.cantidad * dv.precioUnitario) AS ImporteTotal
                FROM Ventas v
                INNER JOIN Clientes c ON c.idCliente = v.idCliente
                INNER JOIN DetalleVentas dv ON dv.idVenta = v.idVenta
                WHERE v.fecha BETWEEN @d AND @h
                GROUP BY c.idCliente, c.nombre, c.apellido, c.dni
                ORDER BY ImporteTotal DESC";
            return ExecDataTable(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@d", desde);
                cmd.Parameters.AddWithValue("@h", hasta);
            });
        }

        private DataTable ReporteVentasPorVendedor(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT
                    u.nombre + ' ' + u.apellido AS Vendedor,
                    u.dni AS DNI,
                    COUNT(DISTINCT v.idVenta) AS CantidadVentas,
                    SUM(dv.cantidad) AS TotalProductos,
                    SUM(dv.cantidad * dv.precioUnitario) AS ImporteTotal
                FROM Ventas v
                INNER JOIN Usuarios u ON u.idUsuario = v.idUsuario
                INNER JOIN DetalleVentas dv ON dv.idVenta = v.idVenta
                WHERE v.fecha BETWEEN @d AND @h
                GROUP BY u.idUsuario, u.nombre, u.apellido, u.dni
                ORDER BY ImporteTotal DESC";
            return ExecDataTable(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@d", desde);
                cmd.Parameters.AddWithValue("@h", hasta);
            });
        }

        private DataTable ReporteStockProductos()
        {
            const string sql = @"
                SELECT 
                    p.Modelo AS Producto,
                    m.nombre AS Marca,
                    p.descripcion AS Descripcion,
                    p.anio AS Año,
                    p.stock AS Stock,
                    p.precio AS Precio,
                    CASE WHEN p.estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado
                FROM Productos p
                INNER JOIN Marcas m ON p.idMarca = m.idMarca
                ORDER BY p.stock DESC, p.Modelo";
            return ExecDataTable(sql);
        }

        private DataTable ReporteProductosMasVendidos(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT TOP 10
                    p.Modelo AS Producto,
                    m.nombre AS Marca,
                    SUM(dv.cantidad) AS CantidadVendida,
                    SUM(dv.cantidad * dv.precioUnitario) AS ImporteTotal
                FROM DetalleVentas dv
                INNER JOIN Productos p ON p.idProducto = dv.idProducto
                INNER JOIN Marcas m ON m.idMarca = p.idMarca
                INNER JOIN Ventas v ON v.idVenta = dv.idVenta
                WHERE v.fecha BETWEEN @d AND @h
                GROUP BY p.idProducto, p.Modelo, m.nombre
                ORDER BY CantidadVendida DESC";
            return ExecDataTable(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@d", desde);
                cmd.Parameters.AddWithValue("@h", hasta);
            });
        }

        private DataTable ReporteClientesConMasCompras(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT TOP 10
                    c.nombre + ' ' + c.apellido AS Cliente,
                    c.dni AS DNI,
                    c.email AS Email,
                    COUNT(DISTINCT v.idVenta) AS CantidadCompras,
                    SUM(dv.cantidad * dv.precioUnitario) AS TotalGastado
                FROM Clientes c
                INNER JOIN Ventas v ON v.idCliente = c.idCliente
                INNER JOIN DetalleVentas dv ON dv.idVenta = v.idVenta
                WHERE v.fecha BETWEEN @d AND @h
                GROUP BY c.idCliente, c.nombre, c.apellido, c.dni, c.email
                ORDER BY TotalGastado DESC";
            return ExecDataTable(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@d", desde);
                cmd.Parameters.AddWithValue("@h", hasta);
            });
        }

        private DataTable ReporteClientes()
        {
            const string sql = @"
                SELECT nombre, apellido, dni, telefono, email
                FROM Clientes
                ORDER BY apellido, nombre";
            return ExecDataTable(sql);
        }

        private DataTable ReporteProductosSinStock()
        {
            const string sql = @"
                SELECT 
                    p.Modelo AS Producto,
                    m.nombre AS Marca,
                    p.descripcion AS Descripcion,
                    p.stock AS Stock,
                    p.precio AS Precio,
                    CASE 
                        WHEN p.stock = 0 THEN 'SIN STOCK'
                        WHEN p.stock <= 5 THEN 'STOCK BAJO'
                        ELSE 'STOCK NORMAL'
                    END AS EstadoStock
                FROM Productos p
                INNER JOIN Marcas m ON p.idMarca = m.idMarca
                WHERE p.stock <= 5
                ORDER BY p.stock ASC, p.Modelo";
            return ExecDataTable(sql);
        }

        private DataTable ReporteVentasDeServicios(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT
                    v.idVenta AS NroVenta,
                    v.fecha AS Fecha,
                    c.nombre + ' ' + c.apellido AS Cliente,
                    c.dni AS DNI_Cliente,
                    u.nombre + ' ' + u.apellido AS Vendedor,
                    u.dni AS DNI_Vendedor,
                    s.Nombre AS Servicio,
                    s.Descripcion AS Descripcion,
                    s.Precio AS Precio,
                    CASE WHEN s.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS EstadoServicio
                FROM Ventas v
                INNER JOIN DetalleServicios ds ON ds.IdVenta = v.idVenta
                INNER JOIN Servicios s ON s.IdServicio = ds.IdServicio
                INNER JOIN Clientes c ON c.idCliente = v.idCliente
                INNER JOIN Usuarios u ON u.idUsuario = v.idUsuario
                WHERE v.fecha BETWEEN @d AND @h
                ORDER BY v.fecha DESC, v.idVenta DESC";
            return ExecDataTable(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@d", desde);
                cmd.Parameters.AddWithValue("@h", hasta);
            });
        }

        // === EXPORTAR SOLO WORD ===

        private void btnExportar_Click(object? sender, EventArgs e)
        {
            if (dgv.DataSource is not DataTable dt || dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Reportes",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Word (*.docx)|*.docx",
                FileName = $"reporte_{cboReporte.Text.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmm}"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                ExportarAWord(dt, sfd.FileName);
                Cursor = Cursors.Default;

                var result = MessageBox.Show($"Exportación completada:\n{sfd.FileName}\n\n¿Desea abrir el archivo?",
                    "Reportes", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("No se pudo exportar.\n\n" + ex.Message,
                    "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarAWord(DataTable dt, string filePath)
        {
            using var doc = DocX.Create(filePath);

            doc.InsertParagraph($"REPORTE: {cboReporte.Text.ToUpper()}")
                .FontSize(14)
                .Bold()
                .SpacingAfter(10);

            doc.InsertParagraph($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                .FontSize(10);

            doc.InsertParagraph($"Total de registros: {dt.Rows.Count:N0}")
                .FontSize(10)
                .SpacingAfter(20);

            var table = doc.AddTable(dt.Rows.Count + 1, dt.Columns.Count);
            table.Design = TableDesign.TableGrid;

            // Encabezados
            for (int c = 0; c < dt.Columns.Count; c++)
                table.Rows[0].Cells[c].Paragraphs[0].Append(dt.Columns[c].ColumnName).Bold();

            // Datos
            for (int r = 0; r < dt.Rows.Count; r++)
                for (int c = 0; c < dt.Columns.Count; c++)
                    table.Rows[r + 1].Cells[c].Paragraphs[0].Append(dt.Rows[r][c]?.ToString() ?? "");

            doc.InsertTable(table);

            doc.InsertParagraph()
                .SpacingBefore(20)
                .Append("Automotors - Sistema de Gestión de Ventas")
                .Italic()
                .FontSize(9)
                .Alignment = Alignment.center;

            doc.Save();
        }
    }
}
