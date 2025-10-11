using System.Drawing;
using System.Windows.Forms;

namespace Automotors
{
    partial class FrmEditarProducto
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.cmbMarca = new ComboBox();
            this.btnAgregarMarca = new Button();
            this.label2 = new Label();
            this.txtModelo = new TextBox();
            this.label3 = new Label();
            this.nudAnio = new NumericUpDown();
            this.label4 = new Label();
            this.nudPrecio = new NumericUpDown();
            this.label5 = new Label();
            this.nudStock = new NumericUpDown();
            this.label6 = new Label();
            this.txtDescripcion = new TextBox();
            this.label7 = new Label();
            this.chkEstado = new CheckBox();
            this.btnAceptar = new Button();
            this.btnCancelar = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).BeginInit();
            SuspendLayout();

            // Etiquetas principales
            label1.AutoSize = true; label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(20, 20); label1.Text = "Marca:";

            cmbMarca.DropDownStyle = ComboBoxStyle.DropDown;
            cmbMarca.Location = new Point(90, 18); cmbMarca.Size = new Size(180, 23);

            btnAgregarMarca.Text = "+";
            btnAgregarMarca.Location = new Point(275, 18);
            btnAgregarMarca.Size = new Size(30, 23);
            btnAgregarMarca.Click += new System.EventHandler(this.btnAgregarMarca_Click);

            label2.AutoSize = true; label2.Location = new Point(20, 55); label2.Text = "Modelo:";
            txtModelo.Location = new Point(90, 52); txtModelo.Size = new Size(215, 23);

            label3.AutoSize = true; label3.Location = new Point(20, 87); label3.Text = "Año:";
            nudAnio.Minimum = 1900; nudAnio.Maximum = 2030; nudAnio.Value = 2024;
            nudAnio.Location = new Point(90, 85); nudAnio.Size = new Size(90, 23);

            label4.AutoSize = true; label4.Location = new Point(20, 119); label4.Text = "Precio ($):";
            nudPrecio.DecimalPlaces = 2; nudPrecio.Maximum = 1000000;
            nudPrecio.Location = new Point(90, 117); nudPrecio.Size = new Size(120, 23);

            label5.AutoSize = true; label5.Location = new Point(20, 151); label5.Text = "Stock:";
            nudStock.Maximum = 10000; nudStock.Location = new Point(90, 149);
            nudStock.Size = new Size(120, 23);

            label6.AutoSize = true; label6.Location = new Point(20, 183); label6.Text = "Descripción:";
            txtDescripcion.Location = new Point(90, 181); txtDescripcion.Multiline = true;
            txtDescripcion.Size = new Size(215, 70); txtDescripcion.ScrollBars = ScrollBars.Vertical;

            label7.AutoSize = true; label7.Location = new Point(20, 258); label7.Text = "Estado:";
            chkEstado.AutoSize = true; chkEstado.Checked = true; chkEstado.Text = "Activo";
            chkEstado.Location = new Point(90, 256);

            btnAceptar.Text = "Guardar";
            btnAceptar.BackColor = Color.SteelBlue;
            btnAceptar.ForeColor = Color.White;
            btnAceptar.FlatStyle = FlatStyle.Flat;
            btnAceptar.Location = new Point(130, 290);
            btnAceptar.Size = new Size(95, 30);
            btnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);

            btnCancelar.Text = "Cancelar";
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Location = new Point(230, 290);
            btnCancelar.Size = new Size(95, 30);
            btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(330, 335);
            Controls.Add(label1); Controls.Add(cmbMarca); Controls.Add(btnAgregarMarca);
            Controls.Add(label2); Controls.Add(txtModelo);
            Controls.Add(label3); Controls.Add(nudAnio);
            Controls.Add(label4); Controls.Add(nudPrecio);
            Controls.Add(label5); Controls.Add(nudStock);
            Controls.Add(label6); Controls.Add(txtDescripcion);
            Controls.Add(label7); Controls.Add(chkEstado);
            Controls.Add(btnAceptar); Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Editar Producto";

            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbMarca;
        private Button btnAgregarMarca;
        private Label label2;
        private TextBox txtModelo;
        private Label label3;
        private NumericUpDown nudAnio;
        private Label label4;
        private NumericUpDown nudPrecio;
        private Label label5;
        private NumericUpDown nudStock;
        private Label label6;
        private TextBox txtDescripcion;
        private Label label7;
        private CheckBox chkEstado;
        private Button btnAceptar;
        private Button btnCancelar;
    }
}
