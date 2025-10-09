namespace AqarakDB.Models
{
    public class ImageAd
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }
    }
}
