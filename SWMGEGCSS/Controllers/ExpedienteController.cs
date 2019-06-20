using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using System.Globalization;
namespace SWMGEGCSS.Controllers
{
    public class ExpedienteController : Controller
    {
        // GET: Expediente
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registrar_Proyecto()
        {
            var model = new ExpedienteViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Registrar_Proyecto(T_expediente_aux Expediente)
        {
          
            var planesmodel = new ExpedienteDataAccess().sp_Obtener_Planes();
            var model = new ExpedienteViewModel();
            model.Expediente = Expediente;
            var plan_expediente = planesmodel.Find(modelo => (modelo.plan_nombre == model.Expediente.plan_nombre));
            var modeloActividades = new ActividadViewModel();
            modeloActividades.list_Actividades_Planeadas = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().FindAll(r=>r.plan_id==plan_expediente.plan_id);
            var modelExpediente = new T_expedientes();
            modelExpediente.plan_id = plan_expediente.plan_id;
            modelExpediente.usu_creador = (int)Session["login"];
            modelExpediente.exp_inicio = model.Expediente.exp_inicio;
            modelExpediente.exp_fin = model.Expediente.exp_fin;
            modelExpediente.tipo_servicio_id = plan_expediente.tipo_servicio_id;
            modelExpediente.exp_ganancia = model.Expediente.exp_ganancia;
            modelExpediente.exp_nombre = model.Expediente.exp_nombre;
            String ruta = Server.MapPath("~/Repositorio/" + plan_expediente.emp_ruc + "/" + plan_expediente.plan_id.ToString() + "/");
            if (!System.IO.File.Exists(ruta))
            {
                String ruta2 = Server.MapPath("~/Repositorio/" + plan_expediente.emp_ruc + "/" + plan_expediente.plan_id.ToString());
                System.IO.Directory.CreateDirectory(ruta2);
            }
        
            subirArchivo(model.Expediente.archivo_ulr_inicio, ruta);
            modelExpediente.archivo_ulr_inicio = ruta;
            var operationResult = new ExpedienteDataAccess().sp_Insertar_Proyecto(modelExpediente);
            modelExpediente = new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(r => r.exp_nombre == modelExpediente.exp_nombre);
            foreach (var item in modeloActividades.list_Actividades_Planeadas)
            {
                T_actividades_desarrollar act_desarrollar = new T_actividades_desarrollar();
                act_desarrollar.exp_id = modelExpediente.exp_id;
                act_desarrollar.usu_creador = modelExpediente.usu_creador;
                act_desarrollar.est_act_id = 1;
                act_desarrollar.act_desa_nombre = item.act_plan_nombre;
                act_desarrollar.act_desa_descripcion = item.act_plan_descripcion;
                var operationResult1 = new ActividadesDataAccess().sp_Insertar_Actividades_Desarrollar(act_desarrollar);
            }
            var operationResult2 = new OperationResult();
            NotificacionesViewModel notificacion = new NotificacionesViewModel();
            notificacion.notificacion = new T_notificaciones();
            notificacion.notificacion.not_nombre = "Creación del expediente " + model.Expediente.exp_nombre;
            notificacion.notificacion.not_descripcion = "El expediente ha sido creado exitosamente y esta listo para la ejecución no olvidar que cuando empieze la fecha de ejecución se comenzará con la asignación de actividades. " +
            "Fecha de ejecución : " + modelExpediente.exp_inicio.ToShortDateString();
            notificacion.notificacion.usu_codigo = 8;
            notificacion.notificacion.usu_envio = 9;
            operationResult2 = new NotificacionesDataAccess().sp_Insertar_Notificaciones(notificacion.notificacion);
            return Json(new { data=operationResult.NewId},JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ModalProyecto()
        {
            var model = new ExpedienteViewModel();
            model.ActividadModel = new ActividadViewModel();
            model.ActividadModel.list_Actividades = new List<T_actividades>();
            model.ActividadModel.list_Actividades_Desarrollar = new List<T_actividades_desarrollar>();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalProyecto(int id)
        {
            var model = new ExpedienteViewModel();
            model.Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().Find(r => (r.exp_id == id));
            model.Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(r=>(r.exp_id==id));
            model.ActividadModel = new ActividadViewModel();
            var model1 = new ExpedienteViewModel();
            model1.ActividadModel = new ActividadViewModel();
            model.ActividadModel.list_Actividades_Planeadas = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().FindAll(r => r.plan_id == model.Expedientes.plan_id);
            model1.ActividadModel.list_Actividades_Desarrollar = new ActividadesDataAccess().sp_Consultar_Actividades_Desarrollar_Expediente().FindAll(r => r.exp_id == id);
            model.ActividadModel.list_Actividades = new ActividadesDataAccess().sp_Consultar_Actividades_Diferentes_Plan(model.Expedientes.tipo_servicio_id);
            for(int i =0;i<model.ActividadModel.list_Actividades.Count;i++)
            {
                var p = model.ActividadModel.list_Actividades_Planeadas.Find(r => r.act_id == model.ActividadModel.list_Actividades.ElementAt(i).act_id);
                if (p != null)
                {
                    model.ActividadModel.list_Actividades.ElementAt(i).act_nombre = p.act_plan_nombre;
                    model.ActividadModel.list_Actividades.ElementAt(i).act_descripcion = p.act_plan_descripcion;
                    model.ActividadModel.list_Actividades.ElementAt(i).act_id = p.act_id;
                }
                else
                {

                }
            }
            Session["actividades_cambiadas"] = model.ActividadModel.list_Actividades;
            model.ActividadModel.list_Actividades_Desarrollar = new ActividadesDataAccess().sp_Consultar_Actividades_Desarrollar_Expediente().FindAll(r=>r.exp_id==id);
            Session["list_actividades"] = model.ActividadModel.list_Actividades_Desarrollar;
            Session["list_actividades_comparadora"] = model1.ActividadModel.list_Actividades_Desarrollar;
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult _EliminarProyecto()
        {
            var model = new ExpedienteViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _EliminarProyecto(int id)
        {
            var model = new ExpedienteViewModel();
            model.Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().Find(r => (r.exp_id == id));
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult EliminarExpediente(int id,string comentario)
        {
            var Expedientes = new ExpedienteViewModel();
            Expedientes.Expedientes= new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(modelo=>modelo.exp_id==id);
            var AuditoriaExpediente = new AuditoriaViewModel();
            AuditoriaExpediente.auditoria_expediente = new T_auditar_expedientes();
 
            AuditoriaExpediente.auditoria_expediente.exp_id = Expedientes.Expedientes.exp_id;
            AuditoriaExpediente.auditoria_expediente.aud_exp_inicio = Expedientes.Expedientes.exp_inicio;
            AuditoriaExpediente.auditoria_expediente.aud_exp_fin = Expedientes.Expedientes.exp_fin;
            AuditoriaExpediente.auditoria_expediente.aud_exp_ganancia = Expedientes.Expedientes.exp_ganancia;
            AuditoriaExpediente.auditoria_expediente.aud_exp_comentario = comentario;
            AuditoriaExpediente.auditoria_expediente.tipo_servicio_id = Expedientes.Expedientes.tipo_servicio_id;
            var operationResultAuditorio = new ExpedienteDataAccess().sp_Insertar_Auditoria_Expediente(AuditoriaExpediente.auditoria_expediente);
            var actividades_desarrollar = new ActividadesDataAccess().sp_Consultar_Actividades_Desarrollar_Expediente().FindAll(r=>r.exp_id==id);
            foreach (var item in actividades_desarrollar)
            {
                var operationResult2 = new ExpedienteDataAccess().sp_Eliminar_Actividades_Desarrollar_Expediente(item.act_desa_id);
            }
            var operationresult3 = new PlanDataAccess().sp_Cancelar_Plan(Expedientes.Expedientes.plan_id, comentario);
            var operationResult = new ExpedienteDataAccess().sp_Eliminar_Proyecto(id,comentario);
            return Json(new { id = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Nombre_Proyecto(string exp_nombre)
        {
            var model = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos();
            var modelEvaluado = model.Find(r => r.exp_nombre == exp_nombre);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Nombre_Plan(string exp_plan)
        {
            var model = new ExpedienteDataAccess().sp_Obtener_Planes();
            var modelEvaluado = model.Find(r => r.plan_nombre == exp_plan);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Autocomplete([Bind(Prefix ="term")]string term)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.listplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(term);
            var namePlanes = model.listplans.Select(r => new
            {
                label = r.plan_nombre
            });
            return Json(namePlanes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _ListarActividadesNoPlan(int id)
        {
            var model =new  ActividadViewModel();
            model.list_Actividades = new ActividadesDataAccess().sp_Consultar_Actividades_Diferentes_Plan(id);
            return PartialView(model);
        }
        public ActionResult AñadirActividad(string act_nombre,int exp_id)
        {
            var Actividad = new List<T_actividades>();
            // Actividad = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().Find(r => r.act_plan_nombre == act_nombre);
            Actividad = (List<T_actividades>)Session["actividades_cambiadas"];
            var ActividadT = Actividad.Find(r => r.act_nombre == act_nombre);
            var ActividadG = new T_actividades();
            ActividadG = new ActividadesDataAccess().sp_Consultar_Actividades().Find(r => r.act_nombre == act_nombre);
            var Actividades_Desarrollar = new T_actividades_desarrollar();
            List<T_actividades_desarrollar> list_actividades = new List<T_actividades_desarrollar>();
            if (Session["list_actividades"] != null)
            {
                list_actividades = (List<T_actividades_desarrollar>)Session["list_actividades"];
            }
            var Actividades_D = new ActividadesDataAccess().sp_Consultar_Actividades_Desarrollar_Expediente().Find(r => r.exp_id==exp_id && r.act_desa_nombre==act_nombre);
            Actividades_Desarrollar.exp_id = exp_id;
            if (Actividades_D != null)
            {
               Actividades_Desarrollar.act_desa_id = Actividades_D.act_desa_id;
               Actividades_Desarrollar.act_desa_nombre = Actividades_D.act_desa_nombre;
               Actividades_Desarrollar.act_desa_descripcion = Actividades_D.act_desa_descripcion;
            }
            else
            {
                Actividades_Desarrollar.act_desa_nombre = ActividadT.act_nombre;
                Actividades_Desarrollar.act_desa_descripcion = ActividadT.act_descripcion;
            }
            Actividades_Desarrollar.usu_creador = (int)Session["login"];
            Actividades_Desarrollar.est_act_id = 1;
            list_actividades.Add(Actividades_Desarrollar);
            int p = 1;
            Session["list_actividades"] = list_actividades;
            return Json(p, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarActividad(string act_nombre, int exp_id)
        {
            var Actividad = new List<T_actividades>();
            // Actividad = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().Find(r => r.act_plan_nombre == act_nombre);
            Actividad = (List<T_actividades>)Session["actividades_cambiadas"];
            var ActividadT = Actividad.Find(r => r.act_nombre == act_nombre);
            var ActividadG = new T_actividades();
            ActividadG = new ActividadesDataAccess().sp_Consultar_Actividades().Find(r=>r.act_nombre==act_nombre);
            var Actividades_Desarrollar = new T_actividades_desarrollar();
            List<T_actividades_desarrollar> list_actividades = new List<T_actividades_desarrollar>();
            if (Session["list_actividades"] != null)
            {
                list_actividades = (List<T_actividades_desarrollar>)Session["list_actividades"];
            }
            Actividades_Desarrollar.exp_id = exp_id;
            Actividades_Desarrollar.usu_creador = (int)Session["login"];
            Actividades_Desarrollar.est_act_id = 1;
            if (Actividad != null)
            {
                Actividades_Desarrollar.act_desa_nombre = ActividadT.act_nombre;
                Actividades_Desarrollar.act_desa_descripcion = ActividadT.act_descripcion;
            }
            if (ActividadG != null)
            {
                Actividades_Desarrollar.act_desa_nombre = ActividadG.act_nombre;
                Actividades_Desarrollar.act_desa_descripcion = ActividadG.act_descripcion;
            }

           var Act_aux= list_actividades.Find(r=>r.act_desa_nombre==act_nombre);
            list_actividades.Remove(Act_aux);
            int p = 2;
            Session["list_actividades"] = list_actividades;
            return Json(p, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ModificarExpediente(string comentario)
        {
            List<T_actividades_desarrollar> list_actividades = new List<T_actividades_desarrollar>();
            List<T_actividades_desarrollar> list_actividades_inicial = new List<T_actividades_desarrollar>();
            list_actividades = (List<T_actividades_desarrollar>)Session["list_actividades"];
            list_actividades_inicial = (List<T_actividades_desarrollar>)Session["list_actividades_comparadora"];

            foreach (var item in list_actividades_inicial)
            {
                T_actividades_desarrollar t_actividades = list_actividades.Find(r => r.act_desa_id == item.act_desa_id);
                if (t_actividades == null)
                {
                    item.act_desa_comentario = comentario;
                    var operationResult = new ExpedienteDataAccess().sp_Insertar_Auditoria_Actividades_Desarrollar(item);
                    var operationResult1 = new ExpedienteDataAccess().sp_Eliminar_Actividades_Desarrollar_Expediente(item.act_desa_id);
                }
               
            }
            foreach (var item in list_actividades)
            {
                T_actividades_desarrollar t_act = list_actividades_inicial.Find(r => r.act_desa_id == item.act_desa_id);
                if (t_act == null)
                {
                    var operationResult = new ActividadesDataAccess().sp_Insertar_Actividades_Desarrollar(item);
                }

            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ModificarExpediente()
        {
            List<T_actividades_desarrollar> list_actividades = new List<T_actividades_desarrollar>();
            List<T_actividades_desarrollar> list_actividades_inicial = new List<T_actividades_desarrollar>();
            list_actividades = (List<T_actividades_desarrollar>)Session["list_actividades"];
            list_actividades_inicial = (List<T_actividades_desarrollar>)Session["list_actividades_comparadora"];

            foreach (var item in list_actividades)
            {
                T_actividades_desarrollar t_act = list_actividades_inicial.Find(r => r.act_desa_id == item.act_desa_id);
                if (t_act == null)
                {
                    var operationResult = new ActividadesDataAccess().sp_Insertar_Actividades_Desarrollar(item);
                }
            
            }
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Comprobar_Validacion_Actividades()
        {
            List<T_actividades_desarrollar> list_actividades = new List<T_actividades_desarrollar>();
            List<T_actividades_desarrollar> list_actividades_inicial = new List<T_actividades_desarrollar>();
            list_actividades = (List<T_actividades_desarrollar>)Session["list_actividades"];
            list_actividades_inicial = (List<T_actividades_desarrollar>)Session["list_actividades_comparadora"];
            int cont = list_actividades_inicial.Count;
            foreach (var item in list_actividades_inicial)
            {
                T_actividades_desarrollar t_actividades = list_actividades.Find(r => r.act_desa_id == item.act_desa_id);
                if (t_actividades != null)
                {
                    cont--;
                }
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Calcular_Costo_Actividades(string plan_nombre)
        {
            double costo = 0;
            var planesmodel = new ExpedienteDataAccess().sp_Obtener_Planes();
            var plan_expediente = planesmodel.Find(modelo => (modelo.plan_nombre == plan_nombre));
            if (plan_nombre == "" || plan_nombre == null||plan_expediente==null)
            {
                costo = 0;
            }
            else
            {
                costo = new ExpedienteDataAccess().sp_Calcular_Suma_Cantidad_Actividades_Planeadas(plan_expediente.plan_id);
            }
           
            return Json(costo, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ConsultarProyecto()
        {
            var model = new ExpedienteViewModel();
            model.ActividadModel = new ActividadViewModel();
            model.ActividadModel.list_Actividades = new List<T_actividades>();
            model.ActividadModel.list_Actividades_Desarrollar = new List<T_actividades_desarrollar>();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ConsultarProyecto(int id)
        {
            var model = new ExpedienteViewModel();
            model.Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().Find(r => (r.exp_id == id));
            model.Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Expedientes().Find(r => (r.exp_id == id));
            model.ActividadModel = new ActividadViewModel();
            model.ActividadModel.list_Actividades_Desarrollar = new ActividadesDataAccess().sp_Consultar_Actividades_Desarrollar_Expediente_Completa();
            model.ActividadModel.list_Actividades_Desarrollar = new ActividadesDataAccess().sp_Consultar_Actividades_Desarrollar_Expediente_Completa().FindAll(r=>(r.exp_id==id));
            return PartialView(model);
        }
        public void subirArchivo(HttpPostedFileBase archivo, String ruta)
        {
            try
            {
                archivo.SaveAs(ruta);
            }
            catch (Exception ez)
            {

            }
        }
        [HttpPost]
        public ActionResult _AgregarArchivoMinam(int exp_id,HttpPostedFile)
        {
            return PartialView();
        }
    }
}