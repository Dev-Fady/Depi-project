using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_PROJECT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditColum_UserIdCommentId_in_LikeComment_and_UserIdPropertyId_in_LikeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LikeProperties_UserID_PropertyId",
                schema: "interactions",
                table: "LikeProperties");

            migrationBuilder.DropIndex(
                name: "IX_LikeComments_UserID_CommentId",
                schema: "interactions",
                table: "LikeComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                schema: "interactions",
                table: "LikeProperties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                schema: "interactions",
                table: "LikeProperties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                schema: "interactions",
                table: "LikeComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CommentId",
                schema: "interactions",
                table: "LikeComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikeProperties_UserID_PropertyId",
                schema: "interactions",
                table: "LikeProperties",
                columns: new[] { "UserID", "PropertyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikeComments_UserID_CommentId",
                schema: "interactions",
                table: "LikeComments",
                columns: new[] { "UserID", "CommentId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LikeProperties_UserID_PropertyId",
                schema: "interactions",
                table: "LikeProperties");

            migrationBuilder.DropIndex(
                name: "IX_LikeComments_UserID_CommentId",
                schema: "interactions",
                table: "LikeComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                schema: "interactions",
                table: "LikeProperties",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                schema: "interactions",
                table: "LikeProperties",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                schema: "interactions",
                table: "LikeComments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CommentId",
                schema: "interactions",
                table: "LikeComments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_LikeProperties_UserID_PropertyId",
                schema: "interactions",
                table: "LikeProperties",
                columns: new[] { "UserID", "PropertyId" },
                unique: true,
                filter: "[UserID] IS NOT NULL AND [PropertyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LikeComments_UserID_CommentId",
                schema: "interactions",
                table: "LikeComments",
                columns: new[] { "UserID", "CommentId" },
                unique: true,
                filter: "[UserID] IS NOT NULL AND [CommentId] IS NOT NULL");
        }
    }
}
