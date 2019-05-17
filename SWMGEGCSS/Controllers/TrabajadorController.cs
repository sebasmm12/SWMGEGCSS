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
        public ActionResult Index(HttpPostedFileBase imagen,int p)
        {
            if(imagen!=null && imagen.ContentLength > 0)
            {
                byte[] imagenData = null;
                using (var binaryImagen= new BinaryReader(imagen.InputStream))
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
    }
}