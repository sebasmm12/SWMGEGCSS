using SWMGEGCSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using PagedList;

namespace SWMGEGCSS.Controllers
{
    public class SecretarioController : Controller
    {
        // GET: Secretario
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Gestionar_Citas(string estado, int page = 1)
        {
            var model = new GestionarCitasViewModel();
            model.DetalleUsuarioModel = new DetalleUsuarioViewModel();
            model.DetalleUsuarioModel.list_usuario = new List<T_detalle_usuario>();
            model.DetalleUsuarioModel.list_usuario = new UsuarioDataAccess().sp_Consultar_Lista_Usuario();
            if (estado == null)
            {
                model.listCitas = new SecretariaDataAccess().sp_Consultar_Lista_Citas().ToPagedList(page, 4);
            }
            if(estado != null)
            {
                if (estado.Equals("1"))//pendiente
                {
                    model.listCitas = new SecretariaDataAccess().sp_Consultar_Lista_Citas().FindAll(r => r.estado_cita_id == Convert.ToInt32(estado)).ToPagedList(page, 4);
                }
                if (estado.Equals("2"))//cancelado
                {
                    model.listCitas = new SecretariaDataAccess().sp_Consultar_Lista_Citas().FindAll(r => r.estado_cita_id == Convert.ToInt32(estado)).ToPagedList(page, 4);
                }
                if (estado.Equals("3"))//No realizado
                {
                    model.listCitas = new SecretariaDataAccess().sp_Consultar_Lista_Citas().FindAll(r => r.estado_cita_id == Convert.ToInt32(estado)).ToPagedList(page, 4);
                }
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaCitas", model);
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult Registrar_Cita()
        {
            var model = new GestionarCitasViewModel();
            model.EmpresaModel = new GestionarEmpresaViewModel();
            model.EmpresaModel.listempresas = new List<T_empresa>();
            model.EmpresaModel.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas();
            model.DetalleUsuarioModel = new DetalleUsuarioViewModel();
            model.DetalleUsuarioModel.list_usuario = new List<T_detalle_usuario>();
            model.DetalleUsuarioModel.list_usuario = new UsuarioDataAccess().sp_Consultar_Lista_Usuario();
            return View(model);
        }

        [HttpPost]
        public ActionResult Registrar_Cita(T_Citas citas, string usu_citado, string cita_empresa, string cita_hora)
        {
            var model = new GestionarCitasViewModel();
            model.citas = citas;
            var operationResult = new OperationResult();
            operationResult = new SecretariaDataAccess().sp_Insertar_Cita(model.citas,cita_empresa, usu_citado, cita_hora);
            return RedirectToAction("Gestionar_Citas", "Secretario");
        }

        [HttpGet]
        public ActionResult Modificar_Cita(int cita_id)
        {
            var model = new GestionarCitasViewModel();
            model.Citas = new SecretariaDataAccess().sp_Consultar_Lista_Citas().Find(r => r.cita_id == cita_id);
            model.EmpresaModel = new GestionarEmpresaViewModel();
            model.EmpresaModel.listempresas = new List<T_empresa>();
            model.EmpresaModel.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Empresas();
            model.DetalleUsuarioModel = new DetalleUsuarioViewModel();
            model.DetalleUsuarioModel.list_usuario = new List<T_detalle_usuario>();
            model.DetalleUsuarioModel.list_usuario = new UsuarioDataAccess().sp_Consultar_Lista_Usuario();
            model.list_estado_cita = new SecretariaDataAccess().sp_Consultar_Lista_Estado_Cita();
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Modificar_Cita(T_Citas_aux cita, string cita_hora_atendido, string cita_hora , int usu_citado)
        {
            var model = new GestionarCitasViewModel();
            model.Citas = cita;
            var operationResult = new OperationResult();
            operationResult = new SecretariaDataAccess().sp_Modificar_Cita(model.Citas, cita_hora_atendido,cita_hora, usu_citado);
            return RedirectToAction("Gestionar_Citas", "Secretario");
        }

        [HttpGet]
        public ActionResult _Eliminar_Cita()
        {
            var model = new GestionarCitasViewModel();
            model.Citas = new T_Citas_aux();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _Eliminar_Cita(int cita_id)
        {
            var model = new GestionarCitasViewModel();
            Session["citaid"] = cita_id;         
            model.Citas = new SecretariaDataAccess().sp_Consultar_Lista_Citas().Find(r => r.cita_id == cita_id);
            Session["estadocita"] = model.Citas.estado_cita_id;
            return PartialView(model);

        }

        [HttpPost]
        public ActionResult EliminaCita(int id_cita, int id_estado_cita)
        {
            var model = new GestionarCitasViewModel();
            model.citas = new T_Citas();
            model.citas.cita_id = id_cita;
            model.citas.estado_cita_id = id_estado_cita;
            var operationResult = new OperationResult();
            operationResult = new SecretariaDataAccess().sp_Eliminar_Cita(model.citas);
            return Json(new { id = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult _verDetalles()
        {
            var model = new GestionarCitasViewModel();
            model.Citas = new T_Citas_aux();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _verDetalles(int id)
        {
            var model = new GestionarCitasViewModel();
            model.Citas = new SecretariaDataAccess().sp_Consultar_Lista_Citas().Find(r => r.cita_id == id);
            return PartialView(model);
        }

        public ActionResult AutoCompleteCita(string term)
        {
            var model = new GestionarCitasViewModel();
            string estado = (string)Session["est_razon_social"];
            model.EmpresaModel = new GestionarEmpresaViewModel();
            model.EmpresaModel.listempresas = new List<T_empresa>();
            model.EmpresaModel.listempresas = new EmpresaDataAccess().sp_Consultar_Lista_Nombre_Empresa(term);
            var nameEmpresas = model.EmpresaModel.listempresas.Select(r => new
            {
                label = r.emp_razon_social
            });
            return Json(nameEmpresas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Evaluar_Fecha(string cita_hora, string cita_fecha)
        {
            var hora = cita_hora.Split(':');
            var hh = Convert.ToInt32(hora[0], 10);
            //Convertir fecha 2019/05/06
            var fecha = cita_fecha.Split('-');
            

            var model = new SecretariaDataAccess().sp_Consultar_Lista_Citas();
            var modelEvaluado = model.Find(r => r.cita_fecha.ToString("hh") == Convert.ToString(hh));
            var modelEvaluadoFecha = model.Find(r => r.cita_fecha.ToString("yyyy-MM-dd") == cita_fecha);
            int cont = 0;
            if (modelEvaluado != null && modelEvaluadoFecha != null)
            {
                cont = 1;
            }
            return Json(cont, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Registrar_Ingresos_Egresos()
        {
            return View();
        }
        public ActionResult Visualizar_I_E()
        {
            return View();
        }
        public ActionResult Visualizar_Cuenta()
        {
            return View();
        }
        public ActionResult Registrar_Cuenta()
        {
            return View();
        }


    }
}