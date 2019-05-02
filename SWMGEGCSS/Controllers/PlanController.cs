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
        public ActionResult Agregar_Plan_de_Proyectos_1()
        {
            var model = new GestionarPlanProyectoViewModel();
            T_plan modelPlan = (T_plan)Session["Plan_datos"];
            model.List_Actividades = new PlanDataAccess().sp_Consultar_Actividades_Plan(modelPlan.tipo_servicio_id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Agregar_Plan_de_Proyectos(T_plan_aux plans_aux)
        {
            /*var planesmodel = new PlanDataAccess().sp_Consultar_Lista_Plan();*/
            var model = new GestionarPlanProyectoViewModel();
            model.plans_aux = plans_aux;

            var tipoServicioModel = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find(x => (x.tipo_servicio_nombre == model.plans_aux.tipo_servicio_nombre));
            var empresaModel = new PlanDataAccess().sp_Consultar_Lista_Empresa().Find(y => (y.emp_razon_social == model.plans_aux.emp_razon_social));

            var modelPlan = new T_plan();
            modelPlan.usu_codigo = (int)Session["login"];
            modelPlan.plan_nombre = model.plans_aux.plan_nombre;
            modelPlan.plan_fecha = model.plans_aux.plan_fecha;
            modelPlan.emp_id = empresaModel.emp_id;
            modelPlan.plan_costo = model.plans_aux.plan_costo;
            modelPlan.tipo_servicio_id = tipoServicioModel.tipo_servicio_id;
            modelPlan.plan_tiempo = model.plans_aux.plan_tiempo;
            //var operationResult = new PlanDataAccess().sp_Agregar_Plan(modelPlan);
            Session["Plan_datos"] = modelPlan;
            //return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet
            //redireccion al Agregar_plan_1
            return Json(new { data = 1}, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Actualizar_Plan_de_Proyectos(int id)
        {
            /*var model = new GestionarPlanProyectoViewModel();*/
            var model = new GestionarPlanProyectoViewModel();
            //se obtiene el plan seleccionado
            model.plans_aux = new PlanDataAccess().sp_Consultar_Lista_Plan().Find(r => (r.plan_id == id));
            //se obtiene los valores del tipo servicio del plan seleccionado
            var tipoServicioModel = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find(x => (x.tipo_servicio_nombre == model.plans_aux.tipo_servicio_nombre));
            //se obtiene la lista de actividades segun el tipo de servicio del plan seleccionado
            model.List_Actividades = new ActividadesDataAccess().sp_Consultar_Actividades_Diferentes_Plan(tipoServicioModel.tipo_servicio_id);
            //se obtiene las actividades planificadas previamente
            model.List_Actividades_planeadas_aux = new  ActividadesDataAccess().sp_Consultar_Lista_Actividades_Planeadas_aux().FindAll(r => (r.plan_nombre == model.plans_aux.plan_nombre));
            model.List_Actividades_planeadas = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().FindAll(r => (r.plan_id == id));
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
            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }
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
        public ActionResult CompletarNombreEmpresas([Bind(Prefix = "term")]string term)
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
        public ActionResult CompletarNombreTipoServicio([Bind(Prefix = "term")]string term)
        {
            var model = new List<T_tipo_servicio>();
            model = new PlanDataAccess().sp_Consultar_Lista_Nombre_Tipo_Servicio(term);
            var nameTipoServicio = model.Select(r => new
            {
                label = r.tipo_servicio_nombre
            });
            return Json(nameTipoServicio, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _Eliminar_Plan_De_Proyecto()
        {
            var model = new GestionarPlanProyectoViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _Eliminar_Plan_De_Proyecto(int id)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.plans_aux = new PlanDataAccess().sp_Consultar_Lista_Plan().Find(r => (r.plan_id == id));
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult EliminarPlanDeProyecto(int id, string comentario)
        {
            var operationResult = new PlanDataAccess().sp_Cancelar_Plan(id, comentario);
            return Json(new { id = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Nombre_Plan(string plan_nombre)
        {
            var model = new PlanDataAccess().sp_Consultar_Lista_Plan();
            var modelEvaluado = model.Find(r => r.plan_nombre == plan_nombre);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Nombre_Empresa_Plan(string plan_emp)
        {
            var model = new EmpresaDataAccess().sp_Consultar_Lista_Empresas();
            var modelEvaluado = model.Find(r => r.emp_razon_social == plan_emp);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Tipo_Servicio_Plan(string plan_tipo_servicio)
        {
            var model = new TipoServicioDataAccess().sp_Consultar_Lista_Tipo_Servicio();
            var modelEvaluado = model.Find(r => r.tipo_servicio_nombre == plan_tipo_servicio);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ModalActualizarActividadesPlanificadas()
        {
            var model = new GestionarPlanProyectoViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalActualizarActividadesPlanificadas(int act_plan_id)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.Actividades_planeadas_aux = new ActividadesDataAccess().sp_Consultar_Lista_Actividades_Planeadas_aux().Find(c => (c.act_plan_id == act_plan_id));
            return PartialView("_ModalActualizarActividadesPlanificadas",model);
        }
        [HttpPost]
      public ActionResult _ModalActualizarActividadesPlanificadasFinal( T_actividades_planeadas act_plan)
        {
                var actividadesPlaneadas = new T_actividades_planeadas();       
                var actplan = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().Find(x => x.act_plan_id == act_plan.act_plan_id);
                actividadesPlaneadas.act_plan_id = act_plan.act_plan_id;
                actividadesPlaneadas.plan_id = act_plan.plan_id;
                actividadesPlaneadas.act_id = act_plan.act_id;
                actividadesPlaneadas.act_plan_nombre = act_plan.act_plan_nombre;
                actividadesPlaneadas.act_plan_descripcion = act_plan.act_plan_descripcion;
                actividadesPlaneadas.act_plan_costo = act_plan.act_plan_costo;
                actividadesPlaneadas.act_plan_tiempo = act_plan.act_plan_tiempo;
                var operationResult = new ActividadesDataAccess().sp_actualizar_actividades_planeadas(actividadesPlaneadas);
            return Json(actplan.plan_id, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ModalEliminarActividadesPlanificadas()
        {
            var model = new GestionarPlanProyectoViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalEliminarActividadesPlanificadas(int act_plan_id)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.Actividades_planeadas_aux = new ActividadesDataAccess().sp_Consultar_Lista_Actividades_Planeadas_aux().Find(c => (c.act_plan_id == act_plan_id));
            return PartialView("_ModalEliminarActividadesPlanificadas", model);
        }
        [HttpPost]
        public ActionResult _ModalEliminarActividadesPlanificadasFinal(int act_plan_id)
        {
            var model = new GestionarPlanProyectoViewModel();
            var actplan = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().Find(x => x.act_plan_id == act_plan_id);
            var operationResult = new ActividadesDataAccess().sp_eliminar_actividades_planeadas(act_plan_id);
            return Json(actplan.plan_id, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult _ModalAgregarActividadesPlanificadas()
        {
            var model = new GestionarPlanProyectoViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalAgregarActividadesPlanificadas(int plan_id, int act_id)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.Actividad_planeada = new T_actividades_planeadas();
            model.Actividad_planeada.plan_id = plan_id;
            model.Actividad_planeada.act_id = act_id;
            return PartialView("_ModalAgregarActividadesPlanificadas", model);
        }
        [HttpPost]
        public ActionResult _ModalRegistrarActividadesPlanificadasFinal(T_actividades_planeadas act_plan)
        {
            var model   = new GestionarPlanProyectoViewModel();
            model.Actividad_planeada = new T_actividades_planeadas();
            model.Actividad_planeada.plan_id = act_plan.plan_id;
            model.Actividad_planeada.act_id = act_plan.act_id;
            model.Actividad_planeada.act_plan_nombre = act_plan.act_plan_nombre;
            model.Actividad_planeada.act_plan_descripcion = act_plan.act_plan_descripcion;
            model.Actividad_planeada.act_plan_costo = act_plan.act_plan_costo;
            model.Actividad_planeada.act_plan_tiempo = act_plan.act_plan_tiempo;
            var operationResult = new ActividadesDataAccess().sp_registrar_actividades_planeadas(model.Actividad_planeada);
            return Json(act_plan.plan_id, JsonRequestBehavior.AllowGet);
        }

    }
}