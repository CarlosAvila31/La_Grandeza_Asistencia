using System;
using System.Collections.Generic;
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

        // Gasto total por sucursal, calculado a partir del detalle que devuelve RN_Calcular.
        // La ultima fila es el TOTAL general.
        public DataTable RN_Gasto_Por_Sucursal(DataTable detalle)
        {
            DataTable res = new DataTable();
            res.Columns.Add("Sucursal", typeof(string));
            res.Columns.Add("Empleados", typeof(int));
            res.Columns.Add("Horas_Trabajadas", typeof(decimal));
            res.Columns.Add("Sueldo_Bruto", typeof(decimal));
            res.Columns.Add("Porcentaje", typeof(decimal));

            if (detalle == null || detalle.Rows.Count == 0) return res;

            List<string> orden = new List<string>();
            Dictionary<string, decimal[]> acum = new Dictionary<string, decimal[]>();   // [empleados, horas, total]

            foreach (DataRow r in detalle.Rows)
            {
                string suc = Convert.ToString(r["Sucursal"]).Trim();
                decimal[] a;
                if (!acum.TryGetValue(suc, out a))
                {
                    a = new decimal[3];
                    acum[suc] = a;
                    orden.Add(suc);
                }
                a[0] += 1;
                a[1] += Convert.ToDecimal(r["Horas_Trabajadas"]);
                a[2] += Convert.ToDecimal(r["Sueldo_Bruto"]);
            }

            decimal totalEmp = 0, totalHoras = 0, total = 0;
            foreach (string suc in orden)
            {
                totalEmp += acum[suc][0];
                totalHoras += acum[suc][1];
                total += acum[suc][2];
            }

            foreach (string suc in orden)
            {
                decimal[] a = acum[suc];
                decimal pct = total > 0 ? Math.Round(a[2] * 100m / total, 1) : 0m;
                res.Rows.Add(suc, (int)a[0], a[1], a[2], pct);
            }

            res.Rows.Add("TOTAL", (int)totalEmp, totalHoras, total, total > 0 ? 100m : 0m);
            return res;
        }
    }
}