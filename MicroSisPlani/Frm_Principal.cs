using MicroSisPlani.Personal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prj_Capa_Datos;
using Prj_Capa_Entidad;
using Prj_Capa_Negocio;
using MicroSisPlani.Msm_Forms;
using System.IO;
using MicroSisPlani.Informes;
using MicroSisPlani.Personal;

namespace MicroSisPlani
{
    public partial class Frm_Principal : Form   
    {
        public Frm_Principal()
        {
            InitializeComponent();
        }

        private void Frm_Principal_Load(object sender, EventArgs e)
        {
            ConfigurationListView();
            ConfigurarListview_Asis();
            CargarHorarios();
            Verificar_Robot_de_Faltas();
        }

        private void Verificar_Robot_de_Faltas()
        {
            string tipo;
            tipo = RN_Utilitario.RN_Listar_TipoFalta(5);

            if (tipo.Trim() == "Si")
            {
                timerFalta.Interval = 60000;   // <-- línea nueva: revisa cada minuto
                timerFalta.Start();
                rdb_ActivarRobot.Checked = true;
            }
            else if (tipo.Trim() == "No")
            {
                timerFalta.Stop();
                rdb_Desact_Robot.Checked = true;
            }
        }

        public void Cargar_Datos_Usuario()
        {
            try
            {
                Frm_Filtro xfil = new Frm_Filtro();

                xfil.Show();
                MessageBox.Show("Bienvenido: " + Cls_Libreria.Apellidos, "Bienvenido al Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                xfil.Hide();

                Lbl_NomUsu.Text = Cls_Libreria.Apellidos;
                lbl_rolNom.Text = Cls_Libreria.Rol;

                if (Cls_Libreria.Foto.Trim().Length == 0 | Cls_Libreria.Foto == null)
                {
                    return;
                }

                if (File.Exists(Cls_Libreria.Foto) == true)
                {
                    pic_user.Load(Cls_Libreria.Foto);
                }
                else
                {
                    //Sino tiene foto cargamos una por defecto que teneos en los recursos del proyecto
                    pic_user.Image = Properties.Resources.user;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void pnl_titu_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button ==MouseButtons.Left )
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_Explorador_Personal pers = new Frm_Explorador_Personal();
            pers.MdiParent = this;
            pers.Show();

        }

        private void bt_personal_Click(object sender, EventArgs e)
        {
           
            elTabPage2 .Visible = true;
            elTab1.SelectedTabPageIndex = 1;
            Cargar_Todo_Personal();
           
          

        }

        private void Frm_Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            //El formulario que se muestra cuando quieres confirmar la salida del programa
            Frm_Sino sino = new Frm_Sino();

            fil.Show();
            sino.Lbl_msm1.Text = "¿Estás seguro de Salir y Abandonar el sistema?";
            sino.ShowDialog();
            fil.Hide();

            if (Convert.ToString(sino.Tag) == "Si")
            {
                Application.ExitThread();
            } 
            else
            {
                e.Cancel = true;
            }
        }

        private void btn_nuevoAsis_Click(object sender, EventArgs e)
        {
           
        }

        private void bt_Explo_Asis_Click(object sender, EventArgs e)
        {
            elTabPage3.Visible = true;
            elTab1.SelectedTabPageIndex = 2;
            Cargar_Asistencias_delDia(dtp_fechadeldia.Value);

        }

        private void Btn_Cerrar_TabPers_Click(object sender, EventArgs e)
        {
            elTabPage2 .Visible = false ;
            elTab1.SelectedTabPageIndex = 0;
        }

        private void btn_cerrarEx_Asis_Click(object sender, EventArgs e)
        {
            elTabPage3.Visible = false;
            elTab1.SelectedTabPageIndex = 0;
        }

        private void bt_copiarNroDNI_Click(object sender, EventArgs e)
        {
            Frm_Advertencia adver = new Frm_Advertencia();
            Frm_Filtro fis = new Frm_Filtro();

            if (lsv_person.SelectedIndices.Count == 0)
            {
                fis.Show();
                adver.Lbl_Msm1.Text = "Seleccione el item que Deseas Copiar";
                adver.ShowDialog();
                fis.Hide(); return;
            }
            else
            {
                var lsv = lsv_person.SelectedItems[0];
                string xdni = lsv.SubItems[1].Text;  //ES [1] pq dni se encuentra en la columna 1 de los Columns.Add

                Clipboard.Clear();
                Clipboard.SetText(xdni.Trim());
            }
        }


        #region "Persona"
        private void ConfigurationListView()
        {
            var lis = lsv_person;
            lis.Columns.Clear();
            lis.Items.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;
            //Configuración de anchos y nombres de columnas
            lis.Columns.Add("Id Persona", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Dni", 95, HorizontalAlignment.Left);
            lis.Columns.Add("Nombres del Socio", 316, HorizontalAlignment.Left);
            lis.Columns.Add("Direccion", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Correo", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Sex", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Fe Nacim.", 110, HorizontalAlignment.Center);
            lis.Columns.Add("Nro Celular", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Rol", 100, HorizontalAlignment.Left);
            lis.Columns.Add("Distrito", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 100, HorizontalAlignment.Left);


        }


        private void Cargar_Todo_Personal()
        {
            RN_Personal obj = new RN_Personal();
            DataTable dt = new DataTable();

            dt = obj.RN_Leer_todoPersona();
            if (dt.Rows.Count > 0)
            {
                //Llama maste aaa (este es el metrodo para llenar el listview chambalares)
                LlenarListView(dt);
            }
        }


        private void Buscar_Personal_PorValor(string xvalor)
        {
            RN_Personal obj = new RN_Personal();
            DataTable dt = new DataTable();

            dt = obj.RN_Buscar_Personal_xValor(xvalor);
            if (dt.Rows.Count > 0)
            {
                //Lamamos al metodo llenar listview para que se actualice en base al filtro
                LlenarListView(dt);
            }
            else
            {
                lsv_person.Items.Clear();
            }
        }

        private void LlenarListView(DataTable data)
        {
            lsv_person.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Pernl"].ToString()); //la cabecera de listview
                list.SubItems.Add(dr["DNIPR"].ToString());
                list.SubItems.Add(dr["Nombre_Completo"].ToString());
                list.SubItems.Add(dr["Domicilio"].ToString());
                list.SubItems.Add(dr["Correo"].ToString());
                list.SubItems.Add(dr["Sexo"].ToString());
                list.SubItems.Add(dr["Fec_Naci"].ToString());
                list.SubItems.Add(dr["Celular"].ToString());
                list.SubItems.Add(dr["NomRol"].ToString());

                list.SubItems.Add(dr["Distrito"].ToString());
                list.SubItems.Add(dr["Estado_Per"].ToString());
                lsv_person.Items.Add(list); //si no ponemos esto el listview nunca se llenara
            }
            Lbl_total.Text = Convert.ToString(lsv_person.Items.Count);
        }


        private void txt_Buscar_OnValueChanged(object sender, EventArgs e)
        {
            if (txt_Buscar.Text.Trim().Length > 2)
            {
                Buscar_Personal_PorValor(txt_Buscar.Text.Trim());
            }
        }

        private void txt_Buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Buscar_Personal_PorValor(txt_Buscar.Text.Trim());
            }
        }

        private void bt_mostrarTodoElPersonal_Click(object sender, EventArgs e)
        {
            Cargar_Todo_Personal();
        }

        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bt_nuevoPersonal_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Registro_Personal per = new Frm_Registro_Personal();

            fil.Show();
            per.xedit = false;
            per.ShowDialog();

            fil.Hide();

            if (Convert.ToString(per.Tag) == "")
                return;
            {
                Cargar_Todo_Personal();
            }
        }

        private void bt_editarPersonal_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Registro_Personal per = new Frm_Registro_Personal();

            if (lsv_person.SelectedIndices.Count == 0)
            {
                //Mostrar mensajes


            }
            else
            {
                var lsv = lsv_person.SelectedItems[0];
                string IdPersona = lsv.SubItems[0].Text;

                fil.Show();
                per.SeVaEditar = true;
                per.Buscar_Personal_ParaEditar(IdPersona);
                per.ShowDialog();
                fil.Hide();

                if (Convert.ToString(per.Tag) == "A")
                {
                    Cargar_Todo_Personal();
                }
            }
        }


        private void bt_eliminarPersonal_Click_1(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            RN_Personal obj = new RN_Personal();

            if (lsv_person.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Seleccione un personal para eliminar",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var lsv = lsv_person.SelectedItems[0];
                string dni = lsv.SubItems[1].Text; // ojo: depende de qué columna tengas el DNI

                obj.RN_Eliminar_Personal(dni);

                if (BD_Personal.deleted == true)
                {
                    fil.Show();
                    ok.Lbl_msm1.Text = "El personal se ha eliminado correctamente";
                    ok.ShowDialog();
                    fil.Hide();

                    Cargar_Todo_Personal(); // refresca la lista
                }

            }
        }
        private void btn_SaveHorario_Click(object sender, EventArgs e)
        {
            try
            {
                //Clases
                RN_Horario hor = new RN_Horario();
                EN_Horario por = new EN_Horario();

                Frm_Filtro fis = new Frm_Filtro();
                Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
                Frm_Advertencia adver = new Frm_Advertencia();

                //Propiedades de la Clase
                por.Idhora = lbl_idHorario.Text;
                por.HoEntrada = dtp_horaIngre.Value;
                por.HoTole = dtp_hora_tolercia.Value;
                por.HoLimite = Dtp_Hora_Limite.Value;
                por.HoSalida = dtp_horaSalida.Value;

                hor.RN_Actualizar_Horario(por);

                if (BD_Horario.saved == true)
                {
                    fis.Show();
                    ok.Lbl_msm1.Text = "El Horario fue actualizado";
                    ok.ShowDialog();
                    fis.Hide();

                    elTabPage4.Visible = false;
                    elTab1.SelectedTabPageIndex = 0;
                }
            }
            catch 
            {

            }
        }

        private void CargarHorarios()
        {
            RN_Horario obj = new RN_Horario();
            DataTable data = new DataTable();

            data = obj.RN_Leer_Horarios();

            if (data.Rows.Count == 0) return;

            lbl_idHorario .Text = Convert.ToString(data.Rows[0]["IdHorario"]);
            dtp_horaIngre.Value = Convert.ToDateTime(data.Rows[0]["HoEntrada"]);
            dtp_hora_tolercia.Value = Convert.ToDateTime(data.Rows[0]["MinuToleran"]);
            Dtp_Hora_Limite.Value = Convert.ToDateTime(data.Rows[0]["HoLimite"]);
            dtp_horaSalida.Value = Convert.ToDateTime(data.Rows[0]["HoSalida"]);
        }

        private void bt_Config_Click(object sender, EventArgs e)
        {
            elTabPage4.Visible = true;
            elTab1.SelectedTabPageIndex = 3;
            CargarHorarios();
        }


        #region "asistencia"
        private void ConfigurarListview_Asis()
        {
            var lis = lsv_asis;
            lis.Columns.Clear();
            lis.Items.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false; //todo lo de aqui a arriba tambien se puede hacer desde propiedades
            //Configuración de anchos y nombres de las columnas
            lis.Columns.Add("Id Asis", 0, HorizontalAlignment.Left); //Se oculta una columna con ancho 0
            lis.Columns.Add("Dni", 80, HorizontalAlignment.Left);
            lis.Columns.Add("Nombres del personal", 316, HorizontalAlignment.Left);
            lis.Columns.Add("Fecha", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Día", 80, HorizontalAlignment.Left);
            lis.Columns.Add("Ho Ingreso", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Tardanza", 70, HorizontalAlignment.Center);
            lis.Columns.Add("Ho Salida", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Adelant", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Justificacion", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 100, HorizontalAlignment.Left);
        }


        private void LlenarListView_Asis(DataTable data)
        {
            lsv_asis.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_asis"].ToString()); //la cabecera del listview
                list.SubItems.Add(dr["DNIPR"].ToString());
                list.SubItems.Add(dr["Nombre_Completo"].ToString());
                list.SubItems.Add(dr["FechaAsis"].ToString());
                list.SubItems.Add(dr["Nombre_dia"].ToString());
                list.SubItems.Add(dr["HoIngreso"].ToString());
                list.SubItems.Add(dr["Tardanzas"].ToString());
                list.SubItems.Add(dr["HoSalida"].ToString());
                list.SubItems.Add(dr["Adelanto"].ToString());

                list.SubItems.Add(dr["Justifacion"].ToString());
                list.SubItems.Add(dr["EstadoAsis"].ToString());
                lsv_asis.Items.Add(list); //si no ponemos esto el listview nunca se llenara
            }
            Lbl_total.Text = Convert.ToString(lsv_asis.Items.Count);
        }


        private void Cargar_todas_Asistencias()
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable dt = new DataTable();

            dt = obj.RN_Ver_Todas_Asistencia();
            if (dt.Rows.Count > 0)
            {
                //Lammamos al metodo llenar listview
                LlenarListView_Asis(dt);
            }
        }

        private void Cargar_Asistencias_delDia(DateTime fechas)
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable dt = new DataTable();

            dt = obj.RN_Ver_Todas_Asistencias_Deldia(fechas);
            if (dt.Rows.Count > 0)
            {
                //Lammamos al metodo llenar listview
                LlenarListView_Asis(dt);
            }
        }


        private void Cargar_Asistencias_delMes(DateTime fechas)
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable dt = new DataTable();

            dt = obj.RN_Ver_Todas_Asistencias_DelMes(fechas);
            if (dt.Rows.Count > 0)
            {
                //Lammamos al metodo llenar listview
                LlenarListView_Asis(dt);
            }
        }



        private void Cargar_Asistencias_porValor(String xvalor)
        {
            RN_Asistencia obj = new RN_Asistencia();
            DataTable dt = new DataTable();

            dt = obj.RN_Ver_Todas_Asistencias_ParaExplorador(xvalor);
            if (dt.Rows.Count > 0)
            {
                //Lammamos al metodo llenar listview
                LlenarListView_Asis(dt);
            }
        }


        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Cargar_todas_Asistencias();
        }


        private void txt_buscarAsis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cargar_Asistencias_porValor(txt_Buscar.Text);
            }

        }

        private void lbl_lupaAsis_Click(object sender, EventArgs e)
        {
            Cargar_todas_Asistencias();
        }



        private void bt_verAsistenciasDelDiaT_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Solo_Fecha solo = new Frm_Solo_Fecha();

            fil.Show();
            solo.ShowDialog();
            fil.Hide();

            if (Convert.ToString(solo.Tag) == "") return;

            DateTime xfecha = solo.dtp_fecha.Value;

            RN_Asistencia obj = new RN_Asistencia();
            DataTable dt = obj.RN_Ver_Todas_Asistencias_Deldia(xfecha);

            if (dt != null && dt.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay asistencias registradas en este día.",
                    "Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
                return;
            }

            Cargar_Asistencias_delDia(xfecha);
        }


        #endregion




        private void elTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bt_registrarHuellaDigital_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Regis_Huella per = new Frm_Regis_Huella();

            //Primero vamos a obtener el ID del personal
            if (lsv_person.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecciona el personal para registrar su huella", "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                var lsv = lsv_person.SelectedItems[0]; //si no selecciono nada nada hacemos  podemos lanzar un mensaje
                string xidsocio = lsv.SubItems[0].Text; //obtenemos el ID del empleado de la primera columna de list view

                fil.Show();
                per.Buscar_Personal_ParaEditar(xidsocio);
                per.ShowDialog();
                fil.Hide();

                if (Convert.ToString(per.Tag) == "")
                    return;
                {
                    Cargar_Todo_Personal();
                }
            }
        }

        private void btn_Asis_With_Huella_Click(object sender, EventArgs e)
        {
            Frm_Filtro fis = new Frm_Filtro();

            Frm_Marcar_Asistencia asis = new Frm_Marcar_Asistencia();
            fis.Show();
            asis.ShowDialog();
            fis.Hide();
        }

        private void txt_buscarAsis_OnValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_Savedrobot_Click(object sender, EventArgs e)
        {
            RN_Utilitario uti = new RN_Utilitario();

            if (rdb_ActivarRobot.Checked == true)
            {
                uti.RN_Actualizar_RobotFalta(5, "Si");
                if (BD_Utilitario.falta == true)
                {
                    Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
                    ok.Lbl_msm1.Text = "El Robot de Faltas fue Activado";
                    ok.ShowDialog();

                    elTab1.SelectedTabPageIndex = 0;
                    elTabPage4.Visible = false;
                }
            }
            else if (rdb_Desact_Robot.Checked == true)
            {
                uti.RN_Actualizar_RobotFalta(5, "No");

                if (BD_Utilitario.falta == true)
                {
                    Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
                    ok.Lbl_msm1.Text = "El Robot de Faltas fue Desactivado";
                    ok.ShowDialog();
                    elTab1.SelectedTabPageIndex = 0;
                    elTabPage4.Visible = false;
                }
            }
        }

        private void timerFalta_Tick(object sender, EventArgs e)
        {
            RN_Asistencia obj = new RN_Asistencia();
            RN_Horario objhor = new RN_Horario();
            RN_Descanso objdes = new RN_Descanso();
            Frm_Filtro fis = new Frm_Filtro();
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();

            DataTable dataper = new RN_Personal().RN_Leer_todoPersona();
            if (dataper == null || dataper.Rows.Count == 0) return;

            DateTime ahora = DateTime.Now;
            string nombreDia = ahora.ToString("dddd", new System.Globalization.CultureInfo("es-ES"));
            nombreDia = char.ToUpper(nombreDia[0]) + nombreDia.Substring(1);

            int faltas = 0, suspensiones = 0;

            timerFalta.Stop();   // evita que un mensaje dispare otra pasada encima
            try
            {
                foreach (DataRow reg in dataper.Rows)
                {
                    string id = Convert.ToString(reg["Id_Pernl"]).Trim();

                    // 1) ¿Ya tiene algún registro hoy (entrada, falta o suspensión)?
                    if (obj.RN_Checar_SiPersonal_TieneAsistencia_Registrada(id)) continue;

                    // 2) ¿Hoy le toca trabajar? (1 = sí, 0 = descanso, -1 = sin programar)
                    if (objdes.RN_Es_Dia_Laboral(id, ahora) != 1) continue;

                    // 3) ¿Ya pasó SU hora límite?
                    DataTable hor = objhor.RN_Leer_Horario_Personal(id);
                    if (hor == null || hor.Rows.Count == 0) continue;
                    DateTime limite = DateTime.Today + Convert.ToDateTime(hor.Rows[0]["HoLimite"]).TimeOfDay;
                    if (ahora < limite) continue;

                    // 4) Falta, o suspensión si acumuló las tardanzas
                    bool suspension = obj.RN_Tardanzas_Pendientes(id) >= RN_Asistencia.TardanzasParaSuspension;
                    obj.RN_Registrar_Falta_Personal(RN_Utilitario.RN_NroDoc(3), id, nombreDia,
                        suspension ? "Suspensión por acumular " + RN_Asistencia.TardanzasParaSuspension + " tardanzas"
                                   : "Falta no justificada",
                        suspension ? "Suspension" : "Falta");

                    if (BD_Asistencia.faltasaved == true)
                    {
                        RN_Utilitario.RN_Actualiza_Tipo_Doc(3);
                        if (suspension) suspensiones++; else faltas++;
                    }
                }

                if (faltas + suspensiones > 0)
                {
                    fis.Show();
                    ok.Lbl_msm1.Text = "Se registraron " + faltas + " falta(s) y " + suspensiones + " suspensión(es)";
                    ok.ShowDialog();
                    fis.Hide();
                }
            }
            finally
            {
                timerFalta.Start();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Frm_Rol_Descansos rol = new Frm_Rol_Descansos();

            rol.Show();
        }


        private void bt_imprimirAsistenciasDelDíaTool_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Solo_Fecha solo = new Frm_Solo_Fecha();

            fil.Show();
            solo.ShowDialog();
            fil.Hide();

            if (Convert.ToString(solo.Tag) == "") return;

            DateTime xdia = solo.dtp_fecha.Value;

            // Verificamos si existen asistencias para ese día
            RN_Asistencia obj = new RN_Asistencia();
            DataTable dt = obj.RN_Ver_Todas_Asistencias_Deldia(xdia);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No hay asistencias registradas para este día.",
                    "Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
                return;
            }

            // Solo abre el formulario de impresión si hay asistencias
            Frm_PrintAsis_deldia asis = new Frm_PrintAsis_deldia();

            fil.Show();
            asis.tipoInfo = "deldia";
            asis.Tag = xdia;
            asis.ShowDialog();

            fil.Hide();
        }


        private void bt_imprimirAsistenciaDelMes_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_PrintAsis_deldia asis = new Frm_PrintAsis_deldia();
            Frm_Solo_Fecha solo = new Frm_Solo_Fecha();

            fil.Show();
            solo.ShowDialog();
            fil.Hide();

            if (solo.Tag.ToString() == "") return;

            DateTime xdia = solo.dtp_fecha.Value;

            fil.Show();
            asis.tipoInfo = "delmes";
            asis.Tag = xdia;
            asis.ShowDialog();

            fil.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Frm_Filtro fis = new Frm_Filtro();

            Frm_Marcar_Asistencia asis = new Frm_Marcar_Asistencia();
            fis.Show();
            asis.ShowDialog();
            fis.Hide();
        }

        private void bt_nomina1_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            fil.Show();
            new Frm_Nomina().ShowDialog();
            fil.Hide();
        }

        private void btn_apagar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
