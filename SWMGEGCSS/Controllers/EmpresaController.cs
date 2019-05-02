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
            model.empresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().Find(r => r.emp_id == emp_id);
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
            model.empresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().Find(r => r.emp_id == emp_id);
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












































































        public ActionResult Gestionar_Empresas(string searchTerm, string estado, int page = 1)
        {
            GestionarEmpresaViewModel model = new GestionarEmpresaViewModel();
            if (searchTerm == null && estado == null)
                model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).ToPagedList(page, 2);
            if (estado != null)
            {
                if (searchTerm != null)
                {
                    if (estado.Equals("Todos"))
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).ToPagedList(page, 2);
                    }
                    else
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).FindAll(r => (r.emp_estado == Convert.ToBoolean(estado))).ToPagedList(page, 2);
                    }
                }
            }
            if (estado != null)
            {
                model.tipo_estado = estado;
                Session["est_razon_social"] = model.tipo_estado;
            }

            else
            {
                model.tipo_estado = "Todos";
                Session["est_razon_social"] = model.tipo_estado;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaEmpresa", model);
            }
            model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().ToPagedList(page, 4);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaEmpresa", model);
            }
            return View(model);
        }

        public ActionResult AutoCompleteEmpresa(string term)
        {
            var model = new GestionarEmpresaViewModel();
            string estado = (string)Session["est_razon_social"];
            /*if (estado.Equals("Todos"))
            {
                model.listempresas= new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term);
            }
            else
            {
                model.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term).FindAll(r => (r.emp_estado == Convert.ToBoolean(estado)));
            }*/
            model.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term);

            var nameExpedientes = model.listempresas.Select(r => new
            {
                label = r.emp_razon_social
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }

    }
}