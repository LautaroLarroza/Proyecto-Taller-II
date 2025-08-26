namespace Automotors
{
    partial class Form1
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
            BUsuarios = new Button();
            BBackUp = new Button();
            BVentas = new Button();
            BClientes = new Button();
            BProductos = new Button();
            button6 = new Button();
            button7 = new Button();
            panel1 = new Panel();
            panelContenedor = new Panel(); // <- Nuevo panel contenedor
            panel1.SuspendLayout();
            SuspendLayout();

            // 
            // BUsuarios
            // 
            BUsuarios.Image = Properties.Resources.Usuario;
            BUsuarios.Location = new Point(0, 27);
            BUsuarios.Name = "BUsuarios";
            BUsuarios.Size = new Size(212, 80);
            BUsuarios.TabIndex = 1;
            BUsuarios.Text = "Usuarios";
            BUsuarios.TextAlign = ContentAlignment.BottomCenter;
            BUsuarios.UseVisualStyleBackColor = true;
            BUsuarios.Click += BUsuarios_Click;
            // 
            // BBackUp
            // 
            BBackUp.Image = Properties.Resources.Back_Up;
            BBackUp.Location = new Point(1, 133);
            BBackUp.Name = "BBackUp";
            BBackUp.Size = new Size(212, 80);
            BBackUp.TabIndex = 2;
            BBackUp.Text = "Back Up";
            BBackUp.TextAlign = ContentAlignment.BottomCenter;
            BBackUp.UseVisualStyleBackColor = true;
            // 
            // BVentas
            // 
            BVentas.Image = Properties.Resources.Ventas;
            BVentas.Location = new Point(0, 235);
            BVentas.Name = "BVentas";
            BVentas.Size = new Size(212, 80);
            BVentas.TabIndex = 3;
            BVentas.Text = "Ventas";
            BVentas.TextAlign = ContentAlignment.BottomCenter;
            BVentas.UseVisualStyleBackColor = true;
            // 
            // BClientes
            // 
            BClientes.Image = Properties.Resources.Clientes;
            BClientes.Location = new Point(0, 348);
            BClientes.Name = "BClientes";
            BClientes.Size = new Size(212, 80);
            BClientes.TabIndex = 4;
            BClientes.Text = "Clientes";
            BClientes.TextAlign = ContentAlignment.BottomCenter;
            BClientes.UseVisualStyleBackColor = true;
            // 
            // BProductos
            // 
            BProductos.Image = Properties.Resources.Productos;
            BProductos.Location = new Point(0, 462);
            BProductos.Name = "BProductos";
            BProductos.Size = new Size(212, 80);
            BProductos.TabIndex = 5;
            BProductos.Text = "Productos";
            BProductos.TextAlign = ContentAlignment.BottomCenter;
            BProductos.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Image = Properties.Resources.Reportes;
            button6.Location = new Point(0, 567);
            button6.Name = "button6";
            button6.Size = new Size(212, 90);
            button6.TabIndex = 6;
            button6.Text = "Reportes";
            button6.TextAlign = ContentAlignment.BottomCenter;
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(0, 685);
            button7.Name = "button7";
            button7.Size = new Size(212, 60);
            button7.TabIndex = 7;
            button7.Text = "Salir";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(button7);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(BProductos);
            panel1.Controls.Add(BClientes);
            panel1.Controls.Add(BVentas);
            panel1.Controls.Add(BBackUp);
            panel1.Controls.Add(BUsuarios);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(216, 764);
            panel1.TabIndex = 2;

            // 
            // panelContenedor
            // 
            panelContenedor.Location = new Point(220, 0);
            panelContenedor.Size = new Size(1269, 761);
            panelContenedor.BorderStyle = BorderStyle.FixedSingle;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1489, 761);
            Controls.Add(panelContenedor); // <- agregamos el contenedor primero
            Controls.Add(panel1);          // <- luego el menú
            Name = "Form1";
            Text = "Automotors - Menú Principal";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button BUsuarios;
        private Button BBackUp;
        private Button BVentas;
        private Button BClientes;
        private Button BProductos;
        private Button button6;
        private Button button7;
        private Panel panel1;
        private Panel panelContenedor; // <- declaramos aquí
    }
}
