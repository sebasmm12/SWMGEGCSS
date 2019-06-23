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
using System.Text.RegularExpressions;
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
            model.PLista_Actividades_a_Desarrollar = new TrabajadorDataAccess().sp_listar_plan_por_usuario_asignado(Usuario).ToPagedList(page, 3);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaTareasAsignadas", model);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult AgregarArchivo(int id)
        {
            TareasAsignadasViewModel model = new TareasAsignadasViewModel();
            Session["ArchivoId"] = id;
            //insertar cuando entra
            T_auditoria_actividades_desarrollo modelAudi = new T_auditoria_actividades_desarrollo();
            modelAudi.act_desa_id = id;
            modelAudi.audi_act_comentario = "El usuario ingresó a realizar la tarea asiganda, hora: "+ DateTime.Now.ToShortDateString();
            modelAudi.audi_act_archivo_url = null;
            modelAudi.audi_act_archivo_nombre = null;
            modelAudi.usu_asignado = (int)Session["login"];
            var operationResult1 = new TrabajadorDataAccess().sp_Insertar_Tarea_Asignada_Auditoria(modelAudi);
            return View(model);
        }
        [HttpPost]
        public ActionResult AgregarArchivo(T_actividades_desarrollar_aux3 Actividad_aux3)
        {
            var valores = new TrabajadorDataAccess().sp_Consultar_Ruc_Plan_por_Act_Desa((int)Session["ArchivoId"]);
            if (Actividad_aux3.archivo != null)
            {
                String ruta = Server.MapPath("~/Repositorio/"+ valores.emp_ruc + "/" + valores.plan_id.ToString() + "/");
                if (!System.IO.File.Exists(ruta))
                {
                    String ruta2 = Server.MapPath("~/Repositorio/" + valores.emp_ruc + "/" + valores.plan_id.ToString());
                    System.IO.Directory.CreateDirectory(ruta2);
                }
                String nombreArchivo = Actividad_aux3.archivo.FileName;
                String ruta3 = ruta;
                ruta += nombreArchivo;
                String rutaevaluar = ruta;
                if (System.IO.File.Exists(rutaevaluar))
                {
                    String[] listadelnombre= nombreArchivo.Split('.');
                    String evaluar= listadelnombre[listadelnombre.Length - 2]  ;
                    string validar = "_V[\\d]{1,}$";
                    Regex r1 = new Regex(validar);
                    MatchCollection m1 = r1.Matches(evaluar);
                    List<String> lista = new List<string>();
                    foreach (var item in m1)
                    {
                       lista.Add(item.ToString());
                    }
                    if (lista.Count > 0){
                        String version = "";
                        String aux = "";
                        int versionado = 0;
                        for (int x= evaluar.Length-1;x>0;x--)
                        {
                            aux = evaluar.Substring(x, 1);
                            if (aux.Equals("V")) {
                                versionado = Int32.Parse(version);
                                versionado++;
                                evaluar = evaluar.Substring(0, x+1) +versionado;
                                break;
                            }
                            else
                            {
                                version = aux + version;
                            }
                        }
                    }
                    else
                    {
                        evaluar += "_V2";
                    }
                    listadelnombre[listadelnombre.Length - 2] = evaluar;
                    nombreArchivo = "";
                    String delimitador = "";
                    foreach (String abc in listadelnombre)
                    {
                        nombreArchivo += delimitador + abc;
                        delimitador = ".";
                    }
                    ruta = ruta3 + nombreArchivo;
                }
                subirArchivo(Actividad_aux3.archivo, ruta);
                
                var modelAct = new T_actividades_desarrollar();
                modelAct.act_desa_id = (int)Session["ArchivoId"];
                modelAct.act_desa_archivo_nombre = nombreArchivo;
                modelAct.act_desa_archivourl = ruta;
                modelAct.act_desa_comentario = Actividad_aux3.act_desa_comentario;

                var modelAudi = new T_auditoria_actividades_desarrollo();
                modelAudi.act_desa_id = (int)Session["ArchivoId"];
                modelAudi.audi_act_archivo_nombre = nombreArchivo;
                modelAudi.audi_act_archivo_url = ruta;
                modelAudi.audi_act_comentario = Actividad_aux3.act_desa_comentario;
                modelAudi.usu_asignado = (int)Session["login"];

                var operationResult = new TrabajadorDataAccess().sp_Agregar_Tarea_Asignada(modelAct);
                var operationResult1 = new TrabajadorDataAccess().sp_Insertar_Tarea_Asignada_Auditoria(modelAudi);
                Session["ArchivoId"] = null;

                return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
            }
            var operationResulta = new OperationResult();
            operationResulta.NewId = 0;
            return Json(new { data = operationResulta.NewId }, JsonRequestBehavior.AllowGet);
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
        public FileResult DescargarArchivo()
        {
            var valores = new TrabajadorDataAccess().sp_Consultar_Ruc_Plan_por_Act_Desa((int)Session["ArchivoId"]);
            string nombre = valores.act_desa_archivo_nombre;
            String rut = Server.MapPath("~/Repositorio/" + valores.emp_ruc + "/" + valores.plan_id.ToString() + "/" + nombre );
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
        [HttpGet]
        public ActionResult ModificarArchivo(int id)
        {
            TareasAsignadasViewModel model = new TareasAsignadasViewModel();
            Session["ArchivoId"] = id;

            var valores = new TrabajadorDataAccess().sp_Consultar_Ruc_Plan_por_Act_Desa((int)Session["ArchivoId"]);
            //nuevo procedure
            model.Lista_Observaciones = new TrabajadorDataAccess().sp_Listar_Observacion_por_Actividad((int)Session["ArchivoId"]);
            Session["obs_respondidas"] = model.Lista_Observaciones.Count;
            Session["comentarius"] = valores.act_desa_revisor_obs;
            return View(model);
        }
        [HttpPost]
        public ActionResult ModificarArchivo(T_actividades_desarrollar_aux3 Actividad_aux3)
        {
            var operationResultp = new OperationResult();
            if ((int)Session["obs_respondidas"] != 0)
            {
                operationResultp.NewId = 100;
                return Json(operationResultp.NewId, JsonRequestBehavior.AllowGet);
            }
             var valores = new TrabajadorDataAccess().sp_Consultar_Ruc_Plan_por_Act_Desa((int)Session["ArchivoId"]);
            if (Actividad_aux3.archivo != null)
            {
                String ruta = Server.MapPath("~/Repositorio/" + valores.emp_ruc + "/" + valores.plan_id.ToString() + "/");
                String nombreArchivo = Actividad_aux3.archivo.FileName;
                String ruta3 = ruta;
                ruta += nombreArchivo;                
                String rutaevaluar = ruta;
                if (System.IO.File.Exists(rutaevaluar))
                {
                    String[] listadelnombre = nombreArchivo.Split('.');
                    String evaluar = listadelnombre[listadelnombre.Length - 2];
                    string validar = "_V[\\d]{1,}$";
                    Regex r1 = new Regex(validar);
                    MatchCollection m1 = r1.Matches(evaluar);
                    List<String> lista = new List<string>();
                    foreach (var item in m1)
                    {
                        lista.Add(item.ToString());
                    }
                    if (lista.Count > 0)
                    {
                        String version = "";
                        String aux = "";
                        int versionado = 0;
                        for (int x = evaluar.Length - 1; x > 0; x--)
                        {
                            aux = evaluar.Substring(x , 1);
                            if (aux.Equals("V"))
                            {
                                versionado = Int32.Parse(version);
                                versionado++;
                                evaluar = evaluar.Substring(0, x+1) + versionado;


                                break;
                            }
                            else
                            {
                                version = aux + version;
                            }

                        }

                    }
                    else
                    {
                        evaluar += "_V2";

                    }
                    listadelnombre[listadelnombre.Length - 2] = evaluar;
                    nombreArchivo = "";
                    String delimitador = "";
                    foreach (String abc in listadelnombre)
                    {
                        nombreArchivo += delimitador + abc;
                        delimitador = ".";
                    }
                    ruta = ruta3 + nombreArchivo;
                }

                subirArchivo(Actividad_aux3.archivo, ruta);

                var modelAct = new T_actividades_desarrollar();
                modelAct.act_desa_id = (int)Session["ArchivoId"];
                modelAct.act_desa_archivo_nombre = nombreArchivo;
                modelAct.act_desa_archivourl = ruta;
                modelAct.act_desa_comentario = Actividad_aux3.act_desa_comentario;

                var modelAudi = new T_auditoria_actividades_desarrollo();
                modelAudi.act_desa_id = (int)Session["ArchivoId"];
                modelAudi.audi_act_archivo_nombre = nombreArchivo;
                modelAudi.audi_act_archivo_url = ruta;
                modelAudi.audi_act_comentario = Actividad_aux3.act_desa_comentario;
                modelAudi.usu_asignado = (int)Session["login"];

                var operationResult = new TrabajadorDataAccess().sp_Agregar_Tarea_Asignada(modelAct);
                var operationResult1 = new TrabajadorDataAccess().sp_Insertar_Tarea_Asignada_Auditoria(modelAudi);
                Session["ArchivoId"] = null;

                return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
            }
            operationResultp.NewId = 0;
            return Json(operationResultp.NewId, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ResponderObservacion()
        {
            TareasAsignadasViewModel model = new TareasAsignadasViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ResponderObservacion(T_observacion_actividades t_obs)
        {
            var model = new T_observacion_actividades();
            model.obs_act_id = t_obs.obs_act_id;
            model.obs_act_usuario = t_obs.obs_act_usuario;
            Session["obs_respondidas"] = (int)Session["obs_respondidas"] - 1;
            //Ver si retrocedo sin guardar cambios
            var operationResult = new TrabajadorDataAccess().sp_Actualizar_observaciones(model);
            var operationResult1 = new TrabajadorDataAccess().sp_Insertar_observaciones_auditoria(model);

            return Json(new { data = operationResult1.NewId }, JsonRequestBehavior.AllowGet);
        }
    }
}