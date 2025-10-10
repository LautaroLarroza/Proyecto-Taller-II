using System.Drawing;
using System.Windows.Forms;

namespace Automotors
{
    partial class FrmServicios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblPrecio = new Label();
            nudPrecio = new NumericUpDown();
            chkActivo = new CheckBox();
            btnGuardar = new Button();
            btnCancelar = new Button();

            ((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Text = "Gestión de Servicio";

            // lblNombre
            lblNombre.Text = "Nombre:";
            lblNombre.Location = new Point(20, 60);
            lblNombre.Size = new Size(80, 23);

            // txtNombre
            txtNombre.Location = new Point(110, 58);
            txtNombre.Size = new Size(250, 23);

            // lblDescripcion
            lblDescripcion.Text = "Descripción:";
            lblDescripcion.Location = new Point(20, 95);
            lblDescripcion.Size = new Size(80, 23);

            // txtDescripcion
            txtDescripcion.Location = new Point(110, 93);
            txtDescripcion.Size = new Size(250, 60);
            txtDescripcion.Multiline = true;

            // lblPrecio
            lblPrecio.Text = "Precio ($):";
            lblPrecio.Location = new Point(20, 165);
            lblPrecio.Size = new Size(80, 23);

            // nudPrecio
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Maximum = 1000000;
            nudPrecio.Location = new Point(110, 163);
            nudPrecio.Size = new Size(120, 23);

            // chkActivo
            chkActivo.Text = "Activo";
            chkActivo.Location = new Point(250, 163);
            chkActivo.Checked = true;

            // btnGuardar
            btnGuardar.Text = "Guardar";
            btnGuardar.BackColor = Color.SteelBlue;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Location = new Point(110, 210);
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.Click += btnGuardar_Click;

            // btnCancelar
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(220, 210);
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.Click += btnCancelar_Click;

            // FrmServicios
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 270);
            Controls.Add(lblTitulo);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(lblPrecio);
            Controls.Add(nudPrecio);
            Controls.Add(chkActivo);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestión de Servicios";
            Load += FrmServicios_Load;

            ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private Label lblPrecio;
        private NumericUpDown nudPrecio;
        private CheckBox chkActivo;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}
