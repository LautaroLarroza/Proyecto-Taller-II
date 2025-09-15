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
            btnCerrarSesion = new Button();
            panel1 = new Panel();
            panelContenedor = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // BUsuarios
            // 
            BUsuarios.Image = Properties.Resources.Usuario;
            BUsuarios.Location = new Point(0, 13);
            BUsuarios.Margin = new Padding(3, 4, 3, 4);
            BUsuarios.Name = "BUsuarios";
            BUsuarios.Size = new Size(242, 107);
            BUsuarios.TabIndex = 1;
            BUsuarios.Text = "Usuarios";
            BUsuarios.TextAlign = ContentAlignment.BottomCenter;
            BUsuarios.UseVisualStyleBackColor = true;
            BUsuarios.Click += BUsuarios_Click;
            // 
            // BBackUp
            // 
            BBackUp.Image = Properties.Resources.Back_Up;
            BBackUp.Location = new Point(3, 128);
            BBackUp.Margin = new Padding(3, 4, 3, 4);
            BBackUp.Name = "BBackUp";
            BBackUp.Size = new Size(242, 107);
            BBackUp.TabIndex = 2;
            BBackUp.Text = "Back Up";
            BBackUp.TextAlign = ContentAlignment.BottomCenter;
            BBackUp.UseVisualStyleBackColor = true;
            // 
            // BVentas
            // 
            BVentas.Image = Properties.Resources.Ventas;
            BVentas.Location = new Point(0, 243);
            BVentas.Margin = new Padding(3, 4, 3, 4);
            BVentas.Name = "BVentas";
            BVentas.Size = new Size(242, 107);
            BVentas.TabIndex = 3;
            BVentas.Text = "Ventas";
            BVentas.TextAlign = ContentAlignment.BottomCenter;
            BVentas.UseVisualStyleBackColor = true;
            // 
            // BClientes
            // 
            BClientes.Image = Properties.Resources.Clientes;
            BClientes.Location = new Point(0, 358);
            BClientes.Margin = new Padding(3, 4, 3, 4);
            BClientes.Name = "BClientes";
            BClientes.Size = new Size(242, 107);
            BClientes.TabIndex = 4;
            BClientes.Text = "Clientes";
            BClientes.TextAlign = ContentAlignment.BottomCenter;
            BClientes.UseVisualStyleBackColor = true;
            // 
            // BProductos
            // 
            BProductos.Image = Properties.Resources.Productos;
            BProductos.Location = new Point(0, 473);
            BProductos.Margin = new Padding(3, 4, 3, 4);
            BProductos.Name = "BProductos";
            BProductos.Size = new Size(242, 107);
            BProductos.TabIndex = 5;
            BProductos.Text = "Productos";
            BProductos.TextAlign = ContentAlignment.BottomCenter;
            BProductos.UseVisualStyleBackColor = true;
            BProductos.Click += BProductos_Click;
            // 
            // button6
            // 
            button6.Image = Properties.Resources.Reportes;
            button6.Location = new Point(0, 588);
            button6.Margin = new Padding(3, 4, 3, 4);
            button6.Name = "button6";
            button6.Size = new Size(242, 120);
            button6.TabIndex = 6;
            button6.Text = "Reportes";
            button6.TextAlign = ContentAlignment.BottomCenter;
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(2, 804);
            button7.Margin = new Padding(3, 4, 3, 4);
            button7.Name = "button7";
            button7.Size = new Size(242, 80);
            button7.TabIndex = 7;
            button7.Text = "Salir";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(3, 716);
            btnCerrarSesion.Margin = new Padding(3, 4, 3, 4);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(242, 80);
            btnCerrarSesion.TabIndex = 8;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCerrarSesion);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(BProductos);
            panel1.Controls.Add(BClientes);
            panel1.Controls.Add(BVentas);
            panel1.Controls.Add(BBackUp);
            panel1.Controls.Add(BUsuarios);
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(247, 1019);
            panel1.TabIndex = 2;
            // 
            // panelContenedor
            // 
            panelContenedor.BorderStyle = BorderStyle.FixedSingle;
            panelContenedor.Location = new Point(251, 0);
            panelContenedor.Margin = new Padding(3, 4, 3, 4);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1450, 1014);
            panelContenedor.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1702, 886);
            Controls.Add(panelContenedor);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
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
        private Panel panelContenedor;
        private Button btnCerrarSesion;
    }
}