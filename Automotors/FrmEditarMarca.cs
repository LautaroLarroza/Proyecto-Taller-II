using System;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmEditarMarca : Form
    {
        public Marca MarcaEditada { get; private set; }

        public FrmEditarMarca() : this(null) { }

        public FrmEditarMarca(Marca marca)
        {
            InitializeComponent();

            if (marca != null)
            {
                txtNombre.Text = marca.Nombre;
                txtDescripcion.Text = marca.Descripcion;
                this.Text = "Modificar Marca";
            }
            else
            {
                this.Text = "Agregar Marca";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre de la marca es obligatorio.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                return;
            }

            MarcaEditada = new Marca
            {
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}