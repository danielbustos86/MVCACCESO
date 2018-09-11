using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BOL;
using DAL;
namespace CaboFrowardMVC.Controllers
{
    public class IngresoController : Controller
    {
        // GET: Ingreso
        public ActionResult Index()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        
        [HttpPost]
        public JsonResult VerIngreso(int rut,string pasaporte)
        {          

            var respuesta = new { mensaje = "", solicitud = new Ingreso(), existe= 0 , patente ="", total_patente = 0 ,aprobadores=""};
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            Ingreso resultado = new Ingreso();
            StringBuilder vehiculo_html = new StringBuilder();
            string aprobadores_html = "";
            try
            {
                resultado = IngresoDAL.BuscarIngreso(rut,pasaporte);
                vehiculos = IngresoDAL.ListarPatente(resultado.Idsolicitud);
                                              


                if (vehiculos.Count() > 1)
                {
                    vehiculo_html.AppendLine("<option value='" + "0" + "' selected>" + "Sin Vehículo" + "</option>");
                    foreach (Vehiculo item in vehiculos)
                    {
                        vehiculo_html.AppendLine("<option value='" + item.Id + "'>" + item.Patente + "</option>");
                    }
                    
                }
                else if (vehiculos.Count() == 1)
                {
                    vehiculo_html.AppendLine("<option value='" + "0" + "'>" + "Sin Vehículo" + "</option>");
                    foreach (Vehiculo item in vehiculos)
                    {
                        vehiculo_html.AppendLine("<option value='" + item.Id + "' selected>" + item.Patente + "</option>");
                    }                                      
                }
                else
                {
                    vehiculo_html.AppendLine("<option value='" + "0" + "' selected>" + "Sin Vehículo" + "</option>");
                }                              
                
                if (resultado.Idsolicitud != 0)
                {

                    aprobadores_html = IngresoDAL.DevuelveAprobadores(resultado.Idsolicitud, rut);                                       


                    respuesta = new { mensaje = "", solicitud = resultado, existe = 1 , patente = vehiculo_html.ToString(), total_patente = vehiculos.Count() ,aprobadores = aprobadores_html };
                    return Json(respuesta);
                }
                else
                 {
                    respuesta = new { mensaje = "", solicitud = new Ingreso(), existe = 0, patente = "", total_patente = 0 ,aprobadores = aprobadores_html };
                    return Json(respuesta);
                 }                            
              

            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), solicitud= new Ingreso(), existe = 0 , patente = "", total_patente = 0 , aprobadores = "" };
                return Json(respuesta);
            }

        }


       
        public JsonResult GuardaVehiculo(string patente, int tipo, string descripcion, int id)

        {

            StringBuilder vehiculo_html = new StringBuilder();
            List<Vehiculo> vehiculos = new List<Vehiculo>();
            var respuesta = new { mensaje = "",patente = "" };
            try
            {                

                VehiculoDAL.GuardaPatenteIngreso(patente, tipo, descripcion, id);

                //Obtenemos nuevo listado de patente
                vehiculo_html.AppendLine("<option value='" + "0" + "'>" + "Sin Vehículo" + "</option>");
                vehiculos = IngresoDAL.ListarPatente(id);
                foreach (Vehiculo item in vehiculos)
                {

                    if (patente == item.Patente)
                    {
                        vehiculo_html.AppendLine("<option value='" + item.Id + "'" + " selected>" + item.Patente + "</option>");
                    }
                    else
                    {
                        vehiculo_html.AppendLine("<option value='" + item.Id + "'" + ">" + item.Patente + "</option>");
                    }                 
                }

                respuesta = new { mensaje = "", patente = vehiculo_html.ToString() };
                return Json(respuesta);                
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = "ERROR: " + ex.Message.ToString(), patente = "" };
                return Json(respuesta);
            }
           }

        [HttpPost]
        public JsonResult GuardaIngreso(int id, string patente, int registro)
        {
            if (patente == "0")
            {
                patente = null;
            }

            var respuesta = new { mensaje = "" };
            try
            {
                
                IngresoDAL.RegistraAcceso(registro, "",id, patente);
                respuesta = new { mensaje = "" };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message };
                return Json(respuesta);
            }

        }


        [HttpPost]
        public JsonResult UltimasSolicitudes(int rut,string pasaporte)
        {

            Login login = new Login();
            login = (Login)Session["UsuarioAutentificado"];
            var respuesta = new { mensaje = "", html = "" , nombre="" };
            string ultimas_solicitudes = "";
            string mi_nombre = "";
            try
            {
                ultimas_solicitudes = IngresoDAL.UltimasSolicitudes(rut,ref mi_nombre, pasaporte);
                respuesta =  new { mensaje = "", html = ultimas_solicitudes, nombre = mi_nombre };
                return Json(respuesta);

            }
            catch (Exception ex)
            {

                respuesta = new { mensaje = ex.Message.ToString(), html = "",nombre="" };
                return Json(respuesta);
            }

        }

        [HttpPost]
        public JsonResult UltimaPatente(int idPersonaAprobada)
        {

            Login login = new Login();
            login = (Login)Session["UsuarioAutentificado"];
            var respuesta = new { mensaje = "", idVehiculo = "", patente = "" };

            try
            {

                Vehiculo vel = IngresoDAL.ValidaVehiculo(idPersonaAprobada);
                respuesta = new { mensaje = "", idVehiculo = vel.Id.ToString(), patente = vel.Patente };
                return Json(respuesta);

            }
            catch (Exception ex)
            {

                respuesta = new { mensaje = "", idVehiculo = "0", patente = "" };
                return Json(respuesta);
            }

        }
    }

}
