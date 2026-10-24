using Klik.Windows.Forms.v1.EntryLib;
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
    public partial class Frm_Admin_Home : Form
    {
        public Frm_Admin_Home()
        {
            InitializeComponent();

        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Dashboard_Click(object sender, EventArgs e)
        {
            if (sidebar.Width == 70)
            {
                sidebar.Width = 200;
            }
            else
            {
                sidebar.Width = 70; 
            }
        }

        private void btn_Inicio_Click(object sender, EventArgs e)
        {
           tabControl1.SelectedTab = Tab_Inicio;
        }

        private void btn_Asistencia_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Tab_Asistencia;
        }

        private void btn_Justi_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Tab_Justi;
        }

        private void btn_Personal_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Tab_Personal;
        }
    }
}
