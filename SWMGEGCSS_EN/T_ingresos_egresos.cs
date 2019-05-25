using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_ingresos_egresos
    {
        public int ing_egr_id { get; set; }
        public string ing_egr_nombre { get; set; }
        public string ing_egr_descripcion { get; set; }
        public int usu_codigo { get; set; }
        public DateTime ing_egr_fecha { get; set; }
        public bool ing_egr_ingrso { get; set; }
        public double ing_egr_monto { get; set; }
    }
}
