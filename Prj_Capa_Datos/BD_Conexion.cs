using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_Capa_Datos
{
   public class BD_Conexion
    {

        public string Conectar()
        {
            return @"Data Source=localhost\SQLEXPRESS; Initial Catalog=MicroSisPlanifull;Integrated Security=True;";
            //return @"Data Source=PC-ADMIN\SQLEXPRESS; Initial Catalog=MicroSisPlani;uid=sa;pwd=admin"; ;
        }

        public static string Conectar2()
        {
            return @"Data Source=localhost\SQLEXPRESS; Initial Catalog=MicroSisPlanifull;Integrated Security=True;";
            //return @"Data Source=PC-ADMIN\SQLEXPRESS; Initial Catalog=MicroSisPlani;uid=sa;pwd=admin"; ;
        }

    }
}
