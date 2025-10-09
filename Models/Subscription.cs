using AqarakDB.Models.Enums;

namespace AqarakDB.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public SubscriptionsType Type { get; set; }
        public string Name => Type.ToString();

        public DateTime StartDate = DateTime.UtcNow;
        public DateTime? EndDate;

        public int? MaxAds { get; set; }
        public bool IsUnlimitedAds { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
