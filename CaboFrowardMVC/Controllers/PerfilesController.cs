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
    public class PerfilesController : Controller
    {
        private CaboFroward2018Entities db = new CaboFroward2018Entities();

        // GET: Perfiles
        public ActionResult Index()
        {

            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.PERFILES.ToList());
        }

        // GET: Perfiles/Details/5
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
            PERFILES pERFILES = db.PERFILES.Find(id);
            if (pERFILES == null)
            {
                return HttpNotFound();
            }
            return View(pERFILES);
        }

        // GET: Perfiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Perfiles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PERFIL,NOMBRE")] PERFILES pERFILES)
        {


            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Details", "Login");
            }


            if (ModelState.IsValid)
            {
                db.PERFILES.Add(pERFILES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pERFILES);
        }

        // GET: Perfiles/Edit/5
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
            PERFILES pERFILES = db.PERFILES.Find(id);
            if (pERFILES == null)
            {
                return HttpNotFound();
            }
            return View(pERFILES);
        }

        // POST: Perfiles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PERFIL,NOMBRE")] PERFILES pERFILES)
        {


            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Details", "Login");
            }


            if (ModelState.IsValid)
            {
                db.Entry(pERFILES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pERFILES);
        }

        // GET: Perfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERFILES pERFILES = db.PERFILES.Find(id);
            if (pERFILES == null)
            {
                return HttpNotFound();
            }
            return View(pERFILES);
        }

        // POST: Perfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PERFILES pERFILES = db.PERFILES.Find(id);
            db.PERFILES.Remove(pERFILES);
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
