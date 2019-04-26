using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_auditar_expedientes
    {
        public int exp_id { get; set; }
        public int aud_exp_id { get; set; }
        public DateTime aud_exp_inicio { get; set; }
        public DateTime aud_exp_fin { get; set; }
        public int tipo_servicio_id { get; set; }
        public double aud_exp_ganancia { get; set; }
        public string aud_exp_comentario { get; set; }
    }
}
