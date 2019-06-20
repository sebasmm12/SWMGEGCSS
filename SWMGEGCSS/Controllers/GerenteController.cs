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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;

namespace SWMGEGCSS.Controllers
{
    public class GerenteController : Controller
    {
        // GET: Gerente
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Consultar_Capital()
        {
            return View();
        }
        public ActionResult Generar_Reportes()
        {
            var model = new ExpedienteViewModel();
            model.List_Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos();

            /*List<T_expediente_aux> listExp = new List<T_expediente_aux>();
            listExp = model.List_Expediente;
            foreach (var item in listExp)
            {

            }*/
            return View(model);
        }
        public ActionResult ExportarReporte()
        {
            var model = new ExpedienteViewModel();

            model.Estado_Expediente = new EstadoExpedienteDataAccess().sp_Consultar_Lista_Estado_Expediente().Find(X => (X.est_exp_id == 2));
            model.List_Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos_Reporte().FindAll(X => (X.est_exp_nombre == model.Estado_Expediente.est_exp_nombre));
            ReportDocument rp = new ReportDocument();
            //rp.Load(Path.Combine(Server.MapPath("~/Reporte"), "reporteProyecto.rpt"));
            rp.Load(@"C:\Users\USUARIO\Desktop\PROYECTO TP3\SWMGEGCSS_EN\Reporte\reporteProyecto.rpt");
            rp.SetDataSource(model.List_Expediente);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ReporteProyectos.pdf");


        }
        public ActionResult ExportarReporteFinanciero(DateTime fechaIni, DateTime fechaFin)
        {
            var model = new Gestionar_I_EViewModel();
            //model.reporteFechas.fechaInicio = fechaIni;
            //model.reporteFechas.fechaFin = fechaFin;
            List<T_ingresos_egresos> list_Ing_Eg = new List<T_ingresos_egresos>();
            List<T_ingresos_egresos_aux> list_Ing_Eg_Aux = new List<T_ingresos_egresos_aux>();
            list_Ing_Eg = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr_X_fecha(fechaIni, fechaFin);
            // model.lista_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr().FindAll(X => X.ing_egr_fecha > Convert.ToDateTime(fechaIni) &&  X.ing_egr_fecha < Convert.ToDateTime(fechaFin));
            
            foreach (T_ingresos_egresos tie in list_Ing_Eg)
            {
                T_ingresos_egresos_aux tiea = new T_ingresos_egresos_aux();
                tiea.ing_egr_nombre = tie.ing_egr_nombre;
                if(tie.ing_egr_ingrso == true)
                {
                    tiea.ing_egr_ingrso = "Ingreso";
                    tiea.ing_egr_monto = tie.ing_egr_monto;
                }
                else
                {
                    tiea.ing_egr_ingrso = "Egreso";
                    tiea.ing_egr_monto = -tie.ing_egr_monto;
                }
                tiea.ing_egr_descripcion = tie.ing_egr_descripcion;
                tiea.ing_egr_fecha = tie.ing_egr_fecha;
                list_Ing_Eg_Aux.Add(tiea);
            }
            model.lista_ingresos_egresos_aux = list_Ing_Eg_Aux;
            ReportDocument rp = new ReportDocument();
            //C:\Users\hp\Desktop\Ing. Informatica Ciclo 2019 - I\Taller de Proyectos II\TP-2\SWMGEGCSS_EN\Reporte
            rp.Load(@"C:\Users\USUARIO\Desktop\PROYECTO TP3\SWMGEGCSS_EN\Reporte\reporteIE.rpt");
            rp.SetDataSource(model.lista_ingresos_egresos_aux);      
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ReporteFinanciero.pdf");
        }
        public ActionResult Gestionar_Reporte_I_E()
        {
            return View();
        }
        public ActionResult Gestionar_Formularios()
        {
            return RedirectToAction("Index", "EvaluarFormulario"); ;
        }
        public ActionResult Gestionar_Servicios()
        {
            return RedirectToAction("VisualizarServicios", "Servicios");
        }
        public ActionResult Gestionar_Cuenta(string searchTerm, string estado, int page = 1)
        {
            var model = new GestionarCuentasViewModel();
            if (searchTerm == null && estado == null)
            {
                model.Plist_cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Cuentas().ToPagedList(page, 5);
            }
            if (estado != null)
            {
                if (searchTerm != null && searchTerm == "")
                {
                    if (estado.Equals("Todos"))
                    {
                        model.Plist_cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Cuentas().ToPagedList(page, 4);
                    }
                    else
                    {
                        //model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().FindAll(r => r.emp_estado == (estado.Equals("1"))).ToPagedList(page, 4);
                        model.Plist_cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Cuentas().FindAll(r => r.rol_nombre == estado).ToPagedList(page, 4);
                    }
                }
                else
                {
                    if (estado.Equals("Todos"))
                    {
                        model.Plist_cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Nombre_Usuario_Cuenta(searchTerm).ToPagedList(page, 4);
                        //model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).ToPagedList(page, 4);
                    }
                    else
                    {
                        model.Plist_cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Nombre_Usuario_Cuenta(searchTerm).FindAll(r => r.rol_nombre == estado).ToPagedList(page, 4);
                        //model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).FindAll(r => r.emp_estado == (estado.Equals("1"))).ToPagedList(page, 4);
                    }


                }
            }
            if (estado != null)
            {

                Session["estado"] = estado;
            }
            else
            {

                Session["estado"] = estado;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaCuentaUsuario", model);
            }
            return View(model);
        }

        public ActionResult Gestionar_Proyectos(string searchTerm,string estado, int page = 1)
        {
            ExpedienteViewModel model = new ExpedienteViewModel();
            if (searchTerm == null&&estado==null)
                model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Proyectos().ToPagedList(page, 3);
            if (estado != null)
            {
                if (searchTerm != null)
                {
                    if (estado.Equals("Todos"))
                    {
                        model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(searchTerm).ToPagedList(page, 3);
                    }
                    else
                    {
                        model.PList_Expedientes = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(searchTerm).FindAll(r => (r.est_exp_nombre == estado)).ToPagedList(page, 3);
                    }
                   
                }
            }
            if (estado != null)
            {
                model.tipo_estado = estado;
                Session["est_nombre"] = model.tipo_estado;
            }
            else
            {
                model.tipo_estado = "Todos";
                Session["est_nombre"] = model.tipo_estado;
            }
            foreach (var item in model.PList_Expedientes)
            {
                item.cantidad_actividades_desarrollar = new ActividadesDataAccess().sp_Consultar_Actividades_Desarrollar_Expediente().FindAll(r=>r.exp_id==item.exp_id&&r.est_act_id!=3).Count;
                item.cantidad_actividades_terminar = new ActividadesDataAccess().sp_Consultar_Actividades_Desarrollar_Expediente().FindAll(r => r.exp_id == item.exp_id&&r.est_act_id==6).Count;
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaProyecto", model);
            }
            model.List_Estado_Expediente = new EstadoExpedienteDataAccess().sp_Consultar_Lista_Estado_Expediente();           
            return View(model);
        }
        public ActionResult Gestionar_I_E(string searchTerm, string estado, int page = 1)
        {
            /* Gestionar_I_EViewModel model = new Gestionar_I_EViewModel();
             model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr().ToPagedList(page, 4);

             return View(model);*/

            Gestionar_I_EViewModel model = new Gestionar_I_EViewModel();

            if (searchTerm == null && estado == null)
            {
                model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr().ToPagedList(page, 4);
            }
            if (estado != null)
            {
                if (searchTerm != null && searchTerm == "")
                {
                    if (estado.Equals("Todos"))
                    {
                        model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr().ToPagedList(page, 4);
                    }
                    else
                    {
                        model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr_Estado(estado).ToPagedList(page, 4);
                    } 
                }
                else
                {
                    if (estado.Equals("Todos"))
                    {
                        model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr_Nombre(searchTerm).ToPagedList(page, 4);
                    }
                    else
                    {
                        model.list_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr_Nombre_Estado(searchTerm,estado).ToPagedList(page, 4);
                    }
                    


                }
            }
            if (estado != null)
            {
                model.tipo_estado = estado;
                Session["est_nombre"] = model.tipo_estado;
            }

            else
            {
                model.tipo_estado = "Todos";
                Session["est_nombre"] = model.tipo_estado;
            }


            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaIng_Egr", model);
            }
            return View(model);
        }
        
        public ActionResult Gestionar_Empresas(string searchTerm, string estado, int page = 1)
        {
            GestionarEmpresaViewModel model = new GestionarEmpresaViewModel();

            if (searchTerm == null && estado == null)
            {
                model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().ToPagedList(page, 4);
            }
            if (estado != null)
            {
                if (searchTerm != null && searchTerm =="")
                {
                    if (estado.Equals("Todos"))
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().ToPagedList(page, 4);
                    }
                    else
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas().FindAll(r => r.emp_estado == (estado.Equals("1"))).ToPagedList(page, 4);
                    }
                }
                else
                {
                    if (estado.Equals("Todos"))
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).ToPagedList(page, 4);
                    }
                    else
                    {
                        model.listEmpresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(searchTerm).FindAll(r => r.emp_estado == (estado.Equals("1"))).ToPagedList(page, 4);
                    }


                }
            }
            if (estado != null)
            {
                model.tipo_estado = estado;
                Session["estado"] = model.tipo_estado;
            }
            else
            {
                model.tipo_estado = "Todos";
                Session["estado"] = model.tipo_estado;
            }

           
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaEmpresa", model);
            }
            return View(model);
        }
        public ActionResult Visualizar_Personal_Proyecto()
        {
            return View();
        }
        public ActionResult Gestionar_Plan_Proyecto(string searchTerm, string estado, int page = 1)
        {
            Session["ListaActPlanTemp"] = null;
            GestionarPlanProyectoViewModel model = new GestionarPlanProyectoViewModel();
            if (searchTerm == null && estado == null) {
                model.listPplans = new PlanDataAccess().sp_Consultar_Lista_Plan().ToPagedList(page, 3); }
            if (estado != null)
            {
                if (searchTerm != null)
                {
                    if (estado.Equals("Todos"))
                    {
                        model.listPplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(searchTerm).ToPagedList(page, 3);
                    }
                    else
                    {
                        model.listPplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(searchTerm).FindAll(r => (r.plan_estado_nobre == estado)).ToPagedList(page, 3);
                    }
                }
            }
            if (estado != null)
            {
                model.tipo_estado = estado;
                Session["est_plan"] = model.tipo_estado;
            }
            else
            {
                model.tipo_estado = "Todos";
                Session["est_plan"] = model.tipo_estado;
            }
            model.List_Estado_Plan = new EstadoPlanDataAccess().sp_Consultar_Lista_Estado_Plan();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaPlan", model);
            }
            return View(model);
        }

        public ActionResult AutoCompleteEmpresa(string term)
        {
            var model = new GestionarEmpresaViewModel();
            string estado = (string)Session["estado"];
            if (estado.Equals("Todos"))
            {
                model.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term);
            }
            else if (estado.Equals("1"))
            {
                model.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term).FindAll(r => (r.emp_estado == true));
            }
            else
            {
                model.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term).FindAll(r => (r.emp_estado == false));
            }


            var nameExpedientes = model.listempresas.Select(r => new
            {
                label = r.emp_razon_social
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoCompleteING(string term)
        {
            var model = new Gestionar_I_EViewModel();
            string estado = (string)Session["est_nombre"];

            model.lista_ingresos_egresos = new Ing_EgrDataAccess().sp_Consultar_Lista_Ing_Egr_Nombre(term);

            var nameExpedientes = model.lista_ingresos_egresos.Select(r => new
            {
                label = r.ing_egr_nombre
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutoComplete(string term)
        {
            var model = new ExpedienteViewModel();
            string estado = (string)Session["est_nombre"];
            if (estado.Equals("Todos"))
            {
                model.List_Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(term);
            }
            else
            {
                model.List_Expediente = new ExpedienteDataAccess().sp_Consultar_Lista_Tipo_Proyectos_Nombre(term).FindAll(r => (r.est_exp_nombre == estado));
            }

            var nameExpedientes = model.List_Expediente.Select(r => new
            {
                label = r.exp_nombre
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CompletarNombrePlanes(string term)
        {
            var model = new GestionarPlanProyectoViewModel();
            string estado = (string)Session["est_plan"];
            if (estado.Equals("Todos"))
            {
                model.listplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(term);
            }
            else
            {
                model.listplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(term).FindAll(r => (r.plan_estado_nobre == estado));
            }
            var namePlan = model.listplans.Select(r => new
            {
                label = r.plan_nombre
            });
            return Json(namePlan, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteCuenta(string term)
        {
            var model = new GestionarCuentasViewModel();
            string estado = (string)Session["estado"];
            
            if(estado.Equals("Todos"))
            {
                model.list_cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Nombre_Usuario_Cuenta(term);
            }
            else
            {
                model.list_cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Nombre_Usuario_Cuenta(term).FindAll(r => r.rol_nombre == estado);
            }

            var nameExpedientes = model.list_cuentas.Select(r => new
            {
                label = r.usu_usuario
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ObtenerNotificaciones(int usu_codigo)
        {
            NotificacionesViewModel notificaciones = new NotificacionesViewModel();
            notificaciones.list_notificaciones = new NotificacionesDataAccess().sp_Consultar_Notificaciones(usu_codigo);
            return PartialView(notificaciones);
        }
    }
}