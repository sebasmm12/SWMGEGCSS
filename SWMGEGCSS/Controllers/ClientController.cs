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
    }
}