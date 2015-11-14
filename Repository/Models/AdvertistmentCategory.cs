namespace Repository.Models
{
    public class AdvertistmentCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AdvertistmentId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Advertisement Advertistment { get; set; }
    }
}