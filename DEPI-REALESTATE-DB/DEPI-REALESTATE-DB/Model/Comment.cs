namespace DEPI_REALESTATE_DB.Model
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string CommentText { get; set; }

        public bool isLiked { get; set; }
        public DateTime DateComment { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
