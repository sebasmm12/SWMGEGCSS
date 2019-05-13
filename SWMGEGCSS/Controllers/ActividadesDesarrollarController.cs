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
            Session["act_desa_id"] = act_desa_id;

            if ((T_detalle_usuario)Session["detUsuTrabajador"] != null)
            {
                model.usuarioEncargado = (T_detalle_usuario)Session["detUsuTrabajador"];
            }
            if ((T_detalle_usuario)Session["detUsuRevisor"] != null)
            {
                model.usuarioRevisor = (T_detalle_usuario)Session["detUsuRevisor"];
            }
            model.expedienteActDesarrollar = new T_expedientes();
            model.expedienteActDesarrollar = new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(X => (X.exp_id == model.actividadesDesarrollar.exp_id));
            return View(model);
        }
        [HttpPost]
        public ActionResult RegistrarAsignacionActividades_Desarrollar(T_actividades_desarrollar act_desa )
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.usuarioEncargado = (T_detalle_usuario)Session["detUsuTrabajador"];
            model.usuarioRevisor = (T_detalle_usuario)Session["detUsuRevisor"];

            model.actividadesDesarrollar.act_desa_id = act_desa.act_desa_id;
            model.actividadesDesarrollar.act_desa_fecha_inicio = act_desa.act_desa_fecha_inicio;
            model.actividadesDesarrollar.act_desa_fecha_inicio = act_desa.act_desa_fecha_fin;
            model.actividadesDesarrollar.usu_revisor = model.usuarioRevisor.usu_codigo;
            model.actividadesDesarrollar.usu_asignado = model.usuarioEncargado.usu_codigo;
            var operationResult = new ActividadesDesarrollarDataAccess().sp_actualizar_actividades_Desarrollar_asignacion(model.actividadesDesarrollar);
            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
            
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
            model.rol_usuario_aux = new ActividadesDesarrollarDataAccess().sp_listar_roles_usuario().Find(X => (X.usu_codigo == usu_codigo));
            return PartialView("_ModalAsignarTrabajadorResponsable", model);
        }
        [HttpPost]
        public ActionResult AsignarTrabajadorResponsableFinal(int usu_codigo)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            var detusuTrabajador = (T_detalle_usuario)Session["detUsuTrabajador"];
            var detusuRevisor = (T_detalle_usuario)Session["detUsuRevisor"];
            if (detusuTrabajador == null)
            {
                model.detalle_Usuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => (X.usu_codigo == usu_codigo));
                Session["detUsuTrabajador"] = model.detalle_Usuario;
            }
            else
            {
                if(detusuRevisor == null)
                {
                    model.detalle_Usuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => (X.usu_codigo == usu_codigo));
                    Session["detUsuRevisor"] = model.detalle_Usuario;
                }
            }
            var act_desa_id = (int)Session["act_desa_id"];
            return Json(act_desa_id, JsonRequestBehavior.AllowGet);
        }
    }
    
}