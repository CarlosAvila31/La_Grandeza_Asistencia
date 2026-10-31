namespace MicroSisPlani.Personal
{
    partial class Frm_Horas_Extra
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
                        this.pnl_titulo = new System.Windows.Forms.Panel();
            this.lbl_titulo = new System.Windows.Forms.Label();
            this.lbl_linea = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.lbl_cuenta = new System.Windows.Forms.Label();
            this.dgv_extra = new System.Windows.Forms.DataGridView();
            this.colAutorizar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSucursal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHorario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_confirmar = new System.Windows.Forms.Button();
            this.btn_nadie = new System.Windows.Forms.Button();
            this.pnl_titulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_extra)).BeginInit();
            this.SuspendLayout();
            //
            // pnl_titulo
            //
            this.pnl_titulo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_titulo.Controls.Add(this.lbl_titulo);
            this.pnl_titulo.Controls.Add(this.lbl_linea);
            this.pnl_titulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_titulo.Location = new System.Drawing.Point(0, 0);
            this.pnl_titulo.Name = "pnl_titulo";
            this.pnl_titulo.Size = new System.Drawing.Size(760, 50);
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
            this.lbl_titulo.Text = "HORAS EXTRA DEL PERSONAL";
            this.lbl_titulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_titulo_MouseMove);
            //
            // lbl_linea
            //
            this.lbl_linea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lbl_linea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_linea.Location = new System.Drawing.Point(0, 48);
            this.lbl_linea.Name = "lbl_linea";
            this.lbl_linea.Size = new System.Drawing.Size(760, 2);
            this.lbl_linea.TabIndex = 1;
            //
            // lbl_info
            //
            this.lbl_info.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_info.ForeColor = System.Drawing.Color.DimGray;
            this.lbl_info.Location = new System.Drawing.Point(16, 62);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(728, 40);
            this.lbl_info.TabIndex = 1;
            this.lbl_info.Text = "Marca a quienes SE QUEDAN en horas extra hoy. A los demas se les registrara la salida automatica a su hora de salida.";
            //
            // lbl_cuenta
            //
            this.lbl_cuenta.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cuenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lbl_cuenta.Location = new System.Drawing.Point(16, 104);
            this.lbl_cuenta.Name = "lbl_cuenta";
            this.lbl_cuenta.Size = new System.Drawing.Size(728, 24);
            this.lbl_cuenta.TabIndex = 2;
            this.lbl_cuenta.Text = "";
            //
            // dgv_extra
            //
            this.dgv_extra.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_extra.BackgroundColor = System.Drawing.Color.White;
            this.dgv_extra.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_extra.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAutorizar,
            this.colId,
            this.colEmpleado,
            this.colSucursal,
            this.colHorario,
            this.colEntrada});
            this.dgv_extra.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_extra.Location = new System.Drawing.Point(16, 134);
            this.dgv_extra.Name = "dgv_extra";
            this.dgv_extra.Size = new System.Drawing.Size(728, 300);
            this.dgv_extra.TabIndex = 3;
            //
            // colAutorizar
            //
            this.colAutorizar.FillWeight = 14F;
            this.colAutorizar.HeaderText = "Se queda";
            this.colAutorizar.Name = "colAutorizar";
            //
            // colId
            //
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colEmpleado
            //
            this.colEmpleado.FillWeight = 36F;
            this.colEmpleado.HeaderText = "Empleado";
            this.colEmpleado.Name = "colEmpleado";
            this.colEmpleado.ReadOnly = true;
            //
            // colSucursal
            //
            this.colSucursal.FillWeight = 22F;
            this.colSucursal.HeaderText = "Sucursal";
            this.colSucursal.Name = "colSucursal";
            this.colSucursal.ReadOnly = true;
            //
            // colHorario
            //
            this.colHorario.FillWeight = 20F;
            this.colHorario.HeaderText = "Horario";
            this.colHorario.Name = "colHorario";
            this.colHorario.ReadOnly = true;
            //
            // colEntrada
            //
            this.colEntrada.FillWeight = 14F;
            this.colEntrada.HeaderText = "Entrada";
            this.colEntrada.Name = "colEntrada";
            this.colEntrada.ReadOnly = true;
            //
            // btn_confirmar
            //
            this.btn_confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_confirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_confirmar.FlatAppearance.BorderSize = 0;
            this.btn_confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confirmar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_confirmar.ForeColor = System.Drawing.Color.White;
            this.btn_confirmar.Location = new System.Drawing.Point(584, 446);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(160, 40);
            this.btn_confirmar.TabIndex = 4;
            this.btn_confirmar.Text = "Guardar";
            this.btn_confirmar.UseVisualStyleBackColor = false;
            this.btn_confirmar.Click += new System.EventHandler(this.btn_confirmar_Click);
            //
            // btn_nadie
            //
            this.btn_nadie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_nadie.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_nadie.FlatAppearance.BorderSize = 0;
            this.btn_nadie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_nadie.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_nadie.ForeColor = System.Drawing.Color.DimGray;
            this.btn_nadie.Location = new System.Drawing.Point(408, 446);
            this.btn_nadie.Name = "btn_nadie";
            this.btn_nadie.Size = new System.Drawing.Size(160, 40);
            this.btn_nadie.TabIndex = 5;
            this.btn_nadie.Text = "Cerrar";
            this.btn_nadie.UseVisualStyleBackColor = false;
            this.btn_nadie.Click += new System.EventHandler(this.btn_nadie_Click);
            //
            // Frm_Horas_Extra
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(760, 500);
            this.Controls.Add(this.btn_nadie);
            this.Controls.Add(this.btn_confirmar);
            this.Controls.Add(this.dgv_extra);
            this.Controls.Add(this.lbl_cuenta);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.pnl_titulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Horas_Extra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Horas extra";
            this.Load += new System.EventHandler(this.Frm_Horas_Extra_Load);
            this.pnl_titulo.ResumeLayout(false);
            this.pnl_titulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_extra)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnl_titulo;
        private System.Windows.Forms.Label lbl_titulo;
        private System.Windows.Forms.Label lbl_linea;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.Label lbl_cuenta;
        private System.Windows.Forms.DataGridView dgv_extra;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAutorizar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmpleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSucursal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHorario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntrada;
        private System.Windows.Forms.Button btn_confirmar;
        private System.Windows.Forms.Button btn_nadie;
    }
}
