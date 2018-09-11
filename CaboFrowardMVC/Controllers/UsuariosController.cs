using CaboFrowardMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DAL;
using BOL;



namespace CaboFrowardMVC.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        MisUsuarios usuario = new MisUsuarios();
        public ActionResult Index()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(usuario.Listado_usuarios());        
        }
        

        public ActionResult Edit(int id)
        {
            return View(usuario.Info_usuario(id));
        }
        [HttpPost]
        public ActionResult Edit(MisUsuarios Model)
        {

            int id=0;
            string nombre="";
            string telefono = "";
            bool activo = false;
            bool aprueba_nave = true;
            string clave = "";           

            id = Model.Id;
            nombre = Model.Usuario;
            telefono = Model.Telefono;
            clave = Model.Clave;
            if (Model.Activo == null)
            {
                activo = true;
            }
            if (Model.Aprueba_nave == null || Model.Aprueba_nave == "N")
            {
                aprueba_nave = false;
            }



            try
            {
                usuario.Actualiza(id, nombre, telefono, clave, activo, aprueba_nave);
                return View("Index", usuario.Listado_usuarios());
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [HttpPost]
        public ActionResult Create(MisUsuarios model)
        {
            int id = 0;
            string nombre = "";
            string telefono = "";          
            string clave = "";
            id = model.Id;
            nombre = model.Usuario;
            telefono = model.Telefono;
            clave = model.Clave;          
            
            try
            {
                usuario.CreaUsuario(id, nombre, telefono, clave);
                return View("Index", usuario.Listado_usuarios());
            }
            catch (Exception)
            {

                throw;
            }            

           

        }

        public ActionResult Create()
        {
            return View();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                usuario = null;
            }
            base.Dispose(disposing);
        }

        
        [HttpPost]
        public JsonResult RecuperaNombre(string rut)
        {
            var respuesta = new { mensaje = "", id = 0, nombre="", rut="" };
            var resul = new JavaScriptSerializer();
            DataTable datos = new DataTable();
            try
            {
               
                datos = usuario.GetPersona(rut);
                if (datos.Rows.Count> 0)
                {
                    respuesta = new { mensaje = "", id = Int32.Parse(datos.Rows[0]["ID_PERSONA"].ToString()), nombre = datos.Rows[0]["NOMBRE"].ToString(), rut = datos.Rows[0]["RUT"].ToString() };
                }
                else
                {
                    respuesta = new { mensaje = "", id = 0, nombre = "", rut = "" };
                }
                           
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), id = 0, nombre = "", rut = "" };
                return Json(respuesta);
            }
        }



        [HttpPost]
        public JsonResult GetPerfilUsuario1(int id)
        {
            var respuesta = new { mensaje = "", html="" };
            string locacion_html = "";
            try
            {
                List<PerfilesPersona> ls_perfiles = new List<PerfilesPersona>();
                locacion_html = UsuarioPerfiles.GetUsuarioPerfil(id);
                // ls_perfiles = UsuarioPerfiles.GetUsuarioPerfil1(id);                
                respuesta = new { mensaje = "", html= locacion_html };              
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), html = "" };
                return Json(respuesta);

            }
        }

        [HttpPost]
        public JsonResult GetUsuarioLocacion(int id)
        {
            var respuesta = new { mensaje = "", html = "" };
            string locacion_html = "";
            try
            {
                locacion_html = UsuarioLocacionesDAL.GetUsuarioLocaciones(id);
                // ls_perfiles = UsuarioPerfiles.GetUsuarioPerfil1(id);                
                respuesta = new { mensaje = "", html = locacion_html };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), html = "" };
                return Json(respuesta);

            }
        }

        [HttpPost]
        public JsonResult ModificaAsignacion(int idusuario,int perfil, string accion)
        {

            var respuesta = new { mensaje = "" };
            try
            {
                UsuarioPerfiles.ModificarPerfiles(idusuario, perfil, accion); 
        
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
        public JsonResult ModificaLocacion(int idusuario, int locacion, string accion)
        {

            var respuesta = new { mensaje = "" };
            try
            {
                UsuarioLocacionesDAL.ModificarLocaciones(idusuario, locacion, accion);

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