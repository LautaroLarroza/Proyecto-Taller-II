using System;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmAgregarUsuario : Form
    {
        private FrmUsuarios formPadre; // referencia al formulario de usuarios

        // Constructor modificado para recibir el formulario padre
        public FrmAgregarUsuario(FrmUsuarios padre)
        {
            InitializeComponent();

            formPadre = padre;

            // Configurar eventos de los controles existentes
            CheckContraseña.CheckedChanged += CheckContraseña_CheckedChanged;
            BGuardar.Click += BGuardar_Click;

            // Inicializar contraseña oculta
            TContraseña.PasswordChar = '*';

            // Configurar roles
            CRol.Items.Clear();
            CRol.Items.Add("Vendedor");
            CRol.Items.Add("Administrador");
            CRol.Items.Add("Gerente");
            CRol.SelectedIndex = 0; // Selecciona por defecto la primera opción
        }

        // ====================
        // Propiedades públicas
        // ====================
        public string Nombre
        {
            get => TNombre.Text;
            set => TNombre.Text = value;
        }

        public string Apellido
        {
            get => TApellido.Text;
            set => TApellido.Text = value;
        }

        public string Usuario
        {
            get => TUsuario.Text;
            set => TUsuario.Text = value;
        }

        public string Contraseña
        {
            get => TContraseña.Text;
            set => TContraseña.Text = value;
        }

        public string Rol
        {
            get => CRol.Text;
            set => CRol.Text = value;
        }

        // Mostrar/ocultar contraseña
        private void CheckContraseña_CheckedChanged(object? sender, EventArgs e)
        {
            TContraseña.PasswordChar = CheckContraseña.Checked ? '\0' : '*';
        }

        // Botón Guardar
        private void BGuardar_Click(object? sender, EventArgs e)
        {
            // Validar que se hayan ingresado todos los campos
            if (string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Apellido) ||
                string.IsNullOrWhiteSpace(Usuario) ||
                string.IsNullOrWhiteSpace(Contraseña))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            // Validar que se haya seleccionado un rol
            if (string.IsNullOrEmpty(Rol))
            {
                MessageBox.Show("Debe seleccionar un rol.");
                return;
            }

            // Agregar la fila al DataGridView de FrmUsuarios
            formPadre.AgregarUsuarioANuevaFila(Nombre, Apellido, Usuario, Rol);

            MessageBox.Show($"Usuario guardado:\n{Nombre} {Apellido} ({Usuario}) - {Rol}");

            this.Close(); // cerramos el formulario después de guardar
        }

        // Eventos opcionales del diseñador, se pueden dejar vacíos
        private void FrmAgregarUsuario_Load(object? sender, EventArgs e) { }
        private void panel1_Paint(object? sender, PaintEventArgs e) { }
        private void checkBox1_CheckedChanged(object? sender, EventArgs e) { }
    }
}
