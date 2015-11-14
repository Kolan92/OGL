using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class Category
    {
        public Category()
        {

        }

        [Key]
        [Display(Name = "Id kategorii: ")]
        public int Id { get; set; }

        [Display(Name = "Nazwa kategorii")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Treść ogłoszenia")]
        [Required]
        public string Text { get; set; }

        [Display(Name = "Id rodzica: ")]
        [Required]
        public int ParenId { get; set; }

        #region SEO

        [Display(Name = "Tytuł w Google: ")]
        [MaxLength(72)]
        public string MetaTitle { get; set; }

        [Display(Name = " Opis strony w google: ")]
        [MaxLength(160)]
        public string MetaDescription { get; set; }

        [Display(Name = "Słowa kluczowe Google: ")]
        [MaxLength(500)]
        public string MetaKeyWords { get; set; }

        #endregion

        public ICollection<AdvertistmentCategory> AdvertistmentCategory { get; set; }

    }
}