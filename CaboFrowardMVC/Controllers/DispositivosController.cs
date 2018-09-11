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
    public class DispositivosController : Controller
    {
        private CaboFroward2018Entities db = new CaboFroward2018Entities();

        // GET: Dispositivos
        public ActionResult Index()
        {

            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.DISPOSITIVOS.ToList());
        }

        // GET: Dispositivos/Details/5
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
            DISPOSITIVOS dISPOSITIVOS = db.DISPOSITIVOS.Find(id);
            if (dISPOSITIVOS == null)
            {
                return HttpNotFound();
            }
            return View(dISPOSITIVOS);
        }

        // GET: Dispositivos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dispositivos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DISPOSITIVO,NOMBRE,IP")] DISPOSITIVOS dISPOSITIVOS)
        {
            if (ModelState.IsValid)
            {
                db.DISPOSITIVOS.Add(dISPOSITIVOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dISPOSITIVOS);
        }

        // GET: Dispositivos/Edit/5
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
            DISPOSITIVOS dISPOSITIVOS = db.DISPOSITIVOS.Find(id);
            if (dISPOSITIVOS == null)
            {
                return HttpNotFound();
            }
            return View(dISPOSITIVOS);
        }

        // POST: Dispositivos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DISPOSITIVO,NOMBRE,IP")] DISPOSITIVOS dISPOSITIVOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dISPOSITIVOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dISPOSITIVOS);
        }

        // GET: Dispositivos/Delete/5
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
            DISPOSITIVOS dISPOSITIVOS = db.DISPOSITIVOS.Find(id);
            if (dISPOSITIVOS == null)
            {
                return HttpNotFound();
            }
            return View(dISPOSITIVOS);
        }

        // POST: Dispositivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DISPOSITIVOS dISPOSITIVOS = db.DISPOSITIVOS.Find(id);
            db.DISPOSITIVOS.Remove(dISPOSITIVOS);
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
    }
}
