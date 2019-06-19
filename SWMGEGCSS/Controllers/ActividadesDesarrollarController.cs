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
    public class ActividadesDesarrollarController : Controller
    {
        // GET: ActividadesDesarrollar
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult RegistrarAsignacionActividades_Desarrollar(int act_desa_id, int page = 1)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.actividadesDesarrollarAux = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux().Find(X => (X.act_desa_id == act_desa_id));
            model.actividadesDesarrollar = new  ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar().Find(X => (X.act_desa_id == act_desa_id));
            model.listDetalleUsuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador_asignacion();
            model.listPagedDetalleUsuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador_asignacion().ToPagedList(page, 4);
            Session["actividadesDesarrollar"] = model.actividadesDesarrollar;
            Session["act_desa_id"] = act_desa_id;

            if ((T_detalle_usuario)Session["detUsuTrabajador"] != null)
            {
                model.usuarioEncargado = (T_detalle_usuario)Session["detUsuTrabajador"];
            }
            
            model.expedienteActDesarrollar = new T_expedientes();
            model.expedienteActDesarrollar = new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(X => (X.exp_id == model.actividadesDesarrollar.exp_id));
            model.actividadesDesarrollar = new T_actividades_desarrollar();
            model.actividadesDesarrollar = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar().Find(X => X.act_desa_id == act_desa_id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaDetalleUsuario", model);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult RegistrarAsignacionActividades_Desarrollar(T_actividades_desarrollar_aux actividadesDesarrollarAux)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.usuarioEncargado = (T_detalle_usuario)Session["detUsuTrabajador"];
           
            int act_desa_id = (int)Session["act_desa_id"];
            var tad = (T_actividades_desarrollar)Session["actividadesDesarrollar"];
            string nombreActividadDesarrollar = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux().Find(X => (X.act_desa_id == act_desa_id)).act_desa_nombre;
            model.actividadesDesarrollar = new T_actividades_desarrollar();
            tad.act_desa_id = act_desa_id;
            tad.act_desa_fecha_inicio = actividadesDesarrollarAux.act_desa_fecha_inicio;
            tad.act_desa_fecha_fin = actividadesDesarrollarAux.act_desa_fecha_fin;
            tad.usu_revisor = (int)Session["login"];
            tad.usu_asignado = model.usuarioEncargado.usu_codigo;
           
            model.actividadesDesarrollar = tad;
            Session["actividadesDesarrollar"] = tad;

            model.actividadesDesarrollarAux = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux().Find(X => (X.act_desa_id == act_desa_id));

            model.auditoriaActividadesDesarrollar = new T_auditoria_actividades_desarrollo();
            model.auditoriaActividadesDesarrollar.act_desa_id = act_desa_id;
            model.auditoriaActividadesDesarrollar.audi_act_revisor = (int)Session["login"];
            model.auditoriaActividadesDesarrollar.audi_act_fecha_inicio = actividadesDesarrollarAux.act_desa_fecha_inicio;
            model.auditoriaActividadesDesarrollar.audi_act_fecha_fin = actividadesDesarrollarAux.act_desa_fecha_fin;
            model.auditoriaActividadesDesarrollar.usu_asignado = model.usuarioEncargado.usu_codigo;
            // model.auditoriaActividadesDesarrollar.audi_act_comentario = null;
            model.detalle_Usuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador_asignacion().Find(X => (X.usu_codigo == model.usuarioEncargado.usu_codigo));
            model.rol_usuario_aux = new ActividadesDesarrollarDataAccess().sp_listar_roles_usuario().Find(X => (X.usu_codigo == model.usuarioEncargado.usu_codigo));

            model.auditoriaActividadesDesarrollar.audi_act_nombre = nombreActividadDesarrollar;
            Session["auditoriaActividades"] = model.auditoriaActividadesDesarrollar;
            return PartialView("_ModalRegistrarAuditoriaActividadDesarrollar", model);

            //falta nombre y comentario

            /*var operationResult2 = new ActividadesDesarrollarDataAccess().sp_registrar_actividades_desarrollar_auditoria(model.auditoriaActividadesDesarrollar);
            var operationResult = new ActividadesDesarrollarDataAccess().sp_actualizar_actividades_Desarrollar_asignacion(model.actividadesDesarrollar);*/
            //return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }
        
        /*modal de registro final de auditoria*/
        [HttpGet]
        public ActionResult _ModalRegistrarAuditoriaActividadDesarrollar()
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
           /* var actividadesDesarrollar = (T_actividades_desarrollar)Session["actividadesDesarrollar"];
            var auditoriaActividades = (T_auditoria_actividades_desarrollo)Session["auditoriaActividades"];
            if (actividadesDesarrollar!= null && auditoriaActividades != null)
            {
                int act_desa_id = (int)Session["act_desa_id"];
                model.actividadesDesarrollar = (T_actividades_desarrollar)Session["actividadesDesarrollar"];
                model.actividadesDesarrollarAux = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux().Find(X => (X.act_desa_id == act_desa_id));
                model.auditoriaActividadesDesarrollar = (T_auditoria_actividades_desarrollo)Session["auditoriaActividades"];
                model.usuarioEncargado = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => X.usu_codigo == model.auditoriaActividadesDesarrollar.usu_asignado);
            }*/
            return View(model);
        }
        /*[HttpPost]
        public ActionResult _ModalRegistrarAuditoriaActividadDesarrollarPost()
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.actividadesDesarrollar = (T_actividades_desarrollar)Session["actividadesDesarrollar"];
            model.actividadesDesarrollarAux = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux().Find(X => (X.act_desa_id == model.actividadesDesarrollar.act_desa_id));
            model.auditoriaActividadesDesarrollar = (T_auditoria_actividades_desarrollo)Session["auditoriaActividades"];

            model.usuarioEncargado = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => X.usu_codigo == model.auditoriaActividadesDesarrollar.usu_asignado);
            return PartialView("_ModalRegistrarAuditoriaActividadDesarrollar", model);
        }*/
        [HttpGet]
        public ActionResult RegistrarAuditoriaActividadDesarrollarFinal(string comentario)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.actividadesDesarrollar = (T_actividades_desarrollar)Session["actividadesDesarrollar"];
            model.auditoriaActividadesDesarrollar = (T_auditoria_actividades_desarrollo)Session["auditoriaActividades"];
            model.auditoriaActividadesDesarrollar.audi_act_comentario = comentario;

            model.usuarioEncargado = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => X.usu_codigo == model.auditoriaActividadesDesarrollar.usu_asignado);
            var operationResult = new ActividadesDesarrollarDataAccess().sp_actualizar_actividades_Desarrollar_asignacion(model.actividadesDesarrollar);
            
            var operationResult2 = new ActividadesDesarrollarDataAccess().sp_registrar_actividades_desarrollar_auditoria(model.auditoriaActividadesDesarrollar);
            return Json(new { data = operationResult2.NewId }, JsonRequestBehavior.AllowGet);
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
        public ActionResult _ListaDetalleUsuario(int usu_codigo, int page = 1)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();           
            model.detalle_Usuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => (X.usu_codigo == usu_codigo));
            Session["detUsuTrabajador"] = model.detalle_Usuario;           
            var act_desa_id = (int)Session["act_desa_id"];
            model.listPagedDetalleUsuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().ToPagedList(page, 4);
            model.actividadesDesarrollar = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar().Find(X => X.act_desa_id == act_desa_id);
            model.usuarioEncargado = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => X.usu_codigo == usu_codigo);
            return PartialView( "_ListaDetalleUsuario", model);
        }

        public ActionResult Evaluar_UsuarioAsignado()
        {
            T_detalle_usuario tdu = new T_detalle_usuario();
            tdu = (T_detalle_usuario)Session["detUsuTrabajador"];
            int cont = 0;
            if (tdu.usu_codigo != 0)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }


        /*ACTUALIZACION DE ASIGNACION DE TAREAS*/
        [HttpGet]
        public ActionResult ActualizarAsignacionActividades_Desarrollar(int act_desa_id)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.actividadesDesarrollarAux = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux().Find(X => (X.act_desa_id == act_desa_id));
            model.actividadesDesarrollar = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar().Find(X => (X.act_desa_id == act_desa_id));
            model.listDetalleUsuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador_asignacion();
            model.usuarioEncargado = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador_asignacion().Find(X => X.usu_codigo == model.actividadesDesarrollar.usu_asignado);
            Session["actividadesDesarrollar"] = model.actividadesDesarrollar;
            Session["act_desa_id"] = act_desa_id;

            //Session["detUsuTrabajador"] = model.usuarioEncargado;
            if ((T_detalle_usuario)Session["detUsuTrabajador"] != null)
            {
                model.usuarioEncargado = (T_detalle_usuario)Session["detUsuTrabajador"];
                Session["detUsuTrabajador"] = model.usuarioEncargado;
            }
            else
            {
                Session["detUsuTrabajador"] = model.usuarioEncargado;
            }

            model.expedienteActDesarrollar = new T_expedientes();
            model.expedienteActDesarrollar = new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(X => (X.exp_id == model.actividadesDesarrollar.exp_id));
            return View(model);
        }
        [HttpPost]
        public ActionResult ActualizarAsignacionActividades_Desarrollar(T_actividades_desarrollar_aux actividadesDesarrollarAux)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.usuarioEncargado = (T_detalle_usuario)Session["detUsuTrabajador"];

            int act_desa_id = (int)Session["act_desa_id"];
            var tad = (T_actividades_desarrollar)Session["actividadesDesarrollar"];
            string nombreActividadDesarrollar = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux().Find(X => (X.act_desa_id == act_desa_id)).act_desa_nombre;
            model.actividadesDesarrollar = new T_actividades_desarrollar();
            tad.act_desa_id = act_desa_id;
            tad.act_desa_fecha_inicio = actividadesDesarrollarAux.act_desa_fecha_inicio;
            tad.act_desa_fecha_fin = actividadesDesarrollarAux.act_desa_fecha_fin;
            tad.usu_revisor = (int)Session["login"];
            tad.usu_asignado = model.usuarioEncargado.usu_codigo;

            model.actividadesDesarrollar = tad;
            Session["actividadesDesarrollar"] = tad;

            model.actividadesDesarrollarAux = new ActividadesDesarrollarDataAccess().sp_Listar_Actividades_Desarrollar_Aux().Find(X => (X.act_desa_id == act_desa_id));

            model.auditoriaActividadesDesarrollar = new T_auditoria_actividades_desarrollo();
            model.auditoriaActividadesDesarrollar.act_desa_id = act_desa_id;
            model.auditoriaActividadesDesarrollar.audi_act_revisor = (int)Session["login"];
            model.auditoriaActividadesDesarrollar.audi_act_fecha_inicio = actividadesDesarrollarAux.act_desa_fecha_inicio;
            model.auditoriaActividadesDesarrollar.audi_act_fecha_fin = actividadesDesarrollarAux.act_desa_fecha_fin;
            model.auditoriaActividadesDesarrollar.usu_asignado = model.usuarioEncargado.usu_codigo;
            // model.auditoriaActividadesDesarrollar.audi_act_comentario = null;
            model.detalle_Usuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => (X.usu_codigo == model.usuarioEncargado.usu_codigo));
            model.rol_usuario_aux = new ActividadesDesarrollarDataAccess().sp_listar_roles_usuario().Find(X => (X.usu_codigo == model.usuarioEncargado.usu_codigo));

            model.auditoriaActividadesDesarrollar.audi_act_nombre = nombreActividadDesarrollar;
            Session["auditoriaActividades"] = model.auditoriaActividadesDesarrollar;
            return PartialView("_ModalActualizarAuditoriaActividadDesarrollar", model);

            //falta nombre y comentario

            /*var operationResult2 = new ActividadesDesarrollarDataAccess().sp_registrar_actividades_desarrollar_auditoria(model.auditoriaActividadesDesarrollar);
            var operationResult = new ActividadesDesarrollarDataAccess().sp_actualizar_actividades_Desarrollar_asignacion(model.actividadesDesarrollar);*/
            //return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ModalActualizarAuditoriaActividadDesarrollar()
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            return View(model);
        }
        [HttpGet]
        public ActionResult _ModalAsignarTrabajadorResponsableMod()
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalAsignarTrabajadorResponsableMod(int usu_codigo)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.detalle_Usuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => (X.usu_codigo == usu_codigo));
            model.rol_usuario_aux = new ActividadesDesarrollarDataAccess().sp_listar_roles_usuario().Find(X => (X.usu_codigo == usu_codigo));
            return PartialView("_ModalAsignarTrabajadorResponsableMod", model);
        }
        [HttpPost]
        public ActionResult AsignarTrabajadorResponsableFinalMod(int usu_codigo)
        {
            var model = new GestionarAsignacionActividadesDesarrollar();
            model.detalle_Usuario = new ActividadesDesarrollarDataAccess().sp_listar_detalle_usuario_trabajador().Find(X => (X.usu_codigo == usu_codigo));
            Session["detUsuTrabajador"] = model.detalle_Usuario;
            var act_desa_id = (int)Session["act_desa_id"];
            return Json(act_desa_id, JsonRequestBehavior.AllowGet);
        }
    }

}