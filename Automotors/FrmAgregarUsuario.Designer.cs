namespace Automotors
{
    partial class FrmAgregarUsuario
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
            TNombre = new TextBox();
            TApellido = new TextBox();
            TContraseña = new TextBox();
            CRol = new ComboBox();
            BGuardar = new Button();
            panel1 = new Panel();
            CheckContraseña = new CheckBox();
            TUsuario = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // TNombre
            // 
            TNombre.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TNombre.Location = new Point(18, 65);
            TNombre.Name = "TNombre";
            TNombre.Size = new Size(150, 23);
            TNombre.TabIndex = 0;
            // 
            // TApellido
            // 
            TApellido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TApellido.Location = new Point(271, 65);
            TApellido.Name = "TApellido";
            TApellido.Size = new Size(150, 23);
            TApellido.TabIndex = 1;
            // 
            // TContraseña
            // 
            TContraseña.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TContraseña.Location = new Point(271, 128);
            TContraseña.Name = "TContraseña";
            TContraseña.PasswordChar = '*';
            TContraseña.Size = new Size(150, 23);
            TContraseña.TabIndex = 2;
            // 
            // CRol
            // 
            CRol.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            CRol.FormattingEnabled = true;
            CRol.Location = new Point(519, 65);
            CRol.Name = "CRol";
            CRol.Size = new Size(150, 23);
            CRol.TabIndex = 3;
            // 
            // BGuardar
            // 
            BGuardar.Location = new Point(519, 278);
            BGuardar.Name = "BGuardar";
            BGuardar.Size = new Size(150, 50);
            BGuardar.TabIndex = 4;
            BGuardar.Text = "Guardar";
            BGuardar.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(CheckContraseña);
            panel1.Controls.Add(BGuardar);
            panel1.Controls.Add(TUsuario);
            panel1.Controls.Add(TNombre);
            panel1.Controls.Add(CRol);
            panel1.Controls.Add(TApellido);
            panel1.Controls.Add(TContraseña);
            panel1.Location = new Point(187, 51);
            panel1.Name = "panel1";
            panel1.Size = new Size(750, 450);
            panel1.TabIndex = 5;
            panel1.Paint += panel1_Paint;
            // 
            // CheckContraseña
            // 
            CheckContraseña.AutoSize = true;
            CheckContraseña.Location = new Point(427, 132);
            CheckContraseña.Name = "CheckContraseña";
            CheckContraseña.Size = new Size(130, 19);
            CheckContraseña.TabIndex = 5;
            CheckContraseña.Text = "Mostrar Contraseña";
            CheckContraseña.UseVisualStyleBackColor = true;
            CheckContraseña.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // TUsuario
            // 
            TUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TUsuario.Location = new Point(18, 128);
            TUsuario.Name = "TUsuario";
            TUsuario.Size = new Size(150, 23);
            TUsuario.TabIndex = 4;
            // 
            // FrmAgregarUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1493, 648);
            Controls.Add(panel1);
            Name = "FrmAgregarUsuario";
            Text = "FrmAgregarUsuario";
            Load += FrmAgregarUsuario_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
    }
}