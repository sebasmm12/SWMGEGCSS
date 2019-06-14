using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_auditoria_observacion_actividades
    {
        public int audi_obs_act_id { get; set; }
        public string audi_obs_act_nombre { get; set; }
        public string audi_obs_act_revisor { get; set; }
        public string audi_obs_act_usuario { get; set; }
        public DateTime audi_obs_act_fecha_revisor { get; set; }
        public DateTime audi_obs_act_fecha_usuario { get; set; }
        public DateTime audi_obs_act_creacion { get; set; }
        public int obs_act_id { get; set; }
        public int audi_obs_act_desa_id { get; set; }
    }
}
