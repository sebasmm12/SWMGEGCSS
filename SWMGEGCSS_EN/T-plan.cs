﻿using System;
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
        public string emp_ruc { get; set; }
        public int plan_estado { get; set; }
        public double plan_costo { get; set; }
        public int plan_tiempo { get; set; }
        public int plan_tipo { get; set; }
        public int tipo_servicio_id { get; set; }
        public string plan_comentario { get; set; }
    }
    public class T_plan_aux
    {
        public int plan_id { get; set; }
        public string plan_nombre { get; set; }
        public DateTime plan_fecha { get; set; }
        public int usu_codigo { get; set; }
        public string emp_razon_social { get; set; }
        public string plan_estado_nobre { get; set; }
        public double plan_costo { get; set; }
        public int plan_tiempo { get; set; }
        public int plan_tipo { get; set; }
        public string tipo_servicio_nombre { get; set; }
        public string plan_comentario { get; set; }
    }
}
