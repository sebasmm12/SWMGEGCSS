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
            var modelExpediente = new T_expedientes();
            modelExpediente.plan_id = plan_expediente.plan_id;
            modelExpediente.usu_creador = (int)Session["login"];
            modelExpediente.exp_inicio = model.Expediente.exp_inicio;
            modelExpediente.exp_fin = model.Expediente.exp_fin;
            modelExpediente.tipo_servicio_id = plan_expediente.tipo_servicio_id;
            modelExpediente.exp_ganancia = model.Expediente.exp_ganancia;
            modelExpediente.exp_nombre = model.Expediente.exp_nombre;
            var operationResult = new ExpedienteDataAccess().sp_Insertar_Proyecto(modelExpediente);
            return View();
        }
        [HttpGet]
        public ActionResult _ModalProyecto()
        {
            var model = new ExpedienteViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalProyecto(int id)
        {
            var model = new ExpedienteViewModel();
            model.Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().Find(r => (r.exp_id == id));
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
    }
}