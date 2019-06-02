using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWMGEGCSS.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Agregar_Plan_de_Proyectos(string tipo_servicio_nombre)
        {
            var model = new GestionarPlanProyectoViewModel();
            if (tipo_servicio_nombre == null)
            {
                //
            }
            if (tipo_servicio_nombre != null && tipo_servicio_nombre == "")
            {
                 //
            }
            else
            {
                var tipoServicioModel = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find(x => (x.tipo_servicio_nombre == tipo_servicio_nombre));
                if (tipoServicioModel != null) {
                    model.List_Actividades = new ActividadesDataAccess().sp_Consultar_Actividades_Diferentes_Plan(tipoServicioModel.tipo_servicio_id);
                    /*List<int> cantidadesActividadesPermitidas = new List<int>();
                    foreach (var item in model.List_Actividades)
                    {
                        var actPlanRest = item.act_cantidad_maxima;
                        cantidadesActividadesPermitidas.Add(actPlanRest);
                    }
                    Session["ListaCantidadPermitida"] = cantidadesActividadesPermitidas;*/
                }
                else
                {
                    //
                }
            }
            if (Request.IsAjaxRequest())
            {
                model.List_Actividades_planeadas = new List<T_actividades_planeadas>();
                Session["ListaActividades"] = null;
                //Session["ListaCantidadPermitida"] = null;
                return PartialView("_ListarActividadesPlan", model);
            }
            return View(model);
        }
        /*public ActionResult Agregar_Plan_de_Proyectos_1()
        {
            var model = new GestionarPlanProyectoViewModel();
            T_plan modelPlan = (T_plan)Session["Plan_datos"];
            model.List_Actividades = new PlanDataAccess().sp_Consultar_Actividades_Plan(modelPlan.tipo_servicio_id);
            return View(model);
        }
        [HttpPost]
        public ActionResult _ModalAgregarActividadesPlanificadas()
        {
            var model = new GestionarPlanProyectoViewModel();
            T_plan modelPlan = (T_plan)Session["Plan_datos"];
            model.plans = new PlanDataAccess().sp_Consultar_Plan_Por_Nombre(modelPlan.plan_nombre);
            model.List_Actividades = new PlanDataAccess().sp_Consultar_Actividades_Plan(modelPlan.tipo_servicio_id);
            //var v = model.List_Actividades.;
            model.Actividad_planeada = new T_actividades_planeadas();
            model.Actividad_planeada.plan_id = model.plans.plan_id;
            model.Actividad_planeada.act_id = /*consultar sobre;
            return PartialView("_ModalAgregarActividadesPlanificadas", model);
        }
        [HttpPost]
        public ActionResult _ModalRegistrarActividades1PlanificadasFinal(T_actividades_planeadas act_plan)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.Actividad_planeada = new T_actividades_planeadas();
            model.Actividad_planeada.plan_id = act_plan.plan_id;
            model.Actividad_planeada.act_id = act_plan.act_id;
            model.Actividad_planeada.act_plan_nombre = act_plan.act_plan_nombre;
            model.Actividad_planeada.act_plan_descripcion = act_plan.act_plan_descripcion;
            model.Actividad_planeada.act_plan_costo = act_plan.act_plan_costo;
            model.Actividad_planeada.act_plan_tiempo = act_plan.act_plan_tiempo;
            var operationResult = new ActividadesDataAccess().sp_registrar_actividades_planeadas(model.Actividad_planeada);
            return Json(act_plan.plan_id, JsonRequestBehavior.AllowGet);
        }*/
        [HttpPost]
        public ActionResult Agregar_Plan_de_Proyectos(T_plan_aux plans_aux)
        {
            var operationResultp = new OperationResult();
            if (Session["ListaActividades"] == null)
            {
                operationResultp.NewId = 5;
                return Json( operationResultp.NewId, JsonRequestBehavior.AllowGet);
            }
            /*var planesmodel = new PlanDataAccess().sp_Consultar_Lista_Plan();*/
            var model = new GestionarPlanProyectoViewModel();
            model.plans_aux = plans_aux;

            var tipoServicioModel = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find(x => (x.tipo_servicio_nombre == model.plans_aux.tipo_servicio_nombre));
            var empresaModel = new PlanDataAccess().sp_Consultar_Lista_Empresa().Find(y => (y.emp_razon_social == model.plans_aux.emp_razon_social));

            var modelPlan = new T_plan();
            modelPlan.usu_codigo = (int)Session["login"];
            modelPlan.plan_nombre = model.plans_aux.plan_nombre;
            modelPlan.plan_fecha = model.plans_aux.plan_fecha;
            modelPlan.emp_ruc = empresaModel.emp_ruc;
            //modelPlan.plan_costo = model.plans_aux.plan_costo;
            modelPlan.tipo_servicio_id = tipoServicioModel.tipo_servicio_id;
            //modelPlan.plan_tiempo = model.plans_aux.plan_tiempo;
            var operationResult1 = new PlanDataAccess().sp_Agregar_Plan(modelPlan);
            //Session["Plan_datos"] = modelPlan;
            var planId = new PlanDataAccess().sp_Consultar_Lista_Plan().Find(z => (z.plan_nombre == modelPlan.plan_nombre));
            var listaActividad = (List<T_actividades_planeadas>)Session["ListaActividades"];
            var costototal = 0.0;
            var tiempototal = 0;
            foreach (var item in listaActividad)
            {
                item.plan_id = planId.plan_id;
            }
            foreach (var item in listaActividad)
            {
                var operationResult2 = new ActividadesDataAccess().sp_registrar_actividades_planeadas(item);
                costototal += item.act_plan_costo;
                tiempototal += item.act_plan_tiempo;
            }
            var operationResult3 = new PlanDataAccess().sp_Actualizar_Costo_Tiempo_Actividades(costototal,tiempototal, planId.plan_id);
            if (operationResult3.NewId == 1)
            {
                String ruta = Server.MapPath("~/Repositorio/" + modelPlan.emp_ruc + "/" + planId.plan_id);
                String ruta2 = Server.MapPath("~/Repositorio/" + modelPlan.emp_ruc);
                if (!System.IO.File.Exists(ruta2))
                {
                    System.IO.Directory.CreateDirectory(ruta);
                }
                else
                {
                    System.IO.Directory.CreateDirectory(ruta);
                }
            }
            return Json(operationResult3.NewId, JsonRequestBehavior.AllowGet);
            //redireccion al Agregar_plan_1
            //return Json(new { data = 1}, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Actualizar_Plan_de_Proyectos(int id)
        {
            /*var model = new GestionarPlanProyectoViewModel();*/
            var model = new GestionarPlanProyectoViewModel();
            //se obtiene el plan seleccionado
            model.plans_aux = new PlanDataAccess().sp_Consultar_Lista_Plan().Find(r => (r.plan_id == id));
            Session["nombrePlanActual"] = (string)model.plans_aux.plan_nombre;
            //obtener estados

            //se obtiene los valores del tipo servicio del plan seleccionado
            var tipoServicioModel = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find(x => (x.tipo_servicio_nombre == model.plans_aux.tipo_servicio_nombre));
            //se obtiene la lista de actividades segun el tipo de servicio del plan seleccionado
            model.List_Actividades = new ActividadesDataAccess().sp_Consultar_Actividades_Diferentes_Plan(tipoServicioModel.tipo_servicio_id);
            
            //se obtiene las actividades planificadas previamente
            model.List_Actividades_planeadas_aux = new  ActividadesDataAccess().sp_Consultar_Lista_Actividades_Planeadas_aux().FindAll(r => (r.plan_nombre == model.plans_aux.plan_nombre));
            model.List_Actividades_planeadas = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().FindAll(r => (r.plan_id == id));

            /*restar valores*/

            var costoTotalPlan = 0.0;
            var tiempoTotalPlan = 0;
            /*selector de lista o de session que varia  dependiendo del contenido*/
            var ListaActPlanTemp = (List<T_actividades_planeadas>)Session["ListaActPlanTemp"];
            if (ListaActPlanTemp != null)
            {
                model.List_Actividades_planeadas = ListaActPlanTemp;
                foreach (var it in ListaActPlanTemp)
                {
                    costoTotalPlan = costoTotalPlan + it.act_plan_costo;
                    tiempoTotalPlan = tiempoTotalPlan + it.act_plan_tiempo;
                }

            }
            else
            {
                ListaActPlanTemp = model.List_Actividades_planeadas;
                foreach (var it in model.List_Actividades_planeadas_aux)
                {
                    costoTotalPlan = costoTotalPlan + it.act_plan_costo;
                    tiempoTotalPlan = tiempoTotalPlan + it.act_plan_tiempo;
                }
            }

            //procesos para actualizacion de actividades permitidas
            var listaActividades = model.List_Actividades;
            List<int> cantidadesActividadesPermitidas = new List<int>();
            foreach (var item in listaActividades)
            {
                var actPerTemp = ListaActPlanTemp.FindAll(X => X.act_id == item.act_id).Count();
                var actPlanRest = item.act_cantidad_maxima - actPerTemp;
                cantidadesActividadesPermitidas.Add(actPlanRest);
            }

            Session["ListaActCantPermitida"] = cantidadesActividadesPermitidas;
            Session["ListaActPlanTemp"] = ListaActPlanTemp;
            Session["costoTotalPlan"] = costoTotalPlan;
            Session["tiempoTotalPlan"] = tiempoTotalPlan;

            /*listado de Servicios: al trataarse de otra view model dentro del plan view model, se debe inicializar*/
            model.tipoServicioModel = new TipoServicioViewModel();
            model.tipoServicioModel.ListtipoServicio = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio();
            model.lista_plan_estado = new PlanDataAccess().sp_Consultar_Lista_Plan_Estado();
            return View(model);
            
        }
        [HttpPost]
        public ActionResult Actualizar_Plan_de_Proyectos(T_plan_aux plans_aux)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.plans_aux = plans_aux;
            //var nombreTipoServicio = (int)Session["tipoServicioNombre"];

            var tipoServicioModel = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find(x => (x.tipo_servicio_nombre == model.plans_aux.tipo_servicio_nombre));
            var empresaModel = new PlanDataAccess().sp_Consultar_Lista_Empresa().Find(y => (y.emp_razon_social == model.plans_aux.emp_razon_social));
            var planEstadoModel = new PlanDataAccess().sp_Consultar_Lista_Plan_Estado().Find(z => (z.plan_estado_nombre == model.plans_aux.plan_estado_nobre));

            var modelPlan = new T_plan();
            modelPlan.plan_id = model.plans_aux.plan_id;
            modelPlan.plan_nombre = model.plans_aux.plan_nombre;
            modelPlan.plan_fecha = model.plans_aux.plan_fecha;
            modelPlan.emp_ruc = empresaModel.emp_ruc;
            modelPlan.plan_estado = planEstadoModel.plan_estado_id;
            modelPlan.plan_costo = model.plans_aux.plan_costo;

            modelPlan.tipo_servicio_id = tipoServicioModel.tipo_servicio_id;
            modelPlan.plan_tiempo = model.plans_aux.plan_tiempo;
            /*eliminacion y creacion de actividades planificadas*/
            var ListaActPlan     = (List<T_actividades_planeadas>)Session["ListaActPlanTemp"];

            var eliminar = new ActividadesDataAccess().sp_eliminar_actividades_planeadas(modelPlan.plan_id);
            foreach (var item in ListaActPlan)
            {
                var creacion = new ActividadesDataAccess().sp_registrar_actividades_planeadas(item);
            }   
            ListaActPlan = null;         
            Session["ListaActPlanTemp"] = ListaActPlan;
            Session["tipoServicioNombre"] = null;
            var operationResult = new PlanDataAccess().sp_Actualizar_Plan(modelPlan);
            //return View( "Gestionar_Plan_Proyecto");
            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);   
        }
        public ActionResult CompletarNombrePlanes(string term)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.listplans = new PlanDataAccess().sp_Consultar_Lista_Tipo_Nombre_Planes(term);
            var nameExpedientes = model.listplans.Select(r => new
            {
                label = r.plan_nombre
            });
            return Json(nameExpedientes, JsonRequestBehavior.AllowGet);
        }

        //*Metodos para el autocompletado*//
        public ActionResult CompletarNombreEmpresas([Bind(Prefix = "term")]string term)
        {
            var model = new GestionarEmpresaViewModel();
            model.listempresas = new PlanDataAccess().sp_Consultar_Lista_Nombre_Empresa(term);
            var nameEmpresas = model.listempresas.Select(r => new
            {
                label = r.emp_razon_social
            });
            return Json(nameEmpresas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CompletarNombreEstadoPlan(string term)
        {
            var model = new List<T_plan_estado>();
            model = new PlanDataAccess().Sp_Consultar_Lista_Estado_Plan(term);
            var namePlanEstado = model.Select(r => new
            {
                label = r.plan_estado_nombre
            });
            return Json(namePlanEstado, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CompletarNombreTipoServicio([Bind(Prefix = "term")]string term)
        {
            var model = new List<T_tipo_servicio>();
            model = new PlanDataAccess().sp_Consultar_Lista_Nombre_Tipo_Servicio(term);
            var nameTipoServicio = model.Select(r => new
            {
                label = r.tipo_servicio_nombre
            });
            return Json(nameTipoServicio, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _Eliminar_Plan_De_Proyecto()
        {
            var model = new GestionarPlanProyectoViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _Eliminar_Plan_De_Proyecto(int id)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.plans_aux = new PlanDataAccess().sp_Consultar_Lista_Plan().Find(r => (r.plan_id == id));
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult EliminarPlanDeProyecto(int id, string comentario)
        {
            var operationResult = new PlanDataAccess().sp_Cancelar_Plan(id, comentario);
            return Json(new { id = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Nombre_Plan(string plan_nombre)
        {
            var model = new PlanDataAccess().sp_Consultar_Lista_Plan();
            var modelEvaluado = model.Find(r => r.plan_nombre == plan_nombre);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Nombre_Empresa_Plan(string plan_emp)
        {
            var model = new EmpresaDataAccess().sp_Consultar_Lista_Empresas();
            var modelEvaluado = model.Find(r => r.emp_razon_social == plan_emp);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Evaluar_Tipo_Servicio_Plan(string plan_tipo_servicio)
        {
            var model = new TipoServicioDataAccess().sp_Consultar_Lista_Tipo_Servicio();
            var modelEvaluado = model.Find(r => r.tipo_servicio_nombre == plan_tipo_servicio);
            int cont = 0;
            if (modelEvaluado != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ModalActualizarActividadesPlanificadas()
        {
            var model = new GestionarPlanProyectoViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalActualizarActividadesPlanificadas(int act_plan_id)
        {
            var model = new GestionarPlanProyectoViewModel();
            //model.Actividades_planeadas_aux = new ActividadesDataAccess().sp_Consultar_Lista_Actividades_Planeadas_aux().Find(c => (c.act_plan_id == act_plan_id));
            var listaActividadesPlaneadas = (List<T_actividades_planeadas>)Session["ListaActPlanTemp"];
            model.Actividad_planeada = listaActividadesPlaneadas.Find(x => (x.act_plan_id == act_plan_id));
            Session["plan_id"] = model.Actividad_planeada.plan_id;
            Session["act_id"] = model.Actividad_planeada.act_id;

            var ActPlazo = new ActividadesDataAccess().sp_Consultar_Actividades().Find(X => (X.act_id == model.Actividad_planeada.act_id));
            model.ActividadesModel = new ActividadViewModel();
            model.ActividadesModel.Actividades = ActPlazo;

            var plan_id = model.Actividad_planeada.plan_id;

            var plan = new PlanDataAccess().sp_Consultar_Lista_Plan().Find(X => X.plan_id == plan_id);
            var tipo_servicio = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find(X => X.tipo_servicio_nombre == plan.tipo_servicio_nombre);

            model.tipo_servicio_act = new ActividadesDataAccess().sp_consultar_lista_tipo_servicio_actividades().Find(X => (X.act_id == model.Actividad_planeada.act_id) && (X.tipo_servicio_id == tipo_servicio.tipo_servicio_id));


            //ViewBag.plazo1 = ActPlazo.act_plazo;
            return PartialView("_ModalActualizarActividadesPlanificadas",model);
        }
        [HttpPost]
      public ActionResult _ModalActualizarActividadesPlanificadasFinal( T_actividades_planeadas act_plan)
        {
            T_actividades_planeadas actividadesPlaneadas = new T_actividades_planeadas();
            /*var actplan = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().Find(x => x.act_plan_id == act_plan.act_plan_id);*/
            List<T_actividades_planeadas> listaActividadesPlaneadasTemp = new List<T_actividades_planeadas>();
            listaActividadesPlaneadasTemp = (List<T_actividades_planeadas>)Session["ListaActPlanTemp"];

            int actId = (int)Session["act_id"];
            int planId = (int)Session["plan_id"];

            var actplan = listaActividadesPlaneadasTemp.Find(x => x.act_plan_id == act_plan.act_plan_id);
            actividadesPlaneadas.act_plan_id = act_plan.act_plan_id;
            actividadesPlaneadas.plan_id = planId;
            actividadesPlaneadas.act_id = actId;
            actividadesPlaneadas.act_plan_nombre = act_plan.act_plan_nombre;
            actividadesPlaneadas.act_plan_descripcion = act_plan.act_plan_descripcion;
            actividadesPlaneadas.act_plan_costo = act_plan.act_plan_costo;
            actividadesPlaneadas.act_plan_tiempo = act_plan.act_plan_tiempo;

            /*identificando y almacenado el objeto actualizado*/
            T_actividades_planeadas actPlanTemp = new T_actividades_planeadas();
            foreach (var item in listaActividadesPlaneadasTemp)
            {
                if (item.act_plan_id == actividadesPlaneadas.act_plan_id)
                {
                    actPlanTemp = item;                  
                }
            }

            Session["plan_id"] = null;
            Session["act_id"] = null;

            listaActividadesPlaneadasTemp.Remove(actPlanTemp);
            listaActividadesPlaneadasTemp.Add(actividadesPlaneadas);
            Session["ListaActPlanTemp"] = listaActividadesPlaneadasTemp;
            /*var operationResult = new ActividadesDataAccess().sp_actualizar_actividades_planeadas(actividadesPlaneadas);*/
            return Json(actplan.plan_id, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ModalEliminarActividadesPlanificadas()
        {
            var model = new GestionarPlanProyectoViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalEliminarActividadesPlanificadas(int act_plan_id)
        {
            var model = new GestionarPlanProyectoViewModel();
            var listaActividadesPlaneadas = (List<T_actividades_planeadas>)Session["ListaActPlanTemp"];
            model.Actividad_planeada = listaActividadesPlaneadas.Find(x => (x.act_plan_id == act_plan_id));

            return PartialView("_ModalEliminarActividadesPlanificadas", model);
        }
        [HttpPost]
        public ActionResult _ModalEliminarActividadesPlanificadasFinal(int act_plan_id)
        {
            var model = new GestionarPlanProyectoViewModel();
            var actPlanTemp = new T_actividades_planeadas();
            var listaActividadesPlaneadas = (List<T_actividades_planeadas>)Session["ListaActPlanTemp"];
            foreach (var item in listaActividadesPlaneadas)
            {
                if(item.act_plan_id == act_plan_id)
                {
                    actPlanTemp = item; break;
                }
            }
            listaActividadesPlaneadas.Remove(actPlanTemp);
            Session["ListaActPlanTemp"] = listaActividadesPlaneadas;

            /*model.Actividad_planeada = listaActividadesPlaneadas.Find(x => (x.act_plan_id == act_plan_id));
            var actplan = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().Find(x => x.act_plan_id == act_plan_id);
            var operationResult = new ActividadesDataAccess().sp_eliminar_actividades_planeadas(act_plan_id);*/
            return Json(actPlanTemp.plan_id, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult _ModalAgregarActividadesPlanificadas()
        {
            /*no recibe paraetros, llama a la vista partcial con un model*/
            var model = new GestionarPlanProyectoViewModel();
            return PartialView(model);
        }

        [HttpPost]/*ddd*/
        public ActionResult _ModalAgregarActividadesPlanificadas(int plan_id, int act_id)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.Actividad_planeada = new T_actividades_planeadas();
            model.Actividad_planeada.plan_id = plan_id;
            model.Actividad_planeada.act_id = act_id;
            model.ActividadesModel = new ActividadViewModel();
            var ActPlazo= new ActividadesDataAccess().sp_Consultar_Actividades().Find(X => (X.act_id == model.Actividad_planeada.act_id));

            var plan = new PlanDataAccess().sp_Consultar_Lista_Plan().Find(X => X.plan_id == plan_id);
            var tipo_servicio = new PlanDataAccess().sp_Consultar_Lista_Tipo_Servicio().Find(X => X.tipo_servicio_nombre == plan.tipo_servicio_nombre);

            model.tipo_servicio_act = new ActividadesDataAccess().sp_consultar_lista_tipo_servicio_actividades().Find(X => (X.act_id == model.Actividad_planeada.act_id) && (X.tipo_servicio_id == tipo_servicio.tipo_servicio_id));
            //Session["servicioActCosto"] = model.tipo_servicio_act.costo;
            return PartialView("_ModalAgregarActividadesPlanificadas", model);
        }
        [HttpPost]
        public ActionResult _ModalRegistrarActividadesPlanificadasFinal(T_actividades_planeadas act_plan)
        {         
            var model   = new GestionarPlanProyectoViewModel();
            //List<T_actividades_planeadas> ListaActividadesPlaneadasAux = new List<T_actividades_planeadas>();
            //ListaActividadesPlaneadasAux = (List<T_actividades_planeadas>)ViewBag.ListaActPlaneadasAux;
            //Session["servicioActCosto"] = null;
            List<T_actividades_planeadas> ListaActividadesPlaneadasTemp = new List<T_actividades_planeadas>();  
            ListaActividadesPlaneadasTemp = (List<T_actividades_planeadas>)Session["ListaActPlanTemp"];        
            //ListaActividadesPlaneadasTempCreado = (List<T_actividades_planeadas>)Session["ListaActPlanTempCreado"];
            model.Actividad_planeada = new T_actividades_planeadas();
            model.Actividad_planeada.plan_id = act_plan.plan_id;
            model.Actividad_planeada.act_id = act_plan.act_id;
            model.Actividad_planeada.act_plan_nombre = act_plan.act_plan_nombre;
            model.Actividad_planeada.act_plan_descripcion = act_plan.act_plan_descripcion;
            model.Actividad_planeada.act_plan_costo = act_plan.act_plan_costo;
            model.Actividad_planeada.act_plan_tiempo = act_plan.act_plan_tiempo;

            /*asignacion de act_plan_id temporal para los que se van a agregar*/
            var act_plan_id = ListaActividadesPlaneadasTemp.Count();
            for (int i = 0; i <= ListaActividadesPlaneadasTemp.Count() - 1; i++)
            {
                if(act_plan_id == ListaActividadesPlaneadasTemp[i].act_plan_id)
                {
                    act_plan_id = act_plan_id + 1;
                    i = 0;
                }
            }
            model.Actividad_planeada.act_plan_id = act_plan_id;
            
            ListaActividadesPlaneadasTemp.Add(model.Actividad_planeada);              
            Session["ListaActPlanTemp"] = ListaActividadesPlaneadasTemp;
            return Json(act_plan.plan_id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Evaluar_Nombre_Plan_Actualizar(string plan_nombre)
        {
            var model = new PlanDataAccess().sp_Consultar_Lista_Plan();
            var modelEvaluado = model.Find(r => r.plan_nombre == plan_nombre);
            int cont = 0;
            if (modelEvaluado != null)
            {
                if (modelEvaluado.plan_nombre != (string)Session["nombrePlanActual"])
                {
                    cont = 1;
                }
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult _ModalRegistrarActividadesPlanificadas(/*int id_act*/)
        {
            var model = new GestionarPlanProyectoViewModel();
            model.Actividades_planeadas = new T_actividades_desarrollar();
            //var modeloevaluar = new ActividadesDataAccess().sp_consultar_lista_tipo_servicio_actividades().Find(r => r.act_id == id_act);
            //model.tipo_servicio_act = modeloevaluar;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult _ModalRegistrarActividadesPlanificadas(T_actividades_planeadas act_plan)
        {
            //Session["ListaCantidadPermitida"]
            T_actividades_planeadas actividadesPlaneadas = new T_actividades_planeadas();
            /*var actplan = new ActividadesDataAccess().sp_Consultar_Listar_Actividades_Planeadas().Find(x => x.act_plan_id == act_plan.act_plan_id);*/
            List<T_actividades_planeadas> listaActividadesPlaneadasTemp = new List<T_actividades_planeadas>();
            if (Session["ListaActividades"] == null)
            {
                listaActividadesPlaneadasTemp.Add(act_plan);

            }
            else
            {
                listaActividadesPlaneadasTemp = (List<T_actividades_planeadas>)Session["ListaActividades"];
                listaActividadesPlaneadasTemp.Add(act_plan);

            }
            Session["ListaActividades"] = listaActividadesPlaneadasTemp;

            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}