using System;
using System.Windows.Forms;

namespace Automotors
{
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();

            // Para que, si la ventana queda muy chica, aparezca barra de scroll
            // (no hace falta usar ruedita: también funciona con la barra).
            this.AutoScroll = true;

            // Un piso razonable de tamaño para que no “rompa” el layout.
            this.MinimumSize = new System.Drawing.Size(1000, 540);
        }

        // ==== Handlers que referencia el Designer ====

        private void FrmVentas_Load(object? sender, EventArgs e)
        {
            // Si no querés lógica todavía, lo dejamos vacío.
        }

        private void cboProducto_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Al cambiar producto podrías actualizar precio/stock; por ahora vacío.
        }

        private void RecalcularTotal(object? sender, EventArgs e)
        {
            // Ejemplo mínimo (opcional). Si preferís, dejalo vacío.
            // decimal.TryParse(txtPrecioUnitario.Text, out var p);
            // var qty = (int)nudCantidad.Value;
            // txtTotal.Text = (p * qty).ToString("0.##");
        }

        private void txtObs_TextChanged(object? sender, EventArgs e)
        {
            // Nada por ahora.
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            // Nada por ahora.
        }

        private void tlp_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
