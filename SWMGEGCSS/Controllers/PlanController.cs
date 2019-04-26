using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWMGEGCSS.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Agregar_Plan_de_Proyectos()
        {
            var model = new GestionarPlanProyectoViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Agregar_Plan_de_Proyectos(T_plan plan)
        {
            var planesmodel = new PlanDataAccess().sp_Consultar_Lista_Plan();
            var model = new GestionarPlanProyectoViewModel();
            model.plans = plan;
            var planes = planesmodel.Find(modelo => (modelo.plan_nombre == model.plans.plan_nombre));
            var modelPlan = new T_plan();
            modelPlan.plan_id = planes.plan_id;
            modelPlan.usu_codigo = (int)Session["login"];
            modelPlan.plan_nombre = model.plans.plan_nombre;
            modelPlan.
            var operationResult = new PlanDataAccess().sp_Agregar_Plan(modelPlan);
            return View();
        }
        public ActionResult Actualizar_Plan_de_Proyectos()
        {
            var model = new GestionarPlanProyectoViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Actualizar_Plan_de_Proyectos(T_plan plan)
        {
            return View();
        }
    }
}