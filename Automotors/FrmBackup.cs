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
        private string carpetaBackupsPorDefecto;

        public FrmBackup()
        {
            InitializeComponent();
            ConfigurarCarpetaSegura();
        }

        private void ConfigurarCarpetaSegura()
        {
            // FORZAR una carpeta que NO esté en OneDrive
            string unidadSistema = Path.GetPathRoot(Environment.SystemDirectory); // Ej: "C:\"
            carpetaBackupsPorDefecto = @"C:\Backups_Automotors";
            // Crear la carpeta si no existe
            if (!Directory.Exists(carpetaBackupsPorDefecto))
            {
                try
                {
                    Directory.CreateDirectory(carpetaBackupsPorDefecto);
                }
                catch (Exception ex)
                {
                    // Si falla, usar el escritorio (último recurso)
                    carpetaBackupsPorDefecto = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    MessageBox.Show($"Se usará el Escritorio: {carpetaBackupsPorDefecto}", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void FrmBackup_Load(object sender, EventArgs e)
        {
            lblEstado.Text = "Listo para realizar backup";
            lblEstado.ForeColor = System.Drawing.Color.Black;
            txtDestino.Text = carpetaBackupsPorDefecto;
            MostrarInfoBD();
        }

        private void MostrarInfoBD()
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            DB_NAME() as DatabaseName,
                            (SELECT COUNT(*) FROM sys.tables) as TableCount,
                            (SELECT SUM(size) FROM sys.database_files WHERE type = 0) as DataSizeMB";

                    using (var cmd = new SqlCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int dataSizeMB = reader["DataSizeMB"] != DBNull.Value ?
                                Convert.ToInt32(reader["DataSizeMB"]) * 8 / 1024 : 0;

                            lblInfoBD.Text = $"Base de datos: {reader["DatabaseName"]}\n" +
                                           $"Tamaño aprox: {dataSizeMB} MB\n" +
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
                folderDialog.Description = "Seleccionar carpeta destino para el backup\nRECOMENDADO: Carpetas locales (C:\\Backups_Automotors)";
                folderDialog.ShowNewFolderButton = true;
                folderDialog.RootFolder = Environment.SpecialFolder.MyComputer; // Mostrar desde "Equipo"

                // Sugerir una carpeta en C:\
                string unidadSistema = Path.GetPathRoot(Environment.SystemDirectory);
                folderDialog.SelectedPath = Path.Combine(unidadSistema, "Backups_Automotors");

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

            // Asegurar que la carpeta existe
            if (!Directory.Exists(txtDestino.Text))
            {
                try
                {
                    Directory.CreateDirectory(txtDestino.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se puede crear la carpeta: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnGenerarBackup.Enabled = false;
                lblEstado.Text = "Generando backup...";
                lblEstado.ForeColor = System.Drawing.Color.Blue;
                Application.DoEvents();

                // Generar nombre del archivo con fecha y hora
                string fecha = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string nombreArchivo = $"bd_automotors_{fecha}.bak";
                string archivoCompleto = Path.Combine(txtDestino.Text, nombreArchivo);

                GenerarBackupBak(archivoCompleto);

            }
            catch (Exception ex)
            {
                lblEstado.Text = "✗ Error al crear backup";
                lblEstado.ForeColor = System.Drawing.Color.Red;

                // Mensaje de error más específico
                string mensajeError = ex.Message;
                if (mensajeError.Contains("Access is denied"))
                {
                    mensajeError += "\n\nSOLUCIÓN: Ejecute la aplicación como ADMINISTRADOR";
                }

                MessageBox.Show($"Error al crear backup: {mensajeError}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnGenerarBackup.Enabled = true;
            }
        }

        private void GenerarBackupBak(string archivoCompleto)
        {
            try
            {
                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    // Comando de backup SIMPLIFICADO
                    string backupQuery = $@"
                        BACKUP DATABASE [bd_automotors] 
                        TO DISK = '{archivoCompleto}'
                        WITH FORMAT, STATS = 10";

                    using (var cmd = new SqlCommand(backupQuery, connection))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.ExecuteNonQuery();
                    }
                }

                MostrarExito(archivoCompleto);
            }
            catch (Exception ex)
            {
                // Limpiar archivo corrupto
                try
                {
                    if (File.Exists(archivoCompleto))
                        File.Delete(archivoCompleto);
                }
                catch { }

                throw new Exception(ex.Message);
            }
        }

        private void MostrarExito(string archivoCompleto)
        {
            FileInfo info = new FileInfo(archivoCompleto);
            string tamaño = "";

            if (info.Exists)
            {
                if (info.Length < 1024)
                    tamaño = $"{info.Length} bytes";
                else if (info.Length < 1048576)
                    tamaño = $"{(info.Length / 1024.0):F2} KB";
                else
                    tamaño = $"{(info.Length / 1048576.0):F2} MB";
            }

            lblEstado.Text = "✓ Backup creado exitosamente";
            lblEstado.ForeColor = System.Drawing.Color.Green;

            lblInfoBackup.Text = $"Archivo: {Path.GetFileName(archivoCompleto)}\n" +
                               $"Ubicación: {Path.GetDirectoryName(archivoCompleto)}\n" +
                               $"Tamaño: {tamaño}\n" +
                               $"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            MessageBox.Show($"Backup creado exitosamente:\n\n" +
                          $"Archivo: {Path.GetFileName(archivoCompleto)}\n" +
                          $"Tamaño: {tamaño}",
                          "Backup Completado",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
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
            txtDestino.Text = carpetaBackupsPorDefecto;
            lblInfoBackup.Text = "";
            lblEstado.Text = "Listo para realizar backup";
            lblEstado.ForeColor = System.Drawing.Color.Black;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Backup files (*.bak)|*.bak";
                openDialog.Title = "Seleccionar archivo .bak para restaurar";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    var result = MessageBox.Show("¿Está seguro que desea restaurar la base de datos?\n\n" +
                                               "⚠️ Esta acción sobreescribirá todos los datos actuales.",
                                               "Confirmar Restauración",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        RestaurarBackup(openDialog.FileName);
                    }
                }
            }
        }

        private void RestaurarBackup(string archivoBak)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                using (var connection = Conexion.GetConnection())
                {
                    connection.Open();

                    string restoreQuery = $@"
                        USE master;
                        ALTER DATABASE [bd_automotors] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        RESTORE DATABASE [bd_automotors] 
                        FROM DISK = '{archivoBak}'
                        WITH REPLACE;
                        ALTER DATABASE [bd_automotors] SET MULTI_USER;";

                    using (var cmd = new SqlCommand(restoreQuery, connection))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.ExecuteNonQuery();
                    }
                }

                Cursor = Cursors.Default;
                MessageBox.Show("Base de datos restaurada exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                MostrarInfoBD();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error al restaurar backup: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}