using PagedList;
using SWMGEGCSS_EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWMGEGCSS.Models
{
    public class GestionarPlanProyectoViewModel
    {
        public T_plan_aux plans_aux { get; set; }
        public T_plan plans { get; set; }
        public List<T_plan_aux> listplans { get; set; }

        public IPagedList<T_plan_aux> listPplans
        { get; set; }

        public T_plan_estado Estado_Plan { get; set; }
        public List<T_plan_estado> List_Estado_Plan { get; set; }
        public List<T_actividades> List_Actividades { get; set; }
        public List<T_actividades_planeadas_aux> List_Actividades_planeadas_aux { get; set; }
        public List<T_actividades_planeadas> List_Actividades_planeadas { get; set; }
        public T_actividades_planeadas_aux Actividades_planeadas_aux { get; set; }
        //alguien lo cambio
        public T_actividades_desarrollar Actividades_planeadas { get; set; }
        public T_actividades_planeadas Actividad_planeada { get; set; }
        public TipoServicioViewModel tipoServicioModel { get; set; }
        public ActividadViewModel ActividadesModel { get; set; }
        public List<T_plan_estado> lista_plan_estado { get; set; }
        public string tipo_estado { get; set; }

        public T_tipo_servicio_actividades tipo_servicio_act { get; set; }
    }
}