using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MicroSisPlani.Informes;
using Prj_Capa_Negocio;

namespace MicroSisPlani.Personal
{
    public partial class Frm_Nomina : Form
    {
        private readonly RN_Nomina rn = new RN_Nomina();

        private DateTime lunes, domingo;
        private DataTable datosReporte;
        private readonly HashSet<string> modificados = new HashSet<string>();
        private bool cargando = false;

        public Frm_Nomina()
        {
            InitializeComponent();
        }

        // Estilo común de las dos tablas (el diseño de los controles está en el .Designer)
        private void ConfigurarGrid(DataGridView g, bool editable)
        {
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.RowHeadersVisible = false;
            g.ReadOnly = !editable;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 34;
            g.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.DimGray;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            g.RowTemplate.Height = 30;
            if (editable) g.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ------------------------------------------------------------
        // CARGA INICIAL
        // ------------------------------------------------------------
        private void Frm_Nomina_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrid(dgv_rep, false);
                ConfigurarGrid(dgv_cfg, true);
                CargarSucursales();
                CargarConfig();

                cargando = true;
                dtp_semana.Value = DateTime.Today;
                cargando = false;

                CambiarSemana();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir la nómina: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarSucursales()
        {
            DataTable dt = rn.RN_Listar_Distritos();
            DataRow todas = dt.NewRow();
            todas["Id_Distrito"] = "";
            todas["Distrito"] = "(Todas)";
            dt.Rows.InsertAt(todas, 0);

            cbo_sucursal.DisplayMember = "Distrito";
            cbo_sucursal.ValueMember = "Id_Distrito";
            cbo_sucursal.DataSource = dt;
            cbo_sucursal.SelectedIndex = 0;
        }

        // ------------------------------------------------------------
        // SEMANA (lunes a domingo)
        // ------------------------------------------------------------
        private void dtp_semana_ValueChanged(object sender, EventArgs e)
        {
            if (cargando) return;
            CambiarSemana();
        }

        private void btn_ant_Click(object sender, EventArgs e)
        {
            MoverSemana(-7);
        }

        private void btn_sig_Click(object sender, EventArgs e)
        {
            MoverSemana(7);
        }

        private void MoverSemana(int dias)
        {
            cargando = true;
            dtp_semana.Value = dtp_semana.Value.AddDays(dias);
            cargando = false;
            CambiarSemana();
        }

        private void cbo_sucursal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Calcular();
        }

        // Calcula lunes-domingo de la fecha elegida, actualiza la etiqueta y recalcula
        private void CambiarSemana()
        {
            DateTime f = dtp_semana.Value.Date;
            lunes = f.AddDays(-(((int)f.DayOfWeek + 6) % 7));
            domingo = lunes.AddDays(6);

            lbl_rango.Text = "Lunes " + lunes.ToString("dd/MM/yyyy") + "  al  Domingo " + domingo.ToString("dd/MM/yyyy") +
                             (DateTime.Today >= lunes && DateTime.Today <= domingo ? "   (semana en curso: aún incompleta)" : "");

            Calcular();
        }

        // ------------------------------------------------------------
        // REPORTE
        // ------------------------------------------------------------
        private void Calcular()
        {
            try
            {
                string idDistrito = Convert.ToString(cbo_sucursal.SelectedValue);
                DataTable dt = rn.RN_Calcular(lunes, domingo, idDistrito);
                datosReporte = dt;

                dgv_rep.DataSource = dt;
                FormatearReporte();

                object suma = dt.Compute("SUM(Sueldo_Bruto)", "");
                decimal total = (suma == null || suma == DBNull.Value) ? 0 : Convert.ToDecimal(suma);
                int sinSueldo = dt.Select("Horas_Por_Trabajar = 0").Length;

                lbl_total.Text = "Total a pagar: " + total.ToString("C2") + "   (" + dt.Rows.Count + " empleados" +
                                 (sinSueldo > 0 ? ", " + sinSueldo + " sin sueldo capturado" : "") + ")";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo calcular la nómina: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearReporte()
        {
            Cfg(dgv_rep, "Id_Pernl", null, null, false, 0);
            Cfg(dgv_rep, "Pago_Extra", null, null, false, 0);          // solo para el recibo
            Cfg(dgv_rep, "Sucursal", "Sucursal", null, true, 11);
            Cfg(dgv_rep, "Empleado", "Empleado", null, true, 20);
            Cfg(dgv_rep, "Horario", "Horario", null, true, 12);
            Cfg(dgv_rep, "Sueldo_Fijo", "Sueldo fijo", "C2", true, 9);
            Cfg(dgv_rep, "Horas_Por_Trabajar", "Horas por trabajar", "N2", true, 8);
            Cfg(dgv_rep, "Pago_Por_Hora", "Pago por hora", "C2", true, 8);
            Cfg(dgv_rep, "Horas_Trabajadas", "Horas trabajadas", "N2", true, 8);
            Cfg(dgv_rep, "Horas_Extra", "Horas extra", "N2", true, 7);
            Cfg(dgv_rep, "Faltas", "Faltas", "N0", true, 5);
            Cfg(dgv_rep, "Suspensiones", "Susp.", "N0", true, 5);
            Cfg(dgv_rep, "Tardanzas", "Tardanzas", "N0", true, 6);
            Cfg(dgv_rep, "Descuento", "Descuento", "C2", true, 9);
            Cfg(dgv_rep, "Sueldo_Bruto", "Sueldo bruto", "C2", true, 10);

            // Alinear números a la derecha y resaltar el sueldo bruto
            foreach (DataGridViewColumn c in dgv_rep.Columns)
                if (c.DefaultCellStyle.Format != "")
                    c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (dgv_rep.Columns.Contains("Sueldo_Bruto"))
                dgv_rep.Columns["Sueldo_Bruto"].DefaultCellStyle.Font =
                    new Font("Century Gothic", 9F, FontStyle.Bold);

            // Empleados sin sueldo capturado: fila amarilla
            foreach (DataGridViewRow r in dgv_rep.Rows)
            {
                decimal horas = Convert.ToDecimal(r.Cells["Horas_Por_Trabajar"].Value);
                if (horas == 0) r.DefaultCellStyle.BackColor = Color.LemonChiffon;
            }
        }

        private void btn_pdf_Click(object sender, EventArgs e)
        {
            GenerarPdf(false);
        }

        private void btn_recibos_Click(object sender, EventArgs e)
        {
            GenerarPdf(true);
        }

        private void GenerarPdf(bool recibos)
        {
            if (datosReporte == null || datosReporte.Rows.Count == 0)
            {
                MessageBox.Show("No hay empleados para imprimir en esta semana.", "Nómina",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string ruta = recibos
                    ? NominaPdf.GenerarRecibos(datosReporte, lunes, domingo)
                    : NominaPdf.GenerarReporte(datosReporte, lunes, domingo, cbo_sucursal.Text);

                NominaPdf.Abrir(ruta);    // se abre en el visor de PDF de Windows; desde ahí se imprime
            }
            catch (IOException)
            {
                MessageBox.Show("No se pudo guardar el PDF. Si ya lo tienes abierto, ciérralo y vuelve a intentar.",
                                "Nómina", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar el PDF: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------------------------------------------
        // SUELDOS POR EMPLEADO
        // ------------------------------------------------------------
        private void CargarConfig()
        {
            cargando = true;
            modificados.Clear();

            DataTable dt = rn.RN_Listar_Config();
            dgv_cfg.DataSource = dt;

            Cfg(dgv_cfg, "Id_Pernl", null, null, false, 0);
            Cfg(dgv_cfg, "Empleado", "Empleado", null, true, 40);
            Cfg(dgv_cfg, "Sucursal", "Sucursal", null, true, 20);
            Cfg(dgv_cfg, "Sueldo_Fijo", "Sueldo fijo semanal", "N2", true, 20);
            Cfg(dgv_cfg, "Horas_Por_Trabajar", "Horas por trabajar (semana)", "N2", true, 20);

            dgv_cfg.Columns["Empleado"].ReadOnly = true;
            dgv_cfg.Columns["Sucursal"].ReadOnly = true;
            dgv_cfg.Columns["Sueldo_Fijo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv_cfg.Columns["Horas_Por_Trabajar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            cargando = false;
        }

        private void dgv_cfg_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (cargando || e.RowIndex < 0) return;
            string id = Convert.ToString(dgv_cfg.Rows[e.RowIndex].Cells["Id_Pernl"].Value).Trim();
            if (id != "") modificados.Add(id);
        }

        private void dgv_cfg_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show("Escribe solo números (por ejemplo 1500 o 1500.50).", "Valor no válido",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            dgv_cfg.EndEdit();

            if (modificados.Count == 0)
            {
                MessageBox.Show("No hay cambios por guardar.", "Sueldos",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int guardados = 0;
            List<string> errores = new List<string>();

            foreach (DataGridViewRow r in dgv_cfg.Rows)
            {
                string id = Convert.ToString(r.Cells["Id_Pernl"].Value).Trim();
                if (!modificados.Contains(id)) continue;

                string nombre = Convert.ToString(r.Cells["Empleado"].Value);
                try
                {
                    decimal sueldo = Convert.ToDecimal(r.Cells["Sueldo_Fijo"].Value);
                    decimal horas = Convert.ToDecimal(r.Cells["Horas_Por_Trabajar"].Value);
                    rn.RN_Guardar_Config(id, sueldo, horas);
                    guardados++;
                }
                catch (Exception ex)
                {
                    errores.Add(nombre + ": " + ex.Message);
                }
            }

            // Recarga: deja la lista limpia y recalcula el reporte con los sueldos nuevos
            CargarConfig();
            Calcular();

            string msg = guardados + " empleado(s) guardado(s).";
            if (errores.Count > 0)
                msg += "\r\n\r\nNo se guardaron:\r\n- " + string.Join("\r\n- ", errores);
            MessageBox.Show(msg, "Sueldos",
                            MessageBoxButtons.OK,
                            errores.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        // ------------------------------------------------------------
        // UTILIDAD
        // ------------------------------------------------------------
        // Configura una columna si existe. peso = ancho relativo (FillWeight)
        private void Cfg(DataGridView g, string nombre, string encabezado, string formato, bool visible, float peso)
        {
            if (!g.Columns.Contains(nombre)) return;
            DataGridViewColumn c = g.Columns[nombre];
            c.Visible = visible;
            if (encabezado != null) c.HeaderText = encabezado;
            if (formato != null) c.DefaultCellStyle.Format = formato;
            if (peso > 0) c.FillWeight = peso;
        }
    }
}
