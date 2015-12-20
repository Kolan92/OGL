using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services
{
    public class BlobImage
    {
        //--- ContainerName/SizeName/ImageName
        //--- zdjecia/small/56.jpg
        public byte[] ImgBytes;
        public string SizeName;
        public string ImageName;
    }
}