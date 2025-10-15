using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_REALESTATE_DB.Migrations
{
    /// <inheritdoc />
    public partial class addPropertiesandinteractions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pros");

            migrationBuilder.EnsureSchema(
                name: "interactions");

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "pros",
                columns: table => new
                {
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GoogleMapsUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyPurpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Square = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DateListed = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_Agents_AgentId",
                        column: x => x.AgentId,
                        principalSchema: "accounts",
                        principalTable: "Agents",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                schema: "pros",
                columns: table => new
                {
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasElectricityLine = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HasWaterLine = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HasGasLine = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Amenities_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "pros",
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "interactions",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CommentText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    isLiked = table.Column<bool>(type: "bit", nullable: false),
                    DateComment = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "pros",
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "accounts",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                schema: "interactions",
                columns: table => new
                {
                    ListingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.ListingID);
                    table.ForeignKey(
                        name: "FK_Wishlists_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "pros",
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlists_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "accounts",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PropertyId",
                schema: "interactions",
                table: "Comments",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                schema: "interactions",
                table: "Comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId",
                schema: "pros",
                table: "Properties",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_PropertyID",
                schema: "interactions",
                table: "Wishlists",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserID_PropertyID",
                schema: "interactions",
                table: "Wishlists",
                columns: new[] { "UserID", "PropertyID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities",
                schema: "pros");

            migrationBuilder.DropTable(
                name: "Comments",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "Wishlists",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "pros");
        }
    }
}
