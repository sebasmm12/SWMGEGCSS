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
        public ActionResult VisualizarServicios(string searchTerm,string estado,int page=1)
        {
            var model = new TipoServicioViewModel();
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
                    model.ListtipoServicio = new TipoServicioDataAccess().sp_Consultar_Lista_Servicios_Nombre(term).FindAll(r=>r.tipo_servicio_estado==state);
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
        public ActionResult Registrar_Servicio(int page=1)
        {
            var model = new TipoServicioViewModel();
            model.list_actividades = new ActividadesDataAccess().sp_Consultar_Actividades();
            var list_act = new List<T_actividades>();
            if (Session["lista_actividades"] != null)
            {
                list_act = (List<T_actividades>)Session["lista_actividades"];
            }
            model.Plist_actividades = list_act.ToPagedList(page, 3);
            return View(model);
        }
        public ActionResult Añadir_Actividad(int actividad)
        {
            List<T_actividades> list_actividades = new List<T_actividades>();
            if (Session["lista_actividades"]!=null)
            {
                list_actividades = (List<T_actividades>)Session["lista_actividades"];
            }
            var actividad_encontrada = new ActividadesDataAccess().sp_Consultar_Actividades().Find(r=>r.act_id==actividad);
            list_actividades.Add(actividad_encontrada);
            Session["lista_actividades"] = list_actividades;
            var model = new TipoServicioViewModel();
            model.list_actividades = list_actividades;
            model.Plist_actividades = list_actividades.ToPagedList(1, 3);
            return PartialView("_ListaActividades",model);
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
            model.actividades = new ActividadesDataAccess().sp_Consultar_Actividades().Find(r=>r.act_id==id);
            return View(model);
        }
        public ActionResult EliminarActividad(int id)
        {
            List<T_actividades> list_actividades = new List<T_actividades>();
            if (Session["lista_actividades"] != null)
            {
                list_actividades = (List<T_actividades>)Session["lista_actividades"];
            }
            var actividad_encontrada = list_actividades.Find(r=>r.act_id==id);
            list_actividades.Remove(actividad_encontrada);
            Session["lista_actividades"] = list_actividades;
            var model = new TipoServicioViewModel();
            model.list_actividades = list_actividades;
            model.Plist_actividades = list_actividades.ToPagedList(1, 3);
            return PartialView("_ListaActividades", model);
        }
    }
}