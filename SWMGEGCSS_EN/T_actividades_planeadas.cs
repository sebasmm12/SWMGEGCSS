using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_actividades_planeadas
    {
        public int act_plan_id { get; set; }
        public int plan_id { get; set; }
        public int act_id { get; set; }
        public string act_plan_nombre { get; set; }
        public string act_plan_descripcion { get; set; }
        public double act_plan_costo { get; set; }
        public int tiempo { get; set; }
    }
}
