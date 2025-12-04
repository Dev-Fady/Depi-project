namespace DEPI_PROJECT.DAL.Models
{
    public class PropertyLikesWithUserView
    {
        public Guid PropertyId { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set;}
    }
}