using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Models
{
    public class AdvertistmentsFromCategoryViewModels
    {
        public IList <Advertisement> Advertistments {get;set;}
        public string CategoryName { get; set; }
    }
}