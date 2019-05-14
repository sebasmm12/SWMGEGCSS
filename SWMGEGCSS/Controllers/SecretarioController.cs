using SWMGEGCSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using PagedList;

namespace SWMGEGCSS.Controllers
{
    public class SecretarioController : Controller
    {
        // GET: Secretario
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Gestionar_Citas(int page = 1)
        {
            GestionarCitasViewModel model = new GestionarCitasViewModel();
            model.listCitas = new SecretariaDataAccess().sp_Consultar_Lista_Citas().ToPagedList(page, 4);
            return View(model);
        }


        [HttpGet]
        public ActionResult Registrar_Cita()
        {
            var model = new GestionarCitasViewModel();
            return View();
        }

        [HttpPost]
        public ActionResult Registrar_Cita(T_Citas citas)
        {
            var model = new GestionarCitasViewModel();
            model.citas = citas;
            model.citas.cita_id = (int)Session["login"];
            //var operationResult = new OperationResult();
            //operationResult = new SecretariaDataAccess().sp_Consultar_Lista_Citas(model.citas);
            return View(model);
        }

        [HttpGet]
        public ActionResult Modificar_Cita(int cita_id)
        {
            var model = new GestionarCitasViewModel();
            return View();
        }
        
        [HttpPost]
        public ActionResult Modificar_Cita(T_Citas cita)
        {
            var model = new GestionarCitasViewModel();
            model.citas = cita;
            model.citas.cita_id = (int)Session["login"];
            return View();
        }

        
        public ActionResult Registrar_Ingresos_Egresos()
        {
            return View();
        }
        public ActionResult Visualizar_I_E()
        {
            return View();
        }
        public ActionResult Visualizar_Cuenta()
        {
            return View();
        }
        public ActionResult Registrar_Cuenta()
        {
            return View();
        }


    }
}