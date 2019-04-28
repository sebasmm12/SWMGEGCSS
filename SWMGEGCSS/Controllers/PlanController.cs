﻿using SWMGEGCSS.Models;
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
        public ActionResult Agregar_Plan_de_Proyectos(T_plan plans)
        {
            var planesmodel = new PlanDataAccess().sp_Consultar_Lista_Plan();
            var model = new GestionarPlanProyectoViewModel();
            model.plans = plans;
            /*var planes = planesmodel.Find(modelo => (modelo.plan_nombre == model.plans.plan_nombre));*/
            var modelPlan = new T_plan();
            modelPlan.usu_codigo = (int)Session["login"];
            modelPlan.plan_nombre = model.plans.plan_nombre;
            modelPlan.plan_fecha = model.plans.plan_fecha;
            modelPlan.emp_id = model.plans.emp_id;
            /*modelPlan.plan_estado = model.plans.plan_estado;*/
            modelPlan.plan_costo = model.plans.plan_costo;
            modelPlan.tipo_servicio_id = model.plans.tipo_servicio_id;
            modelPlan.plan_tiempo = model.plans.plan_tiempo;
            var operationResult = new PlanDataAccess().sp_Agregar_Plan(modelPlan);
            return RedirectToAction("Gestionar_Plan_Proyecto", "Gerente");
        }

        public ActionResult Actualizar_Plan_de_Proyectos(int id)
        {
            /*var model = new GestionarPlanProyectoViewModel();*/
            var model = new GestionarPlanProyectoViewModel();
            model.plans = new PlanDataAccess().sp_Consultar_Lista_Plan().Find(r => (r.plan_id == id));
            var estado_plan = new PlanDataAccess().sp_Consultar_Lista_Plan_Estado().Find(r => (r.plan_estado_id == model.plans.plan_estado));
            var nombre_empresa = new PlanDataAccess().sp_Consultar_Lista_Empresa().Find(s => (s.emp_id == model.plans.emp_id));
            var nombre_tipo_servicio = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find( t => (t.tipo_servicio_id == model.plans.tipo_servicio_id));
            return View(model);
        }
        [HttpPost]
        public ActionResult Actualizar_Plan_de_Proyectos(T_plan plans)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.plans = plans;
            var modelPlan = new T_plan();
            modelPlan.plan_id = plans.plan_id;
            modelPlan.plan_nombre = model.plans.plan_nombre;
            modelPlan.plan_fecha = model.plans.plan_fecha;
            modelPlan.emp_id = model.plans.emp_id;
            modelPlan.plan_estado = model.plans.plan_estado;
            modelPlan.plan_costo = model.plans.plan_costo;
            modelPlan.tipo_servicio_id = model.plans.tipo_servicio_id;
            modelPlan.plan_tiempo = model.plans.plan_tiempo;
            var operationResult = new PlanDataAccess().sp_Actualizar_Plan(modelPlan);
            return RedirectToAction("Gestionar_Plan_Proyecto", "Gerente");
        }
    }
}