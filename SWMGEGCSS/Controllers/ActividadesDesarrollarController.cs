using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using System.Web.Mvc;

namespace SWMGEGCSS.Controllers
{
    public class ActividadesDesarrollarController : Controller
    {
        // GET: ActividadesDesarrollar
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RegistrarAsignacionActividades_Desarrollar(int act_desa_id)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.actividadesDesarrollarAux = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux().Find(X => (X.act_desa_id == act_desa_id));
            model.actividadesDesarrollar = new  ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar().Find(X => (X.act_desa_id == act_desa_id));
            model.listDetalleUsuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador();
            model.expedienteActDesarrollar = new T_expedientes();
            model.expedienteActDesarrollar = new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(X => (X.exp_id == model.actividadesDesarrollar.exp_id));
            return View(model);
        }
        [HttpPost]
        public ActionResult RegistrarAsignacionActividades_Desarrollar(T_actividades_desarrollar act_desa )
        {
            return View();
        }
        [HttpGet]
        public ActionResult _ModalAsignarTrabajadorResponsable()
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalAsignarTrabajadorResponsable(int usu_codigo)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.detalle_Usuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => (X.usu_codigo == usu_codigo));
            var rolTrabajador = new ActividadesDesarrollarDataAccess().sp_listar_roles_usuario
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult AsignarTrabajadorResponsableFinal()
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            return PartialView(model);
        }
    }
    
}