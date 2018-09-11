using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BOL;
using BOL.Helpers;
using DAL;

namespace CaboFrowardMVC.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reporte1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Movimientos()
        {
            return View();
        }
        public ActionResult CondicionActual()
        {
            return View();
        }
        public JsonResult GeneraReporte1(int puerto, string fecha_inicio, string fecha_fin, int tipo_ingreso, int empresa)
        {
            var respuesta = new { mensaje = "", html = "" };

            if (fecha_inicio == null || fecha_inicio == "")
            {
                respuesta = new { mensaje = "Problema con Fecha Inicio - Revisar", html = "" };
                return Json(respuesta);
            }
            if (fecha_fin == null || fecha_fin == "")
            {
                respuesta = new { mensaje = "Problema con Fecha Fin - Revisar", html = "" };
                return Json(respuesta);
            }

            if (DateTime.Parse(fecha_inicio) > DateTime.Parse(fecha_fin))
            {
                respuesta = new { mensaje = "Fecha Inicio no puede ser mayor a Fecha Fin", html = "" };
                return Json(respuesta);
            }
            string locacion_html = "";

            try
            {
                locacion_html = Reportes.GetReporte1(puerto, DateTime.Parse(fecha_inicio), DateTime.Parse(fecha_fin), tipo_ingreso, empresa);
                // ls_perfiles = UsuarioPerfiles.GetUsuarioPerfil1(id);                
                if (locacion_html == "")
                {
                    respuesta = new { mensaje = "Sin Resultados", html = locacion_html };

                }
                else {
                    respuesta = new { mensaje = "", html = locacion_html };
                }

                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), html = "" };
                return Json(respuesta);

            }

        }
        public FileContentResult ExportToExcel()
        {
            //Ahi le pasas el data que necesites
            byte[] fileContents = Encoding.UTF8.GetBytes(Util.Exportarxml());
            return File(fileContents, "application/vnd.ms-excel", "name.xls");

        }


        public JsonResult GeneraReporteMov(int puerto, string fecha_inicio, string fecha_fin)
        {
            var respuesta = new { mensaje = "", html = "" };

            if (fecha_inicio == null || fecha_inicio == "")
            {
                respuesta = new { mensaje = "Problema con Fecha Inicio - Revisar", html = "" };
                return Json(respuesta);
            }
            if (fecha_fin == null || fecha_fin == "")
            {
                respuesta = new { mensaje = "Problema con Fecha Fin - Revisar", html = "" };
                return Json(respuesta);
            }

            if (DateTime.Parse(fecha_inicio) > DateTime.Parse(fecha_fin))
            {
                respuesta = new { mensaje = "Fecha Inicio no puede ser mayor a Fecha Fin", html = "" };
                return Json(respuesta);
            }
            if (puerto==0)
            {
                respuesta = new { mensaje = "debe seleccionar un puerto", html = "" };
                return Json(respuesta);

            }
            string reporte2 = "";
            string resp = "";
            try
            {
                reporte2 = Reportes.Getmovimientos(puerto, DateTime.Parse(fecha_inicio), DateTime.Parse(fecha_fin));
                // ls_perfiles = UsuarioPerfiles.GetUsuarioPerfil1(id);     
                if (reporte2 == "")
                {
                    resp = "Sin Resultados";
                }

                respuesta = new { mensaje = resp, html = reporte2 };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), html = "" };
                return Json(respuesta);

            }

        }


        public JsonResult GeneraReportecondicionActual(int puerto)
        {
            var respuesta = new { mensaje = "", html = "" };

          
            if (puerto == 0)
            {
                respuesta = new { mensaje = "debe seleccionar un puerto", html = "" };
                return Json(respuesta);

            }
            string reporte3 = "";
            string resp = "";
            try
            {


                reporte3 = Reportes.Getsituacionactual(puerto);
                // ls_perfiles = UsuarioPerfiles.GetUsuarioPerfil1(id);       
                if (reporte3 == "") {
                    resp = "Sin Resultados";
                }

                respuesta = new { mensaje = resp, html = reporte3 };

                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), html = "" };
                return Json(respuesta);

            }

        }
    }
}