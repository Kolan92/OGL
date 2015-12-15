using Repository.IRepo;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Repo
{
    public class ImageRepo : IImageRepo
    {
        private IApplicationDbContext _db;

        public ImageRepo(IApplicationDbContext db)
        {
            this._db = db;
        }
        public void AddImage(Image image)
        {
            _db.Images.Add(image);
        }

        public void DeleteImageByBlobName(string blobName)
        {
            var image = _db.Images.Where(x => x.Name == blobName).FirstOrDefault();
            _db.Images.Remove(image);
        }

        public List<Image> GetAllImages(string userId)
        {
            return _db.Images.Where(x => x.UserId == userId).ToList();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}