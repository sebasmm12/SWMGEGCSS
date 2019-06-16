using SWMGEGCSS.Models;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SWMGEGCSS.Controllers
{
    public class CuentasController : Controller
    {
        // GET: Cuentas
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registrar_Cuenta()
        {
            var model = new GestionarCuentasViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Registrar_Cuenta(T_usuario_cuentas_aux cuentas, string det_usu_tip_doc, string det_usu_sexo, string tipo_det_usu_tipo, int rol_codigo, string usu_contraseña, HttpPostedFileBase imagen)
        {
            var model = new GestionarCuentasViewModel();
            model.cuentas = cuentas;
            var operationResult = new OperationResult();
            var operationResult1 = new OperationResult();
            var operationResult2 = new OperationResult();
            var modelCuentas = new UsuarioViewModel();
            operationResult = new CuentasUsuariosDataAccess().sp_Insertar_Cuenta_Usuario(model.cuentas, rol_codigo, usu_contraseña);
            modelCuentas.Usuario = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Todos_Usuarios().Find(r => r.usu_usuario == model.cuentas.usu_usuario);
            operationResult1 = new CuentasUsuariosDataAccess().sp_Insertar_Cuenta_Usuario_Detalle(model.cuentas, det_usu_tip_doc, det_usu_sexo, tipo_det_usu_tipo, modelCuentas.Usuario);
            operationResult2 = new CuentasUsuariosDataAccess().sp_Insertar_Cuenta_Usuario_Rol(model.cuentas, rol_codigo, modelCuentas.Usuario);
            ///////////Insertar imagen
            if(imagen != null && imagen.ContentLength > 0)
            {
                byte[] imagenData = null;
                using (var binaryImagen = new BinaryReader(imagen.InputStream))
                {
                    imagenData = binaryImagen.ReadBytes(imagen.ContentLength);
                }
                Imagen imagenes = new Imagen();
                imagenes.Imagenes = imagenData;
                model.cuentas.det_usu_imagem = imagenData;
                var operationReuslt3 = new OperationResult();
                operationReuslt3 = new CuentasUsuariosDataAccess().sp_Insertar_Imagen_Usuario(model.cuentas,modelCuentas.Usuario);
            }
            return RedirectToAction("Gestionar_Cuenta", "Gerente");
        }

        [HttpGet]
        public ActionResult Actualizar_Datos(int usu_codigo)
        {
            var model = new GestionarCuentasViewModel();
            model.cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Cuentas().Find(r => r.usu_codigo == usu_codigo);
            model.RolesViewModel = new RolesViewModel();
            model.RolesViewModel.list_roles = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Roles();
            return View(model);
        }
        [HttpPost]
        public ActionResult Actualizar_Datos(T_usuario_cuentas_aux usuarios, HttpPostedFileBase imagen)
        {
            var model = new GestionarCuentasViewModel();
            model.cuentas = usuarios;
            var modelCuentas = new UsuarioViewModel();
            var operationResult = new CuentasUsuariosDataAccess().sp_Actualizar_Usuario(usuarios);
            var operationResult1 = new CuentasUsuariosDataAccess().sp_Actualizar_Usuario_Rol(usuarios);
            modelCuentas.Usuario = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Todos_Usuarios().Find(r => r.usu_usuario == model.cuentas.usu_usuario);
            ///////////Insertar imagen
            if (imagen != null && imagen.ContentLength > 0)
            {
                byte[] imagenData = null;
                using (var binaryImagen = new BinaryReader(imagen.InputStream))
                {
                    imagenData = binaryImagen.ReadBytes(imagen.ContentLength);
                }
                Imagen imagenes = new Imagen();
                imagenes.Imagenes = imagenData;
                model.cuentas.det_usu_imagem = imagenData;
                var operationReuslt3 = new OperationResult();
                operationReuslt3 = new CuentasUsuariosDataAccess().sp_Insertar_Imagen_Usuario(model.cuentas, modelCuentas.Usuario);
            }
            //return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Gestionar_Cuenta", "Gerente");
        }

        public ActionResult convertirImagen(int usu_codigo)
        {
            var imagenMunicipio = new CuentasUsuariosDataAccess().sp_Consultar_Imagen_Usuario(usu_codigo);
            return File(imagenMunicipio, "image/jpeg");
        }

        [HttpGet]
        public ActionResult _verDetalles()
        {
            var model = new GestionarCuentasViewModel();
            model.cuentas = new T_usuario_cuentas_aux();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _verDetalles(int usu_codigo)
        {
            var model = new GestionarCuentasViewModel();
            model.cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Cuentas().Find(r => r.usu_codigo == usu_codigo);
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult _EliminarCuenta()
        {
            var model = new GestionarCuentasViewModel();
            model.cuentas = new T_usuario_cuentas_aux();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _EliminarCuenta(int usu_codigo)
        {
            var model = new GestionarCuentasViewModel();
            model.cuentas = new CuentasUsuariosDataAccess().sp_Consultar_Lista_Cuentas().Find(r => r.usu_codigo == usu_codigo);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult EliminarUsuario(int usu_codigo)
        {
            var model = new GestionarCuentasViewModel();
            model.cuentas = new T_usuario_cuentas_aux();
            var operationResult = new OperationResult();
            operationResult = new CuentasUsuariosDataAccess().sp_Eliminar_Usuario(usu_codigo);

            return Json(new { id = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }


    }
}