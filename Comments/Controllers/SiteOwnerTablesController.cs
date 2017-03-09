using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Comments.Models;

namespace Comments.Controllers
{
    [Authorize]
    public class SiteOwnerTablesController : Controller
    {
        private CommentsDataEntities db = new CommentsDataEntities();

        // GET: SiteOwnerTables
        public ActionResult Index()
        {
            return View(db.SiteOwnerTables.ToList());
        }

        // GET: SiteOwnerTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteOwnerTable siteOwnerTable = db.SiteOwnerTables.Find(id);
            if (siteOwnerTable == null)
            {
                return HttpNotFound();
            }
            return View(siteOwnerTable);
        }

        // GET: SiteOwnerTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SiteOwnerTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OwnerId,OwnerEmail,Url")] SiteOwnerTable siteOwnerTable)
        {
            if (ModelState.IsValid)
            {
                db.SiteOwnerTables.Add(siteOwnerTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(siteOwnerTable);
        }

        // GET: SiteOwnerTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteOwnerTable siteOwnerTable = db.SiteOwnerTables.Find(id);
            if (siteOwnerTable == null)
            {
                return HttpNotFound();
            }
            return View(siteOwnerTable);
        }

        // POST: SiteOwnerTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OwnerId,OwnerEmail,Url")] SiteOwnerTable siteOwnerTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteOwnerTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(siteOwnerTable);
        }

        // GET: SiteOwnerTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiteOwnerTable siteOwnerTable = db.SiteOwnerTables.Find(id);
            if (siteOwnerTable == null)
            {
                return HttpNotFound();
            }
            return View(siteOwnerTable);
        }

        // POST: SiteOwnerTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiteOwnerTable siteOwnerTable = db.SiteOwnerTables.Find(id);
            db.SiteOwnerTables.Remove(siteOwnerTable);
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
