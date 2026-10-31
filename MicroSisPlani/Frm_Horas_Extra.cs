using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Prj_Capa_Negocio;

namespace MicroSisPlani.Personal
{
    // Pantalla manual: el admin marca quien SE QUEDA en horas extra hoy.
    // A los que no se marcan se les cierra la salida solos a su hora de salida.
    public partial class Frm_Horas_Extra : Form
    {
        public Frm_Horas_Extra()
        {
            InitializeComponent();
        }

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

        private void Frm_Horas_Extra_Load(object sender, EventArgs e)
        {
            dgv_extra.AllowUserToAddRows = false;
            dgv_extra.AllowUserToDeleteRows = false;
            dgv_extra.AllowUserToResizeRows = false;
            dgv_extra.RowHeadersVisible = false;
            dgv_extra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_extra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_extra.EnableHeadersVisualStyles = false;
            dgv_extra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_extra.ColumnHeadersHeight = 34;
            dgv_extra.ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgv_extra.ColumnHeadersDefaultCellStyle.ForeColor = Color.DimGray;
            dgv_extra.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 9F, FontStyle.Bold);
            dgv_extra.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_extra.RowTemplate.Height = 30;

            colEmpleado.ReadOnly = true;
            colSucursal.ReadOnly = true;
            colHorario.ReadOnly = true;
            colEntrada.ReadOnly = true;

            Cargar();
        }

        private void Cargar()
        {
            try
            {
                dgv_extra.Rows.Clear();
                DataTable dt = new RN_HorasExtra().RN_Abiertos();
                foreach (DataRow r in dt.Rows)
                {
                    bool autorizado = r["Autorizado"] != DBNull.Value && Convert.ToBoolean(r["Autorizado"]);
                    dgv_extra.Rows.Add(autorizado,
                                       Convert.ToString(r["Id_Pernl"]).Trim(),
                                       Convert.ToString(r["Empleado"]),
                                       Convert.ToString(r["Sucursal"]),
                                       Convert.ToString(r["Horario"]),
                                       Convert.ToString(r["Entrada"]));
                }
                lbl_cuenta.Text = dt.Rows.Count == 0
                    ? "No hay empleados con la entrada abierta en este momento."
                    : dt.Rows.Count + " empleado(s) con la entrada abierta hoy.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar: " + ex.Message, "Horas extra",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Ids marcados con "Se queda", separados por coma
        private string IdsMarcados()
        {
            dgv_extra.EndEdit();
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow fila in dgv_extra.Rows)
            {
                bool marcado = fila.Cells[colAutorizar.Index].Value != null &&
                               Convert.ToBoolean(fila.Cells[colAutorizar.Index].Value);
                if (!marcado) continue;
                if (sb.Length > 0) sb.Append(",");
                sb.Append(Convert.ToString(fila.Cells[colId.Index].Value).Trim());
            }
            return sb.ToString();
        }

        private void btn_confirmar_Click(object sender, EventArgs e)   // Guardar
        {
            try
            {
                new RN_HorasExtra().RN_Guardar(IdsMarcados());
                MessageBox.Show("Horas extra guardadas.", "Horas extra",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar: " + ex.Message, "Horas extra",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_nadie_Click(object sender, EventArgs e)       // Cerrar
        {
            this.Close();
        }
    }
}
