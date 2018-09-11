using BOL;
using CaboFrowardMVC.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

using BOL.Helpers;
using System.Text;

namespace CaboFrowardMVC.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }


			var agent = Request.UserAgent.ToLower();
			if (agent.Contains("ios"))
			{
				return RedirectToAction("Index", "AccesoClientes");
				// It is an IOS device, redirect to the specified location here
			}
			else if (agent.Contains("android"))
			{
				return RedirectToAction("Index", "AccesoClientes");
				// It is an Android device, redirect to the specified location here
			}
			else
			{

				// It is another device, redirect to the specified location here
				return View();
			}


			
        }
        

        [HttpPost]
        public JsonResult Resumen(int puerto)
        {
            var respuesta = new { mensaje = "", ingresos = 0, vehiculos =0 , solicitudes=0};          
            DashBoard Dash = new DashBoard();            
            try
            {
                Dash = Mantenedores.Resumen(puerto);
                respuesta = new { mensaje = "", ingresos = Dash.Ingresos, vehiculos = Dash.Vehiculos, solicitudes = Dash.Solicitudes };
                return Json(respuesta);
            }
            catch (Exception ex)
            {

                respuesta = new { mensaje = ex.Message.ToString(), ingresos = 0, vehiculos = 0, solicitudes = 0 };
                return Json(respuesta);
            }
        }


        [HttpPost]
        public JsonResult GetSolicitudes(int puerto)
        {
            var respuesta = new { mensaje = "", ingresos = 0, vehiculos = 0, solicitudes = 0 };
            DashBoard Dash = new DashBoard();
            try
            {
                Dash = Mantenedores.Resumen(puerto);
                respuesta = new { mensaje = "", ingresos = Dash.Ingresos, vehiculos = Dash.Vehiculos, solicitudes = Dash.Solicitudes };
                return Json(respuesta);
            }
            catch (Exception ex)
            {

                respuesta = new { mensaje = ex.Message.ToString(), ingresos = 0, vehiculos = 0, solicitudes = 0 };
                return Json(respuesta);
            }
        }



        [HttpPost]
        public JsonResult GetIgresosDia(int puerto)
        {
            var respuesta = new { mensaje = "", html = "" };
            string html_tabla = "";
            try
            {
                html_tabla = Mantenedores.GetIngresosDia(puerto);
                respuesta = new { mensaje = "", html = html_tabla };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), html = "" };
                return Json(respuesta);
            }
        }



        [HttpPost]
        public JsonResult GetVehiculosDia()
        {

            var respuesta = new { mensaje = "", html = "" };
            string html_tabla = "";
            try
            {
                html_tabla = Mantenedores.GetVehiculosDia();
                respuesta = new { mensaje = "", html = html_tabla };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), html = "" };
                return Json(respuesta);
            }
        }



        [HttpPost]
        public JsonResult GetSolicitudesDia(int puerto)
        {
            var respuesta = new { mensaje = "", html = ""};
            string html_tabla = "";
            try
            {
                html_tabla = Mantenedores.GetSolicitudesDia(puerto);
                respuesta = new { mensaje = "", html = html_tabla };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), html =""};
                return Json(respuesta);
            }
        }


        public FileContentResult ExportToExcel()
        {
            //Ahi le pasas el data que necesites
            byte[] fileContents = Encoding.UTF8.GetBytes(Util.Exportarxml());
            return File(fileContents, "application/vnd.ms-excel", "name.xls");

        }


    }
}
