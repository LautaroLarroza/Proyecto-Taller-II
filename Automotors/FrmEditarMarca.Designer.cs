using System.Drawing;
using System.Windows.Forms;

namespace Automotors
{
    partial class FrmEditarMarca
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitulo;
        private Label label1;
        private Label label2;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private Button btnAceptar;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            label1 = new Label();
            label2 = new Label();
            txtNombre = new TextBox();
            txtDescripcion = new TextBox();
            btnAceptar = new Button();
            btnCancelar = new Button();
            SuspendLayout();

            // --- Título ---
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Text = "Agregar Marca";

            // --- Campos ---
            label1.AutoSize = true; label1.Location = new Point(20, 55); label1.Text = "Nombre:";
            txtNombre.Location = new Point(100, 52); txtNombre.Size = new Size(280, 23); txtNombre.MaxLength = 50;

            label2.AutoSize = true; label2.Location = new Point(20, 90); label2.Text = "Descripción:";
            txtDescripcion.Location = new Point(100, 88);
            txtDescripcion.Size = new Size(280, 90);
            txtDescripcion.Multiline = true;
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.MaxLength = 200;

            // --- Botones ---
            btnAceptar.Text = "Guardar";
            btnAceptar.BackColor = Color.SteelBlue;
            btnAceptar.ForeColor = Color.White;
            btnAceptar.FlatStyle = FlatStyle.Flat;
            btnAceptar.Location = new Point(205, 190);
            btnAceptar.Size = new Size(85, 30);
            btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);

            btnCancelar.Text = "Cancelar";
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Location = new Point(295, 190);
            btnCancelar.Size = new Size(85, 30);
            btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // --- Form (estilo emergente) ---
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 240);
            Controls.Add(lblTitulo);
            Controls.Add(label1); Controls.Add(txtNombre);
            Controls.Add(label2); Controls.Add(txtDescripcion);
            Controls.Add(btnAceptar); Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            ShowInTaskbar = false; StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar Marca";

            // Teclas Enter/Esc
            AcceptButton = btnAceptar;
            CancelButton = btnCancelar;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
