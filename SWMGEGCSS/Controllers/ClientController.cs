using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWMGEGCSS.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        int X = 5;
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
            int Y = X + 1;
            return View();
        }
        //piero estuvo aqui  se la come entera
        //se la come entera
        //ALV
    }
}