using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
namespace SWMGEGCSS.Controllers
{
    public class TrabajadorController : Controller
    {
        // GET: Trabajador
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase imagen, int p)
        {
            if (imagen != null && imagen.ContentLength > 0)
            {
                byte[] imagenData = null;
                using (var binaryImagen = new BinaryReader(imagen.InputStream))
                {
                    imagenData = binaryImagen.ReadBytes(imagen.ContentLength);
                }
                Imagen imagenes = new Imagen();
                imagenes.Imagenes = imagenData;
                T_detalle_usuario t = new T_detalle_usuario();
                t.usu_codigo = p;
                t.det_usu_imagem = imagenData;
                var operationResult = new OperationResult();
                operationResult = new TrabajadorDataAccess().sp_Insertar_Imagen_Usuario(t);
            }
            return View();
        }
        public ActionResult G_Formulario()
        {
            return View();
        }
        public ActionResult V_Tareas(int page = 1)
        {
            TareasAsignadasViewModel model = new TareasAsignadasViewModel();
            int Usuario = (int)Session["login"];
            model.PLista_Actividades_a_Desarrollar = new TrabajadorDataAccess().sp_listar_plan_por_usuario_asignado(Usuario).ToPagedList(page, 4);
            return View(model);
        }
        [HttpGet]
        public ActionResult AgregarArchivo(int id)
        {
            TareasAsignadasViewModel model = new TareasAsignadasViewModel();
            Session["ArchivoId"] = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult AgregarArchivo(HttpPostedFileBase archivo,String act_desa_comentario)
        {
            var valores = new TrabajadorDataAccess().sp_Consultar_Ruc_Plan_por_Act_Desa((int)Session["ArchivoId"]);
            if (archivo != null)
            {
                String ruta = Server.MapPath("~/"+ valores.emp_ruc + "/" + valores.plan_id.ToString() + "/");
                String nombreArchivo = archivo.FileName;
                ruta += nombreArchivo;
                subirArchivo(archivo,ruta);
                
                var modelAct = new T_actividades_desarrollar();
                modelAct.act_desa_id = (int)Session["ArchivoId"];
                modelAct.act_desa_archivo_nombre = nombreArchivo;
                modelAct.act_desa_archivourl = ruta;
                modelAct.act_desa_comentario = act_desa_comentario;

                var operationResult = new TrabajadorDataAccess().sp_Agregar_Tarea_Asignada(modelAct);
                Session["ArchivoId"] = null;

                return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
        }
        public void subirArchivo(HttpPostedFileBase archivo,String ruta)
        {
            try
            {
                archivo.SaveAs(ruta);
            }
            catch (Exception ez)
            {

            }
        }
    }
}