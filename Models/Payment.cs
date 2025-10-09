namespace AqarakDB.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid? SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }

        public string PaymentMethod { get; set; } = "Online";
    }
}
