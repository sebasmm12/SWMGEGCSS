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
    public class GerenteController : Controller
    {
        // GET: Gerente
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Consultar_Capital()
        {
            return View();
        }
        public ActionResult Generar_Reportes()
        {
            return View();
        }
        public ActionResult Gestionar_Formularios()
        {
            return View();
        }
        public ActionResult Gestionar_Servicios()
        {
            return View();
        }
        public ActionResult Gestionar_Cuenta()
        {
            return View();
        }
        public ActionResult Gestionar_Proyectos(int page=1)
        {
            ExpedienteViewModel model = new ExpedienteViewModel();
            model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().ToPagedList(page,2);
            return View(model);
        }
        public ActionResult Gestionar_I_E()
        {
            return View();
        }
        public ActionResult Gestionar_Empresas()
        {
            return View();
        }
        public ActionResult Visualizar_Personal_Proyecto()
        {
            return View();
        }
        public ActionResult Gestionar_Plan_Proyecto(int page = 1)
        {
            /*ExpedienteViewModel model = new ExpedienteViewModel();
            model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Plan().ToPagedList(page, 2);*/
            return View(/*model*/);
        }
        public ActionResult AutoComplete(string term)
        {
            var model = new ExpedienteViewModel();
            model.List_Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(term);
            var nameExpedientes = model.List_Expediente.Select(r => new
            {
                label = r.exp_nombre
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);

        }
    }
}