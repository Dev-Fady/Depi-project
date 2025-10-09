namespace AqarakDB.Models
{
    public class VideoAd
    {
        public Guid Id { get; set; }
        public string VideoUrl { get; set; }
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }
    }
}
