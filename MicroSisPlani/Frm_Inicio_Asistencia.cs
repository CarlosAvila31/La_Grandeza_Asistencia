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
            // 1. La fecha en la etiqueta grande (ej: "miércoles, 21 de octubre de 2026")
            lbl_Fecha.Text = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy");

            // 2. La hora en la etiqueta pequeña abajo (ej: "03:51:24 PM")
            lbl_Hora.Text = DateTime.Now.ToString("hh:mm:ss tt");

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
    }
}
