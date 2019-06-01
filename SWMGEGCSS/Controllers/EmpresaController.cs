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
using System.IO;

namespace SWMGEGCSS.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Actualizar_Empresa(string emp_ruc)
        {
            var model = new GestionarEmpresaViewModel();
            model.empresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().Find(r => r.emp_ruc == emp_ruc);
            return View(model);
        }
        [HttpPost]
        public ActionResult Actualizar_Empresa(T_empresa empresas)
        {
            var model = new GestionarEmpresaViewModel();
            model.empresas = empresas;
            model.empresas.usu_codigo = (int)Session["login"];
           
           var operationResult = new EmpresaDataAccess().sp_Actualizar_Empresa(model.empresas);
            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult _EliminarEmpresa()
        {
            var model = new GestionarEmpresaViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _EliminarEmpresa(string emp_ruc)
        {
            var model = new GestionarEmpresaViewModel();
            model.empresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().Find(r => r.emp_ruc == emp_ruc);
            return PartialView(model); 
        }

        [HttpPost]
        public ActionResult RecibeEmpresa(T_empresa empresas)
        {
            var model = new GestionarEmpresaViewModel();
            model.empresas = empresas;
            model.empresas.usu_codigo = (int)Session["login"];
            var operationResult = new OperationResult();
            operationResult = new EmpresaDataAccess().sp_Eliminar_Empresa(model.empresas);
            return RedirectToAction("Gestionar_Empresas", "Gerente");
        }



        [HttpGet]
        public ActionResult Registrar_Empresa()
        {
            var model = new GestionarEmpresaViewModel();
            return View();
        }
        [HttpPost]
        public ActionResult Registrar_Empresa(T_empresa empresas)
        {
            var model = new GestionarEmpresaViewModel();
            model.empresas = empresas;
            model.empresas.usu_codigo = (int)Session["login"];
            var operationResult = new OperationResult();
            operationResult = new EmpresaDataAccess().sp_Insertar_Empresa(model.empresas);
            if (operationResult.NewId == 1)
            {
                String ruta = Server.MapPath("~/Repositorio/" + empresas.emp_ruc);
                Directory.CreateDirectory(ruta);
            }
            
            return RedirectToAction("Gestionar_Empresas", "Gerente");
        }

        public ActionResult Evaluar_Nombre_Empresa(string emp_razon_social)
        {
            var model = new EmpresaDataAccess().sp_Consultar_Lista_Empresas();
            var modelEvaluado = model.Find(r => r.emp_razon_social == emp_razon_social);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Sigla_Empresa(string emp_sigla)
        {
            var model = new EmpresaDataAccess().sp_Consultar_Lista_Empresas();
            var modelEvaluado = model.Find(r => r.emp_sigla == emp_sigla);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Ruc_Empresa(string emp_ruc)
        {
            var model = new EmpresaDataAccess().sp_Consultar_Lista_Empresas();
            var modelEvaluado = model.Find(r => r.emp_ruc == emp_ruc);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }


        

      
    }
}