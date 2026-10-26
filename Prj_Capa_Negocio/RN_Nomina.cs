using System;
using System.Data;
using Prj_Capa_Datos;

namespace Prj_Capa_Negocio
{
    public class RN_Nomina
    {
        public DataTable RN_Calcular(DateTime ini, DateTime fin, string idDistrito)
        {
            return new BD_Nomina().BD_Calcular(ini, fin, idDistrito);
        }

        public DataTable RN_Listar_Config()
        {
            return new BD_Nomina().BD_Listar_Config();
        }

        public void RN_Guardar_Config(string idPerso, decimal sueldoFijo, decimal horasPorTrabajar)
        {
            new BD_Nomina().BD_Guardar_Config(idPerso, sueldoFijo, horasPorTrabajar);
        }

        public DataTable RN_Listar_Distritos()
        {
            return new BD_Nomina().BD_Listar_Distritos();
        }
    }
}
