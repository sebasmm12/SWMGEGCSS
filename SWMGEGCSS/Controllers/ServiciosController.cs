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
    public class ServiciosController : Controller
    {
        // GET: Servicios
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VisualizarServicios(string searchTerm, string estado, int page = 1)
        {
            var model = new TipoServicioViewModel();
            Session["lista_actividades"] = null;
            if (searchTerm == null && estado == null)
                model.PList_tipo_servicio = new TipoServicioDataAccess().sp_Consultar_Tipo_Servicio().ToPagedList(page, 3);
            if (estado != null)
            {
                if (searchTerm != null)
                {
                    if (estado.Equals("Todos"))
                    {
                        model.PList_tipo_servicio = new TipoServicioDataAccess().sp_Consultar_Lista_Servicios_Nombre(searchTerm).ToPagedList(page, 3);
                    }
                    else
                    {
                        bool state = false;
                        if (estado.Equals("Activo"))
                        {
                            state = true;
                        }
                        else
                        {
                            state = false;
                        }
                        model.PList_tipo_servicio = new TipoServicioDataAccess().sp_Consultar_Lista_Servicios_Nombre(searchTerm).FindAll(r => (r.tipo_servicio_estado == state)).ToPagedList(page, 3);
                    }

                }
            }
            if (estado != null)
            {
                model.tipo_servicio_estado = estado;
                Session["est_tipo_serv_nombre"] = model.tipo_servicio_estado;
            }
            else
            {
                model.tipo_servicio_estado = "Todos";
                Session["est_tipo_serv_nombre"] = model.tipo_servicio_estado;
            }
            Session["page_servicio"] = page;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaServicios", model);
            }
            return View(model);

        }
        public ActionResult Autocomplete(string term)
        {
            var state = false;
            string estado = (String)Session["est_tipo_serv_nombre"];
            var model = new TipoServicioViewModel();
            if (estado.Equals("Todos"))
            {
                model.ListtipoServicio = new TipoServicioDataAccess().sp_Consultar_Lista_Servicios_Nombre(term);
            }
            else
            {
                if (estado.Equals("Activo"))
                {
                    state = true;
                    model.ListtipoServicio = new TipoServicioDataAccess().sp_Consultar_Lista_Servicios_Nombre(term).FindAll(r => r.tipo_servicio_estado == state);
                }
                else
                {
                    state = false;
                    model.ListtipoServicio = new TipoServicioDataAccess().sp_Consultar_Lista_Servicios_Nombre(term).FindAll(r => r.tipo_servicio_estado == state);
                }
            }

            var nameServicios = model.ListtipoServicio.Select(r => new
            {
                label = r.tipo_servicio_nombre
            }
            );
            return Json(nameServicios, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Registrar_Servicio(int page = 1)
        {
            var model = new TipoServicioViewModel();
            model.list_actividades = new ActividadesDataAccess().sp_Consultar_Actividades();
            var list_act = new List<T_actividades>();
            if (Session["lista_actividades"] != null)
            {
                list_act = (List<T_actividades>)Session["lista_actividades"];
            }
            model.Plist_actividades = list_act.ToPagedList(page, 3);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaActividades", model);
            }
            return View(model);
        }
        public ActionResult Añadir_Actividad(int actividad)
        {
            List<T_actividades> list_actividades = new List<T_actividades>();
            if (Session["lista_actividades"] != null)
            {
                list_actividades = (List<T_actividades>)Session["lista_actividades"];
            }
            var actividad_encontrada = new ActividadesDataAccess().sp_Consultar_Actividades().Find(r => r.act_id == actividad);
            list_actividades.Add(actividad_encontrada);
            Session["lista_actividades"] = list_actividades;
            var model = new TipoServicioViewModel();
            model.list_actividades = list_actividades;
            model.Plist_actividades = list_actividades.ToPagedList(1, 3);
            return PartialView("_ListaActividades", model);
        }
        [HttpGet]
        public ActionResult _ModalActividad()
        {
            var model = new TipoServicioViewModel();
            model.actividades = new T_actividades();
            return View(model);
        }
        [HttpPost]
        public ActionResult _ModalActividad(int id)
        {
            var model = new TipoServicioViewModel();
            List<T_actividades> list_actividades = new List<T_actividades>();
            if (Session["lista_actividades"] == null)
            {
                model.actividades = new ActividadesDataAccess().sp_Consultar_Actividades().Find(r => r.act_id == id);
            }
            else
            {
                list_actividades = (List<T_actividades>)Session["lista_actividades"];
                model.actividades = list_actividades.Find(r => r.act_id == id);
            }

            return View(model);
        }
        public ActionResult EliminarActividad(int id)
        {
            List<T_actividades> list_actividades = new List<T_actividades>();
            if (Session["lista_actividades"] != null)
            {
                list_actividades = (List<T_actividades>)Session["lista_actividades"];
            }
            var actividad_encontrada = list_actividades.Find(r => r.act_id == id);
            list_actividades.Remove(actividad_encontrada);
            Session["lista_actividades"] = list_actividades;
            var model = new TipoServicioViewModel();
            model.list_actividades = list_actividades;
            model.Plist_actividades = list_actividades.ToPagedList(1, 3);
            return PartialView("_ListaActividades", model);
        }
        [HttpPost]
        public ActionResult ModificarDatosActividad(T_actividades actividad, int page = 1)
        {
            int paginado = page;
            var model = new TipoServicioViewModel();
            List<T_actividades> list_actividades = new List<T_actividades>();
            if (Session["lista_actividades"] != null)
            {
                list_actividades = (List<T_actividades>)Session["lista_actividades"];
            }
            var actividadE = list_actividades.Find(r => r.act_id == actividad.act_id);
            int index = list_actividades.FindIndex(r => r.act_id == actividad.act_id);
            list_actividades.Remove(actividadE);
            list_actividades.Insert(index, actividad);
            Session["lista_actividades"] = list_actividades;
            model.list_actividades = list_actividades;
            model.Plist_actividades = list_actividades.ToPagedList(paginado, 3);
            return PartialView("_ListaActividades", model);
        }
        [HttpPost]
        public ActionResult RegistrarServicioActividad(T_tipo_servicio servicio)
        {
            int cont = 1;
            List<T_actividades> list_actividades = new List<T_actividades>();
            if (Session["lista_actividades"] != null)
            {
                list_actividades = (List<T_actividades>)Session["lista_actividades"];
            }
            foreach (var item in list_actividades)
            {
                if (item.costo == 0)
                {
                    cont--;
                }
            }
            if (cont != 1)
            {
                return Json(cont, JsonRequestBehavior.AllowGet);
            }
            var operationResult1 = new TipoServicioDataAccess().sp_Insertar_Tipo_Servicio(servicio);
            servicio = new TipoServicioDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find(r => r.tipo_servicio_nombre == servicio.tipo_servicio_nombre);
            foreach (var item in list_actividades)
            {
                T_tipo_servicio_actividades t_actividades = new T_tipo_servicio_actividades();
                t_actividades.act_id = item.act_id;
                t_actividades.act_obligatorio = item.act_obligatorio;
                t_actividades.costo = item.costo;
                t_actividades.tipo_servicio_id = servicio.tipo_servicio_id;
                var operationResult = new TipoServicioDataAccess().sp_Insertar_Tipo_Servicio_Actividades(t_actividades);
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _EliminarServicio()
        {
            var model = new TipoServicioViewModel();
            model.tipoServicio = new T_tipo_servicio();
            model.PList_tipo_servicio_actividades = new List<T_tipo_servicio_actividades_aux>().ToPagedList(1, 1);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _EliminarServicio(int tipo_servicio_id)
        {
            var model = new TipoServicioViewModel();
            model.tipoServicio = new TipoServicioDataAccess().sp_Consultar_Tipo_Servicio().Find(r => r.tipo_servicio_id == tipo_servicio_id);
            model.PList_tipo_servicio_actividades = new TipoServicioDataAccess().sp_Consultar_tipos_servicios_actividades().FindAll(r => r.tipo_servicio_id == tipo_servicio_id).ToPagedList(1, 3);
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult EliminarServicio(int tipo_servicio_id)
        {
            var model = new TipoServicioViewModel();
            var operationResult = new TipoServicioDataAccess().sp_Cambiar_Estado_Tipo_Servicio(tipo_servicio_id);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ConsultarDatosServicios()
        {
            var model = new TipoServicioViewModel();
            model.tipoServicio = new T_tipo_servicio();
            model.PList_tipo_servicio_actividades = new List<T_tipo_servicio_actividades_aux>().ToPagedList(1, 1);
            model.PList_Expedientes_Servicio = new List<T_expediente_aux>().ToPagedList(1, 1);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ConsultarDatosServicios(int tipo_servicio_id)
        {
            var model = new TipoServicioViewModel();
            model.tipoServicio = new TipoServicioDataAccess().sp_Consultar_Tipo_Servicio().Find(r => r.tipo_servicio_id == tipo_servicio_id);
            model.PList_tipo_servicio_actividades = new TipoServicioDataAccess().sp_Consultar_tipos_servicios_actividades().FindAll(r => r.tipo_servicio_id == tipo_servicio_id).ToPagedList(1, 3);
            model.PList_Expedientes_Servicio = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().FindAll(r => r.tipo_servicio_nombre == model.tipoServicio.tipo_servicio_nombre).ToPagedList(1, 3);
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult _ActualizarDatosServicio()
        {
            var model = new TipoServicioViewModel();
            model.tipoServicio = new T_tipo_servicio();
            model.PList_tipo_servicio_actividades = new List<T_tipo_servicio_actividades_aux>().ToPagedList(1, 1);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ActualizarDatosServicio(int tipo_servicio_id)
        {
            var model = new TipoServicioViewModel();
            model.tipoServicio = new TipoServicioDataAccess().sp_Consultar_Tipo_Servicio().Find(r => r.tipo_servicio_id == tipo_servicio_id);
            model.PList_tipo_servicio_actividades = new TipoServicioDataAccess().sp_Consultar_tipos_servicios_actividades().FindAll(r => r.tipo_servicio_id == tipo_servicio_id).ToPagedList(1, 3);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult ActualizarServicio(T_tipo_servicio servicio)
        {

            var operationResult = new OperationResult();
            operationResult = new TipoServicioDataAccess().sp_Actualizar_Datos_del_Servicio(servicio);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ActivarServicio(int tipo_servicio_id, int page = 1)
        {
            var model = new TipoServicioViewModel();
            page = (int)Session["page_servicio"];
            var operationResult = new TipoServicioDataAccess().sp_Activar_Servicio(tipo_servicio_id);
            model.PList_tipo_servicio = new TipoServicioDataAccess().sp_Consultar_Tipo_Servicio().ToPagedList(page, 3);
            return PartialView("_ListaServicios", model);
        }
        [HttpGet]
        public ActionResult EvaluarNombreServicio(string tipo_servicio_nombre)
        {
            int cont = 0;
            var servicio = new TipoServicioDataAccess().sp_Consultar_Tipo_Servicio().Find(r => r.tipo_servicio_nombre == tipo_servicio_nombre);
            if (servicio != null)
            {
                cont++;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EvaluarActividades(int tipo_actividad)
        {
            int cont = 0;
            List<T_actividades> list_actividades = new List<T_actividades>();
            if (Session["lista_actividades"] != null)
            {
                list_actividades = (List<T_actividades>)Session["lista_actividades"];
            }
            var actividad = list_actividades.Find(r => r.act_id == tipo_actividad);
            if (actividad != null)
            {
                cont++;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ContarActividades()
        {
            int cont = 0;
            List<T_actividades> list_actividades = null;
            if (Session["lista_actividades"] != null)
            {
                list_actividades = (List<T_actividades>)Session["lista_actividades"];
            }
            if (list_actividades != null)
            {
                if (list_actividades.Count == 0)
                {
                    cont = 0;
                }
                else
                {
                    cont++;
                }
            }
           
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
       

    }
}