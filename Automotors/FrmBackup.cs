using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

namespace Automotors
{
    public partial class FrmBackup : Form
    {
        public FrmBackup()
        {
            InitializeComponent();
        }

        private void FrmBackup_Load(object sender, EventArgs e)
        {
            lblEstado.Text = "Listo para realizar backup";
            lblEstado.ForeColor = System.Drawing.Color.Black;
            cboFormato.SelectedIndex = 0; // SQL por defecto

            MostrarInfoBD();
        }

        private void MostrarInfoBD()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // Obtener información de la base de datos
                    string query = @"
                        SELECT 
                            DB_NAME() as DatabaseName,
                            SUM(size * 8 / 1024) as SizeMB,
                            COUNT(*) as TableCount
                        FROM sys.tables";

                    using (var cmd = new SqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblInfoBD.Text = $"Base de datos: {reader["DatabaseName"]}\n" +
                                           $"Tamaño: {reader["SizeMB"]} MB\n" +
                                           $"Tablas: {reader["TableCount"]}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblInfoBD.Text = $"Error: {ex.Message}";
            }
        }

        private void btnSeleccionarDestino_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Seleccionar carpeta destino para el backup";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDestino.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDestino.Text))
            {
                MessageBox.Show("Por favor, seleccione una carpeta destino", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnGenerarBackup.Enabled = false;

                string formato = cboFormato.SelectedItem.ToString();
                string fecha = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                switch (formato)
                {
                    case "SQL Script":
                        GenerarBackupSQL(fecha);
                        break;
                    case "Word Document":
                        GenerarBackupWord(fecha);
                        break;
                    case "CSV Files":
                        GenerarBackupCSV(fecha);
                        break;
                }

                Cursor = Cursors.Default;
                btnGenerarBackup.Enabled = true;

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                btnGenerarBackup.Enabled = true;

                lblEstado.Text = $"✗ Error al crear backup: {ex.Message}";
                lblEstado.ForeColor = System.Drawing.Color.Red;
                MessageBox.Show($"Error al crear backup: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarBackupSQL(string fecha)
        {
            StringBuilder script = new StringBuilder();
            script.AppendLine("-- BACKUP AUTOMOTORS - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            script.AppendLine("-- GENERADO AUTOMÁTICAMENTE");
            script.AppendLine("USE [bd_automotors]");
            script.AppendLine("GO");
            script.AppendLine();

            // Lista de tablas en orden correcto (para evitar problemas de FK)
            string[] tablas = { "Marcas", "Productos", "Clientes", "Usuarios", "Roles", "Ventas", "DetalleVentas" };

            using (var connection = Conexion.GetConnection())
            {
                connection.Open();

                foreach (string tabla in tablas)
                {
                    script.AppendLine($"-- DATOS DE LA TABLA: {tabla}");
                    script.AppendLine($"DELETE FROM {tabla};");
                    script.AppendLine();

                    string query = $"SELECT * FROM {tabla}";
                    using (var cmd = new SqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StringBuilder insert = new StringBuilder();
                            insert.Append($"INSERT INTO {tabla} (");

                            // Columnas
                            List<string> columnas = new List<string>();
                            List<string> valores = new List<string>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columna = reader.GetName(i);
                                columnas.Add(columna);

                                object valor = reader[i];
                                if (valor == DBNull.Value)
                                {
                                    valores.Add("NULL");
                                }
                                else if (valor is string || valor is DateTime)
                                {
                                    valores.Add("'" + valor.ToString().Replace("'", "''") + "'");
                                }
                                else if (valor is bool)
                                {
                                    valores.Add((bool)valor ? "1" : "0");
                                }
                                else
                                {
                                    valores.Add(valor.ToString());
                                }
                            }

                            insert.Append(string.Join(", ", columnas));
                            insert.Append(") VALUES (");
                            insert.Append(string.Join(", ", valores));
                            insert.Append(");");

                            script.AppendLine(insert.ToString());
                        }
                    }
                    script.AppendLine();
                }
            }

            string archivo = Path.Combine(txtDestino.Text, $"backup_automotors_{fecha}.sql");
            File.WriteAllText(archivo, script.ToString(), Encoding.UTF8);

            MostrarExito("SQL Script", archivo);
        }

        private void GenerarBackupWord(string fecha)
        {
            string archivo = Path.Combine(txtDestino.Text, $"backup_automotors_{fecha}.txt");

            using (StreamWriter writer = new StreamWriter(archivo, false, Encoding.UTF8))
            {
                writer.WriteLine("BACKUP AUTOMOTORS - " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine("=".PadRight(80, '='));
                writer.WriteLine();

                string[] tablas = { "Marcas", "Productos", "Clientes", "Usuarios", "Roles", "Ventas", "DetalleVentas" };

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    foreach (string tabla in tablas)
                    {
                        writer.WriteLine($"TABLA: {tabla}");
                        writer.WriteLine("-".PadRight(80, '-'));

                        string query = $"SELECT * FROM {tabla}";
                        using (var cmd = new SqlCommand(query, connection))
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Escribir encabezados
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                writer.Write($"{reader.GetName(i),-20}");
                            }
                            writer.WriteLine();

                            // Escribir datos
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string valor = reader[i].ToString();
                                    writer.Write($"{valor,-20}");
                                }
                                writer.WriteLine();
                            }
                        }
                        writer.WriteLine();
                        writer.WriteLine();
                    }
                }
            }

            MostrarExito("Word/Text", archivo);
        }

        private void GenerarBackupCSV(string fecha)
        {
            string carpetaCSV = Path.Combine(txtDestino.Text, $"backup_automotors_{fecha}");
            Directory.CreateDirectory(carpetaCSV);

            string[] tablas = { "Marcas", "Productos", "Clientes", "Usuarios", "Roles", "Ventas", "DetalleVentas" };

            using (var connection = Conexion.GetConnection())
            {
                connection.Open();

                foreach (string tabla in tablas)
                {
                    string archivoCSV = Path.Combine(carpetaCSV, $"{tabla}.csv");

                    using (StreamWriter writer = new StreamWriter(archivoCSV, false, Encoding.UTF8))
                    {
                        string query = $"SELECT * FROM {tabla}";
                        using (var cmd = new SqlCommand(query, connection))
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Escribir encabezados
                            List<string> encabezados = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                encabezados.Add(reader.GetName(i));
                            }
                            writer.WriteLine(string.Join(",", encabezados));

                            // Escribir datos
                            while (reader.Read())
                            {
                                List<string> fila = new List<string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string valor = reader[i].ToString().Replace(",", ";").Replace("\"", "'");
                                    fila.Add($"\"{valor}\"");
                                }
                                writer.WriteLine(string.Join(",", fila));
                            }
                        }
                    }
                }
            }

            MostrarExito("CSV", carpetaCSV);
        }

        private void MostrarExito(string formato, string archivo)
        {
            FileInfo info = new FileInfo(archivo);
            string mensaje = archivo;

            if (Directory.Exists(archivo)) // Para CSV que es una carpeta
            {
                var archivos = Directory.GetFiles(archivo);
                mensaje = $"{archivo} ({archivos.Length} archivos)";
            }

            lblEstado.Text = $"✓ Backup {formato} creado exitosamente";
            lblEstado.ForeColor = System.Drawing.Color.Green;

            lblInfoBackup.Text = $"Formato: {formato}\n" +
                               $"Ubicación: {mensaje}\n" +
                               $"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            MessageBox.Show($"Backup {formato} creado exitosamente:\n{mensaje}", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAbrirCarpeta_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDestino.Text) && Directory.Exists(txtDestino.Text))
            {
                Process.Start("explorer.exe", txtDestino.Text);
            }
            else
            {
                MessageBox.Show("La carpeta destino no existe", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtDestino.Text = "";
            lblInfoBackup.Text = "";
            lblEstado.Text = "Listo para realizar backup";
            lblEstado.ForeColor = System.Drawing.Color.Black;
            cboFormato.SelectedIndex = 0;
        }
    }
}