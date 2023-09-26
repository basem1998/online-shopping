using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Graduation_Project.Models;

namespace Graduation_Project.Controllers
{
    public class SizeGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SizeGroups
        public ActionResult Index()
        {
            var sizeGroups = db.SizeGroups.Include(s => s.productype);
            return View(sizeGroups.ToList());
        }

        // GET: SizeGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeGroup sizeGroup = db.SizeGroups.Find(id);
            if (sizeGroup == null)
            {
                return HttpNotFound();
            }
            return View(sizeGroup);
        }

        // GET: SizeGroups/Create
        public ActionResult Create()
        {
            ViewBag.producttypeId = new SelectList(db.producttypes, "Id", "Name");
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "Name");
            return View();
        }

        // POST: SizeGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,producttypeId")] SizeGroup sizeGroup)
        {
            if (ModelState.IsValid)
            {
                db.SizeGroups.Add(sizeGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.producttypeId = new SelectList(db.producttypes, "Id", "Name", sizeGroup.producttypeId);
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "Name", sizeGroup);
            return View(sizeGroup);
        }

        // GET: SizeGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeGroup sizeGroup = db.SizeGroups.Find(id);
            if (sizeGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.producttypeId = new SelectList(db.producttypes, "Id", "Name", sizeGroup.producttypeId);
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "Name", sizeGroup);
            return View(sizeGroup);
        }

        // POST: SizeGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,producttypeId")] SizeGroup sizeGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sizeGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.producttypeId = new SelectList(db.producttypes, "Id", "Name", sizeGroup.producttypeId);
            ViewBag.SizeId = new SelectList(db.Sizes, "Id", "Name", sizeGroup);
            return View(sizeGroup);
        }

        // GET: SizeGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeGroup sizeGroup = db.SizeGroups.Find(id);
            if (sizeGroup == null)
            {
                return HttpNotFound();
            }
            return View(sizeGroup);
        }

        // POST: SizeGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SizeGroup sizeGroup = db.SizeGroups.Find(id);
            db.SizeGroups.Remove(sizeGroup);
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
