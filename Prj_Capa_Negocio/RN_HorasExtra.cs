using System;
using System.Data;
using Prj_Capa_Datos;

namespace Prj_Capa_Negocio
{
    public class RN_HorasExtra
    {
        // Tope de horas extra para quien tiene autorización y olvida marcar su salida
        public const decimal MaxHorasExtra = 4;

        public DataTable RN_Abiertos()
        {
            return new BD_HorasExtra().BD_Abiertos();
        }

        public void RN_Guardar(string ids)
        {
            new BD_HorasExtra().BD_Guardar(ids);
        }

        public bool RN_Esta_Autorizado(string idPerso, DateTime fecha)
        {
            return new BD_HorasExtra().BD_Esta_Autorizado(idPerso, fecha);
        }

        public DataTable RN_AutoSalida()
        {
            return new BD_HorasExtra().BD_AutoSalida(MaxHorasExtra);
        }
    }
}