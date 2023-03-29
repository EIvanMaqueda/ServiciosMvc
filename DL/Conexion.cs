using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        public static string Getconection()
        {
            return ConfigurationManager.ConnectionStrings["EMaquedaControlEscolar"].ConnectionString.ToString();
            //string configuration= ConfigurationManager.AppSettings["urlapi"].ToString();

        }
    }
}
