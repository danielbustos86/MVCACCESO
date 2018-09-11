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
    public class VehiculosController : Controller
    {
        private CaboFroward2018Entities db = new CaboFroward2018Entities();

        // GET: Vehiculos
        public ActionResult Index()
        {

            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var vEHICULOS = db.VEHICULOS.Include(v => v.TIPOS_VEHICULOS);
            return View(vEHICULOS.ToList());
        }

        // GET: Vehiculos/Details/5
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
            VEHICULOS vEHICULOS = db.VEHICULOS.Find(id);
            if (vEHICULOS == null)
            {
                return HttpNotFound();
            }
            return View(vEHICULOS);
        }

        // GET: Vehiculos/Create
        public ActionResult Create()
        {
            ViewBag.ID_TIPO_VEHICULO = new SelectList(db.TIPOS_VEHICULOS, "ID_TIPO_VEHICULO", "NOMBRE");
            return View();
        }

        // POST: Vehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_VEHICULO,ID_TIPO_VEHICULO,PATENTE,INACTIVO")] VEHICULOS vEHICULOS)
        {
            if (ModelState.IsValid)
            {
                if (vEHICULOS.ID_TIPO_VEHICULO == 0)
                {
                    ViewBag.Error = "Debe Seleccionar un tipo de vehiculo";

                }
                else if (vEHICULOS.PATENTE==null) {

                    ViewBag.Error = "Debe ingresar una patente";

                }
                else {

                    vEHICULOS.PATENTE = vEHICULOS.PATENTE.ToUpper();
                    db.VEHICULOS.Add(vEHICULOS);
                    db.SaveChanges();
                    return RedirectToAction("Index");


                }

            }

            ViewBag.ID_TIPO_VEHICULO = new SelectList(db.TIPOS_VEHICULOS, "ID_TIPO_VEHICULO", "NOMBRE", vEHICULOS.ID_TIPO_VEHICULO);
            return View(vEHICULOS);
        }

        // GET: Vehiculos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICULOS vEHICULOS = db.VEHICULOS.Find(id);
            if (vEHICULOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_TIPO_VEHICULO = new SelectList(db.TIPOS_VEHICULOS, "ID_TIPO_VEHICULO", "NOMBRE", vEHICULOS.ID_TIPO_VEHICULO);
            return View(vEHICULOS);
        }

        // POST: Vehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_VEHICULO,ID_TIPO_VEHICULO,PATENTE,INACTIVO")] VEHICULOS vEHICULOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vEHICULOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_TIPO_VEHICULO = new SelectList(db.TIPOS_VEHICULOS, "ID_TIPO_VEHICULO", "NOMBRE", vEHICULOS.ID_TIPO_VEHICULO);
            return View(vEHICULOS);
        }

        // GET: Vehiculos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VEHICULOS vEHICULOS = db.VEHICULOS.Find(id);
            if (vEHICULOS == null)
            {
                return HttpNotFound();
            }
            return View(vEHICULOS);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VEHICULOS vEHICULOS = db.VEHICULOS.Find(id);
            db.VEHICULOS.Remove(vEHICULOS);
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
                var RegFiltrado = (from f in db.VEHICULOS
                                   where f.PATENTE.StartsWith(inpBuscar) ||
                                    f.PATENTE.Contains(inpBuscar)
                                   select f);

                return View(RegFiltrado.ToList());
            }
            else
            {
                return View(db.VEHICULOS.OrderBy(f =>
                f.PATENTE).Take(20).ToList());
            }

        }
    }
}
