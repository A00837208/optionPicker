using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel;
using System.Diagnostics;

namespace OptionsWebSite.Controllers
{
    public class ChoicesController : Controller
    {
        private DiplomaContext db = new DiplomaContext();

        public ActionResult AlreadyChoose()
        {
            return View();
        }


        // GET: Choices
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm);
            return View(choices.ToList());
        }

        // GET: Choices/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // GET: Choices/Create
        [Authorize(Roles = "Student,Admin")]
        public ActionResult Create()
        {
            var flag = db.Choices.Where(u => u.StudentId == User.Identity.Name);
           
            if (flag.Any())
            {
                return RedirectToAction("AlreadyChoose");
            }

            ViewBag.FirstChoiceOptionId = new SelectList(db.Options.Where(o => o.IsActive == true), "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options.Where(o => o.IsActive == true), "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options.Where(o => o.IsActive == true), "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options.Where(o => o.IsActive == true), "OptionId", "Title");
            ViewBag.YearTermId = new SelectList(db.YearTerms.Where(a => a.IsDefault == true), "YearTermId", "YearTermId");
            
            //ViewBag.OptionList = new SelectList(
            //    db.Options
            //    .Where(o => o.IsActive == true)
            //    .OrderBy(o => o.Title),
            //    "OptionId", "Title"
            //    );

            var query = from a in db.YearTerms
                        where a.IsDefault.Equals(true)
                        select a;
            var term = query.FirstOrDefault();
            ViewBag.YearTermCurrent = "Default";
            if (term.Term == 10)
            {
                ViewBag.YearTermCurrent = "Winter " + term.Year;
            }
            else if (term.Term == 20)
            {
                ViewBag.YearTermCurrent = "Spring/Summer " + term.Year;
            }
            if (term.Term == 30)
            {
                ViewBag.YearTermCurrent = "Fall " + term.Year;
            }
            //ViewBag.YearTermId = term.YearTermId;
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student,Admin")]
        public ActionResult Create([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            var query = from a in db.YearTerms
                        where a.IsDefault.Equals(true)
                        select a;
            var term = query.FirstOrDefault();
            choice.YearTermId = term.YearTermId;
            if (ModelState.IsValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);

            if (term.Term == 10)
            {
                ViewBag.YearTermCurrent = "Winter";
            }
            else if (term.Term == 20)
            {
                ViewBag.YearTermCurrent = "Spring/Summer";
            }
            if (term.Term == 30)
            {
                ViewBag.YearTermCurrent = "Fall";
            }
            //ViewBag.YearTermId = term.YearTermId;
            return View(choice);
        }

        // GET: Choices/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            var query = from a in db.YearTerms
                        where a.IsDefault.Equals(true)
                        select a;
            var term = query.FirstOrDefault();
            ViewBag.YearTermCurrent = "Default";
            if (term.Term == 10)
            {
                ViewBag.YearTermCurrent = "Winter " + term.Year;
            }
            else if (term.Term == 20)
            {
                ViewBag.YearTermCurrent = "Spring/Summer " + term.Year;
            }
            if (term.Term == 30)
            {
                ViewBag.YearTermCurrent = "Fall " + term.Year;
            }
            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            var query = from a in db.YearTerms
                        where a.IsDefault.Equals(true)
                        select a;
            var term = query.FirstOrDefault();
            choice.YearTermId = term.YearTermId;

            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            return View(choice);
        }

        // GET: Choices/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
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
