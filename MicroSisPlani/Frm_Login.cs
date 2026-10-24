using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prj_Capa_Negocio;


namespace MicroSisPlani
{
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {

        }

        private void Label14_Click(object sender, EventArgs e)
        {

        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            AccederAlSistema();
        }

        private bool ValidarTextbox() 
        {
            if(txt_usu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el nombre de usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_usu.Focus();
                return false;
            }
            if(txt_pass.Text.Trim().Length == 0)
            {
                MessageBox.Show("Por favor, ingrese la contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_pass.Focus();
                return false;
            }
            return true;
        }


        private void AccederAlSistema()
        { 
            RN_Usuario obj = new RN_Usuario();
            DataTable dt = new DataTable();

            int veces = 0;

            if(ValidarTextbox() == false)
            {
                return;
            }

            string usu, pass;
            usu = txt_usu.Text.Trim();
            pass = txt_pass.Text.Trim();

            if (obj.RN_Verificar_Acceso(usu, pass) == true)
            {
                // Acceso permitido
                //LLENAR EL DATATABLE CON LOS DATOS DEL USUARIO

                //MessageBox.Show("Bienvenido al sistema.", "Acceso permitido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Cls_Libreria.Usuario = usu;

                dt = obj.RN_Leer_Datos_Usuarios(usu);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    Cls_Libreria.IdRol = Convert.ToString(dr["Id_Usu"]);
                    Cls_Libreria.Apellidos = Convert.ToString(dr["Nombre_Completo"]);
                    Cls_Libreria.IdRol = Convert.ToString(dr["Id_Rol"]);
                    Cls_Libreria.Rol = Convert.ToString(dr["NomRol"]);
                    Cls_Libreria.Foto = Convert.ToString(dr["Avatar"]);
                }

                this.Hide();
                Frm_Principal xMenuPrincipal = new Frm_Principal();
                xMenuPrincipal.Show();
                xMenuPrincipal.Cargar_Datos_Usuario();
            }
            else
            {
                //SI no son correctos
                MessageBox.Show("Nombre de usuario o contraseña incorrectos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_pass.Text = "";
                txt_usu.Text = "";
                txt_usu.Focus();
                veces += 1;

                if(veces == 3)
                {
                    MessageBox.Show("Ha excedido el número de intentos permitidos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            Frm_Inicio_Asistencia nuevoForm = new Frm_Inicio_Asistencia();
            nuevoForm.Show();
            this.Hide();
        }

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }


        //FUNCIONALIDAD DEL BOTON ENTER
        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Aceptar_Click(sender, e);
            }
        }

        private void txt_usu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_pass.Focus();
            }
        }

        private void Label13_Click(object sender, EventArgs e)
        {

        }
    }
}
