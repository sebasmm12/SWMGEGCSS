using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_usuario
    {
        public int usu_codigo { get; set; }
        public string usu_usuario { get; set; }
        public string usu_contraseña { get; set; }
        public bool estado { get; set; }
        public bool usu_trabajador { get; set; }
    }
}
