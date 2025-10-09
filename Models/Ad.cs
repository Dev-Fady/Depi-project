using AqarakDB.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AqarakDB.Models
{
    public class Ad
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public string Location { get; set; }

        public AdsTypeEnum Type { get; set; }
        public string TypeName => Type.ToString();

        public AdStatusEnum Status { get; set; } = AdStatusEnum.Active;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid? SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }

        public Guid? AcceptedOfferId { get; set; }
        [NotMapped]
        public Offer? AcceptedOffer { get; set; }

        public ICollection<Offer> Offers { get; set; } = new List<Offer>();

        public ICollection<ImageAd> ImageAds { get; set; } = new List<ImageAd>();

        public VideoAd? VideoAd { get; set; }

        public ICollection<FavoriteAd> FavoriteAds { get; set; } = new List<FavoriteAd>();

        public ICollection<SaveAd> SaveAds { get; set; } = new List<SaveAd>();

        public ICollection<PropertyContractPhoto> PropertyContractPhotos { get; set; } = new List<PropertyContractPhoto>();

    }
}
