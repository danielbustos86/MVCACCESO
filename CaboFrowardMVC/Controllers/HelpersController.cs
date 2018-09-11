using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using BOL;
using System.Web.Security;
using CaboFrowardMVC.Models;

namespace CaboFrowardMVC.Controllers
{
    public class HelpersController : Controller
    {
        // GET: Helpers
        public ActionResult Index()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

 
        [HttpPost]   
        public ActionResult LogOff()
        {
            CerrarSesion();
            return RedirectToAction("Index", "Login");
        }
        public void CerrarSesion()
        {
            Session["UsuarioAutentificado"] = null;
            Session["MisMenus"] = null;
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
        }

        [HttpPost]
        public JsonResult GetPersona(int rut)
        {
            var respuesta = new { mensaje = "", existe = "0", personal = new Personas()};
            Personas Persona = new Personas();
            try
            {
                Persona = PersonasDAL.GetPersona(rut);

                if (Persona != null)
                {
                    respuesta = new { mensaje = "", existe  = "1", personal = Persona};
                    return Json(respuesta);
                }
                else
                {
                    respuesta = new { mensaje = "", existe = "0", personal = new Personas()};
                    return Json(respuesta);
                }                
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message, existe = "0", personal = new Personas()};
                return Json(respuesta);
            } 
                       
        }

		[HttpPost]
		public JsonResult GetPersonaInduccion(int rut,string pasaporte)
		{
			var respuesta = new { mensaje = "", existe = "0", personal = new PERSONAINDUCCION() };
			PERSONAINDUCCION Persona = new PERSONAINDUCCION();
			try
			{
				Persona = PersonasDAL.GetPERSONAINDUCCION(rut, pasaporte);

				if (Persona != null)
				{
					respuesta = new { mensaje = "", existe = "1", personal = Persona };
					return Json(respuesta);
				}
				else
				{
					respuesta = new { mensaje = "", existe = "0", personal = new PERSONAINDUCCION() };
					return Json(respuesta);
				}
			}
			catch (Exception ex)
			{
				respuesta = new { mensaje = ex.Message, existe = "0", personal = new PERSONAINDUCCION() };
				return Json(respuesta);
			}

		}


		[HttpPost]
        public JsonResult GetPersonapasaporte(string pasaporte)
        {
            var respuesta = new { mensaje = "", existe = "0", personal = new Personas() };
            Personas Persona = new Personas();
            try
            {
                Persona = PersonasDAL.GetPersonapasaporte(pasaporte);

                if (Persona != null)
                {
                    respuesta = new { mensaje = "", existe = "1", personal = Persona };
                    return Json(respuesta);
                }
                else
                {
                    respuesta = new { mensaje = "", existe = "0", personal = new Personas() };
                    return Json(respuesta);
                }
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message, existe = "0", personal = new Personas() };
                return Json(respuesta);
            }

        }


        [HttpPost]
        public JsonResult GetVehiculo(string patente)
        {

            //CaboFroward2018Entities db = new CaboFroward2018Entities();      
   


            var respuesta = new { mensaje = "", existe = "0", vehiculos = new Vehiculo() };

            if (VehiculoDAL.GetValidaVehiculo(patente) =="INA")
            {
                 respuesta = new { mensaje = "PATENTE INACTIVA", existe = "0", vehiculos = new Vehiculo() };
                return Json(respuesta);

            }


            Vehiculo aux_vehiculo = new Vehiculo();
            try
            {
                //aux_vehiculo =     (from VEHICULOS elemento in db.VEHICULOS  where elemento.PATENTE.Equals(patente) select  new Vehiculo() {Id =elemento.ID_VEHICULO , Patente = elemento.PATENTE, Tipo = elemento.TIPOS_VEHICULOS.NOMBRE} ).FirstOrDefault();
                //db.Dispose();
                aux_vehiculo = VehiculoDAL.GetVehiculo(patente);
                if (aux_vehiculo != null)
                {
                    respuesta = new { mensaje = "", existe = "1", vehiculos = aux_vehiculo };
                    return Json(respuesta);
                }
                else
                {

                    respuesta = new { mensaje = "", existe = "0", vehiculos = new Vehiculo() };
                    return Json(respuesta);
                }
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message, existe = "0", vehiculos = new Vehiculo() };
                return Json(respuesta);
            }

        }



        [HttpPost]
        public JsonResult GetLocacion(int codigo , string nombre , int puerto)
        {
            var respuesta = new { mensaje = "", total = 0, html = ""};
            string locacion_html = "";
            try
            {
                locacion_html = Mantenedores.GetLocaciones_Html(codigo, nombre, puerto);
                respuesta = new { mensaje = "", total = 1, html = locacion_html };
                return Json(respuesta);
            }
            catch (Exception ex)
            {

                respuesta = new { mensaje = ex.Message.ToString(), total = 0, html = ""};
                return Json(respuesta);
            }
        }

        [HttpPost]
        public JsonResult GetNaves(int codigo, string nombre, int puerto)
        {
            var respuesta = new { mensaje = "", total = 0, html = "" };
            string naves_html = "";
            try
            {
                naves_html = Mantenedores.GetNaves(codigo, nombre, puerto);
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
        public JsonResult GetNavesActivas(int codigo, string nombre, int puerto)
        {
            var respuesta = new { mensaje = "", total = 0, html = "" };
            string naves_html = "";
            try
            {
                naves_html = Mantenedores.GetNavesActivas(codigo, nombre, puerto);
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
        public JsonResult GetEmpresasActivas(int rut,string nombre)
        {
            var respuesta = new { mensaje = "", total = 0, html = "" };
            string empresas_html = "";
            try
            {
                empresas_html = Mantenedores.GetEmpresasActivas(rut, nombre);
                respuesta = new { mensaje = "", total = 1, html = empresas_html };
                return Json(respuesta);
            }
            catch (Exception ex)
            {

                respuesta = new { mensaje = ex.Message.ToString(), total = 0, html = "" };
                return Json(respuesta);
            }
        }









        [HttpPost]      
        public JsonResult GuardaPersona(int nacionalidad, int rut, string dv, string nombre, string paterno, string materno,string pasaporte,string fechainduccion)

        {
            var respuesta = new { mensaje = "" };
            try
            {



                CaboFroward2018Entities db = new CaboFroward2018Entities();



                PERSONAS p = new PERSONAS();

                p.ID_NACIONALIDAD = nacionalidad;
            
                p.RUT = rut;
                p.DV = dv;
                p.NOMBRE = nombre;
                p.APELLIDOMATERNO = materno;
                p.APELLIDOPATERNO = paterno;
                p.PASAPORTE = pasaporte;
				DateTime fecha;

				if (DateTime.TryParse(fechainduccion, out fecha))
				{
					p.FECHAINDUCCION = DateTime.Parse(fechainduccion);

				}


				if (DAL.PersonasDAL.GetPersonaInactiva(rut) != null && rut!=0)
                {
                    respuesta = new { mensaje = "Rut ya existe,valide vigencia de persona"  };
                    return Json(respuesta);


                }



                db.PERSONAS.Add(p);
                db.SaveChanges();
                db.Dispose();
                respuesta = new { mensaje = "" };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = "Error al crear Persona" + ex.InnerException.ToString() };
                return Json(respuesta);
            }            
        }
        

        [HttpPost]

        public JsonResult GuardaVehiculo(string patente, int tipo, string descripcion)

        {
            var respuesta = new { mensaje = "" };
            try
            {

                VehiculoDAL.GuardaPatente(patente, tipo, descripcion);
                respuesta = new { mensaje = "" };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = "Error al crear Persona" + ex.Message.ToString() };
                return Json(respuesta);
            }
           }



    }
}
