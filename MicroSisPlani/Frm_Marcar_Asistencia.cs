using DPFP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Prj_Capa_Negocio;
using MicroSisPlani.Msm_Forms;
using System.IO;
using Prj_Capa_Datos;



namespace MicroSisPlani
{
    public partial class Frm_Marcar_Asistencia : Form
    {
        public Frm_Marcar_Asistencia()
        {
            InitializeComponent();

        }


   

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            Complementos move = new Complementos();
            if (e.Button ==MouseButtons.Left)
            {
                move.Mover_formulario(this);
            }
           

        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void CargarHorarios()
        {
            RN_Horario obj = new RN_Horario();
            DataTable data = new DataTable();

            data = obj.RN_Leer_Horarios();
            if (data.Rows.Count == 0) return;
            dtp_horaIngre.Value = Convert.ToDateTime(data.Rows[0]["HoEntrada"]);
            Lbl_HoraEntrada.Text = dtp_horaIngre.Value.Hour.ToString() + ":" + dtp_horaIngre.Value.Minute.ToString();
            dtp_horaSalida.Value = Convert.ToDateTime(data.Rows[0]["HoSalida"]);
            dtp_hora_tolercia.Value = Convert.ToDateTime(data.Rows[0]["MinuToleran"]);
            Dtp_Hora_Limite.Value = Convert.ToDateTime(data.Rows[0]["HoLimite"]);
        }

        private void Frm_Marcar_Asistencia_Load(object sender, EventArgs e)
        {
            Verificar = new DPFP.Verification.Verification();
            Resultado = new DPFP.Verification.Verification.Result();
            CargarHorarios();
        }


        void calcular_Minutos_Tardanza()
        {
            RN_Asistencia obj = new RN_Asistencia();
            int horaCaptu = DateTime.Now.Hour;
            int minutoCaptu = DateTime.Now.Minute;

            int horaIn = dtp_horaIngre.Value.Hour;
            int MinuIn = dtp_horaIngre.Value.Minute;

            int Mntotole = dtp_hora_tolercia.Value.Minute;

            int totalMinutofijo;
            int totaltardnza;

            DateTime ahora = DateTime.Now;

            // Siempre inicializar para evitar cadenas vacías
            lbl_totaltarde.Text = "0";
            lbl_justifi.Text = "";


            // Hora límite sin tardanza = hora de ingreso + minutos de tolerancia
            DateTime horaIngreso = DateTime.Today
                .AddHours(dtp_horaIngre.Value.Hour)
                .AddMinutes(dtp_horaIngre.Value.Minute);
            DateTime horaTolerada = horaIngreso.AddMinutes(dtp_hora_tolercia.Value.Minute);


            if (ahora > horaTolerada)
            {
                int minutosTarde = (int)(ahora - horaTolerada).TotalMinutes;
                lbl_totaltarde.Text = minutosTarde.ToString();
                lbl_justifi.Text = "Tardanza no justificada";
            }
            //lbl_justifi.Text = "Tardanza no justificada";
            totalMinutofijo = MinuIn + Mntotole; //45

                if (horaCaptu == horaIn & minutoCaptu > totalMinutofijo)
                {
                    totaltardnza = minutoCaptu - totalMinutofijo;
                    lbl_totaltarde.Text = Convert.ToString(totaltardnza);
                }
                else if (horaCaptu > horaIn)
                {
                    int horaTarde;
                    horaTarde = horaCaptu - horaIn;
                    int HoraenMinuto;
                    HoraenMinuto = horaTarde * 60;
                    int totaltardexx = HoraenMinuto - totalMinutofijo;

                    totaltardnza = minutoCaptu + totaltardexx;
                    lbl_totaltarde.Text = Convert.ToString(totaltardnza);
                }
            }

        private DPFP.Verification.Verification Verificar;
        private DPFP.Verification.Verification.Result Resultado;

        private void xVerificationControl_OnComplete(object Control, FeatureSet FeatureSet, ref DPFP.Gui.EventHandlerStatus EventHandlerStatus)
        {
            DPFP.Template TemplateBD = new DPFP.Template();

            RN_Personal obj = new RN_Personal();
            RN_Asistencia objas = new RN_Asistencia();
            DataTable datosPersona = new DataTable();
            DataTable datosAsis = new DataTable();

            string NroIDPersona = "";
            int xitn = 1;
            byte[] finguerByte;
            string rutaFoto;
            bool TeminarBucle = false;
            int totalFila = 0;
            Frm_Filtro fil = new Frm_Filtro();

            int HoLimite = Dtp_Hora_Limite.Value.Hour;
            int MiLimite = Dtp_Hora_Limite.Value.Minute;

            int horaCaptu = DateTime.Now.Hour;
            int minutoCaptu = DateTime.Now.Minute;

            DateTime fecha = DateTime.Now;
            string nombreCompleto = fecha.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
            nombreCompleto = char.ToUpper(nombreCompleto[0]) + nombreCompleto.Substring(1);

            try
            {
                datosPersona = obj.RN_Leer_todoPersona(); //Treamos todo el personal registrado
                totalFila = datosPersona.Rows.Count;
                if (datosPersona.Rows.Count <= 0) return;

                var datoPer = datosPersona.Rows[0];
                foreach (DataRow xitem in datosPersona.Rows)
                {
                    finguerByte = (byte[])xitem["FinguerPrint"];
                    NroIDPersona = Convert.ToString(xitem["Id_Pernl"]);

                    TemplateBD.DeSerialize(finguerByte);

                    Verificar.Verify(FeatureSet, TemplateBD, ref Resultado);
                    if (Resultado.Verified == true)
                    {
                        rutaFoto = Convert.ToString(xitem["Foto"]);
                        lbl_nombresocio.Text = Convert.ToString(xitem["Nombre_Completo"]);
                        Lbl_Idperso.Text = Convert.ToString(xitem["Id_Pernl"]);
                        lbl_Dni.Text = Convert.ToString(xitem["DNIPR"]);

                        if (File.Exists(rutaFoto) == true) { picSocio.Load(rutaFoto.Trim()); }
                        { picSocio.Load(Application.StartupPath + @"\user.png"); }


                        //bool tieneEntrada = objas.RN_Checar_SiPersonal_YaMarco_suEntrada(Lbl_Idperso.Text.Trim());


                        if (objas.RN_Checar_SiPersonal_YaMarco_suEntrada(Lbl_Idperso.Text.Trim()) == true)
                        {
                            Frm_Sinox sino = new Frm_Sinox();
                            TeminarBucle = true;
                            fil.Show();
                            sino.Lbl_msm1.Text = "El usuario ya tiene un registro de entrada, ¿Te gustaria marcar su salida?";
                            sino.ShowDialog();
                            fil.Hide();

                            if (Convert.ToString(sino.Tag) == "Si")
                            {
                                //datosAsis = objas.RN_Buscar_Asistencia_deEntrada(Lbl_Idperso.Text);
                                //if (datosAsis.Rows.Count < 1) return;
                                //lbl_IdAsis.Text = Convert.ToString(datosAsis.Rows[0]["Id_asis"]);
                                //objas.RN_Registrar_Salida_Personal(lbl_IdAsis.Text, Lbl_Idperso.Text, lbl_hora.Text, 8);
                                //if (BD_Asistencia.salida == true)
                                //{
                                //    lbl_msm.BackColor = Color.YellowGreen;
                                //    lbl_msm.ForeColor = Color.White;
                                //    lbl_msm.Text = "La salida del Personal fue registrada exitosamente";
                                //    //tocar_timbre();

                                //    xVerificationControl.Enabled = false;
                                //    pnl_Msm.Visible = true;
                                //    tmr_Conta.Enabled = true;

                                //    lbl_Cont.Text = "10";
                                //    tmr_Conta.Enabled = true;

                                //    TeminarBucle = true;
                                //}


                                if (Convert.ToString(sino.Tag) == "Si")
                                {
                                    datosAsis = objas.RN_Buscar_Asistencia_deEntrada(
                                        Lbl_Idperso.Text.Trim()
                                    );

                                    if (datosAsis == null || datosAsis.Rows.Count == 0)
                                    {
                                        MessageBox.Show(
                                            "No se encontró una entrada pendiente para este empleado."
                                        );
                                        return;
                                    }

                                    lbl_IdAsis.Text = Convert.ToString(
                                        datosAsis.Rows[0]["Id_asis"]
                                    ).Trim();

                                    string horaSalida = DateTime.Now.ToString("HH:mm:ss");

                                    BD_Asistencia bd = new BD_Asistencia();

                                    string mensaje;

                                    bool registrada = bd.BD_Registrar_Salida_Personal_ConTotal(
                                        lbl_IdAsis.Text,
                                        Lbl_Idperso.Text.Trim(),
                                        horaSalida,
                                        0,
                                        out mensaje
                                    );

                                    if (registrada)
                                    {
                                        lbl_msm.BackColor = Color.YellowGreen;
                                        lbl_msm.ForeColor = Color.White;
                                        lbl_msm.Text = mensaje;

                                        xVerificationControl.Enabled = false;
                                        pnl_Msm.Visible = true;

                                        sec = 10;
                                        lbl_Cont.Text = "10";
                                        tmr_Conta.Start();
                                    }
                                    else
                                    {
                                        MessageBox.Show(
                                            "No se pudo registrar la salida: " + mensaje,
                                            "Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error
                                        );
                                    }
                                }

                            }
                        }
                        else
                        {
                            //Entonces marcamos entrada
                            if (horaCaptu >= HoLimite)
                            {
                                lbl_msm.BackColor = Color.MistyRose;
                                lbl_msm.ForeColor = Color.Red;
                                lbl_msm.Text = "Estimado Usuario, su hora de entrada ya caducó, vuelve a casa y regrese mañana";
                                //tocar_timbre();
                                lbl_Cont.Text = "10";
                                xVerificationControl.Enabled = true;
                                pnl_Msm.Visible = true;
                                tmr_Conta.Enabled = true;
                                return;
                            }

                            calcular_Minutos_Tardanza();

                            double tarde = 0;
                            double.TryParse(lbl_totaltarde.Text, out tarde);

                            int totalHoras = 0;
                            int.TryParse(lbl_TotalHotrabajda.Text, out totalHoras);

                            lbl_IdAsis.Text = RN_Utilitario.RN_NroDoc(3);
                            objas.RN_Registrar_Entrada_Personal(lbl_IdAsis.Text, Lbl_Idperso.Text, lbl_hora.Text, Convert.ToDouble(lbl_totaltarde.Text), Convert.ToInt32(lbl_TotalHotrabajda.Text), lbl_justifi.Text, nombreCompleto);
                            if (BD_Asistencia.entrada == true)
                            {
                                RN_Utilitario.RN_Actualiza_Tipo_Doc(3);
                                lbl_msm.BackColor = Color.YellowGreen;
                                lbl_msm.ForeColor = Color.White;
                                lbl_msm.Text = "La entrada del Personal fue registrada exitosamente";
                                //tocar_timbre();

                                xVerificationControl.Enabled = false;
                                pnl_Msm.Visible = true;
                                tmr_Conta.Enabled = true;

                                lbl_Cont.Text = "10";
                                tmr_Conta.Enabled = true;

                                TeminarBucle = true;
                            }

                        }

                        if (objas.RN_Checar_SiPersonal_YaMarco_Asistencia(Lbl_Idperso.Text.Trim()) == true)
                        {
                            lbl_msm.BackColor = Color.MistyRose;
                            lbl_msm.ForeColor = Color.Red;
                            lbl_msm.Text = "El sistema Verificó, que el personal ya marcó su asistencia";
                            //tocar_timbre();
                            lbl_Cont.Text = "10";
                            xVerificationControl.Enabled = true;
                            pnl_Msm.Visible = true;
                            tmr_Conta.Enabled = true;
                            return;
                        }

                        
                    } //todo es true:
                    else
                    {
                        if (xitn == totalFila)
                        {
                            if (TeminarBucle == false)
                            {
                                lbl_msm.Text = "La huella dactilar no existe en la base de datos";
                                lbl_msm.BackColor = Color.MistyRose;
                                lbl_msm.ForeColor = Color.Red;
                                //tocar_timbre();
                                lbl_Cont.Text = "10";
                                xVerificationControl.Enabled = true;
                                pnl_Msm.Visible = true;
                                tmr_Conta.Enabled = true;
                            }
                        }
                    }
                    xitn += 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private int sec = 10;
        private void tmr_Conta_Tick(object sender, EventArgs e)
        {
            sec -= 1;
            lbl_Cont.Text = sec.ToString();
            lbl_Cont.Refresh();

            if (sec == 0)
            {
                LimpiarFormulario();
                sec = 10;
                tmr_Conta.Stop();
                pnl_Msm.Visible = false;
                xVerificationControl.Enabled = true;
                lbl_Cont.Text = "10";
            }
        }

        private void LimpiarFormulario()
        {
            lbl_nombresocio.Text = "";
            lbl_totaltarde.Text = "0";
            lbl_TotalHotrabajda.Text = "0";
            lbl_Dni.Text = "";
            lbl_Cont.Text = "0";
            lbl_IdAsis.Text = "";
            Lbl_Idperso.Text = "";
            lbl_justifi.Text = "";
            lbl_msm.BackColor = Color.Transparent;
            lbl_msm.Text = "";
            picSocio.Image = null;
            xVerificationControl.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_hora.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
