using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Prj_Capa_Entidad;

namespace Prj_Capa_Datos
{
   public  class BD_Asistencia: BD_Conexion 
    {
        public DataTable BD_Ver_Todas_Asistencia()
        {
            SqlConnection xcn = new SqlConnection();
            try
            {
                xcn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Cargar_Todas_Asistencias", xcn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;               
                DataTable Dato = new DataTable();
                da.Fill(Dato);
                da = null;
                return Dato;
            }
            catch (Exception ex)
            {
                if (xcn.State == ConnectionState.Open)
                {
                    xcn.Close();
                    throw new Exception("Error" + ex.Message, ex);
                }
            }
            return null;
        }



        public DataTable BD_Ver_Todas_Asistencias_Deldia(DateTime xfecha)
        {
            SqlConnection xcn = new SqlConnection();
            try
            {
                xcn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Cargar_Asistencia_deldia", xcn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fecha", xfecha);
                DataTable Dato = new DataTable();
                da.Fill(Dato);
                da = null;
                return Dato;
            }
            catch (Exception ex)
            {
                if (xcn.State == ConnectionState.Open)
                {
                    xcn.Close();
                    throw new Exception("Error" + ex.Message, ex);
                }
            }
            return null;
        }



        public DataTable BD_Ver_Todas_Asistencias_DelMes(DateTime xfecha)
        {
            SqlConnection xcn = new SqlConnection();
            try
            {
                xcn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Cargar_Asistencia_xFecha", xcn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@fecha", xfecha);
                DataTable Dato = new DataTable();
                da.Fill(Dato);
                da = null;
                return Dato;
            }
            catch (Exception ex)
            {
                if (xcn.State == ConnectionState.Open)
                {
                    xcn.Close();
                    throw new Exception("Error" + ex.Message, ex);
                }
            }
            return null;
        }

        public DataTable BD_Ver_Todas_Asistencias_ParaExplorador(String xvalor)
        {
            SqlConnection xcn = new SqlConnection();
            try
            {
                xcn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Buscar_Asistencia_paraExplorador", xcn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_Asis", xvalor);
                DataTable Dato = new DataTable();
                da.Fill(Dato);
                da = null;
                return Dato;
            }
            catch (Exception ex)
            {
                if (xcn.State == ConnectionState.Open)
                {
                    xcn.Close();
                    throw new Exception("Error" + ex.Message, ex);
                }
            }
            return null;
        }

        public DataTable BD_Buscar_Asistencia_deEntrada(String idperso)
        {
            SqlConnection xcn = new SqlConnection();
            try
            {
                xcn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Leer_asistencia_reciente", xcn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idper", idperso);
                DataTable Dato = new DataTable();
                da.Fill(Dato);
                da = null;
                return Dato;
            }
            catch (Exception ex)
            {
                if (xcn.State == ConnectionState.Open)
                {
                    xcn.Close();
                }
                MessageBox.Show("Algo malo pasó: " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }



        public static bool entrada = false;
        public static bool salida = false;


        //REGISTRO:
        public void BD_Registrar_Entrada_Personal(string idAsis, string idPerso, string HoIngreso, double tarde, int Totalhora, string justificado, string dia)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("Sp_Registrar_Entrada", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                Console.WriteLine($"{idAsis}, {idPerso}, {HoIngreso}, {tarde}, {Totalhora}, {justificado}, {dia}");

                //agregar los parametros
                cmd.Parameters.AddWithValue("@IdAsis", idAsis);
                cmd.Parameters.AddWithValue("@Id_Persol", idPerso);
                cmd.Parameters.AddWithValue("@nomdia", dia);
                cmd.Parameters.AddWithValue("@Hoingre", HoIngreso);
                cmd.Parameters.AddWithValue("@HoraSalida", "00:00:00");
                cmd.Parameters.AddWithValue("@tardanza", tarde);
                cmd.Parameters.AddWithValue("@TotalHora", Convert.ToDouble(0));
                cmd.Parameters.AddWithValue("@justificado", justificado);
                cmd.Parameters.AddWithValue("@EstadoAsis", "Entrada");

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                entrada = true;
            }
            catch (Exception ex)
            {
                entrada = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                throw new Exception("Error al guardar" + ex.Message, ex);
            }
        } //Fin de asistencia


        public void BD_Registrar_Salida_Personal(string idAsis, string idPerso, string HoSalida, double Totalhora)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand cmd = new SqlCommand("Sp_Registrar_Salida", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                //agregar los parametros
                cmd.Parameters.AddWithValue("@IdAsis", idAsis);
                cmd.Parameters.AddWithValue("@Id_Personal", idPerso);
                cmd.Parameters.AddWithValue("@HoSalida", HoSalida);
                cmd.Parameters.AddWithValue("@TotalHora", Totalhora);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                salida = true;
            }
            catch (Exception ex)
            {
                salida = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                throw new Exception("Error al guardar" + ex.Message, ex);
            }

        }




        //Verificaciones:
        public bool BD_Checar_SiPersonal_YaMarco_Asistencia(string xidPerso)
        {
            bool functionReturnValue = false;
            Int32 xfil = 0;
            SqlConnection Cn = new SqlConnection();
            SqlCommand Cmd = new SqlCommand();
            Cn.ConnectionString = Conectar();
            var _with1 = Cmd;
            _with1.CommandText = "Sp_Validar_RegistroAsistencia";
            _with1.Connection = Cn;
            _with1.CommandTimeout = 20;
            _with1.CommandType = CommandType.StoredProcedure;
            _with1.Parameters.AddWithValue("@Id_Personal", xidPerso);

            try
            {
                Cn.Open();
                xfil = (Int32)Cmd.ExecuteScalar();
                if (xfil > 0)
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }

                Cmd.Parameters.Clear();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
            }
            catch (Exception ex)
            {
                if (Cn.State == ConnectionState.Open)
                    Cn.Close();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
                MessageBox.Show("Error:" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return functionReturnValue;
        }//Fin verificar 1:




        public bool BD_Checar_SiPersonal_YaMarco_suEntrada(string xidPerso)
        {
            bool functionReturnValue = false;
            Int32 xfil = 0;
            SqlConnection Cn = new SqlConnection();
            SqlCommand Cmd = new SqlCommand();
            Cn.ConnectionString = Conectar();
            var _with1 = Cmd;
            _with1.CommandText = "Sp_verificar_IngresoAsis";
            _with1.Connection = Cn;
            _with1.CommandTimeout = 20;
            _with1.CommandType = CommandType.StoredProcedure;
            _with1.Parameters.AddWithValue("@Id_Personal", xidPerso);

            try
            {
                Cn.Open();
                xfil = (Int32)Cmd.ExecuteScalar();
                if (xfil > 0)
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }

                Cmd.Parameters.Clear();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
            }
            catch (Exception ex)
            {
                if (Cn.State == ConnectionState.Open)
                    Cn.Close();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
                MessageBox.Show("Error:" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return functionReturnValue;
        }//Fin verificar 2:


        public bool BD_Verificar_Justificacion_Aprobado(string xidPerso)
        {
            bool functionReturnValue = false;
            Int32 xfil = 0;
            SqlConnection Cn = new SqlConnection();
            SqlCommand Cmd = new SqlCommand();
            Cn.ConnectionString = Conectar();
            var _with1 = Cmd;
            _with1.CommandText = "SP_VerificarJustificacion_Aprobada";
            _with1.Connection = Cn;
            _with1.CommandTimeout = 20;
            _with1.CommandType = CommandType.StoredProcedure;
            _with1.Parameters.AddWithValue("@Id_Personal", xidPerso);

            try
            {
                Cn.Open();
                xfil = (Int32)Cmd.ExecuteScalar();
                if (xfil > 0)
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }

                Cmd.Parameters.Clear();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
            }
            catch (Exception ex)
            {
                if (Cn.State == ConnectionState.Open)
                    Cn.Close();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
                MessageBox.Show("Error:" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return functionReturnValue;
        }//Fin verificar 3:

        public bool BD_Checar_SiPersonal_TieneAsistencia_Registrada(string xidPerso)
        {
            bool functionReturnValue = false;
            Int32 xfil = 0;

            SqlConnection Cn = new SqlConnection();
            SqlCommand Cmd = new SqlCommand();
            Cn.ConnectionString = Conectar();
            var _with1 = Cmd;
            _with1.CommandText = "Sp_Ver_sihay_Registro";
            _with1.Connection = Cn;
            _with1.CommandTimeout = 20;
            _with1.CommandType = CommandType.StoredProcedure;
            _with1.Parameters.AddWithValue("@Id_Personal", xidPerso);

            try
            {
                Cn.Open();
                xfil = (Int32)Cmd.ExecuteScalar();
                if (xfil > 0)
                {
                    functionReturnValue = true;
                } else
                {
                    functionReturnValue = false;
                }

                Cmd.Parameters.Clear();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
            }
            catch (Exception ex)
            {
                if (Cn.State == ConnectionState.Open)
                    Cn.Close();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
                MessageBox.Show("Algo malo paso" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return functionReturnValue;
        }

        public bool BD_Checar_SiPersonal_YaMarco_suFalta(string xidPerso)
        {
            bool functionReturnValue = false;
            Int32 xfil = 0;

            SqlConnection Cn = new SqlConnection();
            SqlCommand Cmd = new SqlCommand();
            Cn.ConnectionString = Conectar();
            var _with1 = Cmd;
            _with1.CommandText = "Sp_Verificar_siMarco_Falta";
            _with1.Connection = Cn;
            _with1.CommandTimeout = 20;
            _with1.CommandType = CommandType.StoredProcedure;
            _with1.Parameters.AddWithValue("@Id_Personal", xidPerso);

            try
            {
                Cn.Open();
                xfil = (Int32)Cmd.ExecuteScalar();
                if (xfil > 0)
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }

                Cmd.Parameters.Clear();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
            }
            catch (Exception ex)
            {
                if (Cn.State == ConnectionState.Open)
                    Cn.Close();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
                MessageBox.Show("Algo malo paso" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return functionReturnValue;
        }

        public static bool faltasaved = false;

        public void BD_Registrar_Falta_Personal(string idAsis, string idPerso)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand Cmd = new SqlCommand("Sp_Registrar_Falta", cn);

            try
            {
                Cmd.CommandTimeout = 20;
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@IdAsis",idAsis);
                Cmd.Parameters.AddWithValue("@Id_Personal",idPerso);

                cn.Open();
                Cmd.ExecuteNonQuery();
                cn.Close();

                faltasaved = true;
            }
            catch (Exception ex)
            {
                faltasaved = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("Falla en registrar la falta" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public bool BD_Registrar_Salida_Personal_ConTotal(
            string idAsis,
            string idPerso,
            string hoSalida,
            double totalHora,
            out string mensaje)
        {
            mensaje = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(Conectar()))
                using (SqlCommand cmd = new SqlCommand("Sp_Registrar_Salida", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdAsis", SqlDbType.Char, 8).Value =
                        idAsis.Trim();

                    cmd.Parameters.Add("@Id_Personal", SqlDbType.Char, 10).Value =
                        idPerso.Trim();

                    cmd.Parameters.Add("@HoSalida", SqlDbType.VarChar, 10).Value =
                        hoSalida;

                    cmd.Parameters.Add("@TotalHora", SqlDbType.Real).Value =
                        (float)totalHora;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                mensaje = "Salida registrada correctamente.";
                return true;
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        public void BD_Registrar_Falta_Personal(string idAsis, string idPerso, string nomdia, string justificacion, string estado)
        {
            SqlConnection cn = new SqlConnection(Conectar());
            SqlCommand Cmd = new SqlCommand("Sp_Registrar_Falta", cn);
            try
            {
                Cmd.CommandTimeout = 20;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@IdAsis", idAsis);
                Cmd.Parameters.AddWithValue("@Id_Personal", idPerso);
                Cmd.Parameters.AddWithValue("@nomdia", nomdia);
                Cmd.Parameters.AddWithValue("@justificacion", justificacion);
                Cmd.Parameters.AddWithValue("@estado", estado);   // "Falta" o "Suspension"
                cn.Open();
                Cmd.ExecuteNonQuery();
                cn.Close();
                faltasaved = true;
            }
            catch (Exception ex)
            {
                faltasaved = false;
                if (cn.State == ConnectionState.Open) cn.Close();
                MessageBox.Show("Falla en registrar la falta: " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int BD_Tardanzas_Pendientes(string idPerso)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlCommand cmd = new SqlCommand("Sp_Tardanzas_Pendientes", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Personal", idPerso.Trim());
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public bool BD_Suspendido_Hoy(string idPerso)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlCommand cmd = new SqlCommand("Sp_Suspension_Hoy", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Personal", idPerso.Trim());
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

    }
}
