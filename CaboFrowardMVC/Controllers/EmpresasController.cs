using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaboFrowardMVC.Models;

namespace CaboFrowardMVC.Controllers
{
    public class EmpresasController : Controller
    {
        private CaboFroward2018Entities db = new CaboFroward2018Entities();

        // GET: Empresas
        public ActionResult Index()
            
        {

            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }




            var eMPRESAS = db.EMPRESAS.Include(e => e.TIPOS_EMPRESAS);
            return View(eMPRESAS.ToList());
        }

        // GET: Empresas/Details/5
        public ActionResult Details(int? id)
        {


            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Details", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESAS eMPRESAS = db.EMPRESAS.Find(id);
            if (eMPRESAS == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESAS);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            ViewBag.ID_TIPO_EMPRESA = new SelectList(db.TIPOS_EMPRESAS, "ID_TIPO_EMPRESA", "NOMBRE");
            return View();
        }

        // POST: Empresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_EMPRESA,ID_TIPO_EMPRESA,RUT,DV,NOMBRES,GIRO")] EMPRESAS eMPRESAS)
        {
            if (ModelState.IsValid)
            {
                String rutaux;
                rutaux = eMPRESAS.RUT.ToString() +"-"+ eMPRESAS.DV;

                if (!BOL.Helpers.Util.RutValido(rutaux))
                {
                    //ViewBag.Script = "<script type='text/javascript'>alert('Rut no Valido');</script>";
                    //return Content("<script language='javascript' type='text/javascript'>alert('Rut No valido!');</script>");
                    //ViewBag.Javascript = "<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>";

                    //return RedirectToAction("Create");

                    ViewBag.Error = "Rut con Formato Incorrecto";
                }
                else {


                    if (eMPRESAS.NOMBRES==null)
                    {

                        ViewBag.Error = "Debe Ingresar un Nombre";
                    }

                    else if (Existe(int.Parse(eMPRESAS.RUT.ToString())))
                    {
                        ViewBag.Error = "EL RUT YA EXISTE";
                    }

                    else
                    {
                        db.EMPRESAS.Add(eMPRESAS);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }

            }

            ViewBag.ID_TIPO_EMPRESA = new SelectList(db.TIPOS_EMPRESAS, "ID_TIPO_EMPRESA", "NOMBRE", eMPRESAS.ID_TIPO_EMPRESA);
   
            return View(eMPRESAS);
            

        }

       public Boolean  Existe(int rut)
        {
            Boolean isValueUnique;
            //isValueUnique= db.EMPRESAS.All(a => a.RUT == rut);

        List<EMPRESAS> emp1 =  db.EMPRESAS.Where(a => a.RUT == rut).ToList();





            if (emp1.Count>0 )
            {

                isValueUnique = true;
            }
            else {
                isValueUnique = false;
            }


            return isValueUnique;
        }


        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Details", "Login");
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESAS eMPRESAS = db.EMPRESAS.Find(id);
            if (eMPRESAS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_TIPO_EMPRESA = new SelectList(db.TIPOS_EMPRESAS, "ID_TIPO_EMPRESA", "NOMBRE", eMPRESAS.ID_TIPO_EMPRESA);
            return View(eMPRESAS);
        }

        // POST: Empresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_EMPRESA,ID_TIPO_EMPRESA,RUT,DV,NOMBRES,GIRO,INACTIVO")] EMPRESAS eMPRESAS)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(eMPRESAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_TIPO_EMPRESA = new SelectList(db.TIPOS_EMPRESAS, "ID_TIPO_EMPRESA", "NOMBRE", eMPRESAS.ID_TIPO_EMPRESA);
            return View(eMPRESAS);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {


            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Details", "Login");
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESAS eMPRESAS = db.EMPRESAS.Find(id);
            if (eMPRESAS == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESAS);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

       
            EMPRESAS eMPRESAS = db.EMPRESAS.Find(id);
            db.EMPRESAS.Remove(eMPRESAS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Index(string inpBuscar)
        {
            if (inpBuscar.Length > 0)
            {
                var RegFiltrado = (from f in db.EMPRESAS
                                   where f.NOMBRES.StartsWith(inpBuscar) ||
                                    f.GIRO.Contains(inpBuscar)
                                   select f);

                return View(RegFiltrado.ToList());
            }
            else
            {
                return View(db.EMPRESAS.OrderBy(f =>
                f.NOMBRES).Take(20).ToList());
            }

        }

    }
}
