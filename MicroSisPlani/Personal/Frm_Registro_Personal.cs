
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


namespace MicroSisPlani.Personal
{
    public partial class Frm_Registro_Personal : Form
    {
        public Frm_Registro_Personal()
        {
            InitializeComponent();
        }

        public bool SeVaEditar = false;

        //public bool editPerso = false;
      
        private void Frm_Registro_Personal_Load(object sender, EventArgs e)
        {
            if (SeVaEditar == false)
            {
                Cargar_Horario();
                Cargar_rol();
                Cargar_Distrito();
                
            }
            
        }

        private void Cargar_rol()
        {
            RN_Rol obj = new RN_Rol();
            DataTable dt = new DataTable();

            try
            {
                dt = obj.RN_Buscar_Todos_Roles();
                if (dt.Rows.Count > 0)
                { 
                    var cbo = cbo_rol;
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "nomRol"; 
                    cbo.ValueMember = "Id_Rol";
                }

                cbo_rol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                
            }
        }




        private void Cargar_Distrito()
        {
            RN_Distrito obj = new RN_Distrito();
            DataTable dt = new DataTable();

            try
            {
                dt = obj.RN_Buscar_Todos_Distrito();
                if (dt.Rows.Count > 0)
                {
                    var cbo = cbo_Distrito;
                    cbo.DataSource = dt;
                    cbo.DisplayMember = "Distrito";
                    cbo.ValueMember = "Id_Distrito";
                }

                cbo_Distrito.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

            }
        }


        private bool ValidarCajasTexto()
        {
            Frm_Advertencia ver = new Frm_Advertencia();
            Frm_Filtro fil = new Frm_Filtro();

            if (txt_Dni.Text.Trim().Length < 18) 
            {
                fil.Show(); ver.Lbl_Msm1.Text = "El curp debe tener 18 dígitos"; ver.ShowDialog(); fil.Hide(); txt_Dni.Focus(); return false;

            }

            if (txt_nombres.Text.Length < 4)
            {
                fil.Show(); ver.Lbl_Msm1.Text = "Ingresa el nombre del personal"; ver.ShowDialog(); fil.Hide(); txt_nombres.Focus(); return false;
            }

            if (txt_direccion.Text.Length < 4)
            {
                fil.Show(); ver.Lbl_Msm1.Text = "Ingresa la direccion del personal"; ver.ShowDialog(); fil.Hide(); txt_direccion.Focus(); return false;

            }

            if (txt_correo.Text.Length < 4)
            {
                fil.Show(); ver.Lbl_Msm1.Text = "Ingresa el correo del personal"; ver.ShowDialog(); fil.Hide(); txt_correo.Focus(); return false;

            }

            if (txt_NroCelular.Text.Length < 8)
            {
                fil.Show(); ver.Lbl_Msm1.Text = "Ingresa el numero telefonico del personal"; ver.ShowDialog(); fil.Hide(); txt_NroCelular.Focus(); return false;

            }

            if (txt_IdPersona.Text.Length < 8)
            {
                fil.Show(); ver.Lbl_Msm1.Text = "El ID del personal no fue generado"; ver.ShowDialog(); fil.Hide(); txt_IdPersona.Focus(); return false;

            }

            if (cbo_sexo.SelectedIndex == -1)
            {
                fil.Show(); ver.Lbl_Msm1.Text = "Seleccione el sexo del personal"; ver.ShowDialog(); fil.Hide(); cbo_sexo.Focus(); return false;

            }

            if (cbo_rol.SelectedIndex == -1)
            {
                fil.Show(); ver.Lbl_Msm1.Text = "Seleccione el rol del personal"; ver.ShowDialog(); fil.Hide(); cbo_rol.Focus(); return false;

            }

            if (cbo_Distrito.SelectedIndex == -1)
            {
                fil.Show(); ver.Lbl_Msm1.Text = "Seleccione el dsitrito del personal"; ver.ShowDialog(); fil.Hide(); cbo_Distrito.Focus(); return false;

            }

            if (string.IsNullOrEmpty(xFotoRuta))
            {
                fil.Show(); ver.Lbl_Msm1.Text = "Debe seleccionar una foto del personal"; ver.ShowDialog(); fil.Hide(); Pic_persona.Focus(); return false;
            }

            if (cbo_horario.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un horario para el empleado");
                return false;   // ajusta al estilo de tu validador
            }


            return true;
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            Frm_Advertencia ok = new Frm_Advertencia();
            Frm_Filtro fil = new Frm_Filtro();
            RN_Personal objper = new RN_Personal();

            if (ValidarCajasTexto() == false) return;

            if (xedit == false)
            {
                if (objper.RN_Verificar_DniPersonal(txt_Dni.Text) == true)
                {
                    MessageBox.Show("El curp ya existe en la Base de Datos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
               
                Guardar_Personal();
            }
            else
            {
                Editar_Personal();
            }

            
        }

        private void Guardar_Personal()
        {
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Filtro fil = new Frm_Filtro();
            RN_Personal obj = new RN_Personal();
            EN_Persona per = new EN_Persona();




            try
            {
                per.Idpersonal = txt_IdPersona.Text;
                per.Dni = txt_Dni.Text;
                per.Nombres = txt_nombres.Text;
                per.anoNacimiento = dtp_fecha.Value;
                per.Sexo = cbo_sexo.Text;
                per.Direccion = txt_direccion.Text;
                per.Correo = txt_correo.Text;
                per.Celular = txt_NroCelular.Text;
                per.IdRol = Convert.ToString(cbo_rol.SelectedValue);
                per.xImagen = xFotoRuta;
                per.IdDistrito = Convert.ToString(cbo_Distrito.SelectedValue);
                per.IdHorario = Convert.ToString(cbo_horario.SelectedValue);

                obj.RN_Registrar_Personal(per);

                if (BD_Personal.saved == true)
                {
                    // El empleado ya existe en la BD: ahora es obligatorio registrar su huella
                    string idNuevo = txt_IdPersona.Text;
                    bool huellaOk = false;

                    while (!huellaOk)
                    {
                        Frm_Regis_Huella frmH = new Frm_Regis_Huella();
                        frmH.Buscar_Personal_ParaEditar(idNuevo);
                        fil.Show();
                        frmH.ShowDialog();
                        fil.Hide();

                        huellaOk = (Convert.ToString(frmH.Tag) == "A");

                        if (!huellaOk)
                        {
                            DialogResult r = MessageBox.Show(
                                "El empleado necesita su huella para poder marcar asistencia.\n¿Deseas intentarlo de nuevo?\n\n" +
                                "Si eliges No, se cancelará el registro del empleado.",
                                "Huella requerida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (r == DialogResult.No)
                            {
                                obj.RN_Eliminar_Personal_SinHuella(idNuevo);
                                MessageBox.Show("Registro cancelado: el empleado no fue guardado.");
                                return;   // el formulario sigue abierto con los datos capturados
                            }
                        }
                    }


                    fil.Show();
                    ok.Lbl_msm1.Text = "los datos y la huella del personal se han guardado de forma correcta";
                    ok.ShowDialog();
                    fil.Hide();

                    this.Tag = "A";
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }

        }




        //EDITAR PERSONAL

        private void Editar_Personal()
        {
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Filtro fil = new Frm_Filtro();
            RN_Personal obj = new RN_Personal();
            EN_Persona per = new EN_Persona();




            try
            {
                per.Idpersonal = txt_IdPersona.Text;
                per.Dni = txt_Dni.Text;
                per.Nombres = txt_nombres.Text;
                per.anoNacimiento = dtp_fecha.Value;
                per.Sexo = cbo_sexo.Text;
                per.Direccion = txt_direccion.Text;
                per.Correo = txt_correo.Text;
                per.Celular = txt_NroCelular.Text;
                per.IdRol = Convert.ToString(cbo_rol.SelectedValue);
                per.xImagen = xFotoRuta;
                per.IdDistrito = Convert.ToString(cbo_Distrito.SelectedValue);
                per.IdHorario = Convert.ToString(cbo_horario.SelectedValue);

                obj.RN_Editar_Personal(per);

                if (BD_Personal.edited == true)
                {
                    fil.Show();
                    ok.Lbl_msm1.Text = "los datos del personal se han editado de forma correcta";
                    ok.ShowDialog();
                    fil.Hide();

                    this.Tag = "A";
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }

        }







        string xFotoRuta;
        private void Pic_persona_Click(object sender, EventArgs e)
        {
            var filepath = string.Empty;
            try
            {
                if (openFileDialog1.ShowDialog ()== DialogResult.OK)
                {
                    xFotoRuta = openFileDialog1.FileName;
                    Pic_persona.Load(xFotoRuta);
                }
            }
            catch (Exception ex)
            {
                xFotoRuta = Application.StartupPath + @"\user.png";
                Pic_persona.Load(Application.StartupPath + @"\user.png");
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void Frm_Registro_Personal_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

        public bool xedit = false;
        private void txt_NroCelular_OnValueChanged(object sender, EventArgs e)
        {
            string xcar1, xcar2;

            if (xedit == false)
            {
                if (txt_Dni.Text.Length == 0) return;
                if (txt_nombres.Text.Length == 0) return;

                xcar1 = Convert.ToString(txt_Dni.Text).Substring(3,5);
                xcar2 = Convert.ToString(txt_Dni.Text).Substring(1, 4);
                txt_IdPersona.Text = xcar1 + "-" + xcar2;
            }
        }


        public void Buscar_Personal_ParaEditar(string idpersonal)
        {
            try
            {
                RN_Personal obj = new RN_Personal();
                DataTable data = new DataTable();

                Cargar_rol();
                Cargar_Distrito();
                Cargar_Horario();

                data = obj.RN_Buscar_Personal_xValor(idpersonal);
                if (data.Rows.Count == 0) return;
                {
                    txt_Dni.Text = Convert.ToString(data.Rows[0]["DNIPR"]);
                    txt_nombres.Text = Convert.ToString(data.Rows[0]["Nombre_Completo"]);
                    txt_direccion.Text = Convert.ToString(data.Rows[0]["Domicilio"]);
                    txt_correo.Text = Convert.ToString(data.Rows[0]["Correo"]);
                    txt_NroCelular.Text = Convert.ToString(data.Rows[0]["Celular"]);
                    dtp_fechaNaci.Value = Convert.ToDateTime(data.Rows[0]["Fec_Naci"]);
                    string sexoGuardado = Convert.ToString(data.Rows[0]["Sexo"]).Trim();

                    if (sexoGuardado == "M")
                    {
                        cbo_sexo.Text = "Masculino";
                    }
                    else if (sexoGuardado == "F")
                    {
                        cbo_sexo.Text = "Femenino";
                    }
                    else
                    {
                        cbo_sexo.Text = sexoGuardado;
                    }
                    cbo_rol.SelectedValue = data.Rows[0]["Id_rol"];
                    cbo_Distrito.SelectedValue = data.Rows[0]["Id_Distrito"];
                    txt_IdPersona.Text = Convert.ToString(data.Rows[0]["Id_Pernl"]);
                    xFotoRuta = Convert.ToString(data.Rows[0]["Foto"]);
                    cbo_horario.SelectedValue = Convert.ToString(data.Rows[0]["Id_Horario"]).Trim();

                }

                xedit = true;
                btn_aceptar.Enabled = true;
                Pic_persona.Load(xFotoRuta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar los datos: " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        //private void Cargar_Horario()
        //{
        //    DataTable dt = new RN_Horario().RN_Leer_Horarios();   // Sp_Buscar_Todos_Horarios
        //    cbo_horario.DataSource = dt;
        //    cbo_horario.DisplayMember = "ResumenHora";   // lo que se ve
        //    cbo_horario.ValueMember = "IdHorario";       // lo que se guarda
        //    cbo_horario.SelectedIndex = -1;              // empieza vacío
        //}

        private void Cargar_Horario()
        {
            DataTable dt = new RN_Horario().RN_Leer_Horarios();   // Sp_Buscar_Todos_Horarios
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay horarios registrados. Crea al menos uno primero.");
                return;
            }

            dt.Columns.Add("Texto", typeof(string));
            foreach (DataRow r in dt.Rows)
            {
                r["Texto"] = Convert.ToDateTime(r["HoEntrada"]).ToString("HH:mm") + " - " +
                             Convert.ToDateTime(r["HoSalida"]).ToString("HH:mm");
                r["IdHorario"] = Convert.ToString(r["IdHorario"]).Trim();   // quita espacios del char(8)
            }

            cbo_horario.DisplayMember = "Texto";
            cbo_horario.ValueMember = "IdHorario";
            cbo_horario.DataSource = dt;
            cbo_horario.SelectedIndex = -1;
        }
    }
}
