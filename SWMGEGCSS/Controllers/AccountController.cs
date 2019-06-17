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
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var model = new UsuarioViewModel();
            T_detalle_usuario t_d_usuario = new T_detalle_usuario();
            model.Usuario = new T_usuario();
            model.Usuario.usu_usuario = username;
            model.Usuario.usu_contraseña = password;
            var contador = new UsuarioDataAccess().sp_Encontrar_Usuario(model.Usuario,t_d_usuario);
            var contador_empresa = new UsuarioDataAccess().sp_Consultar_Sesion_Empresa(model.Usuario);
            var rol_name = new RolDataAccess().sp_Obtener_Rol_Nombre_Usuario(model.Usuario.usu_codigo);
            var l_permiso_usuario = new PermisoDataAccess().sp_Listar_Permisos_Usuario(rol_name);
            if (contador_empresa != 1)
            {
                if (contador == 1)
                {
                    Session["rol_name"] = rol_name;
                    HttpContext.Session["login"] = model.Usuario.usu_codigo;
                    HttpContext.Session["name"] = t_d_usuario.det_usu_nombre;
                    HttpContext.Session["rol_name"] = rol_name;
                    HttpContext.Session["l_permiso_usuario"] = l_permiso_usuario;
                    Session["lista_actividades"] = null;
                    if (rol_name.Equals("SECRETARIO"))
                    {
                        return RedirectToAction("Index", "Secretario");
                    }
                    if (rol_name.Equals("TRABAJADOR"))
                    {
                        return RedirectToAction("Index", "Trabajador");
                    }
                    if (rol_name.Equals("GERENTE"))
                    {
                        return RedirectToAction("Index", "Gerente");
                    }
                    return RedirectToAction("Index", "Administrador");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                HttpContext.Session["login"] = model.Usuario.usu_codigo;
                HttpContext.Session["name"] = t_d_usuario.det_usu_nombre;
                HttpContext.Session["rol_name"] = rol_name;
                HttpContext.Session["l_permiso_usuario"] = l_permiso_usuario;
                return RedirectToAction("Index", "Trabajador");
            }      
        }
        public ActionResult Exit()
        {
            HttpContext.Session.Abandon();
            HttpContext.Session.Clear();
            HttpContext.Session.RemoveAll();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult Gestionar_Datos_Personales()
        {
            int codigo = (int)Session["login"];
            var model = new DetalleUsuarioViewModel();

            model.Detalle_Usuario = new UsuarioDataAccess().sp_Consultar_Lista_Detalle_Usuarios().Find(r=>r.usu_codigo==codigo);
            return View(model);
        }


        [HttpPost]
        public ActionResult Gestionar_Datos_Personales(T_detalle_usuario usuario)
        {
     
            var model = new DetalleUsuarioViewModel();

            model.Detalle_Usuario = usuario;
            model.Detalle_Usuario.usu_codigo = (int)Session["login"];
            var operationResult = new UsuarioDataAccess().sp_Actualizar_Datos_Personales(model.Detalle_Usuario);
          //  return RedirectToAction("Index","Gerente");
             return Json(operationResult.NewId, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GENERAR_FORMULARIOS()
        {
            return RedirectToAction("G_Formulario", "Trabajador");
        }
        public ActionResult VISUALIZAR_TAREAS()
        {
            return RedirectToAction("V_Tareas", "Trabajador");
        }
        public ActionResult GESTIONAR_CITAS()
        {
            return RedirectToAction("Gestionar_Citas", "Secretario");
        }
        public ActionResult REGISTRAR_INGRESOS_EGRESOS()
        {
            return RedirectToAction("Registrar_Ingresos_Egresos", "Secretario");
        }
        public ActionResult CONSULTAR_CAPITAL()
        {
            return RedirectToAction("Consultar_Capital", "Gerente");
        }
        public ActionResult GESTIONAR_HISTORIAL()
        {
            return RedirectToAction("Gestionar_Historial", "Administrador");
        }
        public ActionResult VISUALIZAR_I_E()
        {
            return RedirectToAction("Visualizar_I_E", "Secretario");
        }
        public ActionResult VISUALIZAR_CUENTA()
        {
            return RedirectToAction("Visualizar_Cuenta", "Secretario");
        }
        public ActionResult REGISTRAR_CUENTA()
        {
            return RedirectToAction("Registrar_Cuenta", "Secretario");
        }
        public ActionResult GENERAR_REPORTES()
        {
            return RedirectToAction("Generar_Reportes", "Gerente");
        }
        public ActionResult GESTIONAR_REPORTES_DESEMPEÑO()
        {
            return RedirectToAction("Generar_Reportes_Desempeño", "Administrador");
        }
        public ActionResult VISUALIZAR_TRABAJADORES()
        {
            return RedirectToAction("Visualizar_Trabajadores", "Administrador");
        }
        public ActionResult ASIGNACION_TAREAS()
        {
            return RedirectToAction("Asignacion_Tareas", "Administrador");
        }
        public ActionResult MODIFICAR_I_E()
        {
            return RedirectToAction("Modificar_I_E", "Administrador");
        }
        public ActionResult MODIFICAR_CUENTA()
        {
            return RedirectToAction("Modificar_Cuenta", "Administrador");
        }
        public ActionResult GESTIONAR_FORMULARIOS()
        {
            return RedirectToAction("Gestionar_Formularios", "Gerente");
        }
        public ActionResult GESTIONAR_SERVICIOS()
        {
            return RedirectToAction("Gestionar_Servicios", "Gerente");
        }
        public ActionResult GESTIONAR_CUENTA()
        {
            return RedirectToAction("Gestionar_Cuenta", "Gerente");
        }
        public ActionResult GESTIONAR_PROYECTOS()
        {
            return RedirectToAction("Gestionar_Proyectos", "Gerente");
        }
        public ActionResult GESTIONAR_I_E()
        {
            return RedirectToAction("Gestionar_I_E", "Gerente");
        }
        public ActionResult GESTIONAR_EMPRESAS()
        {
            return RedirectToAction("Gestionar_Empresas", "Gerente");
        }
        public ActionResult VISUALIZAR_PERSONAL_PROYECTO()
        {
            return RedirectToAction("Visualizar_Personal_Proyecto", "Gerente");
        }
        public ActionResult GESTIONAR_PLAN_PROYECTO()
        {
            return RedirectToAction("Gestionar_Plan_Proyecto", "Gerente");
        }
        public ActionResult convertirImagen(int codigo)
        {
            var imagenMunicipio = new TrabajadorDataAccess().sp_Consultar_Imagen_Usuario(codigo);
            return File(imagenMunicipio, "image/jpeg");
        }
    }
}