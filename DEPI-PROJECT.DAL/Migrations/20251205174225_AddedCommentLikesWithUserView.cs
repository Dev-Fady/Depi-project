using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_PROJECT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentLikesWithUserView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW [interactions].[CommentLikesWithUser] AS
                SELECT 
                    CommentId, 
                    COUNT(LikeCommentId) AS LikesCount,
                    CAST(0 AS BIT) AS IsLikedByUser  -- Placeholder column
                FROM interactions.LikeComments
                GROUP BY CommentId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW [interactions].[CommentLikesWithUser]
            ");
        }
    }
}
