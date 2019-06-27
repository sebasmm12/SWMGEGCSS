using PagedList;
using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWMGEGCSS.Controllers
{
    public class Auditoria_ProyectoController : Controller
    {
        // GET: Auditoria_Proyecto
        [HttpGet]
        public ActionResult Visualizar_Auditoria(int exp_id,int page = 1)
        {
            AuditoriaViewModel model = new AuditoriaViewModel();
            model.Plist_aux_auditoria_expediente = new AuditoriaExpedienteDataAccess().sp_Listar_Aux_Expediente_por_ExpedienteId(exp_id).ToPagedList(page, 3);
            model.list_actividades_desarrollar = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_por_ExpedienteId(exp_id);
            Session["expedienteid"] = exp_id;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Lista_Auditoria_Proyecto", model);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Visualizar_Auditoria_Actividades(int act_desa_id, int page = 1)
        {
            AuditoriaViewModel model = new AuditoriaViewModel();
            model.Plist_auditoria_actividades_desarrollar = new AuditoriaExpedienteDataAccess().sp_Listar_Auditoria_Actividades_Desarrollar_por_ActividadesId(act_desa_id).ToPagedList(page, 3);
            model.list_actividades_observaciones = new AuditoriaExpedienteDataAccess().sp_Listar_Observacion_por_Actividad_sin_estado(act_desa_id);
            Session["actividadid"] = act_desa_id;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Lista_Auditoria_Actividades", model);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Visualizar_Auditoria_Observacion(int obs_act_id, int page = 1)
        {
            AuditoriaViewModel model = new AuditoriaViewModel();
            model.Plist_auditoria_observacion = new AuditoriaExpedienteDataAccess().sp_Listar_Auditoria_Observaciones_por_ActividadesId(obs_act_id).ToPagedList(page, 3);
            Session["obsid"] = obs_act_id;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listar_Auditoria_Observacion", model);
            }
            return View(model);
        }
    }
}