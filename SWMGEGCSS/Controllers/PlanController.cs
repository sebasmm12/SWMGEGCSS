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
            model.plans_aux = new PlanDataAccess().sp_Consultar_Lista_Plan().Find(r => (r.plan_id == id));
            return View(model);
        }
        [HttpPost]
        public ActionResult Actualizar_Plan_de_Proyectos(T_plan_aux plans_aux)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.plans_aux = plans_aux;

            var tipoServicioModel = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find(x => (x.tipo_servicio_nombre == model.plans_aux.tipo_servicio_nombre));
            var empresaModel = new PlanDataAccess().sp_Consultar_Lista_Empresa().Find(y => (y.emp_razon_social == model.plans_aux.emp_razon_social));
            var planEstadoModel = new PlanDataAccess().sp_Consultar_Lista_Plan_Estado().Find(z => (z.plan_estado_nombre == model.plans_aux.plan_estado_nobre));         
            
            var modelPlan = new T_plan();
            modelPlan.plan_id = model.plans_aux.plan_id;
            modelPlan.plan_nombre = model.plans_aux.plan_nombre;
            modelPlan.plan_fecha = model.plans_aux.plan_fecha;
            modelPlan.emp_id = empresaModel.emp_id;
            modelPlan.plan_estado = planEstadoModel.plan_estado_id;
            modelPlan.plan_costo = model.plans_aux.plan_costo;

            modelPlan.tipo_servicio_id = tipoServicioModel.tipo_servicio_id;
            modelPlan.plan_tiempo = model.plans_aux.plan_tiempo;
            var operationResult = new PlanDataAccess().sp_Actualizar_Plan(modelPlan);
            return RedirectToAction("Gestionar_Plan_Proyecto", "Gerente");
        }
        /*GERENTE */
        public ActionResult CompletarNombrePlanes(string term)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.listplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(term);
            var nameExpedientes = model.listplans.Select(r => new
            {
                label = r.plan_nombre
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }
        //*Metodos para el autocompletado*//
        public ActionResult CompletarNombreEmpresas(string term)
        {
            var model = new GestionarEmpresaViewModel();
            model.listempresas = new PlanDataAccess().sp_Consultar_Lista_Nombre_Empresa(term);
            var nameEmpresas = model.listempresas.Select(r => new
            {
                label = r.emp_razon_social
            });
            return Json(nameEmpresas, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CompletarNombreEstadoPlan(string term)
        {
            var model = new List<T_plan_estado>();
            model = new PlanDataAccess().Sp_Consultar_Lista_Estado_Plan(term);
            var namePlanEstado = model.Select(r => new
            {
                label = r.plan_estado_nombre
            });
            return Json(namePlanEstado, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CompletarNombreTipoServicio(string term)
        {
            var model = new List<T_tipo_servicio>();
            model = new PlanDataAccess().sp_Consultar_Lista_Nombre_Tipo_Servicio(term);
            var nameTipoServicio = model.Select(r => new
            {
                label = r.tipo_servicio_nombre
            });
            return Json(nameTipoServicio, JsonRequestBehavior.AllowGet);
        }
    }
}