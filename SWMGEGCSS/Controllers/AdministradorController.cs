using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Asignacion_Tareas()
        {
            return View();
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