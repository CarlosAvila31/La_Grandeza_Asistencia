using System;
using System.Data;
using System.Data.SqlClient;

namespace Prj_Capa_Datos
{
    public class BD_HorasExtra : BD_Conexion
    {
        // Empleados con la entrada abierta hoy (con su casilla "se queda")
        public DataTable BD_Abiertos()
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlDataAdapter da = new SqlDataAdapter("Sp_HorasExtra_Abiertos", cn))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Guarda exactamente quien se queda hoy. ids = "ID1,ID2" o "" si nadie
        public void BD_Guardar(string ids)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlCommand cmd = new SqlCommand("Sp_HorasExtra_Guardar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ids", ids ?? "");
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool BD_Esta_Autorizado(string idPerso, DateTime fecha)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlCommand cmd = new SqlCommand("Sp_HorasExtra_Autorizado", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Personal", idPerso.Trim());
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
            }
        }

        // Cierra las entradas vencidas y devuelve a quién se le cerró
        public DataTable BD_AutoSalida(decimal maxHorasExtra)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlDataAdapter da = new SqlDataAdapter("Sp_AutoSalida_Vencidos", cn))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@MaxHorasExtra", maxHorasExtra);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}