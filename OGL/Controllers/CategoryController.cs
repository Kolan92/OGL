using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using Repository.IRepo;
//using Repository.Models;

namespace OGL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _repo;
        
        public CategoryController(ICategoryRepo repo)
        {
            this._repo = repo;
        }

        // GET: /Category/
        public ActionResult Index()
        {
            var categories = _repo.GetCategories().AsNoTracking();
            return View(categories);
        }

        public ActionResult ShowAdvertistment(int id)
        {
            var advertistmetns = _repo.GetAdvertistmensForCategory(id);
            AdvertistmentsFromCategoryViewModels model = new AdvertistmentsFromCategoryViewModels();
            
            model.Advertistments = advertistmetns.ToList();
            model.CategoryName = _repo.GetCategoryNameById(id);
            return View(model);
        }
    }
}
