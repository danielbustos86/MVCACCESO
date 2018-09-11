using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BOL;
using DAL;
using BOL.Helpers;


namespace CaboFrowardMVC.Controllers
{
    public class SolicitudController : Controller
    {
        // GET: Solicitud
        public ActionResult Index()
        {

            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public ActionResult Details()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Details", "Login");
            }
                               
            return View();
        }


		public ActionResult ListadoSolicitudes()
		{
			List<Models.SolicitudProgramada> lis = new List<Models.SolicitudProgramada>();


			return View(lis);
		}

        [HttpPost]
        public JsonResult CargarListados()
        {
            StringBuilder puerto_html = new StringBuilder();
            StringBuilder empresas_html = new StringBuilder();
            StringBuilder tipos_html = new StringBuilder();
            StringBuilder nacion_html = new StringBuilder();
            StringBuilder vehiculo_html = new StringBuilder();

            var respuesta = new { mensaje = "", puerto = "", empresa = "", tipo_ingreso = "", nacion = "", vehiculo = "" };

            List<Puertos> ls_puerto = new List<Puertos>();
            List<Empresas> ls_empresa = new List<Empresas>();
            List<TipoIngreso> ls_tipos = new List<TipoIngreso>();
            List<Nacionalidad> ls_nacion = new List<Nacionalidad>();
            List<TipoVehiculo> ls_vehiculo = new List<TipoVehiculo>();


            ls_puerto = Mantenedores.GetPuertos();
            ls_empresa = Mantenedores.GetEmpresas("111", "N");
            ls_tipos = Mantenedores.GetTipoIngreso();
            ls_nacion = Mantenedores.GetNacionalidad();
            ls_vehiculo = Mantenedores.GetTipoVehiculos();

            //puerto_html.AppendLine("<option value='" + "0" + "' selected>" + "Seleccione" + "</option>");
            empresas_html.AppendLine("<option value='" + "0" + "' selected>" + "No Informada" + "</option>");
            tipos_html.AppendLine("<option value='" + "0" + "' selected>" + "Seleccione" + "</option>");
            nacion_html.AppendLine("<option value='" + "0" + "'>" + "Seleccione" + "</option>");
            vehiculo_html.AppendLine("<option value='" + "0" + "' selected>" + "Seleccione" + "</option>");




            if (ls_puerto.Count > 0)
            {
                foreach (Puertos item in ls_puerto)
                {
                    puerto_html.AppendLine("<option value='" + item.Id + "'>" + item.Nombre + "</option>");
                }

            }

            if (ls_empresa.Count > 0)
            {
                foreach (Empresas item in ls_empresa)
                {
                    empresas_html.AppendLine("<option value='" + item.Id + "'>" + item.Info + "</option>");
                }

            }

            if (ls_tipos.Count > 0)
            {
                foreach (TipoIngreso item in ls_tipos)
                {
                    tipos_html.AppendLine("<option value='" + item.Id + "'>" + item.Nombre + "</option>");
                }

            }
            if (ls_nacion.Count > 0)
            {
                foreach (Nacionalidad item in ls_nacion)
                {

					if (item.Id == 39)
					{
						nacion_html.AppendLine("<option selected='selected'  value='" + item.Id + "'>" + item.Nombre + "</option>");

					}
					else {
						nacion_html.AppendLine("<option value='" + item.Id + "'>" + item.Nombre + "</option>");

					}


				}
            }

            if (ls_vehiculo.Count > 0)
            {
                foreach (TipoVehiculo item in ls_vehiculo)
                {
                    vehiculo_html.AppendLine("<option value='" + item.id + "'>" + item.glosa + "</option>");
                }
            }


            respuesta = new { mensaje = "", puerto = puerto_html.ToString(), empresa = empresas_html.ToString(), tipo_ingreso = tipos_html.ToString(), nacion = nacion_html.ToString(), vehiculo = vehiculo_html.ToString() };
            return Json(respuesta);
        }


        [HttpPost]
        public JsonResult GuadarSolicitud(string puerto , string fechainicio,string fechafin,string tingreso,string empresa,string perxml,string vehiculoxml)
        {



            Login login = new Login();
            login = (Login)Session["UsuarioAutentificado"];

            var respuesta = new { mensaje = ""  , html=""};



            if (puerto == "" || puerto == null || puerto == "0")
            {
                respuesta = new { mensaje = "Ingrese Puerto", html = "" };
                return Json(respuesta);
            }



            if (tingreso == "" || tingreso == null || tingreso == "0")
            {
                respuesta = new { mensaje = "Ingrese Tipo Ingreso", html = "" };
                return Json(respuesta);
            }

            if (empresa  == "" || empresa == null)
            {
                respuesta = new { mensaje = "Ingrese Empresa", html = "" };
                return Json(respuesta);
            }
            


            if (fechainicio == null || fechainicio == "")
            {
                respuesta = new { mensaje = "Problema con Fecha Inicio - Revisar", html = "" };
                return Json(respuesta);
            }
            if (fechafin == null || fechafin == "")
            {
                respuesta = new { mensaje = "Problema con Fecha Fin - Revisar", html = "" };
                return Json(respuesta);
            }

            if (DateTime.Parse(fechainicio) > DateTime.Parse(fechafin))
            {
                respuesta = new { mensaje = "Fecha Inicio no puede ser mayor a Fecha Fin", html = "" };
                return Json(respuesta);
            }


               if (DateTime.Parse(fechainicio) < DateTime.Now.Date)
                {
                    respuesta = new { mensaje = "Fecha Inicio no puede ser menor a Fecha Actual", html = "" };
                    return Json(respuesta);
                }
               

                if (DateTime.Parse(fechafin) < DateTime.Now.Date)
                {
                    respuesta = new { mensaje = "Fecha Fin no puede ser menor a Fecha Actual", html = "" };
                    return Json(respuesta);
                }


            string html_programados = "";
            try
            {
                html_programados = SolicitudDAL.ValidaIngresoSolicitud(puerto, DateTime.Parse(fechainicio), DateTime.Parse(fechafin), tingreso, empresa, perxml, vehiculoxml);
                                        
                if (html_programados == "")
                {
                    SolicitudDAL.GuardaSolicitud(puerto, DateTime.Parse(fechainicio), DateTime.Parse(fechafin), tingreso, empresa, perxml, vehiculoxml, login.Id);
                    respuesta = new { mensaje = "", html = "" };
                    return Json(respuesta);
                }

                else
                {

                    respuesta = new { mensaje = "", html = html_programados };
                    return Json(respuesta);
                }
                                              
            }
            catch (Exception ex)
            {

                respuesta = new { mensaje = ex.Message.ToString() , html = "" };
                return Json(respuesta);
            }
                          
          
          
        }

        [HttpPost]
        public JsonResult RecuperaSolicitudes()
        {
            var respuesta = new { mensaje = "" };
            var resul = new JavaScriptSerializer();            
            try
            {
                respuesta = new { mensaje = "" };
                List<Solicitud> ls_solicitud1 = new List<Solicitud>();

                Login Login = new Login();
                Login = (Login)Session["UsuarioAutentificado"];                
                ls_solicitud1 = SolicitudDAL.SolicitudListado(Login.Id.ToString(),"T");
                
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

            var respuesta = new { mensaje = "" , puerto = "" , desde = "" , hasta="" , tipo ="" , empresa ="" ,persona ="" ,  patente ="" };
            DataSet resultado = new DataSet();            
            try
            {
                
                resultado = SolicitudDAL.VerDetalle(id);
                DataTable personas = new DataTable();
                DataTable vehiculos = new DataTable();
                DataTable encabezado = new DataTable();
                personas = resultado.Tables[1];
                vehiculos = resultado.Tables[2];
                encabezado = resultado.Tables[0];
                personas_html = Util.DevuelveBodyPersonas(personas);
                vehiculos_html = Util.DevuelveBodyHtml(vehiculos);
                respuesta = new { mensaje = "", puerto = encabezado.Rows[0]["Puerto"].ToString(), desde =encabezado.Rows[0]["Desde"].ToString(), hasta = encabezado.Rows[0]["Hasta"].ToString(), tipo = encabezado.Rows[0]["Tipo"].ToString(), empresa = encabezado.Rows[0]["Empresa"].ToString() , persona = personas_html , patente = vehiculos_html};
                return Json(respuesta);
                                

            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), puerto = "", desde = "", hasta = "", tipo = "", empresa = "", persona = "", patente = "" };
                return Json(respuesta);
            }

        }


        [HttpPost]
        public JsonResult EliminarSolicutud(int id)
        {
            var respuesta = new { mensaje = "" };          
            try
            {
                SolicitudDAL.Eliminar(id);
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
        public JsonResult EliminarPersonaMiSolicitud(int id ,int idpersona)
        {
            var respuesta = new { mensaje = "" };
            try
            {
                SolicitudDAL.EliminarPersonaMiSol(id, idpersona);
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
        public JsonResult EliminarPatenteSolicitud(int id, string patente)
        {
            var respuesta = new { mensaje = "" };
            try
            {
                SolicitudDAL.EliminarPatenteMiSol(id, patente);
                respuesta = new { mensaje = "" };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message };
                return Json(respuesta);
            }

        }
    }
}
