using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_PROJECT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIsLikedtointdataTypeinCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "isLiked",
                schema: "interactions",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isLiked",
                schema: "interactions",
                table: "Comments",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
