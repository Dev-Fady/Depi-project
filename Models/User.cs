namespace AqarakDB.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Guid? ParentUserId { get; set; }
        public User ParentUser { get; set; }

        public ICollection<User> SubAccounts { get; set; } = new List<User>();

        public ICollection<FavoriteAd> FavoriteAds { get; set; } = new List<FavoriteAd>();
        public ICollection<SaveAd> SaveAds { get; set; } = new List<SaveAd>();


    }
}
