using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

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

            // Mostrar información de la base de datos actual
            MostrarInfoBD();
        }

        private void MostrarInfoBD()
        {
            try
            {
                string dbPath = "bd_automotors.db";
                if (File.Exists(dbPath))
                {
                    FileInfo info = new FileInfo(dbPath);
                    lblInfoBD.Text = $"Base de datos: {info.Name}\n" +
                                   $"Tamaño: {info.Length / 1024} KB\n" +
                                   $"Última modificación: {info.LastWriteTime}";
                }
                else
                {
                    lblInfoBD.Text = "Base de datos no encontrada";
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
                string dbPath = "bd_automotors.db";
                if (!File.Exists(dbPath))
                {
                    MessageBox.Show("No se encuentra la base de datos", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Crear nombre del archivo con fecha y hora
                string fecha = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupFile = Path.Combine(txtDestino.Text, $"backup_automotors_{fecha}.db");

                // Copiar la base de datos
                File.Copy(dbPath, backupFile, true);

                lblEstado.Text = $"✓ Backup creado exitosamente en: {backupFile}";
                lblEstado.ForeColor = System.Drawing.Color.Green;

                // Mostrar información del backup creado
                FileInfo backupInfo = new FileInfo(backupFile);
                lblInfoBackup.Text = $"Backup: {backupInfo.Name}\n" +
                                   $"Tamaño: {backupInfo.Length / 1024} KB\n" +
                                   $"Ubicación: {backupInfo.DirectoryName}";

                MessageBox.Show($"Backup creado exitosamente:\n{backupFile}", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                lblEstado.Text = $"✗ Error al crear backup: {ex.Message}";
                lblEstado.ForeColor = System.Drawing.Color.Red;
                MessageBox.Show($"Error al crear backup: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        }
    }
}