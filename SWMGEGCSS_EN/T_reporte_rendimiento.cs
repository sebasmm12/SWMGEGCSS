using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_reporte_rendimiento
    {
        public int rep_ren_id { get; set; }
        public int usu_codigo { get; set; }
        public DateTime rep_ren_fecha_inicio { get; set; }
        public DateTime rep_ren_fecha_fin { get; set; }
        public int rep_ren_act_completadas { get; set; }
        public int rep_ren_act_obs_x_error { get; set; }
        public int rep_ren_atraso { get; set; }

    }
}
