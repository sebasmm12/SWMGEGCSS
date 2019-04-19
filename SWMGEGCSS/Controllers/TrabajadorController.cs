using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult V_Tareas()
        {
            return View();
        }
    }
}