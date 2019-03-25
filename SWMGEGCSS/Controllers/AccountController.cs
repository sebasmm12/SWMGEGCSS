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
            model.Usuario = new T_usuario();
            model.Usuario.usu_usuario = username;
            model.Usuario.usu_contraseña = password;
            var contador = new UsuarioDataAccess().sp_Encontrar_Usuario(model.Usuario);
            var rol_name = new RolDataAccess().sp_Obtener_Rol_Nombre_Usuario(model.Usuario.usu_codigo);
            var l_permiso_usuario = new PermisoDataAccess().sp_Listar_Permisos_Usuario(rol_name);
            if (contador == 1)
            {
                HttpContext.Session["login"] = model.Usuario.usu_codigo;
                HttpContext.Session["rol_name"] = rol_name;
                HttpContext.Session["l_permiso_usuario"] = l_permiso_usuario;
                return RedirectToAction("Index", "Trabajador");
            }
            return View();
        }
        public ActionResult Exit()
        {
            HttpContext.Session.Abandon();
            HttpContext.Session.Clear();
            HttpContext.Session.RemoveAll();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult GENERAR_FORMULARIOS()
        {
            return RedirectToAction("G_Formulario", "Trabajador");
        }
        public ActionResult VISUALIZAR_TAREAS()
        {
            return RedirectToAction("V_Tareas", "Trabajador");
        }
    }
}