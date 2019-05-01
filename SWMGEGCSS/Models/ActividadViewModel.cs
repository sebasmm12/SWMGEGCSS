using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS_EN;
using PagedList;
namespace SWMGEGCSS.Models
{
    public class ActividadViewModel
    {
        public T_actividades Actividades {get;set;}
        public List<T_actividades> list_Actividades { get; set; }

        public IPagedList<T_actividades> IPlist_Activiades { get; set; }

        public T_actividades_planeadas Actividades_Planeadas { get; set; }

        public List<T_actividades_planeadas> list_Actividades_Planeadas { get; set; }

        public T_actividades_desarrollar Actividades_Desarrollar { get; set; }
        public List<T_actividades_desarrollar> list_Actividades_Desarrollar { get; set; }
    }
}