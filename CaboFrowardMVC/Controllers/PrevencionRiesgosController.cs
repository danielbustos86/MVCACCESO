using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaboFrowardMVC.Controllers
{
    public class PrevencionRiesgosController : Controller
    {
        // GET: PrevencionRiesgos
        public ActionResult ActualizaInduccion()
				
        {
			
			return View();
        }

		[HttpPost]
		public JsonResult ActualizaFechaInduccion(DateTime fecha, string personas)
		{
			var respuesta = new { mensaje = "",html=""};
			string resp = "";
			string actualiza = "";
			
			try
				
			{
				resp = DAL.PersonaInduccionDAL.ActualizaFecha(fecha, personas);
				actualiza = DAL.PersonaInduccionDAL.ListarPersonas(personas);
				respuesta = new { mensaje =resp, html = actualiza};
					return Json(respuesta);
			}
			catch (Exception ex)
			{
				respuesta = new { mensaje = ex.Message, html = "" };
				return Json(respuesta);
			}

		}


	}
}
