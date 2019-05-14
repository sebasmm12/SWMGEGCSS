﻿using System;
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
        public ActionResult Registrar_Servicio()
        {
            return View();
        }
    }
}