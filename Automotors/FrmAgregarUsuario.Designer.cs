using System.Drawing;
using System.Windows.Forms;

namespace Automotors
{
    partial class FrmAgregarUsuario
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox TNombre;
        private TextBox TApellido;
        private TextBox TUsuario;
        private TextBox TContraseña;
        private ComboBox CRol;
        private Button BGuardar;
        private Button BCancelar;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label lblTitulo;     // izquierda
        private Label label6;        // encabezado derecha (NO se corta ahora)
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private CheckBox CheckContraseña;
        private TextBox TDNI;
        private Label label7;
        private Button btnCambiarContraseña;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAgregarUsuario));
            this.TNombre = new TextBox();
            this.TApellido = new TextBox();
            this.TUsuario = new TextBox();
            this.TContraseña = new TextBox();
            this.CRol = new ComboBox();
            this.BGuardar = new Button();
            this.BCancelar = new Button();
            this.panel1 = new Panel();
            this.btnCambiarContraseña = new Button();
            this.label7 = new Label();
            this.TDNI = new TextBox();
            this.label6 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.CheckContraseña = new CheckBox();
            this.panel2 = new Panel();
            this.pictureBox1 = new PictureBox();
            this.lblTitulo = new Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // --------------------- PANEL IZQUIERDO (oculto en modo popup) ---------------------
            this.panel2.BackColor = Color.FromArgb(52, 73, 94);
            this.panel2.Dock = DockStyle.Left;
            this.panel2.Size = new Size(350, 560);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.lblTitulo);
            this.panel2.Visible = false;                 // <<< lo dejamos oculto para popup

            this.pictureBox1.Dock = DockStyle.Top;
            this.pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            this.pictureBox1.Size = new Size(350, 240);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            this.lblTitulo.Dock = DockStyle.Bottom;
            this.lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.White;
            this.lblTitulo.Height = 72;
            this.lblTitulo.Text = "Sistema Autom";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // --------------------- PANEL DERECHO (contenido) ---------------------
            this.panel1.BackColor = Color.White;
            this.panel1.Dock = DockStyle.Fill;           // llena el form (como popup)
            this.panel1.Padding = new Padding(0);
            this.panel1.Controls.Add(this.btnCambiarContraseña);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.TDNI);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.BCancelar);
            this.panel1.Controls.Add(this.CheckContraseña);
            this.panel1.Controls.Add(this.TUsuario);
            this.panel1.Controls.Add(this.BGuardar);
            this.panel1.Controls.Add(this.TNombre);
            this.panel1.Controls.Add(this.CRol);
            this.panel1.Controls.Add(this.TApellido);
            this.panel1.Controls.Add(this.TContraseña);

            // --------------------- ENCABEZADO (título) ---------------------
            this.label6.AutoSize = false;                // <<< clave: no autosize
            this.label6.Dock = DockStyle.Top;            // ocupa todo el ancho
            this.label6.Height = 56;
            this.label6.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.label6.ForeColor = Color.FromArgb(52, 73, 94);
            this.label6.TextAlign = ContentAlignment.MiddleCenter;
            this.label6.Text = "Agregar Nuevo Usuario";   // cambiar en runtime a "Modificar Usuario" si aplica
            this.label6.Padding = new Padding(0, 6, 0, 0);

            // --------------------- LABELS ---------------------
            this.label1.AutoSize = true; this.label1.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.label1.Location = new Point(30, 80); this.label1.Text = "Nombre:";
            this.label2.AutoSize = true; this.label2.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.label2.Location = new Point(30, 130); this.label2.Text = "Apellido:";
            this.label7.AutoSize = true; this.label7.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.label7.Location = new Point(30, 180); this.label7.Text = "DNI:";
            this.label3.AutoSize = true; this.label3.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.label3.Location = new Point(30, 230); this.label3.Text = "Email:";
            this.label4.AutoSize = true; this.label4.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.label4.Location = new Point(30, 280); this.label4.Text = "Contraseña:";
            this.label5.AutoSize = true; this.label5.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.label5.Location = new Point(30, 330); this.label5.Text = "Rol:";

            // --------------------- INPUTS ---------------------
            int X = 150, W = 280, H = 27, GAP = 50;

            this.TNombre.BackColor = Color.WhiteSmoke; this.TNombre.BorderStyle = BorderStyle.FixedSingle;
            this.TNombre.Font = new Font("Segoe UI", 11F);
            this.TNombre.Location = new Point(X, 78); this.TNombre.Size = new Size(W, H);

            this.TApellido.BackColor = Color.WhiteSmoke; this.TApellido.BorderStyle = BorderStyle.FixedSingle;
            this.TApellido.Font = new Font("Segoe UI", 11F);
            this.TApellido.Location = new Point(X, 128); this.TApellido.Size = new Size(W, H);

            this.TDNI.BackColor = Color.WhiteSmoke; this.TDNI.BorderStyle = BorderStyle.FixedSingle;
            this.TDNI.Font = new Font("Segoe UI", 11F);
            this.TDNI.Location = new Point(X, 178); this.TDNI.Size = new Size(W, H);

            this.TUsuario.BackColor = Color.WhiteSmoke; this.TUsuario.BorderStyle = BorderStyle.FixedSingle;
            this.TUsuario.Font = new Font("Segoe UI", 11F);
            this.TUsuario.Location = new Point(X, 228); this.TUsuario.Size = new Size(W, H);

            this.TContraseña.BackColor = Color.WhiteSmoke; this.TContraseña.BorderStyle = BorderStyle.FixedSingle;
            this.TContraseña.Font = new Font("Segoe UI", 11F);
            this.TContraseña.Location = new Point(X, 278); this.TContraseña.PasswordChar = '•';
            this.TContraseña.Size = new Size(W, H);

            this.CRol.BackColor = Color.WhiteSmoke;
            this.CRol.DropDownStyle = ComboBoxStyle.DropDownList;
            this.CRol.FlatStyle = FlatStyle.Flat;
            this.CRol.Font = new Font("Segoe UI", 11F);
            this.CRol.Items.AddRange(new object[] { "Administrador", "Gerente", "Vendedor" });
            this.CRol.Location = new Point(X, 328); this.CRol.Size = new Size(W, 28);

            this.CheckContraseña.AutoSize = true;
            this.CheckContraseña.Font = new Font("Segoe UI", 10F);
            this.CheckContraseña.Location = new Point(X + W + 10, 281);
            this.CheckContraseña.Text = "Mostrar";

            // --------------------- BOTONES ---------------------
            this.btnCambiarContraseña.BackColor = Color.FromArgb(52, 152, 219);
            this.btnCambiarContraseña.FlatAppearance.BorderSize = 0;
            this.btnCambiarContraseña.FlatStyle = FlatStyle.Flat;
            this.btnCambiarContraseña.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCambiarContraseña.ForeColor = Color.White;
            this.btnCambiarContraseña.Location = new Point(X + W + 10, 312);
            this.btnCambiarContraseña.Size = new Size(120, 30);
            this.btnCambiarContraseña.Text = "Cambiar";

            this.BGuardar.BackColor = Color.SteelBlue;
            this.BGuardar.FlatAppearance.BorderSize = 0;
            this.BGuardar.FlatStyle = FlatStyle.Flat;
            this.BGuardar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.BGuardar.ForeColor = Color.White;
            this.BGuardar.Location = new Point(150, 390);
            this.BGuardar.Size = new Size(150, 40);
            this.BGuardar.Text = "Guardar";
            this.BGuardar.UseVisualStyleBackColor = false;

            this.BCancelar.BackColor = Color.Gray;
            this.BCancelar.FlatAppearance.BorderSize = 0;
            this.BCancelar.FlatStyle = FlatStyle.Flat;
            this.BCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.BCancelar.ForeColor = Color.White;
            this.BCancelar.Location = new Point(315, 390);
            this.BCancelar.Size = new Size(150, 40);
            this.BCancelar.Text = "Cancelar";
            this.BCancelar.UseVisualStyleBackColor = false;

            // --------------------- FORM (modo popup) ---------------------
            this.AcceptButton = this.BGuardar;
            this.CancelButton = this.BCancelar;
            this.AutoScaleDimensions = new SizeF(9F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new Size(540, 460);  // <<< tamaño chico
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // <<< diálogo
            this.Icon = (Icon)resources.GetObject("$this.Icon");
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;                  // <<< emergente
            this.StartPosition = FormStartPosition.CenterParent; // <<< centrado al padre
            this.Text = "Gestión de Usuarios - Automotors";

            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
