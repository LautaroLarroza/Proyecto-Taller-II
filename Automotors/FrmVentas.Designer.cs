using System.Drawing;
using System.Windows.Forms;

namespace Automotors
{
    partial class FrmVentas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            grpListado = new GroupBox();
            tlp = new TableLayoutPanel();
            lblCliente = new Label();
            cboCliente = new ComboBox();
            lblProducto = new Label();
            cboProducto = new ComboBox();
            lblServicio = new Label();
            cboServicio = new ComboBox();
            btnGestionarServicios = new Button();
            lblVendedor = new Label();
            cboVendedor = new ComboBox();
            lblCantidad = new Label();
            nudCantidad = new NumericUpDown();
            lblPrecio = new Label();
            txtPrecioUnitario = new TextBox();
            lblTotal = new Label();
            txtTotal = new TextBox();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblPago = new Label();
            cboFormaPago = new ComboBox();
            lblStockDisp = new Label();
            flowAcciones = new FlowLayoutPanel();
            btnGuardar = new Button();
            btnImprimirFactura = new Button();
            btnCancelar = new Button();

            panel1.SuspendLayout();
            grpListado.SuspendLayout();
            tlp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            flowAcciones.SuspendLayout();
            SuspendLayout();

            // panel1
            panel1.BackColor = Color.SteelBlue;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1100, 60);

            // label1
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(20, 18);
            label1.Name = "label1";
            label1.Size = new Size(187, 24);
            label1.Text = "Registro de Ventas";

            // grpListado
            grpListado.AutoSize = true;
            grpListado.Controls.Add(tlp);
            grpListado.Dock = DockStyle.Fill;
            grpListado.Location = new Point(0, 60);
            grpListado.Name = "grpListado";
            grpListado.Padding = new Padding(12);
            grpListado.Size = new Size(1100, 540);
            grpListado.TabIndex = 0;
            grpListado.TabStop = false;
            grpListado.Text = "Ventas";

            // tlp
            tlp.ColumnCount = 4;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp.Dock = DockStyle.Fill;
            tlp.Location = new Point(12, 28);
            tlp.Name = "tlp";
            tlp.RowCount = 8; // REDUCIDO de 9 a 8 filas (quitamos observaciones)
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F)); // fila 0 - cliente
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F)); // fila 1 - producto
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F)); // fila 2 - servicio
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F)); // fila 3 - vendedor
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F)); // fila 4 - cantidad/precio
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F)); // fila 5 - total/fecha
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F)); // fila 6 - forma pago
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F)); // fila 7 - botones/stock
            tlp.Size = new Size(1076, 500);
            tlp.TabIndex = 0;

            // lblCliente
            lblCliente.Location = new Point(3, 0);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(100, 23);
            lblCliente.Text = "Cliente";
            lblCliente.TextAlign = ContentAlignment.MiddleLeft;
            tlp.Controls.Add(lblCliente, 0, 0);

            // cboCliente
            cboCliente.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCliente.Dock = DockStyle.Fill;
            cboCliente.Name = "cboCliente";
            tlp.SetColumnSpan(cboCliente, 3);
            tlp.Controls.Add(cboCliente, 1, 0);

            // lblProducto
            lblProducto.Location = new Point(3, 36);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(100, 23);
            lblProducto.Text = "Producto";
            lblProducto.TextAlign = ContentAlignment.MiddleLeft;
            tlp.Controls.Add(lblProducto, 0, 1);

            // cboProducto
            cboProducto.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProducto.Dock = DockStyle.Fill;
            cboProducto.Name = "cboProducto";
            cboProducto.SelectedIndexChanged += cboProducto_SelectedIndexChanged;
            tlp.SetColumnSpan(cboProducto, 3);
            tlp.Controls.Add(cboProducto, 1, 1);

            // lblServicio
            lblServicio.Location = new Point(3, 72);
            lblServicio.Name = "lblServicio";
            lblServicio.Size = new Size(100, 23);
            lblServicio.Text = "Servicio";
            lblServicio.TextAlign = ContentAlignment.MiddleLeft;
            tlp.Controls.Add(lblServicio, 0, 2);

            // cboServicio
            cboServicio.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboServicio.DropDownStyle = ComboBoxStyle.DropDownList;
            cboServicio.Dock = DockStyle.Fill;
            cboServicio.Name = "cboServicio";
            cboServicio.SelectedIndexChanged += cboServicio_SelectedIndexChanged;
            tlp.SetColumnSpan(cboServicio, 1);
            tlp.Controls.Add(cboServicio, 1, 2);

            // btnGestionarServicios
            btnGestionarServicios.Text = "Gestionar";
            btnGestionarServicios.AutoSize = true;
            btnGestionarServicios.Name = "btnGestionarServicios";
            btnGestionarServicios.Click += btnGestionarServicios_Click;
            tlp.Controls.Add(btnGestionarServicios, 2, 2);

            // lblVendedor
            lblVendedor.Location = new Point(3, 108);
            lblVendedor.Name = "lblVendedor";
            lblVendedor.Size = new Size(100, 23);
            lblVendedor.Text = "Vendedor";
            lblVendedor.TextAlign = ContentAlignment.MiddleLeft;
            tlp.Controls.Add(lblVendedor, 0, 3);

            // cboVendedor
            cboVendedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVendedor.Dock = DockStyle.Fill;
            cboVendedor.Name = "cboVendedor";
            tlp.SetColumnSpan(cboVendedor, 3);
            tlp.Controls.Add(cboVendedor, 1, 3);

            // lblCantidad
            lblCantidad.Location = new Point(3, 144);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(100, 23);
            lblCantidad.Text = "Cantidad";
            lblCantidad.TextAlign = ContentAlignment.MiddleLeft;
            tlp.Controls.Add(lblCantidad, 0, 4);

            // nudCantidad
            nudCantidad.Anchor = AnchorStyles.Left;
            nudCantidad.Location = new Point(123, 150);
            nudCantidad.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(120, 23);
            nudCantidad.Value = new decimal(new int[] { 0, 0, 0, 0 });
            nudCantidad.ValueChanged += RecalcularTotal;
            tlp.Controls.Add(nudCantidad, 1, 4);

            // lblPrecio
            lblPrecio.Location = new Point(541, 144);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(100, 23);
            lblPrecio.Text = "Precio unit.";
            lblPrecio.TextAlign = ContentAlignment.MiddleLeft;
            tlp.Controls.Add(lblPrecio, 2, 4);

            // txtPrecioUnitario
            txtPrecioUnitario.Dock = DockStyle.Fill;
            txtPrecioUnitario.Location = new Point(661, 147);
            txtPrecioUnitario.Name = "txtPrecioUnitario";
            txtPrecioUnitario.Size = new Size(412, 23);
            txtPrecioUnitario.Text = "0";
            txtPrecioUnitario.TextAlign = HorizontalAlignment.Right;
            txtPrecioUnitario.TextChanged += RecalcularTotal;
            tlp.Controls.Add(txtPrecioUnitario, 3, 4);

            // lblTotal
            lblTotal.Location = new Point(3, 180);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(100, 23);
            lblTotal.Text = "Total";
            lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            tlp.Controls.Add(lblTotal, 0, 5);

            // txtTotal
            txtTotal.Dock = DockStyle.Fill;
            txtTotal.Location = new Point(123, 183);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(412, 23);
            txtTotal.Text = "0";
            txtTotal.TextAlign = HorizontalAlignment.Right;
            tlp.Controls.Add(txtTotal, 1, 5);

            // lblFecha
            lblFecha.Location = new Point(541, 180);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(100, 23);
            lblFecha.Text = "Fecha";
            lblFecha.TextAlign = ContentAlignment.MiddleLeft;
            tlp.Controls.Add(lblFecha, 2, 5);

            // dtpFecha
            dtpFecha.Dock = DockStyle.Fill;
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(661, 183);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(412, 23);
            tlp.Controls.Add(dtpFecha, 3, 5);

            // lblPago
            lblPago.Location = new Point(3, 216);
            lblPago.Name = "lblPago";
            lblPago.Size = new Size(100, 23);
            lblPago.Text = "Forma de pago";
            lblPago.TextAlign = ContentAlignment.MiddleLeft;
            tlp.Controls.Add(lblPago, 0, 6);

            // cboFormaPago
            cboFormaPago.Anchor = AnchorStyles.Left;
            cboFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFormaPago.Location = new Point(123, 222);
            cboFormaPago.Name = "cboFormaPago";
            cboFormaPago.Size = new Size(160, 23);
            tlp.Controls.Add(cboFormaPago, 1, 6);

            // lblStockDisp
            lblStockDisp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStockDisp.ForeColor = Color.Gray;
            lblStockDisp.Location = new Point(3, 252);
            lblStockDisp.Name = "lblStockDisp";
            lblStockDisp.Size = new Size(300, 23);
            lblStockDisp.Text = "Stock disponible: —";
            tlp.SetColumnSpan(lblStockDisp, 2);
            tlp.Controls.Add(lblStockDisp, 0, 7);

            // flowAcciones
            flowAcciones.AutoSize = true;
            flowAcciones.Controls.Add(btnGuardar);
            flowAcciones.Controls.Add(btnImprimirFactura);
            flowAcciones.Controls.Add(btnCancelar);
            flowAcciones.Dock = DockStyle.Fill;
            flowAcciones.FlowDirection = FlowDirection.RightToLeft;
            flowAcciones.Location = new Point(658, 252);
            flowAcciones.Margin = new Padding(0);
            flowAcciones.Name = "flowAcciones";
            flowAcciones.Size = new Size(418, 48);
            tlp.Controls.Add(flowAcciones, 2, 7);
            tlp.SetColumnSpan(flowAcciones, 2);

            // btnGuardar
            btnGuardar.AutoSize = true;
            btnGuardar.BackColor = Color.SteelBlue;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Text = "Guardar venta";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;

            // btnImprimirFactura
            btnImprimirFactura.AutoSize = true;
            btnImprimirFactura.Name = "btnImprimirFactura";
            btnImprimirFactura.Text = "Imprimir factura";
            btnImprimirFactura.Click += btnImprimirFactura_Click;

            // btnCancelar
            btnCancelar.AutoSize = true;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;

            // FrmVentas
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
            ClientSize = new Size(1100, 600);
            Controls.Add(grpListado);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmVentas";
            Text = "Ventas - Sistema Automotors";
            Load += FrmVentas_Load;

            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            grpListado.ResumeLayout(false);
            tlp.ResumeLayout(false);
            tlp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            flowAcciones.ResumeLayout(false);
            flowAcciones.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Visual controls
        private Panel panel1;
        private Label label1;
        private GroupBox grpListado;
        private TableLayoutPanel tlp;
        private Label lblCliente;
        private ComboBox cboCliente;
        private Label lblProducto;
        private ComboBox cboProducto;
        private Label lblServicio;
        private ComboBox cboServicio;
        private Button btnGestionarServicios;
        private Label lblVendedor;
        private ComboBox cboVendedor;
        private Label lblCantidad;
        private NumericUpDown nudCantidad;
        private Label lblPrecio;
        private TextBox txtPrecioUnitario;
        private Label lblTotal;
        private TextBox txtTotal;
        private Label lblFecha;
        private DateTimePicker dtpFecha;
        private Label lblPago;
        private ComboBox cboFormaPago;
        private Label lblStockDisp;
        private FlowLayoutPanel flowAcciones;
        private Button btnGuardar;
        private Button btnImprimirFactura;
        private Button btnCancelar;
    }
}