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
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Eliminar_Empresa()
        {
            


            return View();
        }
        public ActionResult Actualizar_Empresa()
        {
            return View();
        }
        public ActionResult _Registrar_Empresa()
        {
            return View();
        }
    }
}