using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Prj_Capa_Entidad;
using System.Windows.Forms;

namespace Prj_Capa_Datos
{
   public  class BD_usuario : BD_Conexion 
    {

        public bool BD_Verificar_Acceso(string Usuario, string Contraseña)
        {
            bool functionReturnValue = false;
            Int32 xfil = 0;

            SqlConnection Cn = new SqlConnection();
            SqlCommand Cmd = new SqlCommand();
            Cn.ConnectionString = Conectar();
            var _with1 = Cmd;
            _with1.CommandText = "Sp_Login";
            _with1.Connection = Cn;
            _with1.CommandTimeout = 20;
            _with1.CommandType = CommandType.StoredProcedure;
            //Parametros de entrada chambales
            _with1.Parameters.AddWithValue("@Usuario", Usuario);
            _with1.Parameters.AddWithValue("@Contraseña", Contraseña);

            try
            {
                Cn.Open();
                xfil = (Int32)Cmd.ExecuteScalar();
                if(xfil > 0)
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
                {
                    Cn.Close();
                    Cmd.Dispose();
                    Cmd = null;
                    Cn.Close();
                    Cn = null;
                }
                MessageBox.Show(ex.Message, "Error al verificar acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return functionReturnValue;
        } //Fin de la funcion BD_Verificar_Acceso

        public DataTable BD_Leer_Datos_Usuarios(string Usuario)
        {
            SqlConnection Cn = new SqlConnection();
            try
            {
                Cn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Usuario_Login", Cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Usuario", Usuario);
                DataTable dato = new DataTable();
                da.Fill(dato);
                da = null;
                return dato;
            }
            catch (Exception ex)
            {
                if (Cn.State == ConnectionState.Open)
                {
                    Cn.Close();
                    Cn.Close();
                    Cn = null;
                    MessageBox.Show("Error al leer usuarios" + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        } //Fin de la funcion BD_Leer_Usuarios
    }
}
