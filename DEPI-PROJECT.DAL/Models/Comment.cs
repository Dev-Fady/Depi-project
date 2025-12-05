namespace DEPI_PROJECT.DAL.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public DateTime DateComment { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
        public ICollection<LikeComment> LikeComments { get; set; } = new List<LikeComment>(); //new added

        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }

    }
}
