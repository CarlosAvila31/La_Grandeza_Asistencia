namespace MicroSisPlani.Personal
{
    partial class Frm_Rol_Descansos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.pnl_titulo = new System.Windows.Forms.Panel();
            this.lbl_titulo = new System.Windows.Forms.Label();
            this.btn_Salir = new System.Windows.Forms.Button();
            this.lbl_linea = new System.Windows.Forms.Label();
            this.lbl_semana = new System.Windows.Forms.Label();
            this.dtp_semana = new System.Windows.Forms.DateTimePicker();
            this.btn_semAnterior = new System.Windows.Forms.Button();
            this.btn_semSiguiente = new System.Windows.Forms.Button();
            this.lbl_rango = new System.Windows.Forms.Label();
            this.btn_copiar = new System.Windows.Forms.Button();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.lbl_ayuda = new System.Windows.Forms.Label();
            this.pnl_titulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            //
            // pnl_titulo
            //
            this.pnl_titulo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_titulo.Controls.Add(this.lbl_titulo);
            this.pnl_titulo.Controls.Add(this.btn_Salir);
            this.pnl_titulo.Controls.Add(this.lbl_linea);
            this.pnl_titulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_titulo.Location = new System.Drawing.Point(0, 0);
            this.pnl_titulo.Name = "pnl_titulo";
            this.pnl_titulo.Size = new System.Drawing.Size(1000, 50);
            this.pnl_titulo.TabIndex = 0;
            this.pnl_titulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_titulo_MouseMove);
            //
            // lbl_titulo
            //
            this.lbl_titulo.AutoSize = true;
            this.lbl_titulo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_titulo.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_titulo.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_titulo.Location = new System.Drawing.Point(12, 13);
            this.lbl_titulo.Name = "lbl_titulo";
            this.lbl_titulo.Size = new System.Drawing.Size(268, 20);
            this.lbl_titulo.TabIndex = 0;
            this.lbl_titulo.Text = "ROL DE DESCANSOS DEL PERSONAL";
            this.lbl_titulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_titulo_MouseMove);
            //
            // btn_Salir
            //
            this.btn_Salir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Salir.FlatAppearance.BorderSize = 0;
            this.btn_Salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Salir.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Salir.ForeColor = System.Drawing.Color.DimGray;
            this.btn_Salir.Location = new System.Drawing.Point(956, 8);
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(32, 32);
            this.btn_Salir.TabIndex = 1;
            this.btn_Salir.Text = "X";
            this.btn_Salir.UseVisualStyleBackColor = true;
            this.btn_Salir.Click += new System.EventHandler(this.btn_Salir_Click);
            //
            // lbl_linea
            //
            this.lbl_linea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lbl_linea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_linea.Location = new System.Drawing.Point(0, 48);
            this.lbl_linea.Name = "lbl_linea";
            this.lbl_linea.Size = new System.Drawing.Size(1000, 2);
            this.lbl_linea.TabIndex = 2;
            //
            // lbl_semana
            //
            this.lbl_semana.AutoSize = true;
            this.lbl_semana.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_semana.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_semana.Location = new System.Drawing.Point(20, 68);
            this.lbl_semana.Name = "lbl_semana";
            this.lbl_semana.Size = new System.Drawing.Size(70, 20);
            this.lbl_semana.TabIndex = 1;
            this.lbl_semana.Text = "Semana:";
            //
            // dtp_semana
            //
            this.dtp_semana.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_semana.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_semana.Location = new System.Drawing.Point(96, 64);
            this.dtp_semana.Name = "dtp_semana";
            this.dtp_semana.Size = new System.Drawing.Size(140, 29);
            this.dtp_semana.TabIndex = 2;
            this.dtp_semana.ValueChanged += new System.EventHandler(this.dtp_semana_ValueChanged);
            //
            // btn_semAnterior
            //
            this.btn_semAnterior.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_semAnterior.Location = new System.Drawing.Point(246, 62);
            this.btn_semAnterior.Name = "btn_semAnterior";
            this.btn_semAnterior.Size = new System.Drawing.Size(40, 32);
            this.btn_semAnterior.TabIndex = 3;
            this.btn_semAnterior.Text = "<";
            this.btn_semAnterior.UseVisualStyleBackColor = true;
            this.btn_semAnterior.Click += new System.EventHandler(this.btn_semAnterior_Click);
            //
            // btn_semSiguiente
            //
            this.btn_semSiguiente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_semSiguiente.Location = new System.Drawing.Point(292, 62);
            this.btn_semSiguiente.Name = "btn_semSiguiente";
            this.btn_semSiguiente.Size = new System.Drawing.Size(40, 32);
            this.btn_semSiguiente.TabIndex = 4;
            this.btn_semSiguiente.Text = ">";
            this.btn_semSiguiente.UseVisualStyleBackColor = true;
            this.btn_semSiguiente.Click += new System.EventHandler(this.btn_semSiguiente_Click);
            //
            // lbl_rango
            //
            this.lbl_rango.AutoSize = true;
            this.lbl_rango.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rango.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_rango.Location = new System.Drawing.Point(346, 68);
            this.lbl_rango.Name = "lbl_rango";
            this.lbl_rango.Size = new System.Drawing.Size(18, 20);
            this.lbl_rango.TabIndex = 5;
            this.lbl_rango.Text = "-";
            //
            // btn_copiar
            //
            this.btn_copiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_copiar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_copiar.Location = new System.Drawing.Point(612, 60);
            this.btn_copiar.Name = "btn_copiar";
            this.btn_copiar.Size = new System.Drawing.Size(220, 36);
            this.btn_copiar.TabIndex = 6;
            this.btn_copiar.Text = "Copiar semana anterior";
            this.btn_copiar.UseVisualStyleBackColor = true;
            this.btn_copiar.Click += new System.EventHandler(this.btn_copiar_Click);
            //
            // btn_guardar
            //
            this.btn_guardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_guardar.FlatAppearance.BorderSize = 0;
            this.btn_guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_guardar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_guardar.ForeColor = System.Drawing.Color.White;
            this.btn_guardar.Location = new System.Drawing.Point(842, 60);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(138, 36);
            this.btn_guardar.TabIndex = 7;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = false;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            //
            // dgv
            //
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv.Location = new System.Drawing.Point(20, 108);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(960, 440);
            this.dgv.TabIndex = 8;
            //
            // lbl_ayuda
            //
            this.lbl_ayuda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_ayuda.AutoSize = true;
            this.lbl_ayuda.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ayuda.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_ayuda.Location = new System.Drawing.Point(20, 560);
            this.lbl_ayuda.Name = "lbl_ayuda";
            this.lbl_ayuda.Size = new System.Drawing.Size(683, 17);
            this.lbl_ayuda.TabIndex = 9;
            this.lbl_ayuda.Text = "Marca los dias de DESCANSO. \"Heredada\" = aun no se captura esta semana; se usa la ultima semana programada.";
            //
            // Frm_Rol_Descansos
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 590);
            this.Controls.Add(this.lbl_ayuda);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.btn_copiar);
            this.Controls.Add(this.lbl_rango);
            this.Controls.Add(this.btn_semSiguiente);
            this.Controls.Add(this.btn_semAnterior);
            this.Controls.Add(this.dtp_semana);
            this.Controls.Add(this.lbl_semana);
            this.Controls.Add(this.pnl_titulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Rol_Descansos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rol de descansos";
            this.Load += new System.EventHandler(this.Frm_Rol_Descansos_Load);
            this.pnl_titulo.ResumeLayout(false);
            this.pnl_titulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_titulo;
        private System.Windows.Forms.Label lbl_titulo;
        private System.Windows.Forms.Button btn_Salir;
        private System.Windows.Forms.Label lbl_linea;
        private System.Windows.Forms.Label lbl_semana;
        private System.Windows.Forms.DateTimePicker dtp_semana;
        private System.Windows.Forms.Button btn_semAnterior;
        private System.Windows.Forms.Button btn_semSiguiente;
        private System.Windows.Forms.Label lbl_rango;
        private System.Windows.Forms.Button btn_copiar;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lbl_ayuda;
    }
}
