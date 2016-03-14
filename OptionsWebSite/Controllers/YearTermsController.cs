using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel;

namespace OptionsWebSite.Controllers
{
    public class YearTermsController : Controller
    {
        private DiplomaContext db = new DiplomaContext();

        // GET: YearTerms
        public ActionResult Index()
        {
            return View(db.YearTerms.ToList());
        }

        // GET: YearTerms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }
            return View(yearTerm);
        }

        // GET: YearTerms/Create
        public ActionResult Create()
        {
            ViewBag.Term = new SelectList(new List<SelectListItem>
                                       {
                                         new SelectListItem { Selected = true, Text = "10", Value = "10"},
                                         new SelectListItem { Selected = false, Text = "20", Value = "20"},
                                         new SelectListItem { Selected = false, Text = "30", Value = "30"},
                                       }, "Value", "Text", 1);
            return View();
        }

        // POST: YearTerms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YearTermId,Year,Term,IsDefault")] YearTerm yearTerm)
        {
            yearTerm.Term = Int32.Parse(yearTerm.Term.ToString());
            if (yearTerm.IsDefault == true)
            {
                var query = from a in db.YearTerms
                            where a.IsDefault.Equals(true)
                            select a;
                if (query.Any()) {
                    YearTerm term = query.First();

                    term.IsDefault = false;
                    db.SaveChanges();
                }
            }

            if (ModelState.IsValid)
            {
                db.YearTerms.Add(yearTerm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

             ViewBag.Term = new SelectList(new List<SelectListItem>
                                       {
                                         new SelectListItem { Selected = true, Text = "10", Value = "10"},
                                         new SelectListItem { Selected = false, Text = "20", Value = "20"},
                                         new SelectListItem { Selected = false, Text = "30", Value = "30"},
                                       }, "Value", "Text", 1);

            return View(yearTerm);
        }

        // GET: YearTerms/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Term = new SelectList(new List<SelectListItem>
                                       {
                                         new SelectListItem { Selected = true, Text = "10", Value = "10"},
                                         new SelectListItem { Selected = false, Text = "20", Value = "20"},
                                         new SelectListItem { Selected = false, Text = "30", Value = "30"},
                                       }, "Value", "Text", 1);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }
            return View(yearTerm);
        }

        // POST: YearTerms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YearTermId,Year,Term,IsDefault")] YearTerm yearTerm)
        {
            yearTerm.Term = Int32.Parse(yearTerm.Term.ToString());
            if (yearTerm.IsDefault == true)
            {
                var query = from a in db.YearTerms
                            where a.IsDefault.Equals(true)
                            select a;
                if (query.Any())
                {
                    YearTerm term = query.First();

                    term.IsDefault = false;
                    db.SaveChanges();
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(yearTerm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Term = new SelectList(new List<SelectListItem>
                                       {
                                         new SelectListItem { Selected = true, Text = "10", Value = "10"},
                                         new SelectListItem { Selected = false, Text = "20", Value = "20"},
                                         new SelectListItem { Selected = false, Text = "30", Value = "30"},
                                       }, "Value", "Text", 1);

            return View(yearTerm);
        }

        // GET: YearTerms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }
            return View(yearTerm);
        }

        // POST: YearTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm.IsDefault == true)
            {
                var query = from a in db.YearTerms
                            select a;
                if (query.Any())
                {
                    YearTerm term = query.First();
                    term.IsDefault = true;
                }
            }
            db.YearTerms.Remove(yearTerm);
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
