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
        public ActionResult Gestionar_Proyectos(string searchTerm,string estado, int page = 1)
        {
           
            ExpedienteViewModel model = new ExpedienteViewModel();
            if (searchTerm == null&&estado==null)
                model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().ToPagedList(page, 2);
            if (estado != null)
            {
                if (searchTerm != null)
                {
                    if (estado.Equals("Todos"))
                    {
                        model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(searchTerm).ToPagedList(page, 2);
                    }
                    else
                    {
                        model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(searchTerm).FindAll(r => (r.est_exp_nombre == estado)).ToPagedList(page, 2);
                    }
                   
                }
            }
            if (estado != null)
            {
                model.tipo_estado = estado;
                Session["est_nombre"] = model.tipo_estado;
            }

            else
            {
                model.tipo_estado = "Todos";
                Session["est_nombre"] = model.tipo_estado;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaProyecto", model);
            }
            model.List_Estado_Expediente = new EstadoExpedienteDataAccess().sp_Consultar_Lista_Estado_Expediente();           
            return View(model);
        }
        public ActionResult Gestionar_I_E()
        {
            return View();
        }
        
        public ActionResult Visualizar_Personal_Proyecto()
        {
            return View();
        }
        public ActionResult Gestionar_Plan_Proyecto(string searchTerm, string estado, int page = 1)
        { 
            GestionarPlanProyectoViewModel model = new GestionarPlanProyectoViewModel();
            if (searchTerm == null && estado == null) { model.listPplans = new PlanDataAccess().sp_Consultar_Lista_Plan().ToPagedList(page, 3); }
            if (estado != null)
            {
                if (searchTerm != null)
                {
                    if (estado.Equals("Todos"))
                    {
                        model.listPplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(searchTerm).ToPagedList(page, 3);
                    }
                    else
                    {
                        model.listPplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(searchTerm).FindAll(r => (r.plan_estado_nobre == estado)).ToPagedList(page, 3);
                    }
                }
            }
            if (estado != null)
            {
                model.tipo_estado = estado;
                Session["est_plan"] = model.tipo_estado;
            }
            else
            {
                model.tipo_estado = "Todos";
                Session["est_plan"] = model.tipo_estado;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaPlan", model);
            }
            model.List_Estado_Plan = new EstadoPlanDataAccess().sp_Consultar_Lista_Estado_Plan();
            return View(model);
        }
        public ActionResult AutoComplete(string term)
        {
            var model = new ExpedienteViewModel();
            string estado = (string)Session["est_nombre"];
            if (estado.Equals("Todos"))
            {
                model.List_Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(term);
            }
            else
            {
                model.List_Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(term).FindAll(r => (r.est_exp_nombre == estado));
            }

            var nameExpedientes = model.List_Expediente.Select(r => new
            {
                label = r.exp_nombre
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CompletarNombrePlanes(string term)
        {
            var model = new GestionarPlanProyectoViewModel();
            string estado = (string)Session["est_plan"];
            if (estado.Equals("Todos"))
            {
                model.listplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(term);
            }
            else
            {
                model.listplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(term).FindAll(r => (r.plan_estado_nobre == estado));
            }
            var namePlan = model.listplans.Select(r => new
            {
                label = r.plan_nombre
            });
            return Json(namePlan, JsonRequestBehavior.AllowGet);
        }

       
    }

}


















































































































































































































































































































































