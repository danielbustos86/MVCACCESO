using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BOL;
using DAL;
using Newtonsoft.Json;

namespace CaboFrowardMVC.Controllers
{
    public class NavesController : Controller
    {
        // GET: Naves
        public ActionResult Index()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        

        [HttpPost]
        public JsonResult NavesAPI()
        {
            var respuesta = new { mensaje = "", html = "", total = 0 };
            string ruta_loc = Parameter.LeerNaves();
           
            string respuesta_get;
            try
            {

                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    respuesta_get = client.DownloadString(ruta_loc);
                }
                List<ApiNave> ls_nave = new List<ApiNave>();
                                ls_nave = JsonConvert.DeserializeObject<List<ApiNave>>(respuesta_get);
                StringBuilder tabla = new StringBuilder();
                int contador = 0;
                if (ls_nave.Count() > 0)
                {
                    foreach (ApiNave item in ls_nave)
                    {
                        tabla.AppendLine("<tr>");
                        tabla.AppendLine("<td><a href='javaScript:void(0)'><button type='button'>Sel</button></a></td>");
                        tabla.AppendLine("<td>" + item.Id + "</td>");
                        tabla.AppendLine("<td class='campoDesc'>" + item.glosa.ToUpper() + "</td>");
                        tabla.AppendLine("</tr>");
                        contador = 1;
                    }

                }
                respuesta = new { mensaje = "", html = tabla.ToString(), total = contador };
                return Json(respuesta);

            }
            catch (Exception ex)
            {

                respuesta = new { mensaje = ex.Message.ToString(), html = "", total = 0 };
                return Json(respuesta);
            }



        }


        [HttpPost]
        public JsonResult GetNaves()
        {
            var respuesta = new { mensaje = "", total = 0, html = "" };
            string naves_html = "";
            try
            {
                naves_html = Mantenedores.GetNaves_Html();
                respuesta = new { mensaje = "", total = 1, html = naves_html };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), total = 0, html = "" };
                return Json(respuesta);
            }
        }


        [HttpPost]
        public JsonResult GetNavesPrincipal()
        {
            var respuesta = new { mensaje = "", total = 0, html = "" };
            string naves_html = "";
            try
            {
                naves_html = Mantenedores.GetNavesPrincipal_Html();
                respuesta = new { mensaje = "", total = 1, html = naves_html };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), total = 0, html = "" };
                return Json(respuesta);
            }
        }



        [HttpPost]
        public JsonResult GuardarNaves(int id, string nombre)
        {
            var respuesta = new { mensaje = "" };
            
            try
            {
                Mantenedores.AgregaNave(id,nombre);
                respuesta = new { mensaje = ""};
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString()};
                return Json(respuesta);
            }
        }





        [HttpPost]
        public JsonResult ActualizaEstadoNaves(int nave, int opcion)
        {

            var respuesta = new { mensaje = "" };
            try
            {
                Mantenedores.CambiaEstadoNave(nave, opcion);
                respuesta = new { mensaje = "" };
                return Json(respuesta);
            }
            catch (Exception ex)
            {

                respuesta = new { mensaje = ex.Message.ToString() };
                return Json(respuesta);
            }

        }



        [HttpPost]
        public JsonResult ActualizaDescripcionNave(int nave, string descripcion)
        {

            var respuesta = new { mensaje = "" };
            try
            {
                Mantenedores.CambiaDescripcionNave(nave, descripcion);
                respuesta = new { mensaje = "" };
                return Json(respuesta);
            }
            catch (Exception ex)
            {

                respuesta = new { mensaje = ex.Message.ToString() };
                return Json(respuesta);
            }

        }



    }
}