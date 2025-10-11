using System.Drawing;
using System.Windows.Forms;

namespace Automotors
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtUsuario;
        private TextBox txtPassword;
        private Label label1;
        private Label label2;
        private Button btnLogin;
        private Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtUsuario = new TextBox();
            txtPassword = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnLogin = new Button();
            btnCancelar = new Button();

            SuspendLayout();

            // label1 - Usuario
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(25, 25);
            label1.Text = "Usuario:";

            // txtUsuario
            txtUsuario.Location = new Point(110, 22);
            txtUsuario.Size = new Size(160, 23);
            txtUsuario.TabIndex = 0;

            // label2 - Contraseña
            label2.AutoSize = true;
            label2.Location = new Point(25, 62);
            label2.Text = "Contraseña:";

            // txtPassword
            txtPassword.Location = new Point(110, 59);
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(160, 23);
            txtPassword.TabIndex = 1;

            // btnLogin
            btnLogin.Text = "Ingresar";
            btnLogin.BackColor = Color.SteelBlue;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Location = new Point(60, 100);
            btnLogin.Size = new Size(95, 30);
            btnLogin.TabIndex = 2;
            btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // btnCancelar
            btnCancelar.Text = "Cancelar";
            btnCancelar.BackColor = Color.Gray;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Location = new Point(155, 100);
            btnCancelar.Size = new Size(95, 30);
            btnCancelar.TabIndex = 3;
            btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);

            // FrmLogin
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 150);
            Controls.Add(btnCancelar);
            Controls.Add(btnLogin);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPassword);
            Controls.Add(txtUsuario);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Iniciar Sesión";
            FormClosed += new FormClosedEventHandler(this.FrmLogin_FormClosed);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
