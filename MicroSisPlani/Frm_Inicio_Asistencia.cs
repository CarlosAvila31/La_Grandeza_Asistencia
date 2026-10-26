using MicroSisPlani.Msm_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroSisPlani
{
    public partial class Frm_Inicio_Asistencia : Form
    {
        public Frm_Inicio_Asistencia()
        {
            InitializeComponent();
        }

        private void btn_Salir_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            Frm_Login nuevoForm = new Frm_Login();
            nuevoForm.Show();
            this.Hide();
        }

        private void Tiempo_Tick(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;

            // Día con mayúscula inicial
            string nombreCompleto = fecha.ToString("dddd", new System.Globalization.CultureInfo("es-MX"));
            nombreCompleto = char.ToUpper(nombreCompleto[0]) + nombreCompleto.Substring(1);

            // 1. La fecha en la etiqueta grande (ej: "Miércoles, 21 de octubre de 2026")
            lbl_Fecha.Text = nombreCompleto + fecha.ToString(", dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-MX"));

            // 2. La hora en la etiqueta pequeña abajo (ej: "15:51:24")
            lbl_Hora.Text = fecha.ToString("HH:mm:ss");

        }

        private void Label14_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Hora_Click(object sender, EventArgs e)
        {

        }

        private void Frm_Inicio_Asistencia_Load(object sender, EventArgs e)
        {

        }

        private void pnl_titulo_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }

        }

        private void btn_Entrada_Click(object sender, EventArgs e)
        {
            Frm_Filtro fis = new Frm_Filtro();

            Frm_Marcar_Asistencia asis = new Frm_Marcar_Asistencia();
            fis.Show();
            asis.ShowDialog();
            fis.Hide();
        }
    }
}
