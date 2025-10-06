namespace Automotors
{
    partial class FrmAgregarUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgregarUsuario));
            TNombre = new TextBox();
            TApellido = new TextBox();
            TContraseña = new TextBox();
            CRol = new ComboBox();
            BGuardar = new Button();
            panel1 = new Panel();
            btnCambiarContraseña = new Button();
            label7 = new Label();
            TDNI = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            BCancelar = new Button();
            CheckContraseña = new CheckBox();
            TUsuario = new TextBox();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            lblTitulo = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // TNombre
            // 
            TNombre.BackColor = Color.WhiteSmoke;
            TNombre.BorderStyle = BorderStyle.FixedSingle;
            TNombre.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TNombre.Location = new Point(180, 120);
            TNombre.Margin = new Padding(4);
            TNombre.Name = "TNombre";
            TNombre.Size = new Size(250, 32);
            TNombre.TabIndex = 1;
            // 
            // TApellido
            // 
            TApellido.BackColor = Color.WhiteSmoke;
            TApellido.BorderStyle = BorderStyle.FixedSingle;
            TApellido.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TApellido.Location = new Point(180, 180);
            TApellido.Margin = new Padding(4);
            TApellido.Name = "TApellido";
            TApellido.Size = new Size(250, 32);
            TApellido.TabIndex = 2;
            // 
            // TContraseña
            // 
            TContraseña.BackColor = Color.WhiteSmoke;
            TContraseña.BorderStyle = BorderStyle.FixedSingle;
            TContraseña.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TContraseña.Location = new Point(180, 360);
            TContraseña.Margin = new Padding(4);
            TContraseña.Name = "TContraseña";
            TContraseña.PasswordChar = '•';
            TContraseña.Size = new Size(250, 32);
            TContraseña.TabIndex = 5;
            // 
            // CRol
            // 
            CRol.BackColor = Color.WhiteSmoke;
            CRol.DropDownStyle = ComboBoxStyle.DropDownList;
            CRol.FlatStyle = FlatStyle.Flat;
            CRol.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CRol.FormattingEnabled = true;
            CRol.Items.AddRange(new object[] { "Administrador", "Gerente", "Vendedor" });
            CRol.Location = new Point(180, 420);
            CRol.Margin = new Padding(4);
            CRol.Name = "CRol";
            CRol.Size = new Size(250, 33);
            CRol.TabIndex = 6;
            // 
            // BGuardar
            // 
            BGuardar.BackColor = Color.FromArgb(41, 128, 185);
            BGuardar.FlatAppearance.BorderSize = 0;
            BGuardar.FlatStyle = FlatStyle.Flat;
            BGuardar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BGuardar.ForeColor = Color.White;
            BGuardar.Location = new Point(180, 480);
            BGuardar.Margin = new Padding(4);
            BGuardar.Name = "BGuardar";
            BGuardar.Size = new Size(180, 50);
            BGuardar.TabIndex = 8;
            BGuardar.Text = "Guardar";
            BGuardar.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnCambiarContraseña);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(TDNI);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(BCancelar);
            panel1.Controls.Add(CheckContraseña);
            panel1.Controls.Add(TUsuario);
            panel1.Controls.Add(BGuardar);
            panel1.Controls.Add(TNombre);
            panel1.Controls.Add(CRol);
            panel1.Controls.Add(TApellido);
            panel1.Controls.Add(TContraseña);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(350, 0);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(650, 600);
            panel1.TabIndex = 5;
            // 
            // btnCambiarContraseña
            // 
            btnCambiarContraseña.BackColor = Color.FromArgb(52, 152, 219);
            btnCambiarContraseña.FlatAppearance.BorderSize = 0;
            btnCambiarContraseña.FlatStyle = FlatStyle.Flat;
            btnCambiarContraseña.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCambiarContraseña.ForeColor = Color.White;
            btnCambiarContraseña.Location = new Point(440, 355);
            btnCambiarContraseña.Margin = new Padding(4);
            btnCambiarContraseña.Name = "btnCambiarContraseña";
            btnCambiarContraseña.Size = new Size(180, 40);
            btnCambiarContraseña.TabIndex = 15;
            btnCambiarContraseña.Text = "Cambiar Contraseña";
            btnCambiarContraseña.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(180, 210);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(49, 25);
            label7.TabIndex = 14;
            label7.Text = "DNI:";
            // 
            // TDNI
            // 
            TDNI.BackColor = Color.WhiteSmoke;
            TDNI.BorderStyle = BorderStyle.FixedSingle;
            TDNI.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TDNI.Location = new Point(180, 240);
            TDNI.Margin = new Padding(4);
            TDNI.Name = "TDNI";
            TDNI.Size = new Size(250, 32);
            TDNI.TabIndex = 3;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Top;
            label6.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(52, 73, 94);
            label6.Location = new Point(0, 0);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(650, 60);
            label6.TabIndex = 12;
            label6.Text = "Agregar Nuevo Usuario";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(180, 390);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(46, 25);
            label5.TabIndex = 11;
            label5.Text = "Rol:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(180, 330);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(118, 25);
            label4.TabIndex = 10;
            label4.Text = "Contraseña:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(180, 270);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(85, 25);
            label3.TabIndex = 9;
            label3.Text = "Email:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(180, 150);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(91, 25);
            label2.TabIndex = 8;
            label2.Text = "Apellido:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(180, 90);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(91, 25);
            label1.TabIndex = 7;
            label1.Text = "Nombre:";
            // 
            // BCancelar
            // 
            BCancelar.BackColor = Color.FromArgb(192, 57, 43);
            BCancelar.FlatAppearance.BorderSize = 0;
            BCancelar.FlatStyle = FlatStyle.Flat;
            BCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BCancelar.ForeColor = Color.White;
            BCancelar.Location = new Point(370, 480);
            BCancelar.Margin = new Padding(4);
            BCancelar.Name = "BCancelar";
            BCancelar.Size = new Size(180, 50);
            BCancelar.TabIndex = 9;
            BCancelar.Text = "Cancelar";
            BCancelar.UseVisualStyleBackColor = false;
            // 
            // CheckContraseña
            // 
            CheckContraseña.AutoSize = true;
            CheckContraseña.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CheckContraseña.Location = new Point(440, 405);
            CheckContraseña.Margin = new Padding(4);
            CheckContraseña.Name = "CheckContraseña";
            CheckContraseña.Size = new Size(183, 27);
            CheckContraseña.TabIndex = 7;
            CheckContraseña.Text = "Mostrar Contraseña";
            CheckContraseña.UseVisualStyleBackColor = true;
            // 
            // TUsuario
            // 
            TUsuario.BackColor = Color.WhiteSmoke;
            TUsuario.BorderStyle = BorderStyle.FixedSingle;
            TUsuario.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TUsuario.Location = new Point(180, 300);
            TUsuario.Margin = new Padding(4);
            TUsuario.Name = "TUsuario";
            TUsuario.Size = new Size(250, 32);
            TUsuario.TabIndex = 4;
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
            panel2.TabIndex = 6;
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
            // FrmAgregarUsuario
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(236, 240, 241);
            ClientSize = new Size(1000, 600);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAgregarUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Usuarios - Automotors";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox TNombre;
        private TextBox TApellido;
        private TextBox TContraseña;
        private ComboBox CRol;
        private Button BGuardar;
        private Panel panel1;
        private TextBox TUsuario;
        private CheckBox CheckContraseña;
        private Button BCancelar;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label6;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label lblTitulo;
        private Label label7;
        private TextBox TDNI;
        private Button btnCambiarContraseña;
    }
}