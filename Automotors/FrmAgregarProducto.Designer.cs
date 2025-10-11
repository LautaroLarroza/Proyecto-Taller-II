using System.Drawing;
using System.Windows.Forms;

namespace Automotors
{
    partial class FrmAgregarProducto
    {
        private System.ComponentModel.IContainer components = null;
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
        private Button btnGuardar;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

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
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.label1.Location = new Point(20, 20);
            this.label1.Text = "Marca:";
            // 
            // cmbMarca
            // 
            this.cmbMarca.DropDownStyle = ComboBoxStyle.DropDown;
            this.cmbMarca.Location = new Point(90, 18);
            this.cmbMarca.Size = new Size(180, 23);
            // 
            // btnAgregarMarca
            // 
            this.btnAgregarMarca.Text = "+";
            this.btnAgregarMarca.Location = new Point(275, 18);
            this.btnAgregarMarca.Size = new Size(30, 23);
            this.btnAgregarMarca.Click += new System.EventHandler(this.btnAgregarMarca_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(20, 55);
            this.label2.Text = "Modelo:";
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new Point(90, 52);
            this.txtModelo.Size = new Size(215, 23);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new Point(20, 87);
            this.label3.Text = "Año:";
            // 
            // nudAnio
            // 
            this.nudAnio.Minimum = 1900;
            this.nudAnio.Maximum = 2030;
            this.nudAnio.Value = 2024;
            this.nudAnio.Location = new Point(90, 85);
            this.nudAnio.Size = new Size(90, 23);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new Point(20, 119);
            this.label4.Text = "Precio ($):";
            // 
            // nudPrecio
            // 
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.Maximum = 1000000;
            this.nudPrecio.Location = new Point(90, 117);
            this.nudPrecio.Size = new Size(120, 23);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new Point(20, 151);
            this.label5.Text = "Stock:";
            // 
            // nudStock
            // 
            this.nudStock.Maximum = 10000;
            this.nudStock.Location = new Point(90, 149);
            this.nudStock.Size = new Size(120, 23);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new Point(20, 183);
            this.label6.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new Point(90, 181);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Size = new Size(215, 70);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = Color.SteelBlue;
            this.btnGuardar.ForeColor = Color.White;
            this.btnGuardar.FlatStyle = FlatStyle.Flat;
            this.btnGuardar.Location = new Point(130, 265);
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
            this.btnCancelar.Location = new Point(230, 265);
            this.btnCancelar.Size = new Size(95, 30);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmAgregarProducto
            // 
            this.AcceptButton = this.btnGuardar;
            this.CancelButton = this.btnCancelar;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(330, 315);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbMarca);
            this.Controls.Add(this.btnAgregarMarca);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudAnio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudPrecio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudStock);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Agregar Producto";
            this.Load += new System.EventHandler(this.FrmAgregarProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
