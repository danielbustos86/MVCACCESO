using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using BOL;
using CaboFrowardMVC.Models;

namespace CaboFrowardMVC.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]

        public ActionResult Index()
        {
            ViewBag.Err_val = "";
            return View();
        }

        [HttpPost]       
        public ActionResult Index(string usuario, string clave)
        {
                      
                Accesos_Menu Listado = new Accesos_Menu();
                string Mis_menus = "";
                Login inicio = new Login();
                try
                {
                    inicio = AutentifiacionDAL.ValidarAcceso(usuario, clave);
                    inicio.Usuario = usuario;
                    inicio.IP = GetIPAddress(Request);
                    Mis_menus = Listado.Obtener_menus(inicio.Id);
                    Session["MisMenus"] = Mis_menus;
                    Session["UsuarioAutentificado"] = inicio;
					
				return RedirectToAction("Index", "Menu");
				

			
                }
                catch (Exception ex)
                {

                    ViewBag.Err_val = ex.Message.ToString();
                    return View();
                }
           



        }
        
        
        [HttpPost]
        public JsonResult ValidaInicio(string usuario, string clave)
        {

        
            var respuesta = new { mensaje = "", cambio = "" };
            Login inicio = new Login();
            try
            {
                inicio = AutentifiacionDAL.ValidarAcceso(usuario, clave);
                respuesta = new { mensaje = "", cambio = inicio.Inactivo };
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), cambio = "" };
            }
            return Json(respuesta);
        }




        [HttpPost]
        public JsonResult Activa_inicio(string usuario, string clave, string nueva_clave)
        {
            var respuesta = new { mensaje = "" };           
            try
            {
                AutentifiacionDAL.Activa_Inicio(usuario, clave, nueva_clave);
                respuesta = new { mensaje = "" };
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString() };
            }
            return Json(respuesta);
        }



        [HttpPost]
        public JsonResult Cambio_clave(string usuario, string clave, string nueva_clave)
        {
            var respuesta = new { mensaje = "" };
            Login Login = new Login();
            Login = (Login)Session["UsuarioAutentificado"];

            try
            {
                
                AutentifiacionDAL.Activa_Inicio(Login.Usuario, clave, nueva_clave);
                respuesta = new { mensaje = "" };
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString() };
            }
            return Json(respuesta);
        }



        public static string GetIPAddress(HttpRequestBase request) {
            string ip;
            try
            {
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                {
                    if (ip.IndexOf(",") > 0)
                    {
                        string[] ipRange = ip.Split(',');
                        int le = ipRange.Length - 1;
                        ip = ipRange[le];
                    }
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch { ip = null; }
            return ip;
        }


     

    }
}
