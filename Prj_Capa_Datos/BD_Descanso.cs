using System;
using System.Data;
using System.Data.SqlClient;

namespace Prj_Capa_Datos
{
    public class BD_Descanso : BD_Conexion
    {
        public DataTable BD_Listar_Semana(DateTime semana)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlDataAdapter da = new SqlDataAdapter("Sp_Listar_Descansos_Semana", cn))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Semana", semana.Date);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void BD_Guardar_Semana(string idPerso, DateTime semana, string diasDescanso)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlCommand cmd = new SqlCommand("Sp_Guardar_Descanso_Semana", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Personal", idPerso.Trim());
                cmd.Parameters.AddWithValue("@Semana", semana.Date);
                cmd.Parameters.AddWithValue("@Dias", diasDescanso);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void BD_Copiar_Semana(DateTime desde, DateTime hasta)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlCommand cmd = new SqlCommand("Sp_Copiar_Descansos_Semana", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Desde", desde.Date);
                cmd.Parameters.AddWithValue("@Hasta", hasta.Date);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 1 = laboral, 0 = descanso, -1 = nunca programado
        public int BD_Es_Dia_Laboral(string idPerso, DateTime fecha)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlCommand cmd = new SqlCommand("Sp_Es_Dia_Laboral", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Personal", idPerso.Trim());
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}