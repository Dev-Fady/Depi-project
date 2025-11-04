namespace DEPI_PROJECT.DAL.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string CommentText { get; set; }

        public int  isLiked { get; set; }
        public DateTime DateComment { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
        //public ICollection<LikeEntity> LikeEntities { get; set; } = new List<LikeEntity>(); //new added

    }
}
