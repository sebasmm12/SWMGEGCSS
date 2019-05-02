using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using System.Globalization;
namespace SWMGEGCSS.Controllers
{
    public class ExpedienteController : Controller
    {
        // GET: Expediente
        static List<T_actividades_desarrollar> list_desarrollar = new List<T_actividades_desarrollar>();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registrar_Proyecto()
        {
            var model = new ExpedienteViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Registrar_Proyecto(T_expediente_aux Expediente)
        {
          
            var planesmodel = new ExpedienteDataAccess().sp_Obtener_Planes();
            var model = new ExpedienteViewModel();
            model.Expediente = Expediente;
            var plan_expediente = planesmodel.Find(modelo => (modelo.plan_nombre == model.Expediente.plan_nombre));
            var modeloActividades = new ActividadViewModel();
            modeloActividades.list_Actividades_Planeadas = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().FindAll(r=>r.plan_id==plan_expediente.plan_id);
            var modelExpediente = new T_expedientes();
            modelExpediente.plan_id = plan_expediente.plan_id;
            modelExpediente.usu_creador = (int)Session["login"];
            modelExpediente.exp_inicio = model.Expediente.exp_inicio;
            modelExpediente.exp_fin = model.Expediente.exp_fin;
            modelExpediente.tipo_servicio_id = plan_expediente.tipo_servicio_id;
            modelExpediente.exp_ganancia = model.Expediente.exp_ganancia;
            modelExpediente.exp_nombre = model.Expediente.exp_nombre;
            var operationResult = new ExpedienteDataAccess().sp_Insertar_Proyecto(modelExpediente);
            modelExpediente = new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(r => r.exp_nombre == modelExpediente.exp_nombre);
            foreach (var item in modeloActividades.list_Actividades_Planeadas)
            {
                T_actividades_desarrollar act_desarrollar = new T_actividades_desarrollar();
                act_desarrollar.exp_id = modelExpediente.exp_id;
                act_desarrollar.usu_creador = modelExpediente.usu_creador;
                act_desarrollar.est_act_id = 1;
                act_desarrollar.act_desa_nombre = item.act_plan_nombre;
                act_desarrollar.act_desa_descripcion = item.act_plan_descripcion;
                var operationResult1 = new ActividadesDataAccess().sp_Insertar_Actividades_Desarrollar(act_desarrollar);
            }

            return Json(new { data=operationResult.NewId},JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ModalProyecto()
        {
            var model = new ExpedienteViewModel();
            model.ActividadModel = new ActividadViewModel();
            model.ActividadModel.list_Actividades = new List<T_actividades>();
            model.ActividadModel.list_Actividades_Desarrollar = new List<T_actividades_desarrollar>();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalProyecto(int id)
        {
            var model = new ExpedienteViewModel();
            model.Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().Find(r => (r.exp_id == id));
            model.Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(r=>(r.exp_id==id));
            model.ActividadModel = new ActividadViewModel();
            model.ActividadModel.list_Actividades = new ActividadesDataAccess().sp_Consultar_Actividades_Diferentes_Plan(model.Expedientes.tipo_servicio_id);
            model.ActividadModel.list_Actividades_Desarrollar = new ActividadesDataAccess().sp_Consultar_Actividades_Desarrollar_Expediente().FindAll(r=>r.exp_id==id);
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult _EliminarProyecto()
        {
            var model = new ExpedienteViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _EliminarProyecto(int id)
        {
            var model = new ExpedienteViewModel();
            model.Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().Find(r => (r.exp_id == id));
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult EliminarExpediente(int id,string comentario)
        {
            var Expedientes = new ExpedienteViewModel();
            Expedientes.Expedientes= new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(modelo=>modelo.exp_id==id);
            var AuditoriaExpediente = new AuditoriaViewModel();
            AuditoriaExpediente.auditoria_expediente = new T_auditar_expedientes();
 
            AuditoriaExpediente.auditoria_expediente.exp_id = Expedientes.Expedientes.exp_id;
            AuditoriaExpediente.auditoria_expediente.aud_exp_inicio = Expedientes.Expedientes.exp_inicio;
            AuditoriaExpediente.auditoria_expediente.aud_exp_fin = Expedientes.Expedientes.exp_fin;
            AuditoriaExpediente.auditoria_expediente.aud_exp_ganancia = Expedientes.Expedientes.exp_ganancia;
            AuditoriaExpediente.auditoria_expediente.aud_exp_comentario = comentario;
            AuditoriaExpediente.auditoria_expediente.tipo_servicio_id = Expedientes.Expedientes.tipo_servicio_id;
            var operationResultAuditorio = new ExpedienteDataAccess().sp_Insertar_Auditoria_Expediente(AuditoriaExpediente.auditoria_expediente);
            var operationResult = new ExpedienteDataAccess().sp_Eliminar_Proyecto(id,comentario);
            return Json(new { id = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Nombre_Proyecto(string exp_nombre)
        {
            var model = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos();
            var modelEvaluado = model.Find(r => r.exp_nombre == exp_nombre);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Nombre_Plan(string exp_plan)
        {
            var model = new ExpedienteDataAccess().sp_Obtener_Planes();
            var modelEvaluado = model.Find(r => r.plan_nombre == exp_plan);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Autocomplete([Bind(Prefix ="term")]string term)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.listplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(term);
            var namePlanes = model.listplans.Select(r => new
            {
                label = r.plan_nombre
            });
            return Json(namePlanes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _ListarActividadesNoPlan(int id)
        {
            var model =new  ActividadViewModel();
            model.list_Actividades = new ActividadesDataAccess().sp_Consultar_Actividades_Diferentes_Plan(id);
            return PartialView(model);
        }
        public ActionResult AñadirActividad(int act_desa_id)
        {
            var Actividad = new T_actividades_desarrollar();
            list_desarrollar.Add(Actividad);
            int p = 1;
            return Json(p, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarActividad(int act_desa_id)
        {
            var Actividad = new T_actividades_desarrollar();
           // Actividad= new ActividadesDataAccess().sp
            list_desarrollar.Remove(Actividad);
            int p = 2;
            return Json(p, JsonRequestBehavior.AllowGet);
        }


    }
}