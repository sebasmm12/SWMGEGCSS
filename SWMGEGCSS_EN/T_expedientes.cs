using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        public double exp_ganancia { get; set; }
        public string exp_nombre { get; set; }
        public string archivo_ulr_inicio { get; set; }
        public string archivo_url_final { get; set; }
    }
    public class T_expediente_aux
    {
        public DateTime fecha_actual;

        public int exp_id { get; set; }
        public string est_exp_nombre { get; set; }
        public string plan_nombre { get; set; }
        public int usu_creador { get; set; }
        public DateTime exp_inicio { get; set; }
        public DateTime exp_fin { get; set; }
        public string tipo_servicio_nombre { get; set; }
        public double exp_ganancia { get; set; }
        public string exp_nombre { get; set; }
        public int cantidad_actividades_desarrollar { get; set; }
        public int cantidad_actividades_terminar { get; set; }
        public HttpPostedFileBase archivo_ulr_inicio { get; set; }
        public HttpPostedFileBase archivo_url_final { get; set; }

    }
}
