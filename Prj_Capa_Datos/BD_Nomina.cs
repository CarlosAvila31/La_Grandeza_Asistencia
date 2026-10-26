using System;
using System.Data;
using System.Data.SqlClient;

namespace Prj_Capa_Datos
{
    public class BD_Nomina : BD_Conexion
    {
        // Reporte de nómina del periodo. idDistrito vacío o null = todas las sucursales
        public DataTable BD_Calcular(DateTime ini, DateTime fin, string idDistrito)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlDataAdapter da = new SqlDataAdapter("Sp_Nomina_Calcular", cn))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Ini", ini.Date);
                da.SelectCommand.Parameters.AddWithValue("@Fin", fin.Date);
                if (string.IsNullOrWhiteSpace(idDistrito))
                    da.SelectCommand.Parameters.AddWithValue("@Id_Distrito", DBNull.Value);
                else
                    da.SelectCommand.Parameters.AddWithValue("@Id_Distrito", idDistrito.Trim());

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable BD_Listar_Config()
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlDataAdapter da = new SqlDataAdapter("Sp_Nomina_Listar_Config", cn))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void BD_Guardar_Config(string idPerso, decimal sueldoFijo, decimal horasPorTrabajar)
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlCommand cmd = new SqlCommand("Sp_Nomina_Guardar_Config", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Personal", idPerso.Trim());
                cmd.Parameters.AddWithValue("@Sueldo_Fijo", sueldoFijo);
                cmd.Parameters.AddWithValue("@Horas_Por_Trabajar", horasPorTrabajar);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Para el filtro de sucursal
        public DataTable BD_Listar_Distritos()
        {
            using (SqlConnection cn = new SqlConnection(Conectar()))
            using (SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Id_Distrito, Distrito FROM Distrito ORDER BY Distrito", cn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
