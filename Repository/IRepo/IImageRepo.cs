using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepo
{
    public interface IImageRepo
    {
        void AddImage(Image image);
        void DeleteImageByBlobName(string blobName);
        List<Image> GetAllImages(string userId);
        void SaveChanges();
    }
}
