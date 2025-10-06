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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BAgregar = new System.Windows.Forms.Button();
            this.BModificar = new System.Windows.Forms.Button();
            this.BEliminar = new System.Windows.Forms.Button();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.BRoles = new System.Windows.Forms.Button();
            this.lblTituloPrincipal = new System.Windows.Forms.Label();
            this.panelLateral = new System.Windows.Forms.Panel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.lblTituloLateral = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelContenido.SuspendLayout();
            this.panelLateral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 80);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(890, 400);
            this.dataGridView1.TabIndex = 0;
            // 
            // BAgregar
            // 
            this.BAgregar.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.BAgregar.FlatAppearance.BorderSize = 0;
            this.BAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BAgregar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.BAgregar.ForeColor = System.Drawing.Color.White;
            this.BAgregar.Location = new System.Drawing.Point(30, 500);
            this.BAgregar.Margin = new System.Windows.Forms.Padding(4);
            this.BAgregar.Name = "BAgregar";
            this.BAgregar.Size = new System.Drawing.Size(180, 50);
            this.BAgregar.TabIndex = 1;
            this.BAgregar.Text = "Agregar";
            this.BAgregar.UseVisualStyleBackColor = false;
            this.BAgregar.Click += new System.EventHandler(this.BAgregar_Click);
            // 
            // BModificar
            // 
            this.BModificar.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.BModificar.FlatAppearance.BorderSize = 0;
            this.BModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BModificar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.BModificar.ForeColor = System.Drawing.Color.White;
            this.BModificar.Location = new System.Drawing.Point(220, 500);
            this.BModificar.Margin = new System.Windows.Forms.Padding(4);
            this.BModificar.Name = "BModificar";
            this.BModificar.Size = new System.Drawing.Size(180, 50);
            this.BModificar.TabIndex = 2;
            this.BModificar.Text = "Modificar";
            this.BModificar.UseVisualStyleBackColor = false;
            this.BModificar.Click += new System.EventHandler(this.BModificar_Click);
            // 
            // BEliminar
            // 
            this.BEliminar.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.BEliminar.FlatAppearance.BorderSize = 0;
            this.BEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BEliminar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.BEliminar.ForeColor = System.Drawing.Color.White;
            this.BEliminar.Location = new System.Drawing.Point(410, 500);
            this.BEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.BEliminar.Name = "BEliminar";
            this.BEliminar.Size = new System.Drawing.Size(180, 50);
            this.BEliminar.TabIndex = 3;
            this.BEliminar.Text = "Eliminar";
            this.BEliminar.UseVisualStyleBackColor = false;
            this.BEliminar.Click += new System.EventHandler(this.BEliminar_Click);
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.White;
            this.panelContenido.Controls.Add(this.BRoles);
            this.panelContenido.Controls.Add(this.lblTituloPrincipal);
            this.panelContenido.Controls.Add(this.BEliminar);
            this.panelContenido.Controls.Add(this.BModificar);
            this.panelContenido.Controls.Add(this.BAgregar);
            this.panelContenido.Controls.Add(this.dataGridView1);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(350, 0);
            this.panelContenido.Margin = new System.Windows.Forms.Padding(4);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(950, 600);
            this.panelContenido.TabIndex = 4;
            // 
            // BRoles
            // 
            this.BRoles.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.BRoles.FlatAppearance.BorderSize = 0;
            this.BRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BRoles.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.BRoles.ForeColor = System.Drawing.Color.White;
            this.BRoles.Location = new System.Drawing.Point(600, 500);
            this.BRoles.Margin = new System.Windows.Forms.Padding(4);
            this.BRoles.Name = "BRoles";
            this.BRoles.Size = new System.Drawing.Size(180, 50);
            this.BRoles.TabIndex = 14;
            this.BRoles.Text = "Gestión de Roles";
            this.BRoles.UseVisualStyleBackColor = false;
            this.BRoles.Click += new System.EventHandler(this.BRoles_Click);
            // 
            // lblTituloPrincipal
            // 
            this.lblTituloPrincipal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloPrincipal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.lblTituloPrincipal.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.lblTituloPrincipal.Location = new System.Drawing.Point(0, 0);
            this.lblTituloPrincipal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTituloPrincipal.Name = "lblTituloPrincipal";
            this.lblTituloPrincipal.Size = new System.Drawing.Size(950, 60);
            this.lblTituloPrincipal.TabIndex = 13;
            this.lblTituloPrincipal.Text = "Gestión de Usuarios";
            this.lblTituloPrincipal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLateral
            // 
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            this.panelLateral.Controls.Add(this.pictureBoxLogo);
            this.panelLateral.Controls.Add(this.lblTituloLateral);
            this.panelLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLateral.Location = new System.Drawing.Point(0, 0);
            this.panelLateral.Margin = new System.Windows.Forms.Padding(4);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(350, 600);
            this.panelLateral.TabIndex = 5;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(350, 300);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 1;
            this.pictureBoxLogo.TabStop = false;
            // 
            // lblTituloLateral
            // 
            this.lblTituloLateral.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTituloLateral.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            this.lblTituloLateral.ForeColor = System.Drawing.Color.White;
            this.lblTituloLateral.Location = new System.Drawing.Point(0, 500);
            this.lblTituloLateral.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTituloLateral.Name = "lblTituloLateral";
            this.lblTituloLateral.Size = new System.Drawing.Size(350, 100);
            this.lblTituloLateral.TabIndex = 0;
            this.lblTituloLateral.Text = "Sistema Automotors";
            this.lblTituloLateral.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(1300, 600);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelLateral);
            this.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUsuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Usuarios - Automotors";
            this.Load += new System.EventHandler(this.FrmUsuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelContenido.ResumeLayout(false);
            this.panelLateral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BAgregar;
        private System.Windows.Forms.Button BModificar;
        private System.Windows.Forms.Button BEliminar;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label lblTituloLateral;
        private System.Windows.Forms.Label lblTituloPrincipal;
        private System.Windows.Forms.Button BRoles;
    }
}
