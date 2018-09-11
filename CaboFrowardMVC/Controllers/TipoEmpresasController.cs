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
    public class TipoEmpresasController : Controller
    {
        private CaboFroward2018Entities db = new CaboFroward2018Entities();

        // GET: TipoEmpresas
        public ActionResult Index()
        {

            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.TIPOS_EMPRESAS.ToList());
        }

        // GET: TipoEmpresas/Details/5
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
            TIPOS_EMPRESAS tIPOS_EMPRESAS = db.TIPOS_EMPRESAS.Find(id);
            if (tIPOS_EMPRESAS == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_EMPRESAS);
        }

        // GET: TipoEmpresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoEmpresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TIPO_EMPRESA,NOMBRE")] TIPOS_EMPRESAS tIPOS_EMPRESAS)
        {
            if (ModelState.IsValid)
            {
                db.TIPOS_EMPRESAS.Add(tIPOS_EMPRESAS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPOS_EMPRESAS);
        }

        // GET: TipoEmpresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_EMPRESAS tIPOS_EMPRESAS = db.TIPOS_EMPRESAS.Find(id);
            if (tIPOS_EMPRESAS == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_EMPRESAS);
        }

        // POST: TipoEmpresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TIPO_EMPRESA,NOMBRE")] TIPOS_EMPRESAS tIPOS_EMPRESAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOS_EMPRESAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPOS_EMPRESAS);
        }

        // GET: TipoEmpresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOS_EMPRESAS tIPOS_EMPRESAS = db.TIPOS_EMPRESAS.Find(id);
            if (tIPOS_EMPRESAS == null)
            {
                return HttpNotFound();
            }
            return View(tIPOS_EMPRESAS);
        }

        // POST: TipoEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIPOS_EMPRESAS tIPOS_EMPRESAS = db.TIPOS_EMPRESAS.Find(id);
            db.TIPOS_EMPRESAS.Remove(tIPOS_EMPRESAS);
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
