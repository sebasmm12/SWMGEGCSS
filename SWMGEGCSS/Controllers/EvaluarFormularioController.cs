using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;using System.Web.Mvc;
using SWMGEGCSS_DA;
using PagedList;
using SWMGEGCSS_EN;
using SWMGEGCSS.Models;

namespace SWMGEGCSS.Controllers
{
    public class EvaluarFormularioController : Controller
    {
        // GET: EvaluarFormulario
        public ActionResult Index(int page = 1)
        {

            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            model.Lista_Paginada_actividades_desarrollar = new ActividadesDesarrollarDataAccess().sp_Consultar_Lista_Actividades_Desarrollar_Revisar_Gerente(Int32.Parse(Session["login"].ToString())).ToPagedList(page, 10);

            return View(model);
        }
        /*

        public ActionResult ListaParcialIndex(int page)
        {
            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            model.Lista_Paginada_actividades_desarrollar = new ActividadesDesarrollarDataAccess().sp_Consultar_Lista_Actividades_Desarrollar_Revisar_Gerente(Int32.Parse(Session["login"].ToString())).ToPagedList(page, 1);
            return PartialView(model);
        }*/
        [HttpGet]
        public ActionResult RevisarFormulario(int id)
        {
            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            model.actividades_desarrollarGerente = new ActividadesDesarrollarDataAccess().sp_Consultar_Actividad_Desarrollar_Gerente(id);
            Session["ArchivoId"] = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult RevisarFormulario(T_actividades_desarrollar_gerente actividades_desarrollarGerente)
        {
            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            var operationResult = new OperationResult();
            actividades_desarrollarGerente.est_act_id = 5;
            operationResult = new ActividadesDesarrollarDataAccess().sp_Actualizar_Actividades_Desarrollar_Gerente(actividades_desarrollarGerente);

            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RevisarFormulario2(T_actividades_desarrollar_gerente actividades_desarrollarGerente)
        {
            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            var operationResult = new OperationResult();
            actividades_desarrollarGerente.est_act_id = 6;
            operationResult = new ActividadesDesarrollarDataAccess().sp_Actualizar_Actividades_Desarrollar_Gerente(actividades_desarrollarGerente);

            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }



        public FileResult DescargarArchivo()
        {
            var valores = new TrabajadorDataAccess().sp_Consultar_Ruc_Plan_por_Act_Desa((int)Session["ArchivoId"]);
            string nombre = valores.act_desa_archivo_nombre;
            String rut = Server.MapPath("~/Repositorio/" + valores.emp_ruc + "/" + valores.plan_id.ToString() + "/" + nombre);
            String[] lista = nombre.Split('.');
            string extension = lista[lista.Length - 1];
            string tipo = "";
            switch (extension)
            {
                case "txt":
                    tipo = "application/txt";
                    break;
                case "pdf":
                    tipo = "application/pdf";
                    break;
                case "xls":
                    tipo = "application/vnd.ms-excel";
                    break;
                case "rar":
                    tipo = "application/x-rar-compressed";
                    break;
                case "zip":
                    tipo = "application/zip";
                    break;
                case "doc":
                    tipo = "application/msword";
                    break;
                case "xlsx":
                    tipo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case "docx":
                    tipo = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                default:
                    tipo = "application/octet-stream";
                    break;
            }
            return File(rut, tipo, valores.act_desa_archivo_nombre);
        }
    }
}