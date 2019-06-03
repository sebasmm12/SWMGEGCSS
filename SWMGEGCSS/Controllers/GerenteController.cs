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
            return RedirectToAction("Index", "EvaluarFormulario"); ;
        }
        public ActionResult Gestionar_Servicios()
        {
            return RedirectToAction("VisualizarServicios", "Servicios");
        }
        public ActionResult Gestionar_Cuenta(int page = 1)
        {
            var model = new GestionarCuentasViewModel();
            model.Plist_cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Cuentas().ToPagedList(page, 4);
            return View(model);
        }

        public ActionResult Gestionar_Proyectos(string searchTerm,string estado, int page = 1)
        {
            ExpedienteViewModel model = new ExpedienteViewModel();
            if (searchTerm == null&&estado==null)
                model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().ToPagedList(page, 3);
            if (estado != null)
            {
                if (searchTerm != null)
                {
                    if (estado.Equals("Todos"))
                    {
                        model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(searchTerm).ToPagedList(page, 3);
                    }
                    else
                    {
                        model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(searchTerm).FindAll(r => (r.est_exp_nombre == estado)).ToPagedList(page, 3);
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
        public ActionResult Gestionar_I_E(string searchTerm, string estado, int page = 1)
        {
            /* Gestionar_I_EViewModel model = new Gestionar_I_EViewModel();
             model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr().ToPagedList(page, 4);

             return View(model);*/

            Gestionar_I_EViewModel model = new Gestionar_I_EViewModel();

            if (searchTerm == null && estado == null)
            {
                model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr().ToPagedList(page, 4);
            }
            if (estado != null)
            {
                if (searchTerm != null && searchTerm == "")
                {
                    if (estado.Equals("Todos"))
                    {
                        model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr().ToPagedList(page, 4);
                    }
                    else if(estado.Equals("1"))
                    {
                        model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr().FindAll(r => r.ing_egr_ingrso = (estado.Equals("1"))).ToPagedList(page, 4);
                    } else 
                    {
                        model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr().FindAll(r => r.ing_egr_ingrso = (estado.Equals("0"))).ToPagedList(page, 4);
                    }
                }
                else
                {
                    if (estado.Equals("Todos"))
                    {
                        model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr_Nombre(searchTerm).ToPagedList(page, 4);
                    }
                    else if (estado.Equals("1"))
                    {
                        model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr_Nombre(searchTerm).FindAll(r => r.ing_egr_ingrso = (estado.Equals("1"))).ToPagedList(page, 4);
                    }
                    else
                    {
                        model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr_Nombre(searchTerm).FindAll(r => r.ing_egr_ingrso = (estado.Equals("0"))).ToPagedList(page, 4);
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
                return PartialView("_ListaIng_Egr", model);
            }
            return View(model);
        }
        public ActionResult Gestionar_Empresas(string searchTerm, string estado, int page = 1)
        {
            GestionarEmpresaViewModel model = new GestionarEmpresaViewModel();

            if (searchTerm == null && estado == null)
            {
                model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().ToPagedList(page, 4);
            }
            if (estado != null)
            {
                if (searchTerm != null && searchTerm =="")
                {
                    if (estado.Equals("Todos"))
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().ToPagedList(page, 4);
                    }
                    else
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().FindAll(r => r.emp_estado == (estado.Equals("1"))).ToPagedList(page, 4);
                    }
                }
                else
                {
                    if (estado.Equals("Todos"))
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).ToPagedList(page, 4);
                    }
                    else
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).FindAll(r => r.emp_estado == (estado.Equals("1"))).ToPagedList(page, 4);
                    }


                }
            }
            if (estado != null)
            {
                model.tipo_estado = estado;
                Session["estado"] = model.tipo_estado;
            }
            else
            {
                model.tipo_estado = "Todos";
                Session["estado"] = model.tipo_estado;
            }

           
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaEmpresa", model);
            }
            return View(model);
        }
        public ActionResult Visualizar_Personal_Proyecto()
        {
            return View();
        }
        public ActionResult Gestionar_Plan_Proyecto(string searchTerm, string estado, int page = 1)
        {
            Session["ListaActPlanTemp"] = null;
            GestionarPlanProyectoViewModel model = new GestionarPlanProyectoViewModel();
            if (searchTerm == null && estado == null) {
                model.listPplans = new PlanDataAccess().sp_Consultar_Lista_Plan().ToPagedList(page, 3); }
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
            model.List_Estado_Plan = new EstadoPlanDataAccess().sp_Consultar_Lista_Estado_Plan();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaPlan", model);
            }
            return View(model);
        }

        public ActionResult AutoCompleteEmpresa(string term)
        {
            var model = new GestionarEmpresaViewModel();
            string estado = (string)Session["estado"];
            if (estado.Equals("Todos"))
            {
                model.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term);
            }
             else if(estado.Equals("1"))
            {
                model.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term).FindAll(r => (r.emp_estado == estado.Equals(1)));
            }
            else
            {
                model.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term).FindAll(r => (r.emp_estado == estado.Equals(0)));
            }
            

            var nameExpedientes = model.listempresas.Select(r => new
            {
                label = r.emp_razon_social
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoCompleteING(string term)
        {
            var model = new Gestionar_I_EViewModel();
            string estado = (string)Session["est_nombre"];

            model.lista_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr_Nombre(term);

            var nameExpedientes = model.lista_ingresos_egresos.Select(r => new
            {
                label = r.ing_egr_nombre
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
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