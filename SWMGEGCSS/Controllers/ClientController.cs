using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWMGEGCSS.Models;
using SWMGEGCSS_EN;
namespace SWMGEGCSS.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Quienes_Somos()
        {
            return View();
        }
        public ActionResult Clientes()
        {
            return View();
        }
        public ActionResult Servicios()
        {
            return View();
        }
        public ActionResult Contactenos()
        {
             SolicitudViewModel solicitud=new SolicitudViewModel();
            return View(solicitud);
        }
        [HttpPost]
        public ActionResult Contactenos(T_Solicitud Solicitud)
        {
            string m = Solicitud.mensaje;
            return View();
        }
        public ActionResult Enlace_Interes()
        {
            return View();
        }
        public ActionResult Normatividad()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertarSolicitud(T_Solicitud Solicitud)
        {
            var operationResult = new OperationResult() ;
            T_notificaciones notificacion = new T_notificaciones();
            notificacion.usu_envio = 0;
            notificacion.usu_codigo = 1;
            notificacion.not_nombre = Solicitud.asunto;
            notificacion.not_descripcion = Solicitud.mensaje+"Mi nombre de contacto es: "+Solicitud.names+
                " y mi correo electrónico "+Solicitud.correoE;
            notificacion.not_url = "Gestionar_Citas";
            operationResult = new NotificacionesDataAccess().sp_Insertar_Notificaciones(notificacion);
            return Json(operationResult.NewId, JsonRequestBehavior.AllowGet);
        }
    }
}