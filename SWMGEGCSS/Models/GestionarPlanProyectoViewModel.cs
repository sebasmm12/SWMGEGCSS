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
        //public string tipo_estado { get; set; }
    }
}