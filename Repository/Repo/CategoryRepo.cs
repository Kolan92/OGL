using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.IRepo;
using Repository.Models;

namespace Repository.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly IApplicationDbContext _db;

        public CategoryRepo(IApplicationDbContext db)
        {
            this._db = db;
        }

        public IQueryable<Category> GetCategories()
        {
            return _db.Cotegories.AsNoTracking();
        }


        public IQueryable<Advertisement> GetAdvertistmensForCategory(int id)
        {
            var advertistments = from a in _db.Advertistments
                                 join c in _db.AdvertistmentCategory on a.Id equals c.Id
                                 where c.CategoryId == id
                                 select a;
            return advertistments;
        }


        public string GetCategoryNameById(int id)
        {
            return _db.Cotegories.Find(id).Name;
        }
    }
}