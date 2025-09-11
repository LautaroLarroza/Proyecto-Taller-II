namespace Automotors
{
    partial class FrmUsuarios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            BAgregar = new Button();
            BModificar = new Button();
            BEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(231, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(700, 400);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // BAgregar
            // 
            BAgregar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BAgregar.AutoSize = true;
            BAgregar.Location = new Point(231, 434);
            BAgregar.Name = "BAgregar";
            BAgregar.Size = new Size(100, 50);
            BAgregar.TabIndex = 1;
            BAgregar.Text = "Agregar";
            BAgregar.UseVisualStyleBackColor = true;
            BAgregar.Click += BAgregar_Click;
            // 
            // BModificar
            // 
            BModificar.Location = new Point(547, 434);
            BModificar.Name = "BModificar";
            BModificar.Size = new Size(100, 50);
            BModificar.TabIndex = 2;
            BModificar.Text = "Modificar";
            BModificar.UseVisualStyleBackColor = true;
            // 
            // BEliminar
            // 
            BEliminar.Location = new Point(831, 434);
            BEliminar.Name = "BEliminar";
            BEliminar.Size = new Size(100, 50);
            BEliminar.TabIndex = 3;
            BEliminar.Text = "Eliminar";
            BEliminar.UseVisualStyleBackColor = true;
            // 
            // FrmUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1482, 637);
            Controls.Add(BEliminar);
            Controls.Add(BModificar);
            Controls.Add(BAgregar);
            Controls.Add(dataGridView1);
            Name = "FrmUsuarios";
            Text = "Usuarios";
            Load += FrmUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button BAgregar;
        private Button BModificar;
        private Button BEliminar;
    }
}
