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
        public ActionResult Gestionar_Empresas(string searchTerm, string estado, int page = 1)
        {
            GestionarEmpresaViewModel model = new GestionarEmpresaViewModel();
            if (searchTerm == null && estado == null)
                // model.listEmpresas = new EmpresaDataAccessDataAccess().sp_Consultar_Lista_Proyectos().ToPagedList(page, 2);
                model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).ToPagedList(page, 2);
            if (estado != null)
            {
                if (searchTerm != null)
                {
                    if (estado.Equals("Todos"))
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).ToPagedList(page, 2);
                    }
                    else
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).FindAll(r => (r.emp_estado == Convert.ToBoolean(estado))).ToPagedList(page, 2);
                    }

                }
            }
            if (estado != null)
            {
                model.empresas.emp_estado = Convert.ToBoolean(estado);
                Session["est_razon_social"] = model.empresas.emp_estado;
            }

            else
            {
                model.empresas.emp_estado = true;
                Session["est_razon_social"] = model.empresas.emp_estado;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaProyecto", model);
            }
            model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).ToPagedList(page, 4);
            return View(model);
        }

        public ActionResult AutoCompleteEmpresa(string term)
        {
            var model = new GestionarEmpresaViewModel();
            string estado = (string)Session["est_razon_social"];
            if (estado.Equals("Todos"))
            {
                model.listempresas= new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term);
            }
            else
            {
                model.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term).FindAll(r => (r.emp_estado == Convert.ToBoolean(estado)));
            }

            var nameExpedientes = model.listempresas.Select(r => new
            {
                label = r.emp_razon_social
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Visualizar_Personal_Proyecto()
        {
            return View();
        }
        public ActionResult Gestionar_Plan_Proyecto(int page = 1)
        { 
            GestionarPlanProyectoViewModel model = new GestionarPlanProyectoViewModel();
            model.listPplans = new PlanDataAccess().sp_Consultar_Lista_Plan().ToPagedList(page, 3);
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
            model.listplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(term);
            var nameExpedientes = model.listplans.Select(r => new
            {
                label = r.plan_nombre
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }
    }
}