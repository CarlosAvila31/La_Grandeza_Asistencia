using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Prj_Capa_Entidad;

namespace Prj_Capa_Datos
{
    public class BD_Horario : BD_Conexion
    {
        public static bool saved = false;
        public void BD_Actualizar_Horario(EN_Horario P)
        {
            SqlConnection cn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cn.ConnectionString = Conectar();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_Update_Horario";
                cmd.Connection = cn;
                cmd.CommandTimeout = 20; /*Si pasa los 20 segundos va a saltar a la excepcion*/

                //parametros
                cmd.Parameters.AddWithValue("@Idhor", P.Idhora);
                cmd.Parameters.AddWithValue("@HoEntrada", P.HoEntrada);
                cmd.Parameters.AddWithValue("@HoTolere", P.HoTole);
                cmd.Parameters.AddWithValue("@Holimite", P.HoLimite);
                cmd.Parameters.AddWithValue("@HoraSalida", P.HoSalida);

                //Abrimos la cadena de conexion y ejecutamos el comando
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                cmd.Dispose();
                cmd = null;
                cn.Dispose();
                cn = null;

                //MessageBox.Show("Horario actualizado correctamente", "Informe del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                saved = true;
            }
            catch (Exception ex)
            {
                saved = false;
                MessageBox.Show("Error al actualizar horario: " + ex.Message, "Informe del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (cn.State == ConnectionState.Open) cn.Close();
                cmd.Dispose();
                cmd = null;
                cn.Dispose();
                cn = null;
            }
        }

        public DataTable BD_Leer_Horarios()
        {
            SqlConnection Cn = new SqlConnection();
            try
            {
                Cn.ConnectionString = Conectar();
                SqlDataAdapter Da = new SqlDataAdapter("SP_Buscar_Todos_Horarios", Cn);
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable Datos = new DataTable();
                Da.Fill(Datos);
                Da = null;
                return Datos;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar los horarios: " + ex.Message, "Informe del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (Cn.State == ConnectionState.Open) Cn.Close();
                Cn.Dispose();
                Cn = null;
                return null;
            }
        }
    }
}
