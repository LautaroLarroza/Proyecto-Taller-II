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
            lblObservaciones = new Label();
            txtObs = new TextBox();
            lblStockDisp = new Label();
            flowAcciones = new FlowLayoutPanel();
            btnGuardar = new Button();
            btnCancelar = new Button();
            panel1.SuspendLayout();
            grpListado.SuspendLayout();
            tlp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            flowAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.SteelBlue;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1100, 60);
            panel1.TabIndex = 1000;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(187, 24);
            label1.TabIndex = 0;
            label1.Text = "Registro de Ventas";
            // 
            // grpListado
            // 
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
            // 
            // tlp
            // 
            tlp.ColumnCount = 4;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlp.Controls.Add(lblCliente, 0, 0);
            tlp.Controls.Add(cboCliente, 1, 0);
            tlp.Controls.Add(lblProducto, 0, 1);
            tlp.Controls.Add(cboProducto, 1, 1);
            tlp.Controls.Add(lblVendedor, 0, 2);
            tlp.Controls.Add(cboVendedor, 1, 2);
            tlp.Controls.Add(lblCantidad, 0, 3);
            tlp.Controls.Add(nudCantidad, 1, 3);
            tlp.Controls.Add(lblPrecio, 2, 3);
            tlp.Controls.Add(txtPrecioUnitario, 3, 3);
            tlp.Controls.Add(lblTotal, 0, 4);
            tlp.Controls.Add(txtTotal, 1, 4);
            tlp.Controls.Add(lblFecha, 2, 4);
            tlp.Controls.Add(dtpFecha, 3, 4);
            tlp.Controls.Add(lblPago, 0, 5);
            tlp.Controls.Add(cboFormaPago, 1, 5);
            tlp.Controls.Add(lblObservaciones, 0, 6);
            tlp.Controls.Add(txtObs, 1, 6);
            tlp.Controls.Add(lblStockDisp, 0, 7);
            tlp.Controls.Add(flowAcciones, 3, 7);
            tlp.Dock = DockStyle.Fill;
            tlp.Location = new Point(12, 28);
            tlp.Name = "tlp";
            tlp.RowCount = 8;
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlp.Size = new Size(1076, 500);
            tlp.TabIndex = 0;
            tlp.Paint += tlp_Paint;
            // 
            // lblCliente
            // 
            lblCliente.Location = new Point(3, 0);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(100, 23);
            lblCliente.TabIndex = 0;
            lblCliente.Text = "Cliente";
            lblCliente.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboCliente
            // 
            cboCliente.AutoCompleteSource = AutoCompleteSource.ListItems;
            tlp.SetColumnSpan(cboCliente, 3);
            cboCliente.Dock = DockStyle.Fill;
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCliente.Location = new Point(123, 3);
            cboCliente.Name = "cboCliente";
            cboCliente.Size = new Size(950, 23);
            cboCliente.TabIndex = 1;
            // 
            // lblProducto
            // 
            lblProducto.Location = new Point(3, 36);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(100, 23);
            lblProducto.TabIndex = 2;
            lblProducto.Text = "Producto";
            lblProducto.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboProducto
            // 
            cboProducto.AutoCompleteSource = AutoCompleteSource.ListItems;
            tlp.SetColumnSpan(cboProducto, 3);
            cboProducto.Dock = DockStyle.Fill;
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProducto.Location = new Point(123, 39);
            cboProducto.Name = "cboProducto";
            cboProducto.Size = new Size(950, 23);
            cboProducto.TabIndex = 3;
            cboProducto.SelectedIndexChanged += cboProducto_SelectedIndexChanged;
            // 
            // lblVendedor
            // 
            lblVendedor.Location = new Point(3, 72);
            lblVendedor.Name = "lblVendedor";
            lblVendedor.Size = new Size(100, 23);
            lblVendedor.TabIndex = 4;
            lblVendedor.Text = "Vendedor";
            lblVendedor.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboVendedor
            // 
            tlp.SetColumnSpan(cboVendedor, 3);
            cboVendedor.Dock = DockStyle.Fill;
            cboVendedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVendedor.Location = new Point(123, 75);
            cboVendedor.Name = "cboVendedor";
            cboVendedor.Size = new Size(950, 23);
            cboVendedor.TabIndex = 5;
            // 
            // lblCantidad
            // 
            lblCantidad.Location = new Point(3, 108);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(100, 23);
            lblCantidad.TabIndex = 6;
            lblCantidad.Text = "Cantidad";
            lblCantidad.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // nudCantidad
            // 
            nudCantidad.Anchor = AnchorStyles.Left;
            nudCantidad.Location = new Point(123, 114);
            nudCantidad.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(120, 23);
            nudCantidad.TabIndex = 7;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.ValueChanged += RecalcularTotal;
            // 
            // lblPrecio
            // 
            lblPrecio.Location = new Point(541, 108);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(100, 23);
            lblPrecio.TabIndex = 8;
            lblPrecio.Text = "Precio unit.";
            lblPrecio.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPrecioUnitario
            // 
            txtPrecioUnitario.Dock = DockStyle.Fill;
            txtPrecioUnitario.Location = new Point(661, 111);
            txtPrecioUnitario.Name = "txtPrecioUnitario";
            txtPrecioUnitario.Size = new Size(412, 23);
            txtPrecioUnitario.TabIndex = 9;
            txtPrecioUnitario.Text = "0";
            txtPrecioUnitario.TextAlign = HorizontalAlignment.Right;
            txtPrecioUnitario.TextChanged += RecalcularTotal;
            // 
            // lblTotal
            // 
            lblTotal.Location = new Point(3, 144);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(100, 23);
            lblTotal.TabIndex = 10;
            lblTotal.Text = "Total";
            lblTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTotal
            // 
            txtTotal.Dock = DockStyle.Fill;
            txtTotal.Location = new Point(123, 147);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(412, 23);
            txtTotal.TabIndex = 11;
            txtTotal.Text = "0";
            txtTotal.TextAlign = HorizontalAlignment.Right;
            // 
            // lblFecha
            // 
            lblFecha.Location = new Point(541, 144);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(100, 23);
            lblFecha.TabIndex = 12;
            lblFecha.Text = "Fecha";
            lblFecha.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpFecha
            // 
            dtpFecha.Dock = DockStyle.Fill;
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(661, 147);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(412, 23);
            dtpFecha.TabIndex = 13;
            // 
            // lblPago
            // 
            lblPago.Location = new Point(3, 180);
            lblPago.Name = "lblPago";
            lblPago.Size = new Size(100, 23);
            lblPago.TabIndex = 14;
            lblPago.Text = "Forma de pago";
            lblPago.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboFormaPago
            // 
            cboFormaPago.Anchor = AnchorStyles.Left;
            cboFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFormaPago.Items.AddRange(new object[] { "Efectivo", "Transferencia", "Tarjeta", "Otro" });
            cboFormaPago.Location = new Point(123, 186);
            cboFormaPago.Name = "cboFormaPago";
            cboFormaPago.Size = new Size(160, 23);
            cboFormaPago.TabIndex = 15;
            // 
            // lblObservaciones
            // 
            lblObservaciones.Location = new Point(3, 216);
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.Size = new Size(100, 23);
            lblObservaciones.TabIndex = 16;
            lblObservaciones.Text = "Observaciones";
            lblObservaciones.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtObs
            // 
            tlp.SetColumnSpan(txtObs, 3);
            txtObs.Dock = DockStyle.Fill;
            txtObs.Location = new Point(123, 219);
            txtObs.Multiline = true;
            txtObs.Name = "txtObs";
            txtObs.Size = new Size(950, 230);
            txtObs.TabIndex = 17;
            txtObs.TextChanged += txtObs_TextChanged;
            // 
            // lblStockDisp
            // 
            lblStockDisp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tlp.SetColumnSpan(lblStockDisp, 3);
            lblStockDisp.ForeColor = Color.Gray;
            lblStockDisp.Location = new Point(3, 477);
            lblStockDisp.Name = "lblStockDisp";
            lblStockDisp.Size = new Size(200, 23);
            lblStockDisp.TabIndex = 18;
            lblStockDisp.Text = "Stock disponible: —";
            // 
            // flowAcciones
            // 
            flowAcciones.AutoSize = true;
            flowAcciones.Controls.Add(btnGuardar);
            flowAcciones.Controls.Add(btnCancelar);
            flowAcciones.Dock = DockStyle.Fill;
            flowAcciones.FlowDirection = FlowDirection.RightToLeft;
            flowAcciones.Location = new Point(658, 452);
            flowAcciones.Margin = new Padding(0);
            flowAcciones.Name = "flowAcciones";
            flowAcciones.Size = new Size(418, 48);
            flowAcciones.TabIndex = 19;
            // 
            // btnGuardar
            // 
            btnGuardar.AutoSize = true;
            btnGuardar.BackColor = Color.SteelBlue;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(305, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(110, 30);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar venta";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.AutoSize = true;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(209, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(90, 30);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            // 
            // FrmVentas
            // 
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

        // Nuevos (visual): panel superior y estado inferior
        private Panel panel1;
        private Label label1;

        // Existentes: preservados
        private GroupBox grpListado;
        private TableLayoutPanel tlp;
        private Label lblCliente;
        private ComboBox cboCliente;
        private Label lblProducto;
        private ComboBox cboProducto;
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
        private Label lblObservaciones;
        private TextBox txtObs;
        private Label lblStockDisp;
        private FlowLayoutPanel flowAcciones;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}
