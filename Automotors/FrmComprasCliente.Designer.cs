namespace Automotors
{
    partial class FrmComprasCliente
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
            lblCliente = new Label();
            dgvCompras = new DataGridView();
            dgvDetalle = new DataGridView();
            btnCerrar = new Button();
            lblDetalle = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            lblCompras = new Label();
            panel3 = new Panel();
            panelBoton = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvCompras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panelBoton.SuspendLayout();
            SuspendLayout();
            // 
            // lblCliente
            // 
            lblCliente.Dock = DockStyle.Fill;
            lblCliente.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCliente.ForeColor = Color.White;
            lblCliente.Location = new Point(0, 0);
            lblCliente.Margin = new Padding(4, 0, 4, 0);
            lblCliente.Name = "lblCliente";
            lblCliente.Padding = new Padding(27, 0, 0, 0);
            lblCliente.Size = new Size(1200, 92);
            lblCliente.TabIndex = 0;
            lblCliente.Text = "Compras del cliente:";
            lblCliente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvCompras
            // 
            dgvCompras.AllowUserToAddRows = false;
            dgvCompras.AllowUserToDeleteRows = false;
            dgvCompras.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCompras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCompras.Dock = DockStyle.Fill;
            dgvCompras.Location = new Point(13, 53);
            dgvCompras.Margin = new Padding(13, 15, 13, 15);
            dgvCompras.Name = "dgvCompras";
            dgvCompras.ReadOnly = true;
            dgvCompras.RowHeadersVisible = false;
            dgvCompras.RowHeadersWidth = 51;
            dgvCompras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCompras.Size = new Size(1174, 240);
            dgvCompras.TabIndex = 1;
            dgvCompras.SelectionChanged += dgvCompras_SelectionChanged;
            // 
            // dgvDetalle
            // 
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Dock = DockStyle.Fill;
            dgvDetalle.Location = new Point(13, 53);
            dgvDetalle.Margin = new Padding(13, 15, 13, 15);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.ReadOnly = true;
            dgvDetalle.RowHeadersVisible = false;
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.Size = new Size(1174, 155);
            dgvDetalle.TabIndex = 2;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.BackColor = Color.SteelBlue;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCerrar.ForeColor = Color.White;
            btnCerrar.Location = new Point(1028, 15);
            btnCerrar.Margin = new Padding(4, 5, 4, 5);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(133, 54);
            btnCerrar.TabIndex = 3;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // lblDetalle
            // 
            lblDetalle.Dock = DockStyle.Top;
            lblDetalle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetalle.Location = new Point(13, 15);
            lblDetalle.Margin = new Padding(4, 0, 4, 0);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(1174, 38);
            lblDetalle.TabIndex = 4;
            lblDetalle.Text = "Detalle de la venta seleccionada:";
            lblDetalle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.BackColor = Color.SteelBlue;
            panel1.Controls.Add(lblCliente);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1200, 92);
            panel1.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgvCompras);
            panel2.Controls.Add(lblCompras);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 92);
            panel2.Margin = new Padding(4, 5, 4, 5);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(13, 15, 13, 15);
            panel2.Size = new Size(1200, 308);
            panel2.TabIndex = 6;
            // 
            // lblCompras
            // 
            lblCompras.Dock = DockStyle.Top;
            lblCompras.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCompras.Location = new Point(13, 15);
            lblCompras.Margin = new Padding(4, 0, 4, 0);
            lblCompras.Name = "lblCompras";
            lblCompras.Size = new Size(1174, 38);
            lblCompras.TabIndex = 0;
            lblCompras.Text = "Compras realizadas:";
            lblCompras.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            panel3.Controls.Add(dgvDetalle);
            panel3.Controls.Add(lblDetalle);
            panel3.Controls.Add(panelBoton);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 400);
            panel3.Margin = new Padding(4, 5, 4, 5);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(13, 15, 13, 15);
            panel3.Size = new Size(1200, 285);
            panel3.TabIndex = 7;
            // 
            // panelBoton
            // 
            panelBoton.Controls.Add(btnCerrar);
            panelBoton.Dock = DockStyle.Bottom;
            panelBoton.Location = new Point(13, 208);
            panelBoton.Margin = new Padding(4, 5, 4, 5);
            panelBoton.Name = "panelBoton";
            panelBoton.Size = new Size(1174, 62);
            panelBoton.TabIndex = 5;
            // 
            // FrmComprasCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 685);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmComprasCliente";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Compras de Cliente";
            Load += FrmComprasCliente_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCompras).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panelBoton.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.DataGridView dgvCompras;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblCompras;
        private System.Windows.Forms.Panel panelBoton;
    }
}