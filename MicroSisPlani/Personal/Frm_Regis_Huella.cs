using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prj_Capa_Datos;
using Prj_Capa_Entidad;
using Prj_Capa_Negocio;


namespace MicroSisPlani.Personal
{
    public partial class Frm_Regis_Huella : Form
    {
        public Frm_Regis_Huella()
        {
            InitializeComponent();
        }

        private void Frm_Regis_Huella_Load(object sender, EventArgs e)
        {

        }

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button ==MouseButtons.Left )
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        public void Buscar_Personal_ParaEditar(string dni)
        {
            string xFotruta;
            try
            {
                RN_Personal obj = new RN_Personal();
                DataTable data = new DataTable();

                data = obj.RN_Buscar_Personal_xValor(dni);
                if (data.Rows.Count == 0) return;
                {
                    lbl_nroDni.Text = Convert.ToString(data.Rows[0]["DNIPR"]);
                    lbl_nomPersona.Text = Convert.ToString(data.Rows[0]["Nombre_Completo"]);
                    xFotruta = Convert.ToString(data.Rows[0]["Foto"]);
                    lbl_idperso.Text = dni;
                }

                if (File.Exists(xFotruta) == true)
                {
                    picFoto.Load(xFotruta);
                }
                else
                {
                    picFoto.Load(Application.StartupPath + @"\user.png");
                }
            }
            catch (Exception ex)
            {
                picFoto.Load(Application.StartupPath + @"\user.png");
                MessageBox.Show("Error al buscar los datos: " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void EnrollmentControl_OnStartEnroll(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnStartEnroll: [0], finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnSampleQuality(object Control, string ReaderSerialNumber, int Finger, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            ListEvents.Items.Insert(0, String.Format("OnSampleQuality: [0], finger {1}, {2}", ReaderSerialNumber, Finger, CaptureFeedback));
        }

        private void EnrollmentControl_OnReaderDisconnect(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnReaderDisconnect: [0], finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnReaderConnect(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnReaderConnect: [0], finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnFingerTouch(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnFingerTouch: [0], finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnFingerRemove(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnFingerRemove: [0], finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnEnroll(object Control, int FingerMask, DPFP.Template Template, ref DPFP.Gui.EventHandlerStatus EventHandlerStatus)
        {            

            if (Template is null || string.IsNullOrWhiteSpace(lbl_idperso.Text))
            {
                //Template.Serialize(ref bytes);
                MessageBox.Show("No se pudo realizar la opearción requerida", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lbl_idperso.Text = "";
                lbl_nomPersona.Text = "";
                lbl_nroDni.Text = "";
                picFoto.Image = null;
                this.Tag = "";
                this.Close();
                return;

            }

            byte[] bytes = null;
            RN_Personal obj = new RN_Personal();

            Template.Serialize(ref bytes);
            obj.RN_Registrar_Huella_Personal(lbl_idperso.Text, bytes);
            EnrollmentControl.Enabled = false;
            lbl_idperso.Text = "";
            lbl_nomPersona.Text = "";
            lbl_nroDni.Text = "";
            picFoto.Image = null;

            if (BD_Personal.huella == true)
            {
                MessageBox.Show("La Huella Dactilar del Personal, Fue Registrada Exitosamente", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Tag = "A";
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo guardar la huella. Intenta de nuevo.", "Aviso del Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // el control sigue activo para reintentar
            }


        }
    }
}
