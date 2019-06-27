using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;using System.Web.Mvc;
using SWMGEGCSS_DA;
using PagedList;
using SWMGEGCSS_EN;
using SWMGEGCSS.Models;
using System.Net.Mail;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace SWMGEGCSS.Controllers
{
    public class EvaluarFormularioController : Controller
    {
        // GET: EvaluarFormulario
        public ActionResult Index(int page = 1)
        {

            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            model.Lista_Paginada_actividades_desarrollar = new ActividadesDesarrollarDataAccess().sp_Consultar_Lista_Actividades_Desarrollar_Revisar_Gerente(Int32.Parse(Session["login"].ToString())).ToPagedList(page, 10);

            return View(model);
        }
      
        [HttpGet]
        public ActionResult RevisarFormulario(int id)
        {
            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            model.actividades_desarrollarGerente = new ActividadesDesarrollarDataAccess().sp_Consultar_Actividad_Desarrollar_Gerente(id);
            Session["ArchivoId"] = id;
            model.Lista_observaciones_actividades = new ObservacionesDataAccess().sp_Consultar_Observaciones_No_Resueltas(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult RevisarFormulario(T_actividades_desarrollar_gerente actividades_desarrollarGerente)
        {
            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            var operationResult = new OperationResult();
            actividades_desarrollarGerente.est_act_id = 5;
            operationResult = new ActividadesDesarrollarDataAccess().sp_Actualizar_Actividades_Desarrollar_Gerente(actividades_desarrollarGerente);

            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RevisarFormulario2(T_actividades_desarrollar_gerente actividades_desarrollarGerente)
        {
            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            var operationResult = new OperationResult();
            actividades_desarrollarGerente.est_act_id = 6;
            operationResult = new ActividadesDesarrollarDataAccess().sp_Actualizar_Actividades_Desarrollar_Gerente(actividades_desarrollarGerente);

            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }



        public FileResult DescargarArchivo()
        {
            var valores = new TrabajadorDataAccess().sp_Consultar_Ruc_Plan_por_Act_Desa((int)Session["ArchivoId"]);
            string nombre = valores.act_desa_archivo_nombre;
            String rut = Server.MapPath("~/Repositorio/" + valores.emp_ruc + "/" + valores.plan_id.ToString() + "/" + nombre);
            String[] lista = nombre.Split('.');
            string extension = lista[lista.Length - 1];
            string tipo = "";
            switch (extension)
            {
                case "txt":
                    tipo = "application/txt";
                    break;
                case "pdf":
                    tipo = "application/pdf";
                    break;
                case "xls":
                    tipo = "application/vnd.ms-excel";
                    break;
                case "rar":
                    tipo = "application/x-rar-compressed";
                    break;
                case "zip":
                    tipo = "application/zip";
                    break;
                case "doc":
                    tipo = "application/msword";
                    break;
                case "xlsx":
                    tipo = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case "docx":
                    tipo = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                default:
                    tipo = "application/octet-stream";
                    break;
            }
            return File(rut, tipo, valores.act_desa_archivo_nombre);
        }

        [HttpGet]
        public ActionResult AgregarObservacion()
        {
            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            int id = Int32.Parse(Session["ArchivoId"].ToString());
            model.actividades_desarrollarGerente = new ActividadesDesarrollarDataAccess().sp_Consultar_Actividad_Desarrollar_Gerente(id);
            model.observacion_actividad = new T_observacion_actividades();
            return View(model);
        }

        [HttpPost]
        public ActionResult AgregarObservacion(T_observacion_actividades observaciones_actividades, T_actividades_desarrollar_gerente actividades_desarrollarGerente)
        {
            var operationResult = new OperationResult();
            observaciones_actividades.act_desa_id = actividades_desarrollarGerente.act_desa_id;
            operationResult = new ObservacionesDataAccess().sp_registrar_observacion(observaciones_actividades);

            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ModificarObservacion(int id)
        {
            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            model.observacion_actividad = new ObservacionesDataAccess().sp_Consultar_Observacion(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult ModificarObservacion(T_observacion_actividades observaciones_actividades)
        {
            var operationResult = new OperationResult();
            observaciones_actividades.obs_act_est_id = 1;
            operationResult = new ObservacionesDataAccess().sp_actualizar_observacion(observaciones_actividades);
            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ModificarObservacion2(T_observacion_actividades observaciones_actividades)
        {
            var operationResult = new OperationResult();
            observaciones_actividades.obs_act_est_id = 3;
            operationResult = new ObservacionesDataAccess().sp_actualizar_observacion(observaciones_actividades);
            return Json(new { data = operationResult.NewId }, JsonRequestBehavior.AllowGet);
        }
            
        [HttpGet]
        public ActionResult ReporteRenddimiento()
        {
            EvaluarFormularioViewModel model = new EvaluarFormularioViewModel();
            model.Lista_reportes = new ReporteRendimientoDataAccess().sp_Consultar_Lista_Reporte_Rendimiento();
            ReportDocument rp = new ReportDocument();
            //rp.Load(Path.Combine(Server.MapPath("~/Reporte"), "reporteProyecto.rpt"));
            rp.Load(@"C:\Users\hp\Desktop\Ing. Informatica Ciclo 2019 - I\Taller de Proyectos II\TP-2\SWMGEGCSS_EN\Reporte\ReporteDeRendimientoGeneral.rpt");
            rp.SetDataSource(model.Lista_reportes);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rp.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ReporteRendimiento.pdf");
        }


        [HttpGet]
        public ActionResult Correo()
        {
            //Ejemplo para lo del envio de correos
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.To.Add("gerhard.egg@gmail.com");//Destinatario
            msg.From = new MailAddress("a.no.n.im.o@hotmail.com", "Anonimo Anonimo anonimo", System.Text.Encoding.UTF8);//Emisor y nombre de usuario
            msg.Subject = "Olvido de Contraseña";//Asunto del mensaje
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = "Su mensaje";//Mensaje a ser enviado
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            /*  if (sw == true)
              {
                  String sFile = archivo;//conine la ruta del archivo Adjunto
                  Attachment oAttch = new Attachment(sFile);
                  msg.Attachments.Add(oAttch);//adjuntamos el archivo al mensaje
              }*/
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("a.no.n.im.o@hotmail.com", "qwepoi10");
            //hotmail
            client.Port = 587; // ùerto de envio tanto de Hotmail como para Gmail
            client.Host = "smtp.live.com";// Protocolo Simple de Transferencia de Correo de (Hotmail)
                                          //
            client.EnableSsl = true;
            client.Send(msg);
            try
            {
                //Enviamos el mensaje
                // MessageBox.Show("Mensaje Enviado Correctamente", "Correo C#", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //   sw = false;
            }
            catch (System.Net.Mail.SmtpException)
            {
                //   MessageBox.Show("Error");
            }
            return Json(new { data = 1 }, JsonRequestBehavior.AllowGet);
        }



    }
}