﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_actividades_desarrollar
    {
        public int act_desa_id { get; set; }
        public int exp_id { get; set; }
        public int usu_creador { get; set; }
        public DateTime act_desa_fecha_inicio{ get; set; }
        public DateTime act_desa_fecha_fin { get; set; }
        public DateTime act_desa_fecha_finalizada { get; set; }
        public int est_act_id { get; set; }
        public int usu_revisor { get; set; }
        public int usu_asignado { get; set; }
        public string act_desa_nombre { get; set; }
        public string act_desa_descripcion { get; set; }
        public string act_desa_archivourl { get; set; }
        public string act_desa_revisor_obs { get; set; }
        public string act_desa_comentario { get; set; }
        public string act_desa_archivo_nombre { get; set; }
        public DateTime act_desa_fecha_revision { get; set; }
    }
}
