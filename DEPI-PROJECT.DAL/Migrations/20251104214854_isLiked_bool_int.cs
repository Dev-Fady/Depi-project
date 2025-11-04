using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_PROJECT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class isLiked_bool_int : Migration
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

            migrationBuilder.CreateTable(
                name: "LikeEntities",
                schema: "interactions",
                columns: table => new
                {
                    LikeEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeEntities", x => x.LikeEntityId);
                    table.ForeignKey(
                        name: "FK_LikeEntities_Comments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "interactions",
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeEntities_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "pros",
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeEntities_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "accounts",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeEntities_CommentId",
                schema: "interactions",
                table: "LikeEntities",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeEntities_PropertyId",
                schema: "interactions",
                table: "LikeEntities",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeEntities_UserID",
                schema: "interactions",
                table: "LikeEntities",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeEntities",
                schema: "interactions");

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
