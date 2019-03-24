using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
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
            if (contador == 1)
            {
                Session["login"] = model.Usuario.usu_codigo;
                Session["rol_name"] = rol_name;
                return RedirectToAction("Ejemplo", "Home");
            }
            return View();
        }
        public ActionResult Exit()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "Account");
        }
    }
}