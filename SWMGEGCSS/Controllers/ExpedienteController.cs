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
    }
}