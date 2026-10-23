using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prj_Capa_Datos;
using System.Data;

namespace Prj_Capa_Negocio
{
  public   class RN_Asistencia
    {
        public DataTable RN_Ver_Todas_Asistencia()
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Ver_Todas_Asistencia();
        }


        public DataTable RN_Ver_Todas_Asistencias_Deldia(DateTime xfecha)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Ver_Todas_Asistencias_Deldia(xfecha);
        }


        public DataTable RN_Ver_Todas_Asistencias_DelMes(DateTime xfecha)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Ver_Todas_Asistencias_DelMes(xfecha);
        }


        public DataTable RN_Ver_Todas_Asistencias_ParaExplorador(String xvalor)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Ver_Todas_Asistencias_ParaExplorador(xvalor);
        }


        public void RN_Registrar_Entrada_Personal(string idAsis, string idPerso, string HoIngreso, double tarde, int Totalhora, string justificado, string dia)
        {
            BD_Asistencia obj = new BD_Asistencia();
            obj.BD_Registrar_Entrada_Personal(idAsis, idPerso, HoIngreso, tarde, Totalhora, justificado, dia);
        }

        public void RN_Registrar_Salida_Personal(string idAsis, string idPerso, string HoSalida, int Totalhora)
        {
            BD_Asistencia obj = new BD_Asistencia();
            obj.BD_Registrar_Salida_Personal(idAsis, idPerso, HoSalida, Totalhora);
        }

        public bool RN_Checar_SiPersonal_YaMarco_Asistencia(string xidPerso)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Checar_SiPersonal_YaMarco_Asistencia(xidPerso);
        }


        public bool RN_Checar_SiPersonal_YaMarco_suEntrada(string xidPerso)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Checar_SiPersonal_YaMarco_suEntrada(xidPerso);
        }

        public bool RN_Verificar_Justificacion_Aprobado(string xidPerso)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Verificar_Justificacion_Aprobado(xidPerso);
        }

        public DataTable RN_Buscar_Asistencia_deEntrada(String idperso)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Buscar_Asistencia_deEntrada(idperso);
        }

        public bool RN_Checar_SiPersonal_TieneAsistencia_Registrada(string xidPerso)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Checar_SiPersonal_TieneAsistencia_Registrada(xidPerso);
        }

        public bool RN_Checar_SiPersonal_YaMarco_suFalta(string xidPerso)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Checar_SiPersonal_YaMarco_suFalta(xidPerso);
        }

        public void RN_Registrar_Falta_Personal(string idAsis, string idPerso)
        {
            BD_Asistencia obj = new BD_Asistencia();
            obj.BD_Registrar_Falta_Personal(idAsis, idPerso);
        }
    }
}
