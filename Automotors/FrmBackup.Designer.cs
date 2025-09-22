namespace Automotors
{
    partial class FrmBackup
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInfoBD = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAbrirCarpeta = new System.Windows.Forms.Button();
            this.btnGenerarBackup = new System.Windows.Forms.Button();
            this.btnSeleccionarDestino = new System.Windows.Forms.Button();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblInfoBackup = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 60);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Backup de Base de Datos";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblInfoBD);
            this.groupBox1.Location = new System.Drawing.Point(20, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de Base de Datos Actual";
            // 
            // lblInfoBD
            // 
            this.lblInfoBD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfoBD.Location = new System.Drawing.Point(3, 16);
            this.lblInfoBD.Name = "lblInfoBD";
            this.lblInfoBD.Size = new System.Drawing.Size(294, 81);
            this.lblInfoBD.TabIndex = 0;
            this.lblInfoBD.Text = "Cargando información...";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLimpiar);
            this.groupBox2.Controls.Add(this.btnAbrirCarpeta);
            this.groupBox2.Controls.Add(this.btnGenerarBackup);
            this.groupBox2.Controls.Add(this.btnSeleccionarDestino);
            this.groupBox2.Controls.Add(this.txtDestino);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(20, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(644, 150);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuración de Backup";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(500, 110);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(120, 30);
            this.btnLimpiar.TabIndex = 5;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAbrirCarpeta
            // 
            this.btnAbrirCarpeta.Location = new System.Drawing.Point(370, 110);
            this.btnAbrirCarpeta.Name = "btnAbrirCarpeta";
            this.btnAbrirCarpeta.Size = new System.Drawing.Size(120, 30);
            this.btnAbrirCarpeta.TabIndex = 4;
            this.btnAbrirCarpeta.Text = "Abrir Carpeta";
            this.btnAbrirCarpeta.UseVisualStyleBackColor = true;
            this.btnAbrirCarpeta.Click += new System.EventHandler(this.btnAbrirCarpeta_Click);
            // 
            // btnGenerarBackup
            // 
            this.btnGenerarBackup.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGenerarBackup.ForeColor = System.Drawing.Color.White;
            this.btnGenerarBackup.Location = new System.Drawing.Point(240, 110);
            this.btnGenerarBackup.Name = "btnGenerarBackup";
            this.btnGenerarBackup.Size = new System.Drawing.Size(120, 30);
            this.btnGenerarBackup.TabIndex = 3;
            this.btnGenerarBackup.Text = "Generar Backup";
            this.btnGenerarBackup.UseVisualStyleBackColor = false;
            this.btnGenerarBackup.Click += new System.EventHandler(this.btnGenerarBackup_Click);
            // 
            // btnSeleccionarDestino
            // 
            this.btnSeleccionarDestino.Location = new System.Drawing.Point(500, 40);
            this.btnSeleccionarDestino.Name = "btnSeleccionarDestino";
            this.btnSeleccionarDestino.Size = new System.Drawing.Size(120, 25);
            this.btnSeleccionarDestino.TabIndex = 2;
            this.btnSeleccionarDestino.Text = "Seleccionar...";
            this.btnSeleccionarDestino.UseVisualStyleBackColor = true;
            this.btnSeleccionarDestino.Click += new System.EventHandler(this.btnSeleccionarDestino_Click);
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(120, 40);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.ReadOnly = true;
            this.txtDestino.Size = new System.Drawing.Size(370, 20);
            this.txtDestino.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Carpeta Destino:";
            // 
            // lblEstado
            // 
            this.lblEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Location = new System.Drawing.Point(0, 421);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(684, 40);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Listo";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblInfoBackup);
            this.groupBox3.Location = new System.Drawing.Point(340, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(324, 100);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Información del Último Backup";
            // 
            // lblInfoBackup
            // 
            this.lblInfoBackup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfoBackup.Location = new System.Drawing.Point(3, 16);
            this.lblInfoBackup.Name = "lblInfoBackup";
            this.lblInfoBackup.Size = new System.Drawing.Size(318, 81);
            this.lblInfoBackup.TabIndex = 0;
            this.lblInfoBackup.Text = "No se ha generado backup";
            // 
            // FrmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmBackup";
            this.Text = "Backup - Sistema Automotors";
            this.Load += new System.EventHandler(this.FrmBackup_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label label1;
        private GroupBox groupBox1;
        private Label lblInfoBD;
        private GroupBox groupBox2;
        private Button btnLimpiar;
        private Button btnAbrirCarpeta;
        private Button btnGenerarBackup;
        private Button btnSeleccionarDestino;
        private TextBox txtDestino;
        private Label label2;
        private Label lblEstado;
        private GroupBox groupBox3;
        private Label lblInfoBackup;
    }
}