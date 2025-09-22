namespace Automotors
{
    partial class FrmProductos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductos));
            dgvProductos = new DataGridView();
            btnAgregar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            panel1 = new Panel();
            lblTitulo = new Label();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            lblSistema = new Label();
            ((System.ComponentModel.ISupportInitialize)(dgvProductos)).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.BackgroundColor = Color.White;
            this.dgvProductos.BorderStyle = BorderStyle.None;
            this.dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new Point(30, 80);
            this.dgvProductos.Margin = new Padding(4);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersWidth = 51;
            this.dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new Size(890, 400);
            this.dgvProductos.TabIndex = 0;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = Color.FromArgb(41, 128, 185);
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = FlatStyle.Flat;
            this.btnAgregar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnAgregar.ForeColor = Color.White;
            this.btnAgregar.Location = new Point(30, 500);
            this.btnAgregar.Margin = new Padding(4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new Size(180, 50);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = Color.FromArgb(39, 174, 96);
            this.btnModificar.FlatAppearance.BorderSize = 0;
            this.btnModificar.FlatStyle = FlatStyle.Flat;
            this.btnModificar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnModificar.ForeColor = Color.White;
            this.btnModificar.Location = new Point(380, 500);
            this.btnModificar.Margin = new Padding(4);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new Size(180, 50);
            this.btnModificar.TabIndex = 2;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.BtnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = Color.FromArgb(192, 57, 43);
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = FlatStyle.Flat;
            this.btnEliminar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnEliminar.ForeColor = Color.White;
            this.btnEliminar.Location = new Point(740, 500);
            this.btnEliminar.Margin = new Padding(4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new Size(180, 50);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(lblTitulo);
            panel1.Controls.Add(this.btnEliminar);
            panel1.Controls.Add(this.btnModificar);
            panel1.Controls.Add(this.btnAgregar);
            panel1.Controls.Add(this.dgvProductos);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(350, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(950, 600);
            panel1.TabIndex = 4;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(950, 60);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "Gestión de Productos";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(52, 73, 94);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(lblSistema);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(350, 600);
            panel2.TabIndex = 5;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(350, 300);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // lblSistema
            // 
            lblSistema.Dock = DockStyle.Bottom;
            lblSistema.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSistema.ForeColor = Color.White;
            lblSistema.Location = new Point(0, 500);
            lblSistema.Margin = new Padding(4, 0, 4, 0);
            lblSistema.Name = "lblSistema";
            lblSistema.Size = new Size(350, 100);
            lblSistema.TabIndex = 0;
            lblSistema.Text = "Sistema Automotors";
            lblSistema.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmProductos
            // 
            this.AutoScaleDimensions = new SizeF(10F, 25F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(1300, 600);
            this.Controls.Add(panel1);
            this.Controls.Add(panel2);
            this.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Icon = (Icon)resources.GetObject("$this.Icon");
            this.Margin = new Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProductos";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestión de Productos - Automotors";
            this.Load += new System.EventHandler(this.FrmProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label lblSistema;
        private Label lblTitulo;
    }
}