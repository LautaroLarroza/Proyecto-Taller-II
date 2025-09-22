namespace Automotors
{
    partial class FrmEditarProducto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMarca = new System.Windows.Forms.ComboBox();
            this.btnAgregarMarca = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudAnio = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudPrecio = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudStock = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkEstado = new System.Windows.Forms.CheckBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Marca:";
            // 
            // cmbMarca
            // 
            this.cmbMarca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbMarca.Location = new System.Drawing.Point(80, 22);
            this.cmbMarca.Name = "cmbMarca";
            this.cmbMarca.Size = new System.Drawing.Size(150, 21);
            this.cmbMarca.TabIndex = 1;
            // 
            // btnAgregarMarca
            // 
            this.btnAgregarMarca.Location = new System.Drawing.Point(240, 20);
            this.btnAgregarMarca.Name = "btnAgregarMarca";
            this.btnAgregarMarca.Size = new System.Drawing.Size(25, 23);
            this.btnAgregarMarca.TabIndex = 2;
            this.btnAgregarMarca.Text = "+";
            this.btnAgregarMarca.UseVisualStyleBackColor = true;
            this.btnAgregarMarca.Click += new System.EventHandler(this.btnAgregarMarca_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Modelo:";
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(80, 52);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(150, 20);
            this.txtModelo.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Año:";
            // 
            // nudAnio
            // 
            this.nudAnio.Location = new System.Drawing.Point(80, 82);
            this.nudAnio.Maximum = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.nudAnio.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.nudAnio.Name = "nudAnio";
            this.nudAnio.Size = new System.Drawing.Size(80, 20);
            this.nudAnio.TabIndex = 6;
            this.nudAnio.Value = new decimal(new int[] {
            2024,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Precio:";
            // 
            // nudPrecio
            // 
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.Location = new System.Drawing.Point(80, 112);
            this.nudPrecio.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudPrecio.Name = "nudPrecio";
            this.nudPrecio.Size = new System.Drawing.Size(80, 20);
            this.nudPrecio.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Stock:";
            // 
            // nudStock
            // 
            this.nudStock.Location = new System.Drawing.Point(80, 142);
            this.nudStock.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudStock.Name = "nudStock";
            this.nudStock.Size = new System.Drawing.Size(80, 20);
            this.nudStock.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(80, 172);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(200, 60);
            this.txtDescripcion.TabIndex = 12;
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Estado:";
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.Checked = true;
            this.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstado.Location = new System.Drawing.Point(80, 245);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(56, 17);
            this.chkEstado.TabIndex = 14;
            this.chkEstado.Text = "Activo";
            this.chkEstado.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(120, 280);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 15;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(205, 280);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 16;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // FrmEditarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 320);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.chkEstado);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudStock);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudPrecio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudAnio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAgregarMarca);
            this.Controls.Add(this.cmbMarca);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditarProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editar Producto";
            ((System.ComponentModel.ISupportInitialize)(this.nudAnio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbMarca;
        private System.Windows.Forms.Button btnAgregarMarca;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudAnio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudPrecio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudStock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkEstado;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
    }
}