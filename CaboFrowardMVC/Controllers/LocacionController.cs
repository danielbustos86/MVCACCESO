using BOL;
using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CaboFrowardMVC.Controllers
{
    public class LocacionController : Controller
    {
        // GET: Locacion
        public ActionResult Index()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
                                                        

        [HttpPost]
        public JsonResult GetLocacionPuerto(int puerto)
        {
            var respuesta = new { mensaje = "", total = 0, html = "" };
            string locacion_html = "";
            try
            {
                locacion_html = Mantenedores.GetLocacionesPuerto_Html(puerto);
                respuesta = new { mensaje = "", total = 1, html = locacion_html };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), total = 0, html = "" };
                return Json(respuesta);
            }
        }


        [HttpPost]
        public JsonResult GetLocacionPuertoPrincipal(int puerto)
        {
            var respuesta = new { mensaje = "", total = 0, html = "" };
            string locacion_html = "";
            try
            {
                locacion_html = Mantenedores.GetLocacionesPuertoPrincipal_Html(puerto);
                respuesta = new { mensaje = "", total = 1, html = locacion_html };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), total = 0, html = "" };
                return Json(respuesta);
            }
        }



        

        [HttpPost]
        public JsonResult LocacionesAPI(string puerto, string rut)
        {
            var respuesta = new { mensaje = "", html = "",total=0 };
            string ruta_loc = Parameter.LeerLocacion();
            ruta_loc = ruta_loc + puerto;
            string respuesta_get;            
            try
            {

                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    respuesta_get = client.DownloadString(ruta_loc);
                }
                List<ApiLocacion> ls_locaciones = new List<ApiLocacion>();
                List<Locacion> ls_loc = new List<Locacion>();


                ls_locaciones = JsonConvert.DeserializeObject<List<ApiLocacion>>(respuesta_get);

                foreach(ApiLocacion item in ls_locaciones)
                {
                    ls_loc.Add(new Locacion { Id = item.id, Nombre = item.lugar });
                }

              


                StringBuilder tabla = new StringBuilder();
                int contador = 0;
                if (ls_loc.Count() > 0)
                {
                    foreach (Locacion item in ls_loc)
                    {
                        tabla.AppendLine("<tr>");
                        tabla.AppendLine("<td><a href='javaScript:void(0)'><button type='button'>Guardar</button></a></td>");
                        tabla.AppendLine("<td>" + item.Id + "</td>");
                        tabla.AppendLine("<td class='campoDesc'>" + item.Nombre.ToUpper() + "</td>");
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
        public JsonResult CargarListados()
        {
            StringBuilder puerto_html = new StringBuilder();        
            var respuesta = new { mensaje = "", puerto = ""};
            List<Puertos> ls_puerto = new List<Puertos>();                    
            ls_puerto = Mantenedores.GetPuertos();         

            puerto_html.AppendLine("<option value='" + "0" + "' selected>" + "Seleccione" + "</option>");
           
            
            if (ls_puerto.Count > 0)
            {
                foreach (Puertos item in ls_puerto)
                {
                    puerto_html.AppendLine("<option  value='" + item.Id + "' data-rut='" + item .Rut+ "'>" + item.Nombre + "</option>");
                }

            }
            

            respuesta = new { mensaje = "", puerto = puerto_html.ToString()};
            return Json(respuesta);
        }



        [HttpPost]
        public JsonResult GuardarLocacion(int puerto , string descripcion , int locacion ,  string informada)
        {
           
            var respuesta = new { mensaje = "" };
            try
            {
                SolicitudDAL.LocacionGuarda(puerto, descripcion, locacion, informada);
                respuesta = new { mensaje = "" };
                return Json(respuesta);
            }
            catch (Exception ex )
            {

                respuesta = new { mensaje = ex.Message.ToString()};
                return Json(respuesta);
            }
     
        }


        [HttpPost]
        public JsonResult ActualizaDescripcionLoc(int puerto, string descripcion, int locacion)
        {

            var respuesta = new { mensaje = "" };
            try
            {
                Mantenedores.ActualizaLocDescripcion(puerto, locacion,descripcion );
                respuesta = new { mensaje = "" };
                return Json(respuesta);
            }
            catch (Exception ex)            {

                respuesta = new { mensaje = ex.Message.ToString() };
                return Json(respuesta);
            }

        }


        [HttpPost]
        public JsonResult ActualizaEstadoLoc(int puerto, int locacion, int opcion)
        {

            var respuesta = new { mensaje = "" };
            try
            {
                Mantenedores.CambiaEstadoLoc(puerto, locacion, opcion);
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


