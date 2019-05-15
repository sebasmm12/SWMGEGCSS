using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_tipo_servicio_actividades
    {
        public int tipo_servicio_id { get; set; }
        public int act_id { get; set; }
        public double costo { get; set; }
        public bool act_obligatorio { get; set; }
    }
    public class T_tipo_servicio_actividades_aux
    {
        public int tipo_servicio_id { get; set; }
        public int act_id { get; set; }
        public double tipo_servicio_costo { get; set; }
        public bool tipo_servicio_obligatorio { get; set; }
        public string tipo_servicio_nombre { get; set; }
        public string act_nombre { get; set; }
    }
}
