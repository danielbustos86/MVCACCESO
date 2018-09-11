using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaboFrowardMVC.Models;

namespace CaboFrowardMVC.Controllers
{
    public class PersonasController : Controller
    {
        private CaboFroward2018Entities db = new CaboFroward2018Entities();

        // GET: Personas
        public ActionResult Index()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var pERSONAS = db.PERSONAS.Include(p => p.NACIONALIDADES);



            return View(pERSONAS.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONAS pERSONAS = db.PERSONAS.Find(id);
            if (pERSONAS == null)
            {
                return HttpNotFound();
            }
            return View(pERSONAS);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {

            ViewBag.ID_NACIONALIDAD = new SelectList(db.NACIONALIDADES, "ID_NACIONALIDAD", "NACIONALIDAD",0).OrderBy(x => x.Text);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PERSONA,ID_NACIONALIDAD,RUT,DV,NOMBRE,APELLIDOMATERNO,APELLIDOPATERNO,INACTIVO,PASAPORTE,FECHAINDUCCION")] PERSONAS pERSONAS)
        {
            if (ModelState.IsValid)
            {


                String rutaux;
                rutaux = pERSONAS.RUT.ToString() + "-" + pERSONAS.DV;


                if (DAL.PersonasDAL.GetPersona(pERSONAS.RUT)!=null)
                {
                    ViewBag.Error = "Ya existe una persona con el rut";

                }
                else {

                    if (!BOL.Helpers.Util.RutValido(rutaux))
                    {


                        ViewBag.Error = "Rut con Formato Incorrecto";

                    }
                    else
                    {
                        try
                        {
                            pERSONAS.NOMBRE = pERSONAS.NOMBRE.ToUpper();
                            pERSONAS.APELLIDOPATERNO = pERSONAS.APELLIDOPATERNO.ToUpper();
                            pERSONAS.APELLIDOMATERNO = pERSONAS.APELLIDOMATERNO.ToUpper();


                            db.PERSONAS.Add(pERSONAS);
                            db.SaveChanges();
                            return RedirectToAction("Index");

                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = ex.Message.ToString();

                        }
                        //catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
                        //{
                        //    ViewBag.Error = ex.InnerException;
                        //}
                        //catch (System.Data.Entity.Core.EntityCommandCompilationException ex)
                        //{
                        //    ViewBag.Error = ex.InnerException;
                        //}
                        //catch (System.Data.Entity.Core.UpdateException ex)
                        //{
                        //    ViewBag.Error = ex.InnerException;
                        //}

                        //catch (System.Data.Entity.Infrastructure.DbUpdateException ex) //DbContext
                        //{
                        //    ViewBag.Error = ex.InnerException;
                        //}




                    }
                }


            }

            ViewBag.ID_NACIONALIDAD = new SelectList(db.NACIONALIDADES, "ID_NACIONALIDAD", "NACIONALIDAD", pERSONAS.ID_NACIONALIDAD);
            return View(pERSONAS);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONAS pERSONAS = db.PERSONAS.Find(id);
            if (pERSONAS == null)
            {
                return HttpNotFound();
            }

		

			ViewBag.ID_NACIONALIDAD = new SelectList(db.NACIONALIDADES, "ID_NACIONALIDAD", "NACIONALIDAD", pERSONAS.ID_NACIONALIDAD);
            return View(pERSONAS);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PERSONA,ID_NACIONALIDAD,RUT,DV,NOMBRE,APELLIDOMATERNO,APELLIDOPATERNO,INACTIVO,PASAPORTE,FECHAINDUCCION")] PERSONAS pERSONAS)
        {
            if (ModelState.IsValid)
            {
				String rutaux;
				rutaux = pERSONAS.RUT.ToString() + "-" + pERSONAS.DV;
			

				if (!BOL.Helpers.Util.RutValido(rutaux) && rutaux != "")
				{



					ViewBag.Error = "Rut con Formato Incorrecto";

				}
				else if (pERSONAS.PASAPORTE == "" && rutaux == "")
				{

					ViewBag.Error = "Es obligacion Registrar Pasaporte o Rut";
				}
				else{ 

				db.Entry(pERSONAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
				}
			}
            ViewBag.ID_NACIONALIDAD = new SelectList(db.NACIONALIDADES, "ID_NACIONALIDAD", "NACIONALIDAD", pERSONAS.ID_NACIONALIDAD);
            return View(pERSONAS);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONAS pERSONAS = db.PERSONAS.Find(id);
            if (pERSONAS == null)
            {
                return HttpNotFound();
            }
            return View(pERSONAS);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PERSONAS pERSONAS = db.PERSONAS.Find(id);
            db.PERSONAS.Remove(pERSONAS);
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
                var RegFiltrado = (from f in db.PERSONAS
                                   where f.NOMBRE.StartsWith(inpBuscar) ||
                                    f.APELLIDOPATERNO.Contains(inpBuscar)
                                   select f);

                return View(RegFiltrado.ToList());
            }
            else
            {
                return View(db.PERSONAS.OrderBy(f =>
                f.APELLIDOPATERNO).Take(20).ToList());
            }

        }

    }
}
