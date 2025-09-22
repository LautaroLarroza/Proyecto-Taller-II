using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace Automotors
{
    public partial class FrmReportes : Form
    {
        public FrmReportes()
        {
            InitializeComponent();
            // Si prefieres hacerlo en el evento Load, puedes mover esto allí.
            this.Load += FrmReportes_Load;
            btnBuscar.Click += btnBuscar_Click;
            btnExportar.Click += btnExportar_Click;
        }

        private void FrmReportes_Load(object? sender, EventArgs e)
        {
            // Opciones de reporte
            cboReporte.Items.Clear();
            cboReporte.Items.AddRange(new object[]
            {
                "Ventas por fecha (detalle)",
                "Ventas por producto (resumen)",
                "Stock de productos",
                "Listado de clientes"
            });
            cboReporte.SelectedIndex = 0;

            // Rango por defecto: último mes
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
                    2 => ReporteStockProductos(),
                    3 => ReporteClientes(),
                    _ => new DataTable()
                };

                dgv.DataSource = dt;
                ActualizarResumen(dt);
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
            foreach (DataColumn c in dt.Columns)
            {
                if (string.Equals(c.ColumnName, "Total", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.ColumnName, "Importe", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r[c] != DBNull.Value &&
                            decimal.TryParse(Convert.ToString(r[c]), NumberStyles.Any, CultureInfo.InvariantCulture, out var v))
                        {
                            total += v;
                        }
                    }
                    lblResumen.Text = $"Filas: {dt.Rows.Count:N0}   |   Suma {c.ColumnName}: {total:N2}";
                    return;
                }
            }

            lblResumen.Text = $"Filas: {dt.Rows.Count:N0}";
        }

        // =========================================================
        // Helper genérico para ejecutar y devolver un DataTable
        // =========================================================
        private static DataTable ExecDataTable(string sql, Action<SqliteCommand>? addParams = null)
        {
            using var cn = Conexion.GetConnection();
            if (cn.State != ConnectionState.Open) cn.Open();

            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql;
            addParams?.Invoke(cmd);

            using var rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            var dt = new DataTable();
            dt.Load(rd);
            return dt;
        }

        // ==========================
        // Reportes (consultas)
        // ==========================
        private DataTable ReporteVentasDetalle(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT
                    date(v.Fecha) AS Fecha,
                    c.Nombre      AS Cliente,
                    p.Descripcion AS Producto,
                    v.Cantidad,
                    v.PrecioUnitario,
                    (v.Cantidad * v.PrecioUnitario) AS Total
                FROM Ventas v
                JOIN Clientes  c ON c.Id = v.IdCliente
                JOIN Productos p ON p.Id = v.IdProducto
                WHERE date(v.Fecha) BETWEEN date(@d) AND date(@h)
                ORDER BY v.Fecha ASC;";

            return ExecDataTable(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@d", desde.Date);
                cmd.Parameters.AddWithValue("@h", hasta.Date);
            });
        }

        private DataTable ReporteVentasPorProducto(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT
                    p.Descripcion AS Producto,
                    SUM(v.Cantidad)                    AS Cantidad,
                    SUM(v.Cantidad * v.PrecioUnitario) AS Importe
                FROM Ventas v
                JOIN Productos p ON p.Id = v.IdProducto
                WHERE date(v.Fecha) BETWEEN date(@d) AND date(@h)
                GROUP BY p.Id, p.Descripcion
                ORDER BY Importe DESC;";

            return ExecDataTable(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@d", desde.Date);
                cmd.Parameters.AddWithValue("@h", hasta.Date);
            });
        }

        private DataTable ReporteStockProductos()
        {
            const string sql = @"
                SELECT
                    Descripcion,
                    Marca,
                    Modelo,
                    Stock,
                    Precio
                FROM Productos
                ORDER BY Descripcion;";

            return ExecDataTable(sql);
        }

        private DataTable ReporteClientes()
        {
            const string sql = @"
                SELECT
                    Nombre,
                    Apellido,
                    Dni,
                    Telefono,
                    Email
                FROM Clientes
                ORDER BY Apellido, Nombre;";

            return ExecDataTable(sql);
        }

        // ==========================
        // Exportar CSV
        // ==========================
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
                Filter = "CSV (*.csv)|*.csv",
                FileName = $"reporte_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                ExportarDataTableACsv(dt, sfd.FileName);
                MessageBox.Show("Exportación finalizada.", "Reportes",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo exportar.\n\n" + ex.Message,
                    "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ExportarDataTableACsv(DataTable dt, string path)
        {
            var sb = new StringBuilder();

            // Encabezados
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i > 0) sb.Append(';');
                sb.Append(dt.Columns[i].ColumnName.Replace(';', ','));
            }
            sb.AppendLine();

            // Filas
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0) sb.Append(';');
                    var val = row[i]?.ToString()?.Replace('\n', ' ')
                                         .Replace('\r', ' ')
                                         .Replace(';', ',') ?? "";
                    sb.Append(val);
                }
                sb.AppendLine();
            }

            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }
    }
}
