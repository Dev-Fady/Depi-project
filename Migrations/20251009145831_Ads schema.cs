using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AqarakDB.Migrations
{
    /// <inheritdoc />
    public partial class Adsschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ads");

            migrationBuilder.EnsureSchema(
                name: "offers");

            migrationBuilder.CreateTable(
                name: "Ads",
                schema: "ads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcceptedOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ads_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "billing",
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ads_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteAds",
                schema: "ads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteAds_Ads_AdId",
                        column: x => x.AdId,
                        principalSchema: "ads",
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteAds_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageAds",
                schema: "ads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageAds_Ads_AdId",
                        column: x => x.AdId,
                        principalSchema: "ads",
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    OfferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Ads_AdId",
                        column: x => x.AdId,
                        principalSchema: "ads",
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Ads_AdId1",
                        column: x => x.AdId1,
                        principalSchema: "ads",
                        principalTable: "Ads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyContractPhotos",
                schema: "offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyContractPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyContractPhotos_Ads_AdId",
                        column: x => x.AdId,
                        principalSchema: "ads",
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyContractPhotos_Users_ReviewedBy",
                        column: x => x.ReviewedBy,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SaveAds",
                schema: "ads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaveAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaveAds_Ads_AdId",
                        column: x => x.AdId,
                        principalSchema: "ads",
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaveAds_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoAds",
                schema: "ads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoAds_Ads_AdId",
                        column: x => x.AdId,
                        principalSchema: "ads",
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_SubscriptionId",
                schema: "ads",
                table: "Ads",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_UserId",
                schema: "ads",
                table: "Ads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAds_AdId_UserId",
                schema: "ads",
                table: "FavoriteAds",
                columns: new[] { "AdId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAds_UserId",
                schema: "ads",
                table: "FavoriteAds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageAds_AdId",
                schema: "ads",
                table: "ImageAds",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AdId",
                schema: "offers",
                table: "Offers",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AdId1",
                schema: "offers",
                table: "Offers",
                column: "AdId1");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserId",
                schema: "offers",
                table: "Offers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyContractPhotos_AdId",
                schema: "offers",
                table: "PropertyContractPhotos",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyContractPhotos_ReviewedBy",
                schema: "offers",
                table: "PropertyContractPhotos",
                column: "ReviewedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SaveAds_AdId_UserId",
                schema: "ads",
                table: "SaveAds",
                columns: new[] { "AdId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaveAds_UserId",
                schema: "ads",
                table: "SaveAds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoAds_AdId",
                schema: "ads",
                table: "VideoAds",
                column: "AdId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteAds",
                schema: "ads");

            migrationBuilder.DropTable(
                name: "ImageAds",
                schema: "ads");

            migrationBuilder.DropTable(
                name: "Offers",
                schema: "offers");

            migrationBuilder.DropTable(
                name: "PropertyContractPhotos",
                schema: "offers");

            migrationBuilder.DropTable(
                name: "SaveAds",
                schema: "ads");

            migrationBuilder.DropTable(
                name: "VideoAds",
                schema: "ads");

            migrationBuilder.DropTable(
                name: "Ads",
                schema: "ads");
        }
    }
}
