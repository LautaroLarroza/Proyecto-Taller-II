using System.Drawing;
using System.Windows.Forms;

namespace Automotors
{
    partial class FrmAgregarUsuario
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitulo;
        private Label labelNombre;
        private Label labelApellido;
        private Label labelDni;
        private Label labelEmail;
        private Label labelContraseña;
        private Label labelRol;
        private TextBox TNombre;
        private TextBox TApellido;
        private TextBox TDNI;
        private TextBox TUsuario;
        private TextBox TContraseña;
        private ComboBox CRol;
        private CheckBox CheckContraseña;
        private Button BGuardar;
        private Button BCancelar;
        private Button btnCambiarContraseña;
        private Panel headerPanel;
        private Panel contentPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblTitulo = new Label();
            labelNombre = new Label();
            labelApellido = new Label();
            labelDni = new Label();
            labelEmail = new Label();
            labelContraseña = new Label();
            labelRol = new Label();

            TNombre = new TextBox();
            TApellido = new TextBox();
            TDNI = new TextBox();
            TUsuario = new TextBox();
            TContraseña = new TextBox();

            CRol = new ComboBox();
            CheckContraseña = new CheckBox();
            BGuardar = new Button();
            BCancelar = new Button();
            btnCambiarContraseña = new Button();

            headerPanel = new Panel();
            contentPanel = new Panel();

            SuspendLayout();

            // ===== HEADER =====
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 60;
            headerPanel.BackColor = Color.FromArgb(41, 128, 185);
            headerPanel.Padding = new Padding(0);
            headerPanel.Margin = new Padding(0);

            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTitulo.Text = "Agregar Usuario";
            headerPanel.Controls.Add(lblTitulo);

            // ===== CONTENT =====
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.White;
            contentPanel.Padding = new Padding(30);

            int labelWidth = 120;
            int inputWidth = 250;
            int startY = 20;
            int spacingY = 45;

            // Nombre
            labelNombre.Text = "Nombre:";
            labelNombre.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            labelNombre.Location = new Point(20, startY);
            labelNombre.Size = new Size(labelWidth, 25);

            TNombre.Font = new Font("Segoe UI", 10.5F);
            TNombre.BackColor = Color.WhiteSmoke;
            TNombre.BorderStyle = BorderStyle.FixedSingle;
            TNombre.Location = new Point(150, startY);
            TNombre.Size = new Size(inputWidth, 27);

            // Apellido
            labelApellido.Text = "Apellido:";
            labelApellido.Font = labelNombre.Font;
            labelApellido.Location = new Point(20, startY + spacingY);
            labelApellido.Size = labelNombre.Size;

            TApellido.Font = TNombre.Font;
            TApellido.BackColor = Color.WhiteSmoke;
            TApellido.BorderStyle = BorderStyle.FixedSingle;
            TApellido.Location = new Point(150, startY + spacingY);
            TApellido.Size = TNombre.Size;

            // DNI
            labelDni.Text = "DNI:";
            labelDni.Font = labelNombre.Font;
            labelDni.Location = new Point(20, startY + spacingY * 2);
            labelDni.Size = labelNombre.Size;

            TDNI.Font = TNombre.Font;
            TDNI.BackColor = Color.WhiteSmoke;
            TDNI.BorderStyle = BorderStyle.FixedSingle;
            TDNI.Location = new Point(150, startY + spacingY * 2);
            TDNI.Size = TNombre.Size;

            // Email
            labelEmail.Text = "Email:";
            labelEmail.Font = labelNombre.Font;
            labelEmail.Location = new Point(20, startY + spacingY * 3);
            labelEmail.Size = labelNombre.Size;

            TUsuario.Font = TNombre.Font;
            TUsuario.BackColor = Color.WhiteSmoke;
            TUsuario.BorderStyle = BorderStyle.FixedSingle;
            TUsuario.Location = new Point(150, startY + spacingY * 3);
            TUsuario.Size = TNombre.Size;

            // Contraseña
            labelContraseña.Text = "Contraseña:";
            labelContraseña.Font = labelNombre.Font;
            labelContraseña.Location = new Point(20, startY + spacingY * 4);
            labelContraseña.Size = labelNombre.Size;

            TContraseña.Font = TNombre.Font;
            TContraseña.BackColor = Color.WhiteSmoke;
            TContraseña.BorderStyle = BorderStyle.FixedSingle;
            TContraseña.Location = new Point(150, startY + spacingY * 4);
            TContraseña.Size = TNombre.Size;
            TContraseña.PasswordChar = '•';

            CheckContraseña.Text = "Mostrar";
            CheckContraseña.Font = new Font("Segoe UI", 9F);
            CheckContraseña.Location = new Point(410, startY + spacingY * 4 + 3);
            CheckContraseña.AutoSize = true;

            // Botón cambiar contraseña (solo visible en edición)
            btnCambiarContraseña.Text = "Cambiar Contraseña";
            btnCambiarContraseña.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCambiarContraseña.BackColor = Color.FromArgb(52, 152, 219);
            btnCambiarContraseña.ForeColor = Color.White;
            btnCambiarContraseña.FlatStyle = FlatStyle.Flat;
            btnCambiarContraseña.FlatAppearance.BorderSize = 0;
            btnCambiarContraseña.Size = new Size(160, 28);
            btnCambiarContraseña.Location = new Point(150, startY + spacingY * 5);
            btnCambiarContraseña.Visible = false;

            // Rol
            labelRol.Text = "Rol:";
            labelRol.Font = labelNombre.Font;
            labelRol.Location = new Point(20, startY + spacingY * 6);
            labelRol.Size = labelNombre.Size;

            CRol.Font = TNombre.Font;
            CRol.DropDownStyle = ComboBoxStyle.DropDownList;
            CRol.FlatStyle = FlatStyle.Flat;
            CRol.BackColor = Color.WhiteSmoke;
            CRol.Items.AddRange(new object[] { "Administrador", "Gerente", "Vendedor" });
            CRol.Location = new Point(150, startY + spacingY * 6);
            CRol.Size = new Size(inputWidth, 28);

            // Botones
            BGuardar.Text = "Guardar";
            BGuardar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            BGuardar.ForeColor = Color.White;
            BGuardar.BackColor = Color.FromArgb(46, 134, 193);
            BGuardar.FlatStyle = FlatStyle.Flat;
            BGuardar.FlatAppearance.BorderSize = 0;
            BGuardar.Size = new Size(120, 35);
            BGuardar.Location = new Point(100, startY + spacingY * 8);
            BGuardar.Cursor = Cursors.Hand;

            BCancelar.Text = "Cancelar";
            BCancelar.Font = BGuardar.Font;
            BCancelar.ForeColor = Color.White;
            BCancelar.BackColor = Color.Gray;
            BCancelar.FlatStyle = FlatStyle.Flat;
            BCancelar.FlatAppearance.BorderSize = 0;
            BCancelar.Size = BGuardar.Size;
            BCancelar.Location = new Point(240, startY + spacingY * 8);
            BCancelar.Cursor = Cursors.Hand;

            // Añadir controles al contentPanel
            contentPanel.Controls.Add(labelNombre);
            contentPanel.Controls.Add(labelApellido);
            contentPanel.Controls.Add(labelDni);
            contentPanel.Controls.Add(labelEmail);
            contentPanel.Controls.Add(labelContraseña);
            contentPanel.Controls.Add(labelRol);
            contentPanel.Controls.Add(TNombre);
            contentPanel.Controls.Add(TApellido);
            contentPanel.Controls.Add(TDNI);
            contentPanel.Controls.Add(TUsuario);
            contentPanel.Controls.Add(TContraseña);
            contentPanel.Controls.Add(CRol);
            contentPanel.Controls.Add(CheckContraseña);
            contentPanel.Controls.Add(BGuardar);
            contentPanel.Controls.Add(BCancelar);
            contentPanel.Controls.Add(btnCambiarContraseña);

            // ===== FORM =====
            AcceptButton = BGuardar;
            CancelButton = BCancelar;
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(520, 520);
            Controls.Add(contentPanel);
            Controls.Add(headerPanel);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAgregarUsuario";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestión de Usuarios";

            ResumeLayout(false);
        }
    }
}
