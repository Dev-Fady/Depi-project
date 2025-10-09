namespace AqarakDB.Models
{
    public class SaveAd
    {
        public Guid Id { get; set; }
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
