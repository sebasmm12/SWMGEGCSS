using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SWMGEGCSS_DA;
using SWMGEGCSS_EN;
using PagedList;
using SWMGEGCSS.Models;

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
        public ActionResult Registrar_Cuenta(T_usuario_cuentas_aux cuentas, string det_usu_tip_doc, string det_usu_sexo, string tipo_det_usu_tipo, int rol_codigo)
        {
            var model = new GestionarCuentasViewModel();
            model.cuentas = cuentas;
            var operationResult = new OperationResult();
            var operationResult1 = new OperationResult();
            operationResult = new CuentasUsuariosDataAccess().sp_Insertar_Cuenta_Usuario_Detalle(model.cuentas, det_usu_tip_doc, det_usu_sexo, tipo_det_usu_tipo);
            operationResult1 = new CuentasUsuariosDataAccess().sp_Insertar_Cuenta_Usuario(model.cuentas, rol_codigo);
            return RedirectToAction("Gestionar_Cuenta", "Gerente");
        }
    }
}