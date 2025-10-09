using AqarakDB.Models.Enums;

namespace AqarakDB.Models
{
    public class Offer
    {
        public Guid Id { get; set; }

        public decimal OfferAmount { get; set; }

        public string? Message { get; set; }

        public DateTime OfferDate { get; set; } = DateTime.UtcNow;

        public OfferStatusEnum Status { get; set; } = OfferStatusEnum.Pending;
        public string StatusName => Status.ToString();

        public Guid AdId { get; set; }
        public Ad Ad { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
