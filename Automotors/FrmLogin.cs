using System;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmLogin : Form
    {
        // Datos estáticos de usuarios (simulando una base de datos)
        private readonly (string usuario, string password, string tipo)[] usuarios =
        {
            ("admin", "admin123", "Administrador"),
            ("gerente", "gerente123", "Gerente"),
            ("vendedor", "vendedor123", "Vendedor")
        };

        public static string UsuarioLogueado { get; private set; }
        public static string TipoUsuario { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verificar credenciales
            foreach (var user in usuarios)
            {
                if (user.usuario == usuario && user.password == password)
                {
                    UsuarioLogueado = usuario;
                    TipoUsuario = user.tipo;

                    // Abrir formulario principal
                    Form1 formPrincipal = new Form1();
                    formPrincipal.Show();
                    this.Hide(); // Ocultar login
                    return;
                }
            }

            MessageBox.Show("Usuario o contraseña incorrectos", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}