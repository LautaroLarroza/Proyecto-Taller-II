namespace Automotors
{
    partial class FrmUsuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUsuarios));
            dataGridView1 = new DataGridView();
            BAgregar = new Button();
            BModificar = new Button();
            BEliminar = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            lblTitulo = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(30, 80);
            dataGridView1.Margin = new Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(890, 400);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // BAgregar
            // 
            BAgregar.BackColor = Color.FromArgb(41, 128, 185);
            BAgregar.FlatAppearance.BorderSize = 0;
            BAgregar.FlatStyle = FlatStyle.Flat;
            BAgregar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BAgregar.ForeColor = Color.White;
            BAgregar.Location = new Point(30, 500);
            BAgregar.Margin = new Padding(4);
            BAgregar.Name = "BAgregar";
            BAgregar.Size = new Size(180, 50);
            BAgregar.TabIndex = 1;
            BAgregar.Text = "Agregar";
            BAgregar.UseVisualStyleBackColor = false;
            BAgregar.Click += BAgregar_Click;
            // 
            // BModificar
            // 
            BModificar.BackColor = Color.FromArgb(39, 174, 96);
            BModificar.FlatAppearance.BorderSize = 0;
            BModificar.FlatStyle = FlatStyle.Flat;
            BModificar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BModificar.ForeColor = Color.White;
            BModificar.Location = new Point(380, 500);
            BModificar.Margin = new Padding(4);
            BModificar.Name = "BModificar";
            BModificar.Size = new Size(180, 50);
            BModificar.TabIndex = 2;
            BModificar.Text = "Modificar";
            BModificar.UseVisualStyleBackColor = false;
            BModificar.Click += BModificar_Click;
            // 
            // BEliminar
            // 
            BEliminar.BackColor = Color.FromArgb(192, 57, 43);
            BEliminar.FlatAppearance.BorderSize = 0;
            BEliminar.FlatStyle = FlatStyle.Flat;
            BEliminar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BEliminar.ForeColor = Color.White;
            BEliminar.Location = new Point(740, 500);
            BEliminar.Margin = new Padding(4);
            BEliminar.Name = "BEliminar";
            BEliminar.Size = new Size(180, 50);
            BEliminar.TabIndex = 3;
            BEliminar.Text = "Eliminar";
            BEliminar.UseVisualStyleBackColor = false;
            BEliminar.Click += BEliminar_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label6);
            panel1.Controls.Add(BEliminar);
            panel1.Controls.Add(BModificar);
            panel1.Controls.Add(BAgregar);
            panel1.Controls.Add(dataGridView1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(350, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(950, 600);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(52, 73, 94);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(lblTitulo);
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
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Bottom;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(0, 500);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(350, 100);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Sistema Automotors";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(52, 73, 94);
            label6.Location = new Point(0, 0);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(950, 60);
            label6.TabIndex = 13;
            label6.Text = "Gestión de Usuarios";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            ClientSize = new Size(1300, 600);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Usuarios - Automotors";
            Load += FrmUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button BAgregar;
        private Button BModificar;
        private Button BEliminar;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label lblTitulo;
        private Label label6;
    }
}