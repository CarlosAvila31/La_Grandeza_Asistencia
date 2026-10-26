using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroSisPlani.Informes.Helpers
{
    public static class UtilidadesTiempo
    {
        // Devuelve horas en decimal (ej. 7.50 = 7h30m), redondeado a 2 decimales
        public static double CalcularTotalHoras(string hoIngresoStr, string hoSalidaStr, DateTime fechaAsis)
        {
            if (string.IsNullOrWhiteSpace(hoIngresoStr) || string.IsNullOrWhiteSpace(hoSalidaStr))
                return 0.0;

            if (!TimeSpan.TryParse(hoIngresoStr, out TimeSpan hoIngreso))
                hoIngreso = TimeSpan.Zero;
            if (!TimeSpan.TryParse(hoSalidaStr, out TimeSpan hoSalida))
                hoSalida = TimeSpan.Zero;

            DateTime fechaHoraIngreso = fechaAsis.Date + hoIngreso;
            DateTime fechaHoraSalida = fechaAsis.Date + hoSalida;

            if (fechaHoraSalida < fechaHoraIngreso)
                fechaHoraSalida = fechaHoraSalida.AddDays(1); // cruza medianoche

            TimeSpan diff = fechaHoraSalida - fechaHoraIngreso;
            return Math.Round(diff.TotalMinutes / 60.0, 2);
        }
    }
}
