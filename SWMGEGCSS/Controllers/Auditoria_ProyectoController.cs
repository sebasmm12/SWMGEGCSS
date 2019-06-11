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
            /*AuditoriaViewModel model = new AuditoriaViewModel();
            model.Plist_auditoria_expediente = 
            return View(model);*/
            AuditoriaViewModel model = new AuditoriaViewModel();
            model.Plist_aux_auditoria_expediente = new AuditoriaExpedienteDataAccess().sp_Listar_Aux_Expediente_por_ExpedienteId(exp_id).ToPagedList(page, 3);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Lista_Auditoria_Proyecto", model);
            }
            return View(model);
        }
    }
}