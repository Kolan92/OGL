using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OGL.Models
{
    public class Advertisement
    {
        public Advertisement()
        {
            this.AdvertistmentCategory = new HashSet<AdvertistmentCategory>();
        }

        [Display(Name = "Id: ")]
        public int Id { get; set; }

        [Display(Name ="Treść ogłoszenia")]
        public string AdvertisementText { get; set; }

        [Display(Name = "Tytł ogłoszenia: ")]
        [MaxLength(72)]
        public string AdvertistmentTitle { get; set; }

        [Display(Name = "Data dodania: ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        public string UserID { get; set; }

        public virtual ICollection<AdvertistmentCategory> AdvertistmentCategory { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}