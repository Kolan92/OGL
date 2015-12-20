using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Services
{
    public class ImageUpload
    {
        public string UploadImageAndReturnImageName(HttpPostedFileBase fileBase)
        {
            byte[] image = fileBase.InputStream.ReadFully();
            if (!ImageOptimization.ValidateImage(image))
                return null;

            List<BlobImage> imagesToUpload = GenerateImagesMiniatures(image);
            try
            {
                UploadMultipleImagesToBlob(imagesToUpload);
            }
            catch
            {
                return null;
            }
            return imagesToUpload.First().ImageName;
        }

        public void DeleteImageByNameWithMiniatures(string imageNameWithExtension)
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient bloClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = bloClient.GetContainerReference("images");
            foreach (var image in GalleryImages.GalleryDimensionsList)
            {
                DeleteImageByName(container, GetFullBlobName(image.SizeName, imageNameWithExtension));
            }
        }

        public static string GetFullBlobName(string sizeName, string imageNameWithExtension)
        {
            return string.Format("{0}/{1}", sizeName, imageNameWithExtension);
        }

        static string CreateBlobName()
        {
            return Guid.NewGuid().ToString();
        }

        List<BlobImage> GenerateImagesMiniatures(byte[] image)
        {
            List<BlobImage> imagesToUpload = new List<BlobImage>();

            string blobName = CreateBlobName();

            foreach (var imageDimension in GalleryImages.GalleryDimensionsList)
            {
                byte[] imageBytes = ImageOptimization.OptimizeImageFromBytes(imageDimension.Width, imageDimension.Height, image);

                BlobImage blobImage = new BlobImage()
                {
                    ImgBytes = imageBytes,
                    SizeName = imageDimension.SizeName,
                    ImageName = string.Format("{0}.{1}", blobName, ImageOptimization.GetImageExtension(imageBytes).ToString())
                };

                imagesToUpload.Add(blobImage);
            }
            return imagesToUpload;
        }

        void UploadMultipleImagesToBlob(List<BlobImage> images)
        {   
            CloudStorageAccount storageAccount = 
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            foreach (var image in images)
	        {
                CloudBlobContainer container =  blobClient.GetContainerReference("images");//??
                if(container.CreateIfNotExists())
                {
                    var permisions = container.GetPermissions();
                    permisions.PublicAccess = BlobContainerPublicAccessType.Container;
                    container.SetPermissions(permisions);
                }
                string blobName = GetFullBlobName(image.SizeName, image.ImageName);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
                blockBlob.Properties.ContentType = "image/png";

                blockBlob.UploadFromByteArray(image.ImgBytes, 0, image.ImgBytes.Length);
	        }
        }

        void DeleteImageByName(CloudBlobContainer container, string blobName)
        {
            CloudBlockBlob blockBlobe = container.GetBlockBlobReference(blobName);
            blockBlobe.DeleteIfExists();
        }

    }
}