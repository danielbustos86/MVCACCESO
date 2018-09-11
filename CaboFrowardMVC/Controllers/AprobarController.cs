using BOL;
using BOL.Helpers;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaboFrowardMVC.Controllers
{
    public class AprobarController : Controller
    {
        // GET: Aprobar
        public ActionResult Index()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Details", "Login");
            }

            return View();
        }


        [HttpPost]
        public JsonResult ListadoSolAprobar(string opcion)
        {
            var respuesta = new { mensaje = "" };
       
            try
            {
                respuesta = new { mensaje = "" };
                List<Solicitud> ls_solicitud1 = new List<Solicitud>();

                Login Login = new Login();
                Login = (Login)Session["UsuarioAutentificado"];
                ls_solicitud1 = SolicitudDAL.SolicitudListado(null, opcion);

                return Json(ls_solicitud1);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message };
                return Json(respuesta);
            }

        }


        [HttpPost]
        public JsonResult VerSolicitud(int id)
        {
            string personas_html = "";
            string vehiculos_html = "";
            Login Login = new Login();
            Login = (Login)Session["UsuarioAutentificado"];
            var respuesta = new { mensaje = "", puerto = "", desde = "", hasta = "", tipo = "", empresa = "", persona = "", patente = "" };
            DataSet resultado = new DataSet();
            try
            {

                resultado = SolicitudDAL.VerDetallePermiso(id, Login.Id);
                DataTable personas = new DataTable();
                DataTable vehiculos = new DataTable();
                DataTable encabezado = new DataTable();
                personas = resultado.Tables[1];
                vehiculos = resultado.Tables[2];
                encabezado = resultado.Tables[0];
                personas_html = Util.DevuelveBodyHtmlCheckPermiso(personas);
               vehiculos_html = Util.DevuelveBodyHtmlNormal(vehiculos);
                
                respuesta = new { mensaje = "", puerto = encabezado.Rows[0]["Puerto"].ToString(), desde = encabezado.Rows[0]["Desde"].ToString(), hasta = encabezado.Rows[0]["Hasta"].ToString(), tipo = encabezado.Rows[0]["Tipo"].ToString(), empresa = encabezado.Rows[0]["Empresa"].ToString(), persona = personas_html, patente = vehiculos_html };
                return Json(respuesta);


            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), puerto = "", desde = "", hasta = "", tipo = "", empresa = "", persona = "", patente = "" };
                return Json(respuesta);
            }

        }

        
        [HttpPost]
        public JsonResult AprobarSolictud(int id, string idpersona)
        {
            
            Login login = new Login();
            login = (Login)Session["UsuarioAutentificado"];
            var respuesta = new { mensaje = "", html = "" };
            try
            {
                AprobadorDAL.ApruebaSolicitud(id, idpersona, login.Id);
                respuesta = new { mensaje = "", html = "" };
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