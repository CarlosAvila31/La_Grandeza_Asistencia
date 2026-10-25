using System;
using System.Data;
using Prj_Capa_Datos;

namespace Prj_Capa_Negocio
{
    public class RN_Descanso
    {
        public DataTable RN_Listar_Semana(DateTime semana)
        {
            return new BD_Descanso().BD_Listar_Semana(semana);
        }

        public void RN_Guardar_Semana(string idPerso, DateTime semana, string diasDescanso)
        {
            new BD_Descanso().BD_Guardar_Semana(idPerso, semana, diasDescanso);
        }

        public void RN_Copiar_Semana(DateTime desde, DateTime hasta)
        {
            new BD_Descanso().BD_Copiar_Semana(desde, hasta);
        }

        // 1 = laboral, 0 = descanso, -1 = nunca programado
        public int RN_Es_Dia_Laboral(string idPerso, DateTime fecha)
        {
            return new BD_Descanso().BD_Es_Dia_Laboral(idPerso, fecha);
        }
    }
}