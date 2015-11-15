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

namespace OGL.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly IAdvertistmentRepo _repo;
        public AdvertisementsController(IAdvertistmentRepo repo)
        {
            this._repo = repo;
        }

        // GET: Advertisements
        public ActionResult Index()
        {
            //db.Database.Log = message => Trace.WriteLine(message);
            var advertistments = _repo.GetAdvertistments();
            return View(advertistments);
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
            // lol///
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
