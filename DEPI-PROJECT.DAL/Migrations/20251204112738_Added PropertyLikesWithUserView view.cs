using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_PROJECT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertyLikesWithUserViewview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW [interactions].[PropertyLikesWithUser] AS
                SELECT 
                    PropertyId, 
                    COUNT(LikeEntityId) AS LikesCount,
                    CAST(0 AS BIT) AS IsLikedByUser  -- Placeholder column
                FROM interactions.LikeProperties
                GROUP BY PropertyId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW [interactions].[PropertyLikesWithUser]
            ");
        }
    }
}
