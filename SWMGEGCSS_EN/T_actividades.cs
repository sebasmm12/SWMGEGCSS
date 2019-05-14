using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_actividades
    {
        public int act_id { get; set; }
        public string act_nombre { get; set; }
        public string act_descripcion { get; set; }
        public int act_plazo { get; set; }
        public int act_cantidad_maxima { get; set; }
        public double costo { get; set; }
        public bool act_obligatorio { get; set; }
    }
}
