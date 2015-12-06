using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Models;
using Repository.IRepo;
using System.Data.Entity;

namespace Repository.Repo
{
    public class AdvertistmentsRepo : IAdvertistmentRepo
    {

        private readonly IApplicationDbContext _db;
        public AdvertistmentsRepo(IApplicationDbContext db)
        {
            _db = db;
        }

        public void Create(Advertisement advertistment)
        {
            _db.Advertistments.Add(advertistment);
        }

        public Advertisement GetAdvertistmentById(int Id)
        {
            Advertisement advertistment = _db.Advertistments.Find(Id);
            return advertistment;
        }

        public IQueryable<Advertisement> GetAdvertistments()
        {
            var advertistments = _db.Advertistments.AsNoTracking();
            return advertistments;
        }


        public void RemoveAdvertistment(int advertistmentId)
        {
            removeRelatedAdvertistmentCategory(advertistmentId);
            Advertisement advertistment = _db.Advertistments.Find(advertistmentId);
            _db.Advertistments.Remove(advertistment);

        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void UpdateAdvertistment(Advertisement advertistmet)
        {
            _db.Entry(advertistmet).State = EntityState.Modified;
        }

        private void removeRelatedAdvertistmentCategory(int advertistmentId)
        {
            var advertistmentCategoryList = _db.AdvertistmentCategory
                .Where(x => x.AdvertistmentId == advertistmentId);

            foreach (var advertistmentCategory in advertistmentCategoryList)
            {
                _db.AdvertistmentCategory.Remove(advertistmentCategory);
            }
        }

        public IQueryable<Advertisement> RetrivePage(int? page = 1, int? pageSize = 10)
        {
            var advertistements = _db.Advertistments
                                    .OrderByDescending(a => a.PublishDate)
                                      .Skip((page.Value - 1) * pageSize.Value)
                                        .Take(pageSize.Value);
            return advertistements;
        }
    }
}