namespace MicroSisPlani.Personal
{
    partial class Frm_Nomina
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabRep = new System.Windows.Forms.TabPage();
            this.dgv_rep = new System.Windows.Forms.DataGridView();
            this.pnl_total = new System.Windows.Forms.Panel();
            this.lbl_total = new System.Windows.Forms.Label();
            this.pnl_filtros = new System.Windows.Forms.Panel();
            this.lbl_semana = new System.Windows.Forms.Label();
            this.btn_ant = new System.Windows.Forms.Button();
            this.dtp_semana = new System.Windows.Forms.DateTimePicker();
            this.btn_sig = new System.Windows.Forms.Button();
            this.lbl_rango = new System.Windows.Forms.Label();
            this.lbl_sucursal = new System.Windows.Forms.Label();
            this.cbo_sucursal = new System.Windows.Forms.ComboBox();
            this.btn_pdf = new System.Windows.Forms.Button();
            this.btn_recibos = new System.Windows.Forms.Button();
            this.tabCfg = new System.Windows.Forms.TabPage();
            this.dgv_cfg = new System.Windows.Forms.DataGridView();
            this.pnl_cfg = new System.Windows.Forms.Panel();
            this.lbl_ayuda = new System.Windows.Forms.Label();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.pnl_titulo.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabRep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rep)).BeginInit();
            this.pnl_total.SuspendLayout();
            this.pnl_filtros.SuspendLayout();
            this.tabCfg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cfg)).BeginInit();
            this.pnl_cfg.SuspendLayout();
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
            this.pnl_titulo.Size = new System.Drawing.Size(1200, 50);
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
            this.lbl_titulo.Size = new System.Drawing.Size(215, 20);
            this.lbl_titulo.TabIndex = 0;
            this.lbl_titulo.Text = "NOMINA SEMANAL DEL PERSONAL";
            this.lbl_titulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_titulo_MouseMove);
            //
            // btn_Salir
            //
            this.btn_Salir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Salir.FlatAppearance.BorderSize = 0;
            this.btn_Salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Salir.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Salir.ForeColor = System.Drawing.Color.DimGray;
            this.btn_Salir.Location = new System.Drawing.Point(1156, 8);
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
            this.lbl_linea.Size = new System.Drawing.Size(1200, 2);
            this.lbl_linea.TabIndex = 2;
            //
            // tabs
            //
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabRep);
            this.tabs.Controls.Add(this.tabCfg);
            this.tabs.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabs.Location = new System.Drawing.Point(12, 60);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1176, 608);
            this.tabs.TabIndex = 1;
            //
            // tabRep
            //
            this.tabRep.BackColor = System.Drawing.Color.White;
            this.tabRep.Controls.Add(this.dgv_rep);
            this.tabRep.Controls.Add(this.pnl_total);
            this.tabRep.Controls.Add(this.pnl_filtros);
            this.tabRep.Location = new System.Drawing.Point(4, 28);
            this.tabRep.Name = "tabRep";
            this.tabRep.Size = new System.Drawing.Size(1168, 576);
            this.tabRep.TabIndex = 0;
            this.tabRep.Text = "Reporte de nomina";
            //
            // dgv_rep
            //
            this.dgv_rep.BackgroundColor = System.Drawing.Color.White;
            this.dgv_rep.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_rep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_rep.Location = new System.Drawing.Point(0, 92);
            this.dgv_rep.MultiSelect = false;
            this.dgv_rep.Name = "dgv_rep";
            this.dgv_rep.Size = new System.Drawing.Size(1168, 440);
            this.dgv_rep.TabIndex = 2;
            //
            // pnl_total
            //
            this.pnl_total.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_total.Controls.Add(this.lbl_total);
            this.pnl_total.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_total.Location = new System.Drawing.Point(0, 532);
            this.pnl_total.Name = "pnl_total";
            this.pnl_total.Size = new System.Drawing.Size(1168, 44);
            this.pnl_total.TabIndex = 1;
            //
            // lbl_total
            //
            this.lbl_total.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_total.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_total.Location = new System.Drawing.Point(0, 0);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Padding = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.lbl_total.Size = new System.Drawing.Size(1168, 44);
            this.lbl_total.TabIndex = 0;
            this.lbl_total.Text = "Total a pagar: $0.00";
            this.lbl_total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pnl_filtros
            //
            this.pnl_filtros.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_filtros.Controls.Add(this.lbl_semana);
            this.pnl_filtros.Controls.Add(this.btn_ant);
            this.pnl_filtros.Controls.Add(this.dtp_semana);
            this.pnl_filtros.Controls.Add(this.btn_sig);
            this.pnl_filtros.Controls.Add(this.lbl_rango);
            this.pnl_filtros.Controls.Add(this.lbl_sucursal);
            this.pnl_filtros.Controls.Add(this.cbo_sucursal);
            this.pnl_filtros.Controls.Add(this.btn_pdf);
            this.pnl_filtros.Controls.Add(this.btn_recibos);
            this.pnl_filtros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_filtros.Location = new System.Drawing.Point(0, 0);
            this.pnl_filtros.Name = "pnl_filtros";
            this.pnl_filtros.Size = new System.Drawing.Size(1168, 92);
            this.pnl_filtros.TabIndex = 0;
            //
            // lbl_semana
            //
            this.lbl_semana.AutoSize = true;
            this.lbl_semana.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_semana.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_semana.Location = new System.Drawing.Point(16, 22);
            this.lbl_semana.Name = "lbl_semana";
            this.lbl_semana.Size = new System.Drawing.Size(70, 20);
            this.lbl_semana.TabIndex = 0;
            this.lbl_semana.Text = "Semana:";
            //
            // btn_ant
            //
            this.btn_ant.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ant.Location = new System.Drawing.Point(96, 16);
            this.btn_ant.Name = "btn_ant";
            this.btn_ant.Size = new System.Drawing.Size(40, 32);
            this.btn_ant.TabIndex = 1;
            this.btn_ant.Text = "<";
            this.btn_ant.UseVisualStyleBackColor = true;
            this.btn_ant.Click += new System.EventHandler(this.btn_ant_Click);
            //
            // dtp_semana
            //
            this.dtp_semana.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_semana.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_semana.Location = new System.Drawing.Point(142, 18);
            this.dtp_semana.Name = "dtp_semana";
            this.dtp_semana.Size = new System.Drawing.Size(140, 29);
            this.dtp_semana.TabIndex = 2;
            this.dtp_semana.ValueChanged += new System.EventHandler(this.dtp_semana_ValueChanged);
            //
            // btn_sig
            //
            this.btn_sig.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sig.Location = new System.Drawing.Point(288, 16);
            this.btn_sig.Name = "btn_sig";
            this.btn_sig.Size = new System.Drawing.Size(40, 32);
            this.btn_sig.TabIndex = 3;
            this.btn_sig.Text = ">";
            this.btn_sig.UseVisualStyleBackColor = true;
            this.btn_sig.Click += new System.EventHandler(this.btn_sig_Click);
            //
            // lbl_rango
            //
            this.lbl_rango.AutoSize = true;
            this.lbl_rango.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_rango.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_rango.Location = new System.Drawing.Point(16, 60);
            this.lbl_rango.Name = "lbl_rango";
            this.lbl_rango.Size = new System.Drawing.Size(18, 20);
            this.lbl_rango.TabIndex = 4;
            this.lbl_rango.Text = "-";
            //
            // lbl_sucursal
            //
            this.lbl_sucursal.AutoSize = true;
            this.lbl_sucursal.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sucursal.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_sucursal.Location = new System.Drawing.Point(480, 22);
            this.lbl_sucursal.Name = "lbl_sucursal";
            this.lbl_sucursal.Size = new System.Drawing.Size(77, 20);
            this.lbl_sucursal.TabIndex = 5;
            this.lbl_sucursal.Text = "Sucursal:";
            //
            // cbo_sucursal
            //
            this.cbo_sucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_sucursal.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_sucursal.FormattingEnabled = true;
            this.cbo_sucursal.Location = new System.Drawing.Point(563, 18);
            this.cbo_sucursal.Name = "cbo_sucursal";
            this.cbo_sucursal.Size = new System.Drawing.Size(200, 29);
            this.cbo_sucursal.TabIndex = 6;
            this.cbo_sucursal.SelectionChangeCommitted += new System.EventHandler(this.cbo_sucursal_SelectionChangeCommitted);
            //
            // btn_pdf
            //
            this.btn_pdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_pdf.FlatAppearance.BorderSize = 0;
            this.btn_pdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pdf.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pdf.ForeColor = System.Drawing.Color.White;
            this.btn_pdf.Location = new System.Drawing.Point(790, 14);
            this.btn_pdf.Name = "btn_pdf";
            this.btn_pdf.Size = new System.Drawing.Size(170, 36);
            this.btn_pdf.TabIndex = 7;
            this.btn_pdf.Text = "Imprimir reporte";
            this.btn_pdf.UseVisualStyleBackColor = false;
            this.btn_pdf.Click += new System.EventHandler(this.btn_pdf_Click);
            //
            // btn_recibos
            //
            this.btn_recibos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_recibos.FlatAppearance.BorderSize = 0;
            this.btn_recibos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_recibos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_recibos.ForeColor = System.Drawing.Color.White;
            this.btn_recibos.Location = new System.Drawing.Point(970, 14);
            this.btn_recibos.Name = "btn_recibos";
            this.btn_recibos.Size = new System.Drawing.Size(170, 36);
            this.btn_recibos.TabIndex = 8;
            this.btn_recibos.Text = "Imprimir recibos";
            this.btn_recibos.UseVisualStyleBackColor = false;
            this.btn_recibos.Click += new System.EventHandler(this.btn_recibos_Click);
            //
            // tabCfg
            //
            this.tabCfg.BackColor = System.Drawing.Color.White;
            this.tabCfg.Controls.Add(this.dgv_cfg);
            this.tabCfg.Controls.Add(this.pnl_cfg);
            this.tabCfg.Location = new System.Drawing.Point(4, 28);
            this.tabCfg.Name = "tabCfg";
            this.tabCfg.Size = new System.Drawing.Size(1168, 576);
            this.tabCfg.TabIndex = 1;
            this.tabCfg.Text = "Sueldos por empleado";
            //
            // dgv_cfg
            //
            this.dgv_cfg.BackgroundColor = System.Drawing.Color.White;
            this.dgv_cfg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_cfg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_cfg.Location = new System.Drawing.Point(0, 70);
            this.dgv_cfg.MultiSelect = false;
            this.dgv_cfg.Name = "dgv_cfg";
            this.dgv_cfg.Size = new System.Drawing.Size(1168, 506);
            this.dgv_cfg.TabIndex = 1;
            this.dgv_cfg.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_cfg_CellValueChanged);
            this.dgv_cfg.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_cfg_DataError);
            //
            // pnl_cfg
            //
            this.pnl_cfg.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_cfg.Controls.Add(this.lbl_ayuda);
            this.pnl_cfg.Controls.Add(this.btn_guardar);
            this.pnl_cfg.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_cfg.Location = new System.Drawing.Point(0, 0);
            this.pnl_cfg.Name = "pnl_cfg";
            this.pnl_cfg.Size = new System.Drawing.Size(1168, 70);
            this.pnl_cfg.TabIndex = 0;
            //
            // lbl_ayuda
            //
            this.lbl_ayuda.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ayuda.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_ayuda.Location = new System.Drawing.Point(16, 10);
            this.lbl_ayuda.Name = "lbl_ayuda";
            this.lbl_ayuda.Size = new System.Drawing.Size(900, 52);
            this.lbl_ayuda.TabIndex = 0;
            this.lbl_ayuda.Text = "Captura el SUELDO FIJO SEMANAL y las HORAS que debe trabajar en la semana (solo sus dias laborales).\r\nPago por hora = Sueldo fijo / Horas por trabajar.   Total a pagar = Pago por hora x Horas trabajadas.";
            //
            // btn_guardar
            //
            this.btn_guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_guardar.FlatAppearance.BorderSize = 0;
            this.btn_guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_guardar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_guardar.ForeColor = System.Drawing.Color.White;
            this.btn_guardar.Location = new System.Drawing.Point(970, 16);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(170, 36);
            this.btn_guardar.TabIndex = 1;
            this.btn_guardar.Text = "Guardar cambios";
            this.btn_guardar.UseVisualStyleBackColor = false;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            //
            // Frm_Nomina
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 680);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.pnl_titulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Nomina";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nomina semanal";
            this.Load += new System.EventHandler(this.Frm_Nomina_Load);
            this.pnl_titulo.ResumeLayout(false);
            this.pnl_titulo.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabRep.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rep)).EndInit();
            this.pnl_total.ResumeLayout(false);
            this.pnl_filtros.ResumeLayout(false);
            this.pnl_filtros.PerformLayout();
            this.tabCfg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_cfg)).EndInit();
            this.pnl_cfg.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_titulo;
        private System.Windows.Forms.Label lbl_titulo;
        private System.Windows.Forms.Button btn_Salir;
        private System.Windows.Forms.Label lbl_linea;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabRep;
        private System.Windows.Forms.TabPage tabCfg;
        private System.Windows.Forms.Panel pnl_filtros;
        private System.Windows.Forms.Label lbl_semana;
        private System.Windows.Forms.Button btn_ant;
        private System.Windows.Forms.DateTimePicker dtp_semana;
        private System.Windows.Forms.Button btn_sig;
        private System.Windows.Forms.Label lbl_rango;
        private System.Windows.Forms.Label lbl_sucursal;
        private System.Windows.Forms.ComboBox cbo_sucursal;
        private System.Windows.Forms.Button btn_pdf;
        private System.Windows.Forms.Button btn_recibos;
        private System.Windows.Forms.DataGridView dgv_rep;
        private System.Windows.Forms.Panel pnl_total;
        private System.Windows.Forms.Label lbl_total;
        private System.Windows.Forms.Panel pnl_cfg;
        private System.Windows.Forms.Label lbl_ayuda;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.DataGridView dgv_cfg;
    }
}
