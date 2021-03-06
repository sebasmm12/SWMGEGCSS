﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        public string est_act_nombre { get; set; }
        public string usu_revisor_nombre { get; set; }
        public string usu_asignado_nombre { get; set; }
    }
    public class T_actividades_desarrollar_aux
    {
        public int act_desa_id { get; set; }
        public string exp_nombre { get; set; }
        public int usu_creador { get; set; }
        public DateTime act_desa_fecha_inicio { get; set; }
        public DateTime act_desa_fecha_fin { get; set; }
        public DateTime act_desa_fecha_finalizada { get; set; }
        public string est_act_nombre { get; set; }
        public int usu_revisor { get; set; }
        public int usu_asignado { get; set; }
        public string act_desa_nombre { get; set; }
        public string act_desa_descripcion { get; set; }
        public string act_desa_archivourl { get; set; }
        public string act_desa_revisor_obs { get; set; }
        public string act_desa_comentario { get; set; }
        public string act_desa_archivo_nombre { get; set; }
        public DateTime act_desa_fecha_revision { get; set; }
        public DateTime exp_fin { get; set; }
    }
    public class T_actividades_desarrollar_aux2
    {
        public int act_desa_id { get; set; }
        public string emp_ruc { get; set; }
        public int plan_id { get; set; }
        public string act_desa_archivo_nombre { get; set; }
        public string act_desa_comentario { get; set; }
        public string act_desa_revisor_obs { get; set; }
    }
    public class T_actividades_desarrollar_aux3
    {
        public string act_desa_comentario { get; set; }
        public HttpPostedFileBase archivo { get; set; }
        public string act_desa_revisor_obs { get; set; }
    }


    public class T_actividades_desarrollar_gerente
    {
        public string emp_razon_social { get; set; }
        public string act_desa_nombre { get; set; }
        public int act_desa_id { get; set; }
        public string act_desa_descripcion { get; set; }
        public int est_act_id { get; set; }
        public string act_desa_archivo_nombre { get; set; }
        public string act_desa_comentario { get; set; }
        public string act_desa_revisor_obs { get; set; }
        public DateTime act_desa_fecha_inicio { get; set; }
        public DateTime act_desa_fecha_fin { get; set; }
        public DateTime act_desa_fecha_finalizada { get; set; }
    }

    public class T_actividades_desarrollar_usuarios
    {
        public int usu_codigo { get; set; }
        public string det_usu_nombre { get; set; }
        public int cant_actividades { get; set; }
        public string usu_usuario { get; set; }
        public int act_desa_id { get; set; }
        public DateTime act_desa_fecha_inicio { get; set; }
        public DateTime act_desa_fecha_fin { get; set; }
        public string act_desa_nombre { get; set; }
        public string plan_nombre { get; set; }
        public int count { get; set; }
    }

}
