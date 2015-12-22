using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Repository.Models;
using Repository.IRepo;
using Repository.Repo;
using System.Collections.Generic;
using Services; 

namespace OGL.Controllers
{
    public class GalleryController : Controller
    {
        private IImageRepo _repo;
        public GalleryController(IImageRepo repo)
        {
            this._repo = repo;
        }

        public ActionResult GalleryList()
        {
            List<Image> images = _repo.GetAllImages(User.Identity.GetUserId());
            return View(images);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(HttpPostedFileBase fileBase)
        {
            if (fileBase != null && fileBase.ContentLength > 0)
            {
                try 
                {
                    ImageUpload imageUpload = new ImageUpload();
                    string nameWithExtension = imageUpload.UploadImageAndReturnImageName(fileBase);

                    if(string.IsNullOrEmpty(nameWithExtension))
                        return RedirectToAction("GalleryList", "Gallery");

                    try
                    {
                        Image image = new Image()
                        {
                            UserId = User.Identity.GetUserId(),
                            Name = nameWithExtension
                        };

                        _repo.AddImage(image);
                        _repo.SaveChanges();
                    }
                    catch
                    {
                        imageUpload.DeleteImageByNameWithMiniatures(nameWithExtension);
                    }
                }
                catch
                {
                    throw new Exception("Error while uploiding image");
                }
            }
            return RedirectToAction("GalleryList", "Gallery");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DeleteImage(string blobName)
        {
            if (string.IsNullOrEmpty(blobName))
                return false;

            try
            {
                ImageUpload imageUpload = new ImageUpload();
                imageUpload.DeleteImageByNameWithMiniatures(blobName);

                _repo.DeleteImageByBlobName(blobName);
                _repo.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}