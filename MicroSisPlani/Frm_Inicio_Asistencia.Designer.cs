namespace MicroSisPlani
{
    partial class Frm_Inicio_Asistencia
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Inicio_Asistencia));
            this.btn_Salir = new System.Windows.Forms.Button();
            this.Elipse_btn_Salir = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.btn_Login = new System.Windows.Forms.Button();
            this.Elipse_btn_Login = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.lbl_Hora = new System.Windows.Forms.Label();
            this.Tiempo = new System.Windows.Forms.Timer(this.components);
            this.lbl_Fecha = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnl_titulo = new System.Windows.Forms.Panel();
            this.Text1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.btn_Entrada = new System.Windows.Forms.Button();
            this.Elipse_btn_Entrada = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.Elipse_btn_Salida = new Bunifu.Framework.UI.BunifuElipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_titulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Salir
            // 
            this.btn_Salir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Salir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(202)))), ((int)(((byte)(39)))));
            this.btn_Salir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Salir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Salir.FlatAppearance.BorderSize = 0;
            this.btn_Salir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(180)))), ((int)(((byte)(39)))));
            this.btn_Salir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(180)))), ((int)(((byte)(39)))));
            this.btn_Salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Salir.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Salir.Image = ((System.Drawing.Image)(resources.GetObject("btn_Salir.Image")));
            this.btn_Salir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Salir.Location = new System.Drawing.Point(1138, 613);
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(50, 50);
            this.btn_Salir.TabIndex = 465;
            this.btn_Salir.UseVisualStyleBackColor = false;
            this.btn_Salir.Click += new System.EventHandler(this.btn_Salir_Click_1);
            // 
            // Elipse_btn_Salir
            // 
            this.Elipse_btn_Salir.ElipseRadius = 10;
            this.Elipse_btn_Salir.TargetControl = this.btn_Salir;
            // 
            // btn_Login
            // 
            this.btn_Login.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(202)))), ((int)(((byte)(39)))));
            this.btn_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Login.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Login.FlatAppearance.BorderSize = 0;
            this.btn_Login.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(180)))), ((int)(((byte)(39)))));
            this.btn_Login.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(180)))), ((int)(((byte)(39)))));
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.ForeColor = System.Drawing.Color.Transparent;
            this.btn_Login.Image = ((System.Drawing.Image)(resources.GetObject("btn_Login.Image")));
            this.btn_Login.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Login.Location = new System.Drawing.Point(1055, 613);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(50, 50);
            this.btn_Login.TabIndex = 466;
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // Elipse_btn_Login
            // 
            this.Elipse_btn_Login.ElipseRadius = 10;
            this.Elipse_btn_Login.TargetControl = this.btn_Login;
            // 
            // lbl_Hora
            // 
            this.lbl_Hora.BackColor = System.Drawing.Color.Black;
            this.lbl_Hora.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Hora.ForeColor = System.Drawing.Color.White;
            this.lbl_Hora.Location = new System.Drawing.Point(-2, 112);
            this.lbl_Hora.Name = "lbl_Hora";
            this.lbl_Hora.Size = new System.Drawing.Size(1202, 50);
            this.lbl_Hora.TabIndex = 467;
            this.lbl_Hora.Text = "Hora";
            this.lbl_Hora.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Hora.Click += new System.EventHandler(this.lbl_Hora_Click);
            // 
            // Tiempo
            // 
            this.Tiempo.Enabled = true;
            this.Tiempo.Tick += new System.EventHandler(this.Tiempo_Tick);
            // 
            // lbl_Fecha
            // 
            this.lbl_Fecha.BackColor = System.Drawing.Color.Black;
            this.lbl_Fecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Fecha.ForeColor = System.Drawing.Color.White;
            this.lbl_Fecha.Location = new System.Drawing.Point(-7, 57);
            this.lbl_Fecha.Name = "lbl_Fecha";
            this.lbl_Fecha.Size = new System.Drawing.Size(1207, 46);
            this.lbl_Fecha.TabIndex = 468;
            this.lbl_Fecha.Text = "Dia";
            this.lbl_Fecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(43, 343);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(448, 420);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 469;
            this.pictureBox1.TabStop = false;
            // 
            // pnl_titulo
            // 
            this.pnl_titulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(202)))), ((int)(((byte)(39)))));
            this.pnl_titulo.Controls.Add(this.Text1);
            this.pnl_titulo.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pnl_titulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_titulo.ForeColor = System.Drawing.Color.Transparent;
            this.pnl_titulo.Location = new System.Drawing.Point(0, 0);
            this.pnl_titulo.Name = "pnl_titulo";
            this.pnl_titulo.Size = new System.Drawing.Size(1200, 43);
            this.pnl_titulo.TabIndex = 470;
            this.pnl_titulo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_titulo_Paint);
            this.pnl_titulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_titulo_MouseMove);
            // 
            // Text1
            // 
            this.Text1.BackColor = System.Drawing.Color.Transparent;
            this.Text1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Text1.ForeColor = System.Drawing.Color.Black;
            this.Text1.Location = new System.Drawing.Point(-4, 0);
            this.Text1.Name = "Text1";
            this.Text1.Size = new System.Drawing.Size(1201, 43);
            this.Text1.TabIndex = 472;
            this.Text1.Text = "Sistema Integral de Asistencia";
            this.Text1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Text1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_titulo_MouseMove);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(-4, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1204, 137);
            this.label2.TabIndex = 473;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(202)))), ((int)(((byte)(39)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-1, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1201, 25);
            this.label1.TabIndex = 473;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.ForeColor = System.Drawing.Color.DimGray;
            this.Label14.Location = new System.Drawing.Point(1051, 583);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(178, 21);
            this.Label14.TabIndex = 471;
            this.Label14.Text = "La Grandeza ® 2026";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_Entrada
            // 
            this.btn_Entrada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(202)))), ((int)(((byte)(39)))));
            this.btn_Entrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Entrada.Location = new System.Drawing.Point(355, 230);
            this.btn_Entrada.Name = "btn_Entrada";
            this.btn_Entrada.Size = new System.Drawing.Size(478, 116);
            this.btn_Entrada.TabIndex = 472;
            this.btn_Entrada.Text = "REGISTRAR ASISTENCIA";
            this.btn_Entrada.UseVisualStyleBackColor = false;
            this.btn_Entrada.Click += new System.EventHandler(this.btn_Entrada_Click);
            // 
            // Elipse_btn_Entrada
            // 
            this.Elipse_btn_Entrada.ElipseRadius = 10;
            this.Elipse_btn_Entrada.TargetControl = this.btn_Entrada;
            // 
            // Elipse_btn_Salida
            // 
            this.Elipse_btn_Salida.ElipseRadius = 10;
            this.Elipse_btn_Salida.TargetControl = this;
            // 
            // Frm_Inicio_Asistencia
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_Hora);
            this.Controls.Add(this.lbl_Fecha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Entrada);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.pnl_titulo);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.btn_Salir);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_Inicio_Asistencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Inicio_Asistencia";
            this.Load += new System.EventHandler(this.Frm_Inicio_Asistencia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_titulo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Salir;
        private Bunifu.Framework.UI.BunifuElipse Elipse_btn_Salir;
        private System.Windows.Forms.Button btn_Login;
        private Bunifu.Framework.UI.BunifuElipse Elipse_btn_Login;
        private System.Windows.Forms.Label lbl_Hora;
        private System.Windows.Forms.Timer Tiempo;
        private System.Windows.Forms.Label lbl_Fecha;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Panel pnl_titulo;
        internal System.Windows.Forms.Label Label14;
        private System.Windows.Forms.Label Text1;
        private System.Windows.Forms.Button btn_Entrada;
        private Bunifu.Framework.UI.BunifuElipse Elipse_btn_Entrada;
        private Bunifu.Framework.UI.BunifuElipse Elipse_btn_Salida;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}