using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using PagedList;
using SWMGEGCSS.Models;

namespace SWMGEGCSS.Controllers
{
    public class CuentasController : Controller
    {
        // GET: Cuentas
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registrar_Cuenta()
        {
            var model = new GestionarCuentasViewModel();
            return View(model);
        }
    }
}