using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using System.Globalization;
using PagedList;

namespace SWMGEGCSS.Controllers
{
    public class Ing_EgrController : Controller
    {
        // GET: Ing_Egr
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Actualizar_Ing_Egr(int ing_egr_id)
        {
            var model = new Gestionar_I_EViewModel();
            model.ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr().Find(r => r.ing_egr_id == ing_egr_id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Actualizar_Ing_Egr(T_ingresos_egresos ingresos_egresos)
        {
            var model = new Gestionar_I_EViewModel();
            model.ingresos_egresos = ingresos_egresos;
            model.ingresos_egresos.usu_codigo = (int)Session["login"];
            var operationResult = new Ing_EgrDataAccess().sp_Actualizar_Ing_Egr(model.ingresos_egresos);
            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult Registrar_Ing_Egr()
        {
            var model = new Gestionar_I_EViewModel();
            return View();
        }
        [HttpPost]
        public ActionResult Registrar_Ing_Egr(T_ingresos_egresos ingresos_egresos)
        {
            var model = new Gestionar_I_EViewModel();
            model.ingresos_egresos = ingresos_egresos;
            model.ingresos_egresos.usu_codigo = (int)Session["login"];
            var operationResult = new Ing_EgrDataAccess().sp_Insertar_Ing_Egr(model.ingresos_egresos);
            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }
    }
}