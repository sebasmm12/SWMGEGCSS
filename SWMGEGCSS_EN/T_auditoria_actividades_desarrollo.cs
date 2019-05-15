using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_auditoria_actividades_desarrollo
    {
        public int adi_act_desa_id { get; set; }
        public int act_desa_id { get; set; }
        public DateTime audi_act_fecha_inicio { get; set; }
        public DateTime audi_act_fecha_fin { get; set; }
        public DateTime audi_act_fecha_revision { get; set; }
        public DateTime audi_act_fecha_finalizada { get; set; }
        public int audi_act_revisor { get; set; }
        public string audi_act_revisor_obs { get; set; }
        public int usu_asignado { get; set; }
        public string audi_act_archivo_url { get; set; }
        public string audi_act_comentario { get; set; }
        public string audi_act_archivo_nombre { get; set; }
        public string audi_act_nombre { get; set; }
    }
}
