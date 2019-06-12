using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_notificaciones
    {
        public int not_id { get; set; }
        public string not_nombre { get; set; }
        public string not_descripcion { get; set; }
        public int usu_codigo { get; set; }
        public DateTime noti_fecha{ get; set; }
        public int usu_envio { get; set; }
    }
}
