using System.Drawing;
using System.Windows.Forms;

namespace Automotors
{
    partial class FrmReportes
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
            main = new TableLayoutPanel();
            pnlFiltros = new TableLayoutPanel();
            lblReporte = new Label();
            cboReporte = new ComboBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            btnBuscar = new Button();
            dgv = new DataGridView();
            pnlBottom = new TableLayoutPanel();
            lblResumen = new Label();
            btnGrafico = new Button();
            btnExportar = new Button();
            main.SuspendLayout();
            pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // main
            // 
            main.ColumnCount = 1;
            main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            main.Controls.Add(pnlFiltros, 0, 0);
            main.Controls.Add(dgv, 0, 1);
            main.Controls.Add(pnlBottom, 0, 2);
            main.Dock = DockStyle.Fill;
            main.Location = new Point(0, 0);
            main.Name = "main";
            main.Padding = new Padding(12);
            main.RowCount = 3;
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            main.Size = new Size(980, 640);
            main.TabIndex = 0;
            // 
            // pnlFiltros
            // 
            pnlFiltros.ColumnCount = 7;
            pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            pnlFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            pnlFiltros.Controls.Add(lblReporte, 0, 0);
            pnlFiltros.Controls.Add(cboReporte, 1, 0);
            pnlFiltros.Controls.Add(lblDesde, 2, 0);
            pnlFiltros.Controls.Add(dtpDesde, 3, 0);
            pnlFiltros.Controls.Add(lblHasta, 4, 0);
            pnlFiltros.Controls.Add(dtpHasta, 5, 0);
            pnlFiltros.Controls.Add(btnBuscar, 6, 0);
            pnlFiltros.Dock = DockStyle.Fill;
            pnlFiltros.Location = new Point(15, 15);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Padding = new Padding(0, 4, 0, 4);
            pnlFiltros.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            pnlFiltros.Size = new Size(950, 58);
            pnlFiltros.TabIndex = 0;
            // 
            // lblReporte
            // 
            lblReporte.Dock = DockStyle.Fill;
            lblReporte.Location = new Point(3, 4);
            lblReporte.Name = "lblReporte";
            lblReporte.Size = new Size(64, 50);
            lblReporte.TabIndex = 0;
            lblReporte.Text = "Reporte";
            lblReporte.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cboReporte
            // 
            cboReporte.Dock = DockStyle.Fill;
            cboReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cboReporte.Location = new Point(73, 7);
            cboReporte.Name = "cboReporte";
            cboReporte.Size = new Size(319, 23);
            cboReporte.TabIndex = 1;
            // 
            // lblDesde
            // 
            lblDesde.Dock = DockStyle.Fill;
            lblDesde.Location = new Point(398, 4);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(54, 50);
            lblDesde.TabIndex = 2;
            lblDesde.Text = "Desde";
            lblDesde.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpDesde
            // 
            dtpDesde.Dock = DockStyle.Fill;
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(458, 7);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(156, 23);
            dtpDesde.TabIndex = 3;
            // 
            // lblHasta
            // 
            lblHasta.Dock = DockStyle.Fill;
            lblHasta.Location = new Point(620, 4);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(54, 50);
            lblHasta.TabIndex = 4;
            lblHasta.Text = "Hasta";
            lblHasta.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpHasta
            // 
            dtpHasta.Dock = DockStyle.Fill;
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(680, 7);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(156, 23);
            dtpHasta.TabIndex = 5;
            // 
            // btnBuscar
            // 
            btnBuscar.Dock = DockStyle.Fill;
            btnBuscar.Location = new Point(842, 7);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(105, 44);
            btnBuscar.TabIndex = 6;
            btnBuscar.Text = "Buscar";
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(15, 79);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.Size = new Size(950, 506);
            dgv.TabIndex = 1;
            // 
            // pnlBottom
            // 
            pnlBottom.ColumnCount = 3;
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            pnlBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            pnlBottom.Controls.Add(lblResumen, 0, 0);
            pnlBottom.Controls.Add(btnGrafico, 1, 0);
            pnlBottom.Controls.Add(btnExportar, 2, 0);
            pnlBottom.Dock = DockStyle.Fill;
            pnlBottom.Location = new Point(15, 591);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            pnlBottom.Size = new Size(950, 34);
            pnlBottom.TabIndex = 2;
            // 
            // lblResumen
            // 
            lblResumen.Dock = DockStyle.Fill;
            lblResumen.Location = new Point(3, 0);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(664, 34);
            lblResumen.TabIndex = 0;
            lblResumen.Text = "—";
            lblResumen.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnGrafico
            // 
            btnGrafico.Dock = DockStyle.Fill;
            btnGrafico.Location = new Point(673, 3);
            btnGrafico.Name = "btnGrafico";
            btnGrafico.Size = new Size(134, 28);
            btnGrafico.TabIndex = 2;
            btnGrafico.Text = "Gráfico";
            // 
            // btnExportar
            // 
            btnExportar.Dock = DockStyle.Fill;
            btnExportar.Location = new Point(813, 3);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new Size(134, 28);
            btnExportar.TabIndex = 1;
            btnExportar.Text = "Exportar";
            // 
            // FrmReportes
            // 
            ClientSize = new Size(980, 640);
            Controls.Add(main);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmReportes";
            Text = "Reportes";
            Load += FrmReportes_Load;
            btnBuscar.Click += btnBuscar_Click;
            btnExportar.Click += btnExportar_Click;
            btnGrafico.Click += btnGrafico_Click;
            main.ResumeLayout(false);
            pnlFiltros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel main;
        private TableLayoutPanel pnlFiltros;
        private Label lblReporte;
        private ComboBox cboReporte;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Button btnBuscar;
        private DataGridView dgv;
        private TableLayoutPanel pnlBottom;
        private Label lblResumen;
        private Button btnGrafico;
        private Button btnExportar;
    }
}