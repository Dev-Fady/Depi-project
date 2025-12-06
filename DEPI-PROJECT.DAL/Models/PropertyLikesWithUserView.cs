namespace DEPI_PROJECT.DAL.Models
{
    public class PropertyLikesWithUserView
    {
        public Guid PropertyId { get; set; }
        public int? LikesCount { get; set; }  // Nullable to handle NULL from LEFT JOIN
        public bool? IsLiked { get; set; }    // Nullable to handle NULL from LEFT JOIN
    }
}