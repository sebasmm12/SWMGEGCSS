using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_plan
    {
        public int plan_id { get; set; }
        public string plan_nombre { get; set; }
        public DateTime plan_fecha { get; set; }
        public int usu_codigo { get; set; }
        public int emp_id { get; set; }
        public int plan_estado { get; set; }
        public double plan_costo { get; set; }
        public int plan_tiempo { get; set; }
        public int plan_tipo { get; set; }
        public int tipo_servicio_id { get; set; }
    }
}
