using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_expedientes
    {
        public int exp_id { get; set; }
        public int est_exp_id { get; set; }
        public int plan_id { get; set; }
        public int usu_creador { get; set; }
        public DateTime exp_inicio { get; set; }
        public DateTime exp_fin { get; set; }
        public int tipo_servicio_id { get; set; }
        public double exp_ganacia { get; set; }
    }
}
