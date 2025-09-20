using System;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmEditarProducto : Form
    {
        public Producto ProductoEditado { get; private set; }

        public FrmEditarProducto() : this(null) { }

        public FrmEditarProducto(Producto producto)
        {
            InitializeComponent();

            if (producto != null)
            {
                txtMarca.Text = producto.Marca;
                txtModelo.Text = producto.Modelo;
                nudAnio.Value = producto.Anio ?? 0; // ✅ CORREGIDO: Manejar valor nulo
                nudPrecio.Value = producto.Precio;
                nudStock.Value = producto.CantidadStock;
                chkEstado.Checked = producto.Estado;
                this.Text = "Modificar Producto";
            }
            else
            {
                this.Text = "Agregar Producto";
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                MessageBox.Show("La marca es obligatoria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMarca.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtModelo.Text))
            {
                MessageBox.Show("El modelo es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtModelo.Focus();
                return;
            }

            ProductoEditado = new Producto
            {
                Marca = txtMarca.Text.Trim(),
                Modelo = txtModelo.Text.Trim(),
                Anio = (int)nudAnio.Value,
                Precio = nudPrecio.Value,
                CantidadStock = (int)nudStock.Value,
                Estado = chkEstado.Checked
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}