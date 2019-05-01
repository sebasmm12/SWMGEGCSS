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





        [HttpGet]
        public ActionResult Actualizar_Empresa(int emp_id)
        {
            var model = new GestionarEmpresaViewModel();
            model.empresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresa().Find(r => r.emp_id == emp_id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Actualizar_Empresa(T_empresa empresas)
        {
            var model = new GestionarEmpresaViewModel();
            model.empresas = empresas;
            model.empresas.usu_codigo = (int)Session["login"];
            var operationResult = new OperationResult();
            operationResult = new EmpresaDataAccess().sp_Actualizar_Empresa(model.empresas);
            return View();

        }

               [HttpGet]
        public ActionResult _Eliminar_Empresa(int emp_id)
        {
            var model = new GestionarEmpresaViewModel();
            model.empresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresa().Find(r => r.emp_id == emp_id);
                return View(model);
        }
        [HttpPost]
        public ActionResult _Eliminar_Empresa(T_empresa empresas)
        {
            var model = new GestionarEmpresaViewModel();
            model.empresas = empresas;
            model.empresas.usu_codigo = (int)Session["login"];
            var operationResult = new OperationResult();
            operationResult = new EmpresaDataAccess().sp_Eliminar_Empresa(model.empresas);
            return View();
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
            return View();
        }


        
       
    }
}