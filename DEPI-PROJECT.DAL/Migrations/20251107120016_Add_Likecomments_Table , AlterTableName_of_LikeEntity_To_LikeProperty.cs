using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_PROJECT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_Likecomments_TableAlterTableName_of_LikeEntity_To_LikeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeEntities",
                schema: "interactions");

            migrationBuilder.CreateTable(
                name: "LikeComments",
                schema: "interactions",
                columns: table => new
                {
                    LikeCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeComments", x => x.LikeCommentId);
                    table.ForeignKey(
                        name: "FK_LikeComments_Comments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "interactions",
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeComments_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "accounts",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "LikeProperties",
                schema: "interactions",
                columns: table => new
                {
                    LikeEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeProperties", x => x.LikeEntityId);
                    table.ForeignKey(
                        name: "FK_LikeProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "pros",
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeProperties_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "accounts",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeComments_CommentId",
                schema: "interactions",
                table: "LikeComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeComments_UserID_CommentId",
                schema: "interactions",
                table: "LikeComments",
                columns: new[] { "UserID", "CommentId" },
                unique: true,
                filter: "[UserID] IS NOT NULL AND [CommentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LikeProperties_PropertyId",
                schema: "interactions",
                table: "LikeProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeProperties_UserID_PropertyId",
                schema: "interactions",
                table: "LikeProperties",
                columns: new[] { "UserID", "PropertyId" },
                unique: true,
                filter: "[UserID] IS NOT NULL AND [PropertyId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeComments",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "LikeProperties",
                schema: "interactions");

            migrationBuilder.CreateTable(
                name: "LikeEntities",
                schema: "interactions",
                columns: table => new
                {
                    LikeEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        principalColumn: "CommentId");
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
                name: "IX_LikeEntities_UserID_PropertyId",
                schema: "interactions",
                table: "LikeEntities",
                columns: new[] { "UserID", "PropertyId" },
                unique: true,
                filter: "[UserID] IS NOT NULL AND [PropertyId] IS NOT NULL");
        }
    }
}
