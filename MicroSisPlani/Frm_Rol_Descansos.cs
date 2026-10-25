using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Prj_Capa_Negocio;

namespace MicroSisPlani.Personal
{
    public partial class Frm_Rol_Descansos : Form
    {
        public Frm_Rol_Descansos()
        {
            InitializeComponent();
        }

        // Regla y estado de cada fila (se guarda en row.Tag)
        private class FilaInfo
        {
            public HashSet<int> Permitidos = new HashSet<int>();
            public int Cantidad = 1;          // dias de descanso exactos por semana
            public bool Programada;           // la semana ya estaba capturada en BD
            public bool Modificada;           // cambios sin guardar
        }

        private readonly string[] _nombresDias = { "Lun", "Mar", "Mie", "Jue", "Vie", "Sab", "Dom" };
        private bool _cargando = false;
        private DateTime _lunesActual = DateTime.MinValue;

        // Lunes de la semana a la que pertenece una fecha
        private static DateTime Lunes(DateTime f)
        {
            int dif = ((int)f.DayOfWeek + 6) % 7;   // Lunes = 0
            return f.Date.AddDays(-dif);
        }

        private void Frm_Rol_Descansos_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CargarSemana();
        }

        private void ConfigurarGrid()
        {
            lbl_ayuda.Text = "Gris oscuro = dia no permitido para su rol (asistencia obligatoria). " +
                             "Una semana registrada queda BLOQUEADA y ya no se puede modificar.";

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 34;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.DimGray;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.RowTemplate.Height = 30;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.GridColor = Color.Gainsboro;

            dgv.Columns.Clear();

            dgv.Columns.Add("Id", "Id");
            dgv.Columns["Id"].Visible = false;

            dgv.Columns.Add("Nombre", "Empleado");
            dgv.Columns["Nombre"].ReadOnly = true;
            dgv.Columns["Nombre"].FillWeight = 200;

            dgv.Columns.Add("Rol", "Rol");
            dgv.Columns["Rol"].ReadOnly = true;
            dgv.Columns["Rol"].FillWeight = 110;

            for (int i = 0; i < 7; i++)
            {
                DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
                c.Name = "d" + (i + 1);          // d1 = Lunes ... d7 = Domingo
                c.HeaderText = _nombresDias[i];
                c.FillWeight = 40;
                dgv.Columns.Add(c);
            }

            dgv.Columns.Add("Estado", "Estado");
            dgv.Columns["Estado"].ReadOnly = true;
            dgv.Columns["Estado"].FillWeight = 100;

            dgv.CurrentCellDirtyStateChanged += dgv_CurrentCellDirtyStateChanged;
            dgv.CellValueChanged += dgv_CellValueChanged;
        }

        // ------------------------------------------------------------
        // Cargar
        // ------------------------------------------------------------
        private void CargarSemana()
        {
            _cargando = true;
            try
            {
                DateTime lunes = Lunes(dtp_semana.Value);
                DataTable dt = new RN_Descanso().RN_Listar_Semana(lunes);

                dgv.Rows.Clear();
                if (dt != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        FilaInfo info = new FilaInfo();
                        info.Programada = Convert.ToBoolean(r["Programada"]);
                        info.Cantidad = Convert.ToInt32(r["Descansos"]);
                        foreach (string s in Convert.ToString(r["Dias_Permitidos"]).Split(','))
                        {
                            int n;
                            if (int.TryParse(s, out n)) info.Permitidos.Add(n);
                        }

                        HashSet<int> actuales = new HashSet<int>();
                        foreach (string s in Convert.ToString(r["Dias_Descanso"]).Split(','))
                        {
                            int n;
                            if (int.TryParse(s, out n)) actuales.Add(n);
                        }

                        int fila = dgv.Rows.Add();
                        DataGridViewRow row = dgv.Rows[fila];
                        row.Tag = info;
                        row.Cells["Id"].Value = Convert.ToString(r["Id_Pernl"]).Trim();
                        row.Cells["Nombre"].Value = Convert.ToString(r["Nombre_Completo"]);
                        row.Cells["Rol"].Value = Convert.ToString(r["NomRol"]);

                        for (int i = 1; i <= 7; i++)
                        {
                            DataGridViewCell cel = row.Cells["d" + i];
                            bool permitido = info.Permitidos.Contains(i);
                            // Un descanso no permitido para el rol no se muestra marcado
                            cel.Value = permitido && actuales.Contains(i);
                            if (!permitido)
                            {
                                cel.ReadOnly = true;
                                cel.Style.BackColor = Color.Gainsboro;
                                cel.Style.SelectionBackColor = Color.Gainsboro;
                            }
                            else if (info.Programada)
                            {
                                // Semana ya registrada: bloqueada, no se puede modificar
                                cel.ReadOnly = true;
                                cel.Style.BackColor = Color.WhiteSmoke;
                                cel.Style.SelectionBackColor = Color.WhiteSmoke;
                            }
                        }

                        if (info.Programada)
                            row.DefaultCellStyle.ForeColor = Color.Gray;

                        ActualizarEstado(row);
                    }
                }

                _lunesActual = lunes;
                lbl_rango.Text = lunes.ToString("dd/MM/yyyy") + "  al  " + lunes.AddDays(6).ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la semana: " + ex.Message, "Aviso del Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cargando = false;
            }
        }

        // ------------------------------------------------------------
        // Reglas en pantalla
        // ------------------------------------------------------------
        private int ContarMarcados(DataGridViewRow row)
        {
            int n = 0;
            for (int i = 1; i <= 7; i++)
            {
                object v = row.Cells["d" + i].Value;
                if (v is bool && (bool)v) n++;
            }
            return n;
        }

        private void ActualizarEstado(DataGridViewRow row)
        {
            FilaInfo info = (FilaInfo)row.Tag;
            DataGridViewCell est = row.Cells["Estado"];

            if (info.Programada)
            {
                est.Value = "Bloqueada";
                est.Style.ForeColor = Color.DimGray;
            }
            else if (ContarMarcados(row) != info.Cantidad)
            {
                est.Value = "Falta elegir";
                est.Style.ForeColor = Color.Red;
            }
            else if (info.Modificada)
            {
                est.Value = "Sin guardar";
                est.Style.ForeColor = Color.RoyalBlue;
            }
            else
            {
                est.Value = "Heredada";
                est.Style.ForeColor = Color.DarkOrange;
            }
        }

        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Confirma la casilla al instante (sin esperar a salir de la celda)
            if (dgv.IsCurrentCellDirty)
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_cargando || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string col = dgv.Columns[e.ColumnIndex].Name;
            if (col.Length != 2 || col[0] != 'd') return;   // solo las casillas d1..d7

            DataGridViewRow row = dgv.Rows[e.RowIndex];
            FilaInfo info = (FilaInfo)row.Tag;
            object valor = row.Cells[e.ColumnIndex].Value;
            bool marcado = valor is bool && (bool)valor;

            _cargando = true;   // evita que los cambios de abajo vuelvan a disparar este evento
            try
            {
                if (marcado)
                {
                    if (info.Cantidad == 1)
                    {
                        // Un solo dia: elegir otro mueve la seleccion
                        for (int i = 1; i <= 7; i++)
                            if ("d" + i != col) row.Cells["d" + i].Value = false;
                    }
                    else if (ContarMarcados(row) > info.Cantidad)
                    {
                        row.Cells[e.ColumnIndex].Value = false;
                        MessageBox.Show("Este empleado solo puede tener " + info.Cantidad + " dias de descanso por semana.",
                                        "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                info.Modificada = true;
                ActualizarEstado(row);
            }
            finally
            {
                _cargando = false;
            }
        }

        private bool HayCambiosSinGuardar()
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                FilaInfo info = row.Tag as FilaInfo;
                if (info != null && info.Modificada) return true;
            }
            return false;
        }

        // ------------------------------------------------------------
        // Cambio de semana
        // ------------------------------------------------------------
        private void dtp_semana_ValueChanged(object sender, EventArgs e)
        {
            if (_cargando) return;

            DateTime nueva = Lunes(dtp_semana.Value);
            if (nueva == _lunesActual) return;   // misma semana: no recargar

            if (HayCambiosSinGuardar())
            {
                DialogResult r = MessageBox.Show(
                    "Hay cambios sin guardar en la semana actual.\n¿Cambiar de semana y perderlos?",
                    "Cambios sin guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r != DialogResult.Yes)
                {
                    _cargando = true;
                    dtp_semana.Value = _lunesActual;   // regresa a la semana que se estaba editando
                    _cargando = false;
                    return;
                }
            }

            CargarSemana();
        }

        private void btn_semAnterior_Click(object sender, EventArgs e)
        {
            dtp_semana.Value = dtp_semana.Value.AddDays(-7);   // dispara ValueChanged
        }

        private void btn_semSiguiente_Click(object sender, EventArgs e)
        {
            dtp_semana.Value = dtp_semana.Value.AddDays(7);
        }

        // ------------------------------------------------------------
        // Guardar
        // ------------------------------------------------------------
        private void btn_guardar_Click(object sender, EventArgs e)
        {
            dgv.EndEdit();   // confirma la casilla que se este editando

            // 1) Separar: ya bloqueadas (se ignoran), listas para guardar y incompletas
            List<DataGridViewRow> listas = new List<DataGridViewRow>();
            int incompletas = 0, heredadas = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                FilaInfo info = row.Tag as FilaInfo;
                if (info == null || info.Programada) continue;   // ya registrada y bloqueada

                if (ContarMarcados(row) != info.Cantidad) { incompletas++; continue; }

                listas.Add(row);
                if (!info.Modificada) heredadas++;
            }

            if (listas.Count == 0)
            {
                MessageBox.Show(incompletas > 0
                    ? "Ningun empleado esta listo: falta elegir el dia de descanso."
                    : "No hay nada que guardar: todos los empleados de esta semana ya estan registrados.",
                    "Rol de descansos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2) Confirmar: despues de guardar ya no se puede modificar
            StringBuilder msg = new StringBuilder();
            msg.AppendLine("Se registraran los descansos de " + listas.Count + " empleado(s).");
            if (heredadas > 0)
                msg.AppendLine(heredadas + " de ellos sin cambios (tomados de la ultima semana programada).");
            if (incompletas > 0)
                msg.AppendLine(incompletas + " empleado(s) sin dia elegido NO se guardaran y seguiran editables.");
            msg.AppendLine();
            msg.AppendLine("UNA VEZ REGISTRADOS NO PODRAN MODIFICARSE.");
            msg.AppendLine("¿Continuar?");

            if (MessageBox.Show(msg.ToString(), "Confirmar registro de descansos",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            // 3) Guardar
            DateTime lunes = Lunes(dtp_semana.Value);
            RN_Descanso rn = new RN_Descanso();

            try
            {
                foreach (DataGridViewRow row in listas)
                {
                    string id = Convert.ToString(row.Cells["Id"].Value);

                    List<string> dias = new List<string>();
                    for (int i = 1; i <= 7; i++)
                    {
                        object v = row.Cells["d" + i].Value;
                        if (v is bool && (bool)v) dias.Add(i.ToString());
                    }
                    rn.RN_Guardar_Semana(id, lunes, string.Join(",", dias.ToArray()));
                }

                MessageBox.Show("Los descansos se registraron correctamente", "Aviso del Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Aviso del Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CargarSemana();   // refleja lo que si alcanzo a guardarse
            }
        }

        private void btn_copiar_Click(object sender, EventArgs e)
        {
            DateTime lunes = Lunes(dtp_semana.Value);

            DialogResult r = MessageBox.Show(
                "Se copiaran los descansos de la semana anterior a esta semana,\n" +
                "solo para los empleados que aun no la tienen programada\n" +
                "y solo los que cumplan la regla de su rol.\n\n¿Continuar?",
                "Copiar semana anterior", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            try
            {
                new RN_Descanso().RN_Copiar_Semana(lunes.AddDays(-7), lunes);
                CargarSemana();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al copiar: " + ex.Message, "Aviso del Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}