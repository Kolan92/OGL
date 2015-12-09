using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using System.Diagnostics;
using Repository.IRepo;
using Microsoft.AspNet.Identity;
using PagedList;

namespace OGL.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertistmentRepo _repo;
        public AdvertisementController(IAdvertistmentRepo repo)
        {
            this._repo = repo;
        }

        // GET: Advertisements
        public ActionResult Index(int? page, string sortOrder)
        {
            int currentPage = page ?? 1;
            int advertistmentsPerPage = 3;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortId = string.IsNullOrEmpty(sortOrder) ? "IdAsc" : string.Empty;
            ViewBag.Date = sortOrder == "DateAsc" ? "Date" : "DateAsc";
            ViewBag.TextSort = sortOrder == "TextAsc" ? "Text" : "TextAsc";
            ViewBag.TitleSort = sortOrder == "TitleAsc" ? "Title" : "TitleAsc";
            var advertistments = GetAdvertistmentsBySortType(sortOrder);

            var model = new PagedList<Advertisement>(advertistments, currentPage, advertistmentsPerPage);

            return View(model);
        }

        private IEnumerable<Advertisement>GetAdvertistmentsBySortType(string sortOrder)
        {
            var advertistments = _repo.GetAdvertistments();
            switch (sortOrder)
            {
                case"Date":
                    return advertistments.OrderBy(x => x.PublishDate);
                case "DateAsc":
                    return advertistments.OrderByDescending(x => x.PublishDate);
                case "Text":
                    return advertistments.OrderBy(x => x.AdvertisementText);
                case "TextAsc":
                    return advertistments.OrderByDescending(x => x.AdvertisementText);
                case "Title":
                    return advertistments.OrderBy(x => x.AdvertistmentTitle);
                case "TitleAsc":
                    return advertistments.OrderByDescending(x => x.AdvertistmentTitle);
                case "IdAsc":
                    return advertistments.OrderBy(x => x.Id);
                default:
                    return advertistments.OrderByDescending(x => x.Id);
            }
        }



        //// GET: Advertisements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = _repo.GetAdvertistmentById((int)id);//db.Advertistments.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // GET: Advertisements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdvertisementText,AdvertistmentTitle")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                advertisement.UserID = User.Identity.GetUserId();
                advertisement.PublishDate = DateTime.Now;

                try
                {
                    _repo.Create(advertisement);
                    _repo.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View(advertisement);

                }
            }
     
           return View(advertisement);
        }

        // GET: Advertisements/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = _repo.GetAdvertistmentById((int)id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            else if (advertisement.UserID != User.Identity.GetUserId() &&
                !(User.IsInRole("Admin")||User.IsInRole("Worker")))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(advertisement);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdvertisementText,AdvertistmentTitle,PublishDate,UserID")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.UpdateAdvertistment(advertisement);
                    _repo.SaveChanges();
                    
                }
                catch (Exception ex)
                {
                    ViewBag.error = true;
                    return View(advertisement);
                }
                
            }
            ViewBag.error = false;
            return View(advertisement);
        }

        //// GET: Advertisements/Delete/5
        public ActionResult Delete(int? id, bool? error)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = _repo.GetAdvertistmentById((int)id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            else if (advertisement.UserID != User.Identity.GetUserId() &&
                        !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(error!=null)
            {
                ViewBag.error = true;
            }
           
            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.RemoveAdvertistment(id);
            try
            {
                _repo.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("Delete", new { id = id, error = true });
            }
            return RedirectToAction("Index");
    
        }
        //GET: /Advertistments
        public ActionResult Partial()
        {
            var advertistments = _repo.GetAdvertistments();
            return PartialView("Index", advertistments);
            // lol///jdghj
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
