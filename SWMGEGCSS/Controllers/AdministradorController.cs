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
        [HttpGet]
        public ActionResult Visualizar_Trabajadores(int page = 1)
        {
            var model = new VisualizarTrabajadoresViewModel();
            model.IPlist_actividades_usuario = new ActividadesUsuarioDataAccess().sp_Consultar_Lista_Actividades_Usuario_Paged().ToPagedList(page, 4);
            return View(model);
        }
        //Aquí prueba
        [HttpGet]
        public ActionResult Visualizar_Actividades(int page = 1)
        {
            var model = new VisualizarTrabajadoresViewModel();
            model.IPlist_actividades_usuario = new ActividadesUsuarioDataAccess().sp_Consultar_Lista_Actividades_Usuario().ToPagedList(page, 4);
            return View(model);
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

        [HttpGet]
        public ActionResult _verDetalles()
        {
            int page = 1;
            var model = new VisualizarTrabajadoresViewModel();
            model.actividades_usuario = new T_actividades_desarrollar_usuarios();
            model.IPlist_actividades_usuario = new ActividadesUsuarioDataAccess().sp_Consultar_Lista_Actividades_Usuario().ToPagedList(page, 4);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _verDetalles(int usu_codigo)
        {
            int page = 1;
            var model = new VisualizarTrabajadoresViewModel();
            model.actividades_usuario = new ActividadesUsuarioDataAccess().sp_Consultar_Lista_Actividades_Usuario().Find(r => r.usu_codigo == usu_codigo);
            model.IPlist_actividades_usuario = new ActividadesUsuarioDataAccess().sp_Consultar_Lista_Actividades_Usuario().ToPagedList(page, 4);
            return PartialView(model);
        }
    }
}