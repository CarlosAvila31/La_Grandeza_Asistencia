using Klik.Windows.Forms.v1.EntryLib;
using MicroSisPlani.Msm_Forms;
using MicroSisPlani.Personal;
using Prj_Capa_Datos;
using Prj_Capa_Entidad;
using Prj_Capa_Negocio;
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
            ConfigurationListView();
            Cargar_Todo_Personal();
            ConfigurationListview_Asis();
            Cargar_todas_Asistencias();
            CargarHorarios();
        }

        public void Cargar_Datos_Usuario()
        {
            try
            {
                Frm_Filtro xfil = new Frm_Filtro();

                xfil.Show();
                MessageBox.Show("Bienvenido: " + Cls_Libreria.Apellidos, "Bienvenido al Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                xfil.Hide();

                lbl_Nombre_Usuario.Text = Cls_Libreria.Apellidos;
                lbl_Rol.Text = Cls_Libreria.Rol;
            }
            catch (Exception ex)
            {

            }
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



        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void LlenarListView(DataTable data)
        {
            // 1. Congelar el diseño del ListView para evitar parpadeos y fallas visuales
            lsv_person.BeginUpdate();

            lsv_person.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];

                // Columna 0 (Oculta con ancho 0): Id Persona
                ListViewItem list = new ListViewItem(dr["Id_Pernl"].ToString());

                // SubItems (Columnas visibles siguientes)
                list.SubItems.Add(dr["DNIPR"].ToString());
                list.SubItems.Add(dr["Nombre_Completo"].ToString());
                list.SubItems.Add(dr["Domicilio"].ToString());
                list.SubItems.Add(dr["Correo"].ToString());
                list.SubItems.Add(dr["Sexo"].ToString());

                // Si el formato de fecha se ve mal/largo, puedes formatearlo así:
                if (dr["Fec_Naci"] != DBNull.Value)
                {
                    DateTime fecha = Convert.ToDateTime(dr["Fec_Naci"]);
                    list.SubItems.Add(fecha.ToString("dd/MM/yyyy"));
                }
                else
                {
                    list.SubItems.Add("");
                }

                list.SubItems.Add(dr["Celular"].ToString());
                list.SubItems.Add(dr["NomRol"].ToString());
                list.SubItems.Add(dr["Distrito"].ToString());
                list.SubItems.Add(dr["Estado_Per"].ToString());

                lsv_person.Items.Add(list);
            }

            // 2. Reactivar y refrescar el diseño de golpe de forma limpia
            lsv_person.EndUpdate();
        }


        private void ConfigurationListView()
        {
            var lis = lsv_person;

            lis.View = View.Details;
            lis.FullRowSelect = true;
            lis.GridLines = true;
            lis.Scrollable = true;
            lis.HideSelection = false;
            lis.HeaderStyle = ColumnHeaderStyle.Clickable;

            // Activar los colores personalizados
            lis.OwnerDraw = true;
            lis.DrawColumnHeader -= Lis_DrawColumnHeader;
            lis.DrawColumnHeader += Lis_DrawColumnHeader;
            lis.DrawSubItem -= Lis_DrawSubItem;
            lis.DrawSubItem += Lis_DrawSubItem;

            lis.Columns.Clear();
            lis.Items.Clear();

            // Tus columnas originales con los anchos correctos
            lis.Columns.Add("Id Persona", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Dni", 95, HorizontalAlignment.Left);
            lis.Columns.Add("Nombre del Empleado", 316, HorizontalAlignment.Left);
            lis.Columns.Add("Direccion", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Correo", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Sexo", 60, HorizontalAlignment.Center);
            lis.Columns.Add("Fecha de Nacimiento", 110, HorizontalAlignment.Center);
            lis.Columns.Add("Telefono", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Rol", 100, HorizontalAlignment.Left);
            lis.Columns.Add("Sucursal", 100, HorizontalAlignment.Left);
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

        private void ConfigurarListview_Asis()
        {
            var lis = lsv_asis; // <-- Trabaja con Asistencias

            lis.View = View.Details;
            lis.FullRowSelect = true;
            lis.GridLines = true;
            lis.Scrollable = true;
            lis.HideSelection = false;
            lis.HeaderStyle = ColumnHeaderStyle.Clickable;
            lis.OwnerDraw = true;

            lis.DrawColumnHeader -= Lis_DrawColumnHeader;
            lis.DrawColumnHeader += Lis_DrawColumnHeader;
            lis.DrawSubItem -= Lis_DrawSubItem;
            lis.DrawSubItem += Lis_DrawSubItem;

            lis.Columns.Clear();
            lis.Items.Clear();

            lis.Columns.Add("Id Asis", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Dni", 85, HorizontalAlignment.Left);
            lis.Columns.Add("Nombre del Empleado", 240, HorizontalAlignment.Left);
            lis.Columns.Add("Fecha", 90, HorizontalAlignment.Center);
            lis.Columns.Add("Día", 80, HorizontalAlignment.Left);
            lis.Columns.Add("Hora de Ingreso", 90, HorizontalAlignment.Center);
            lis.Columns.Add("Tardanza", 75, HorizontalAlignment.Center);
            lis.Columns.Add("Hora de Salida", 90, HorizontalAlignment.Center);
            lis.Columns.Add("Adelanto", 80, HorizontalAlignment.Center);
            lis.Columns.Add("Justificacion", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 100, HorizontalAlignment.Left);
        }


        private void btn_Carga_Personal_Click(object sender, EventArgs e)
        {
            Cargar_Todo_Personal();
        }

        private void btn_Agregar_Personal_Click(object sender, EventArgs e)
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

        private void btn_Editar_Personal_Click(object sender, EventArgs e)
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

        private void btn_Eliminar_Personal_Click(object sender, EventArgs e)
        {

        }



        private void ConfigurationListview_Asis()
        {
            var lis = lsv_asis;

            lis.View = View.Details;
            lis.FullRowSelect = true;
            lis.GridLines = true;
            lis.Scrollable = true;
            lis.HideSelection = false;
            lis.HeaderStyle = ColumnHeaderStyle.Clickable;

            // OBLIGATORIO: Permitir que nosotros pintemos el control por código
            lis.OwnerDraw = true;

            // Vincular los eventos de dibujo (Asegúrate de no duplicar estos eventos si usas el diseñador)
            lis.DrawColumnHeader -= Lis_DrawColumnHeader;
            lis.DrawColumnHeader += Lis_DrawColumnHeader;
            lis.DrawSubItem -= Lis_DrawSubItem;
            lis.DrawSubItem += Lis_DrawSubItem;

            lis.Columns.Clear();
            lis.Items.Clear();

            lis.Columns.Add("Id Asis", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Dni", 85, HorizontalAlignment.Left);
            lis.Columns.Add("Nombres del personal", 240, HorizontalAlignment.Left);
            lis.Columns.Add("Fecha", 90, HorizontalAlignment.Center);
            lis.Columns.Add("Día", 80, HorizontalAlignment.Left);
            lis.Columns.Add("Ho Ingreso", 90, HorizontalAlignment.Center);
            lis.Columns.Add("Tardanza", 75, HorizontalAlignment.Center);
            lis.Columns.Add("Ho Salida", 90, HorizontalAlignment.Center);
            lis.Columns.Add("Adelant", 80, HorizontalAlignment.Center);
            lis.Columns.Add("Justificacion", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 100, HorizontalAlignment.Left);
        }


        // 1. Pintar los Encabezados (Headers) con fondo azul claro y letra gruesa
        private void Lis_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            // Fondo azul claro para el encabezado
            using (System.Drawing.SolidBrush backBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(210, 225, 245)))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            // Borde gris del encabezado
            e.Graphics.DrawRectangle(System.Drawing.Pens.LightGray, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

            // FORZAR FUENTE GLOBAL: Ajusta "Segoe UI" y el tamaño (10) al estilo que prefieras
            using (System.Drawing.Font customBoldFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold))
            {
                TextFormatFlags flags = TextFormatFlags.VerticalCenter;

                if (e.Header.TextAlign == HorizontalAlignment.Center) flags |= TextFormatFlags.HorizontalCenter;
                else if (e.Header.TextAlign == HorizontalAlignment.Right) flags |= TextFormatFlags.Right;
                else flags |= TextFormatFlags.Left;

                TextRenderer.DrawText(e.Graphics, e.Header.Text, customBoldFont, e.Bounds, System.Drawing.Color.Black, flags);
            }
        }


        // 2. Pintar las filas e intercalar colores (Azul muy claro y Blanco)
        private void Lis_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            // DEFINIR MISMA FUENTE BASE PARA LOS DATOS (Tamaño 10 Regular)
            using (System.Drawing.Font customNormalFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular))
            {
                // 1. SI LA ROW ESTÁ SELECCIONADA
                if (e.Item.Selected)
                {
                    using (System.Drawing.SolidBrush selectBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(49, 116, 222)))
                    {
                        e.Graphics.FillRectangle(selectBrush, e.Bounds);
                    }

                    e.Graphics.DrawRectangle(System.Drawing.Pens.Gainsboro, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                    TextFormatFlags flagsSelect = TextFormatFlags.VerticalCenter;
                    if (e.Header.TextAlign == HorizontalAlignment.Center) flagsSelect |= TextFormatFlags.HorizontalCenter;
                    else if (e.Header.TextAlign == HorizontalAlignment.Right) flagsSelect |= TextFormatFlags.Right;
                    else flagsSelect |= TextFormatFlags.Left;

                    TextRenderer.DrawText(e.Graphics, e.SubItem.Text, customNormalFont, e.Bounds, System.Drawing.Color.White, flagsSelect);
                }
                // 2. SI LA ROW ESTÁ EN REPOSO (Intercalada)
                else
                {
                    System.Drawing.Color rowColor = (e.ItemIndex % 2 == 0)
                        ? System.Drawing.Color.FromArgb(240, 245, 255)
                        : System.Drawing.Color.White;

                    using (System.Drawing.SolidBrush backBrush = new System.Drawing.SolidBrush(rowColor))
                    {
                        e.Graphics.FillRectangle(backBrush, e.Bounds);
                    }

                    e.Graphics.DrawRectangle(System.Drawing.Pens.Gainsboro, e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

                    TextFormatFlags flagsNormal = TextFormatFlags.VerticalCenter;
                    if (e.Header.TextAlign == HorizontalAlignment.Center) flagsNormal |= TextFormatFlags.HorizontalCenter;
                    else if (e.Header.TextAlign == HorizontalAlignment.Right) flagsNormal |= TextFormatFlags.Right;
                    else flagsNormal |= TextFormatFlags.Left;

                    TextRenderer.DrawText(e.Graphics, e.SubItem.Text, customNormalFont, e.Bounds, System.Drawing.Color.Black, flagsNormal);
                }
            }
        }

        private void LlenarListView_Asis(DataTable data)
        {

            // 1. CONGELAR el diseño del ListView para evitar parpadeos y desalineaciones de columnas
            lsv_asis.BeginUpdate();

            lsv_asis.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];

                // Columna 0 (Oculta, ID Asistencia)
                ListViewItem list = new ListViewItem(dr["Id_asis"].ToString());

                // SubItems (Columnas visibles)
                list.SubItems.Add(dr["DNIPR"].ToString());
                list.SubItems.Add(dr["Nombre_Completo"].ToString());

                // --- OPTIMIZACIÓN DE FECHA ---
                if (dr["FechaAsis"] != DBNull.Value && DateTime.TryParse(dr["FechaAsis"].ToString(), out DateTime fecha))
                {
                    list.SubItems.Add(fecha.ToString("dd/MM/yyyy")); // Muestra solo: 25/10/2026
                }
                else
                {
                    list.SubItems.Add(dr["FechaAsis"].ToString());
                }

                list.SubItems.Add(dr["Nombre_dia"].ToString());

                // --- OPTIMIZACIÓN DE HORAS (Quita segundos/milisegundos si los hay) ---
                list.SubItems.Add(FormatearHora(dr["HoIngreso"].ToString()));
                list.SubItems.Add(dr["Tardanzas"].ToString());
                list.SubItems.Add(FormatearHora(dr["HoSalida"].ToString()));
                list.SubItems.Add(dr["Adelanto"].ToString());

                list.SubItems.Add(dr["Justifacion"].ToString());
                list.SubItems.Add(dr["EstadoAsis"].ToString());

                lsv_asis.Items.Add(list);
            }

            // 2. RENDERIZAR todo el contenido limpio y alineado de un solo golpe
            lsv_asis.EndUpdate();
        }

        private string FormatearHora(string horaOriginal)
        {
            if (TimeSpan.TryParse(horaOriginal, out TimeSpan tiempo))
            {
                return tiempo.ToString(@"hh\:mm"); // Muestra estrictamente: 08:30
            }
            return horaOriginal;
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

        // Variable global en tu formulario (ponla arriba, abajo de public partial class...)
        private int columnaSeleccionada = -1;
        private void lsv_person_MouseDown(object sender, MouseEventArgs e)
        {
        // Evento MouseDown para lsv_person
            ListViewItem item = lsv_person.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                // Buscar qué columna exacta tocó el clic
                var info = lsv_person.HitTest(e.Location);
                int colIndex = item.SubItems.IndexOf(info.SubItem);

                if (colIndex != columnaSeleccionada)
                {
                    columnaSeleccionada = colIndex;
                    lsv_person.Invalidate(); // Fuerza a redibujar con la nueva celda activa
                }
            }
        }

        // Evento MouseDown para lsv_asis
        private void lsv_asis_MouseDown_1(object sender, MouseEventArgs e)
        {
            ListViewItem item = lsv_asis.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                var info = lsv_asis.HitTest(e.Location);
                int colIndex = item.SubItems.IndexOf(info.SubItem);

                if (colIndex != columnaSeleccionada)
                {
                    columnaSeleccionada = colIndex;
                    lsv_asis.Invalidate();
                }
            }
        }

        private void txt_BuscarPerson_TextChanged(object sender, EventArgs e)
        {
            string valorBusqueda = txt_BuscarPerson.Text.Trim();

            if (valorBusqueda.Length > 0)
            {
                // Llamas a tu método que ya tienes programado para filtrar
                Buscar_Personal_PorValor(valorBusqueda);
            }
            else
            {
                // Si borran todo el texto del cuadro, vuelve a mostrar a todos de golpe
                Cargar_Todo_Personal();
            }
        }

        private void txt_BuscarAsis_TextChanged(object sender, EventArgs e)
        {
            string valorBusqueda = txt_BuscarAsis.Text.Trim();

            if (valorBusqueda.Length > 0)
            {
                // Llama a tu método de asistencias pasándole lo que escribe el usuario
                Cargar_Asistencias_porValor(valorBusqueda);
            }
            else
            {
                // Si el cuadro queda vacío, vuelve a listar todas las asistencias completas
                Cargar_todas_Asistencias();
            }
        }

        private void btn_Config_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Tab_Config;
        }

        private void btn_Reporte_Dia_Click(object sender, EventArgs e)
        {

        }

        private void btn_Reporte_Mes_Click(object sender, EventArgs e)
        {

        }

        private void btn_Cargar_Asistencias_Click(object sender, EventArgs e)
        {
            Cargar_todas_Asistencias();
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

                    Tab_Config.Visible = false;
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
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

            lbl_idHorario.Text = Convert.ToString(data.Rows[0]["IdHorario"]);
            dtp_horaIngre.Value = Convert.ToDateTime(data.Rows[0]["HoEntrada"]);
            dtp_hora_tolercia.Value = Convert.ToDateTime(data.Rows[0]["MinuToleran"]);
            Dtp_Hora_Limite.Value = Convert.ToDateTime(data.Rows[0]["HoLimite"]);
            dtp_horaSalida.Value = Convert.ToDateTime(data.Rows[0]["HoSalida"]);
        }

        private void btn_registrarHuellaDigital_Click(object sender, EventArgs e)
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
    }
}

