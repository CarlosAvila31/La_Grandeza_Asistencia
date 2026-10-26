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

        // Minutos antes de su hora de entrada en que ya puede marcar (ajustable)
        private const int MinutosAntesPermitidos = 60;

        private DPFP.Verification.Verification Verificar;
        private DPFP.Verification.Verification.Result Resultado;
        private int sec = 10;

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            Complementos move = new Complementos();
            if (e.Button == MouseButtons.Left)
            {
                move.Mover_formulario(this);
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_Marcar_Asistencia_Load(object sender, EventArgs e)
        {
            Verificar = new DPFP.Verification.Verification();
            Resultado = new DPFP.Verification.Verification.Result();
            // Ya no se carga un horario global: cada empleado carga el suyo al identificarse
        }

        // ------------------------------------------------------------
        // HORARIO DEL EMPLEADO
        // ------------------------------------------------------------
        private bool CargarHorario_Personal(string idPersona)
        {
            DataTable data = new RN_Horario().RN_Leer_Horario_Personal(idPersona);
            if (data == null || data.Rows.Count == 0) return false;

            dtp_horaIngre.Value = Convert.ToDateTime(data.Rows[0]["HoEntrada"]);
            dtp_hora_tolercia.Value = Convert.ToDateTime(data.Rows[0]["MinuToleran"]);
            Dtp_Hora_Limite.Value = Convert.ToDateTime(data.Rows[0]["HoLimite"]);
            dtp_horaSalida.Value = Convert.ToDateTime(data.Rows[0]["HoSalida"]);
            Lbl_HoraEntrada.Text = dtp_horaIngre.Value.ToString("HH:mm");
            return true;
        }

        // ------------------------------------------------------------
        // MENSAJES EN PANTALLA
        // ------------------------------------------------------------
        private void MostrarError(string texto)
        {
            lbl_msm.BackColor = Color.MistyRose;
            lbl_msm.ForeColor = Color.Red;
            lbl_msm.Text = texto;
            sec = 10;
            lbl_Cont.Text = "10";
            xVerificationControl.Enabled = true;
            pnl_Msm.Visible = true;
            tmr_Conta.Enabled = true;
        }

        private void MostrarOk(string texto)
        {
            lbl_msm.BackColor = Color.YellowGreen;
            lbl_msm.ForeColor = Color.White;
            lbl_msm.Text = texto;
            sec = 10;
            lbl_Cont.Text = "10";
            xVerificationControl.Enabled = false;
            pnl_Msm.Visible = true;
            tmr_Conta.Enabled = true;
        }

        // ------------------------------------------------------------
        // 1) IDENTIFICAR A LA PERSONA POR SU HUELLA
        // ------------------------------------------------------------
        private void xVerificationControl_OnComplete(object Control, FeatureSet FeatureSet, ref DPFP.Gui.EventHandlerStatus EventHandlerStatus)
        {
            try
            {
                DataTable datosPersona = new RN_Personal().RN_Leer_todoPersona();
                if (datosPersona == null || datosPersona.Rows.Count == 0)
                {
                    MostrarError("No hay personal registrado");
                    return;
                }

                DPFP.Template TemplateBD = new DPFP.Template();
                bool encontrado = false;

                foreach (DataRow xitem in datosPersona.Rows)
                {
                    byte[] finguerByte = xitem["FinguerPrint"] as byte[];          // NULL -> null
                    if (finguerByte == null || finguerByte.Length < 20) continue;  // sin huella real

                    TemplateBD.DeSerialize(finguerByte);
                    Verificar.Verify(FeatureSet, TemplateBD, ref Resultado);

                    if (Resultado.Verified == true)
                    {
                        encontrado = true;
                        ProcesarMarcaje(xitem);
                        break;   // ya identificamos a la persona
                    }
                }

                if (!encontrado)
                    MostrarError("La huella dactilar no existe en la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // ------------------------------------------------------------
        // 2) DECIDIR: SALIDA, JORNADA COMPLETA O ENTRADA (según SU horario)
        // ------------------------------------------------------------
        private void ProcesarMarcaje(DataRow xitem)
        {
            RN_Asistencia objas = new RN_Asistencia();
            Frm_Filtro fil = new Frm_Filtro();

            // Datos de la persona en pantalla
            string rutaFoto = Convert.ToString(xitem["Foto"]).Trim();
            lbl_nombresocio.Text = Convert.ToString(xitem["Nombre_Completo"]);
            Lbl_Idperso.Text = Convert.ToString(xitem["Id_Pernl"]);
            lbl_Dni.Text = Convert.ToString(xitem["DNIPR"]);

            if (File.Exists(rutaFoto)) picSocio.Load(rutaFoto);
            else picSocio.Load(Application.StartupPath + @"\user.png");

            string idPersona = Lbl_Idperso.Text.Trim();

            // Cargar el horario propio de este empleado
            if (!CargarHorario_Personal(idPersona))
            {
                MostrarError("Este empleado no tiene un horario asignado");
                return;
            }

            DateTime ahora = DateTime.Now;
            DateTime hEntrada = DateTime.Today + dtp_horaIngre.Value.TimeOfDay;
            DateTime hLimite = DateTime.Today + Dtp_Hora_Limite.Value.TimeOfDay;
            TimeSpan hSalida = dtp_horaSalida.Value.TimeOfDay;

            // ---------- A) Tiene entrada abierta -> SALIDA ----------
            if (objas.RN_Checar_SiPersonal_YaMarco_suEntrada(idPersona) == true)
            {
                TimeSpan faltan = hSalida - ahora.TimeOfDay;

                Frm_Sinox sino = new Frm_Sinox();
                if (faltan > TimeSpan.Zero)
                    sino.Lbl_msm1.Text = "Tu horario de salida es a las " + hSalida.ToString(@"hh\:mm") +
                                         " (faltan " + (int)faltan.TotalMinutes + " min).\n¿Seguro que deseas registrar tu salida?";
                else
                    sino.Lbl_msm1.Text = "¿Estás seguro de registrar tu salida?";

                fil.Show();
                sino.ShowDialog();
                fil.Hide();

                if (Convert.ToString(sino.Tag) != "Si")
                {
                    LimpiarFormulario();   // canceló: no se registra nada
                    return;
                }

                DataTable datosAsis = objas.RN_Buscar_Asistencia_deEntrada(idPersona);
                if (datosAsis == null || datosAsis.Rows.Count == 0)
                {
                    MostrarError("No se encontró una entrada pendiente para este empleado");
                    return;
                }

                lbl_IdAsis.Text = Convert.ToString(datosAsis.Rows[0]["Id_asis"]).Trim();

                // Horas trabajadas = hora actual - hora de entrada guardada
                double totalHoras = 0;
                TimeSpan horaEntrada;
                if (TimeSpan.TryParse(Convert.ToString(datosAsis.Rows[0]["HoIngreso"]), out horaEntrada))
                {
                    totalHoras = Math.Round((ahora.TimeOfDay - horaEntrada).TotalHours, 2);
                    if (totalHoras < 0) totalHoras = 0;
                }

                BD_Asistencia bd = new BD_Asistencia();
                string mensaje;
                bool registrada = bd.BD_Registrar_Salida_Personal_ConTotal(
                    lbl_IdAsis.Text,
                    idPersona,
                    ahora.ToString("HH:mm:ss"),
                    totalHoras,
                    out mensaje);

                if (registrada)
                    MostrarOk(mensaje);
                else
                    MessageBox.Show("No se pudo registrar la salida: " + mensaje, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ---------- B) Ya tiene entrada y salida hoy ----------
            if (objas.RN_Checar_SiPersonal_YaMarco_Asistencia(idPersona) == true)
            {
                if (objas.RN_Suspendido_Hoy(idPersona))
                    MostrarError("Estás suspendido el día de hoy por acumular " +
                                 RN_Asistencia.TardanzasParaSuspension + " tardanzas");
                else if (objas.RN_Checar_SiPersonal_YaMarco_suFalta(idPersona))
                    MostrarError("Hoy se te registró falta por no marcar tu entrada antes de tu hora límite");
                else
                    MostrarError("El sistema verificó que el personal ya completó su asistencia de hoy");
                return;
            }

            // ---------- C) Sin registro hoy -> ENTRADA ----------
            string nombreDia = ahora.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
            nombreDia = char.ToUpper(nombreDia[0]) + nombreDia.Substring(1);

            // 1 = día laboral, 0 = día de descanso, -1 = semana sin programar
            // (VA PRIMERO: antes de validar horas, para que el descanso gane sobre "hora límite")
            int laboral = new RN_Descanso().RN_Es_Dia_Laboral(idPersona, ahora);

            // a) Día de descanso: no se registra asistencia
            if (laboral == 0)
            {
                MostrarError("Hoy es tu día de descanso, no puedes registrar asistencia");
                return;
            }

            // b) Suspensión / tardanzas acumuladas (solo en día laboral)
            if (laboral == 1)
            {
                if (objas.RN_Suspendido_Hoy(idPersona))
                {
                    MostrarError("Estás suspendido el día de hoy por acumular " +
                                 RN_Asistencia.TardanzasParaSuspension + " tardanzas");
                    return;
                }

                if (!objas.RN_Checar_SiPersonal_TieneAsistencia_Registrada(idPersona) &&
                    objas.RN_Tardanzas_Pendientes(idPersona) >= RN_Asistencia.TardanzasParaSuspension)
                {
                    objas.RN_Registrar_Falta_Personal(RN_Utilitario.RN_NroDoc(3), idPersona, nombreDia,
                        "Suspensión por acumular " + RN_Asistencia.TardanzasParaSuspension + " tardanzas", "Suspension");
                    if (BD_Asistencia.faltasaved == true) RN_Utilitario.RN_Actualiza_Tipo_Doc(3);

                    MostrarError("Estás suspendido el día de hoy por acumular " +
                                 RN_Asistencia.TardanzasParaSuspension + " tardanzas");
                    return;
                }
            }

            // c) Validaciones de horario
            DateTime desde = hEntrada.AddMinutes(-MinutosAntesPermitidos);
            if (ahora < desde)
            {
                MostrarError("Tu horario inicia a las " + hEntrada.ToString("HH:mm") +
                             ". Podrás marcar desde las " + desde.ToString("HH:mm"));
                return;
            }

            if (ahora >= hLimite)
            {
                MostrarError("Tu hora límite de entrada (" + hLimite.ToString("HH:mm") + ") ya pasó");
                return;
            }

            // Tardanza: solo si pasó la tolerancia de SU horario
            DateTime tolerada = hEntrada.AddMinutes(dtp_hora_tolercia.Value.Minute);
            double tarde = 0;
            string justif = "";
            if (ahora > tolerada)
            {
                tarde = Math.Floor((ahora - tolerada).TotalMinutes);
                justif = "Tardanza no justificada";
            }
            lbl_totaltarde.Text = tarde.ToString();
            lbl_justifi.Text = justif;


            lbl_IdAsis.Text = RN_Utilitario.RN_NroDoc(3);
            objas.RN_Registrar_Entrada_Personal(lbl_IdAsis.Text, idPersona, ahora.ToString("HH:mm:ss"),
                                                tarde, 0, justif, nombreDia);

            if (BD_Asistencia.entrada == true)
            {
                RN_Utilitario.RN_Actualiza_Tipo_Doc(3);
                MostrarOk(tarde > 0
                    ? "Entrada registrada con " + (int)tarde + " min de tardanza"
                    : "La entrada del Personal fue registrada exitosamente");
            }
        }

        // ------------------------------------------------------------
        // TEMPORIZADOR Y LIMPIEZA
        // ------------------------------------------------------------
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