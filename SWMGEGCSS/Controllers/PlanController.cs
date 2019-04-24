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
            /*var planesmodel = new PlanDataAccess().sp_Consultar_Lista_Plan();
            var model = new GestionarPlanProyectoViewModel();
            model.plans = plan;
            var plan_expediente = planesmodel.Find(modelo => (modelo.plan_nombre == model.Expediente.plan_nombre));
            var modelExpediente = new T_expedientes();
            modelExpediente.plan_id = plan_expediente.plan_id;
            modelExpediente.usu_creador = (int)Session["login"];
            modelExpediente.exp_inicio = model.Expediente.exp_inicio;
            modelExpediente.exp_fin = model.Expediente.exp_fin;
            modelExpediente.tipo_servicio_id = plan_expediente.tipo_servicio_id;
            modelExpediente.exp_ganancia = model.Expediente.exp_ganancia;
            modelExpediente.exp_nombre = model.Expediente.exp_nombre;
            var operationResult = new PlanDataAccess().sp_Insertar_Proyecto(modelExpediente);*/
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