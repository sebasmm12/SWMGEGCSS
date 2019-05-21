using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using System.Web.Mvc;
using PagedList;

namespace SWMGEGCSS.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Gestionar_Historial()
        {
            return View();
        }
        public ActionResult Generar_Reportes_Desempeño()
        {
            return View();
        }
        public ActionResult Visualizar_Trabajadores()
        {
            return View();
        }
        public ActionResult Asignacion_Tareas(int page = 1)
        {
            Session["detUsuTrabajador"] = null;
            Session["act_desa_id"] = null;
            Session["detUsuRevisor"] = null;
            Session["auditoriaActividades"] = null;
            Session["actividadesDesarrollar"] = null;
            GestionarAsignacionActividadesDesarrollar model = new GestionarAsignacionActividadesDesarrollar();
            //model.listActividadesDesarrollarAux = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux();
           
            model.listPagedActividadesDesarrollarAux = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux().ToPagedList(page, 4);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaActividadesDesarrollar", model);
            }
            return View(model);
        }
        public ActionResult Modificar_I_E()
        {
            return View();
        }
        public ActionResult Modificar_Cuenta()
        {
            return View();
        }
    }
}