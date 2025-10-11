using System.Drawing;
using System.Windows.Forms;

namespace Automotors
{
    partial class FrmClienteDetalle
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtDNI;
        private TextBox txtEmail;
        private TextBox txtTelefono;
        private Button btnGuardar;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.txtNombre = new TextBox();
            this.txtApellido = new TextBox();
            this.txtDNI = new TextBox();
            this.txtEmail = new TextBox();
            this.txtTelefono = new TextBox();
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTitulo.ForeColor = Color.FromArgb(44, 62, 80);
            this.lblTitulo.Location = new Point(20, 15);
            this.lblTitulo.Text = "Nuevo Cliente";
            // 
            // labels
            // 
            this.label1.AutoSize = true; this.label1.Location = new Point(20, 55); this.label1.Text = "Nombre:";
            this.label2.AutoSize = true; this.label2.Location = new Point(20, 95); this.label2.Text = "Apellido:";
            this.label3.AutoSize = true; this.label3.Location = new Point(20, 135); this.label3.Text = "DNI:";
            this.label4.AutoSize = true; this.label4.Location = new Point(20, 175); this.label4.Text = "Email:";
            this.label5.AutoSize = true; this.label5.Location = new Point(20, 215); this.label5.Text = "Teléfono:";
            // 
            // inputs
            // 
            this.txtNombre.Location = new Point(110, 52); this.txtNombre.Size = new Size(320, 23);
            this.txtApellido.Location = new Point(110, 92); this.txtApellido.Size = new Size(320, 23);
            this.txtDNI.Location = new Point(110, 132); this.txtDNI.Size = new Size(150, 23);
            this.txtEmail.Location = new Point(110, 172); this.txtEmail.Size = new Size(320, 23);
            this.txtTelefono.Location = new Point(110, 212); this.txtTelefono.Size = new Size(200, 23);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = Color.SteelBlue;
            this.btnGuardar.ForeColor = Color.White;
            this.btnGuardar.FlatStyle = FlatStyle.Flat;
            this.btnGuardar.Location = new Point(240, 255);
            this.btnGuardar.Size = new Size(95, 30);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = Color.Gray;
            this.btnCancelar.ForeColor = Color.White;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.Location = new Point(335, 255);
            this.btnCancelar.Size = new Size(95, 30);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmClienteDetalle
            // 
            this.AcceptButton = this.btnGuardar;
            this.CancelButton = this.btnCancelar;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            this.ClientSize = new Size(450, 305);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.label1); this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label2); this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.label3); this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.label4); this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label5); this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.btnGuardar); this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false; this.MinimizeBox = false;
            this.ShowInTaskbar = false; this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Nuevo Cliente";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
