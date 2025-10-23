using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateBroker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ADDSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "accounts");

            migrationBuilder.EnsureSchema(
                name: "properties");

            migrationBuilder.EnsureSchema(
                name: "interactions");

            migrationBuilder.CreateTable(
                name: "Compounds",
                schema: "properties",
                columns: table => new
                {
                    CompoundID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compounds", x => x.CompoundID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "accounts",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                schema: "accounts",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Agents_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "accounts",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brokers",
                schema: "accounts",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalID = table.Column<int>(type: "int", nullable: false),
                    LicenseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brokers", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Brokers_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "accounts",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "properties",
                columns: table => new
                {
                    PropertyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoogleMapsURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyPurpose = table.Column<int>(type: "int", nullable: false),
                    PropertyStatus = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Square = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateListed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AgentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompoundID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_Properties_Agents_AgentID",
                        column: x => x.AgentID,
                        principalSchema: "accounts",
                        principalTable: "Agents",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Compounds_CompoundID",
                        column: x => x.CompoundID,
                        principalSchema: "properties",
                        principalTable: "Compounds",
                        principalColumn: "CompoundID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                schema: "properties",
                columns: table => new
                {
                    PropertyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasElectricityLine = table.Column<bool>(type: "bit", nullable: false),
                    HasWaterLine = table.Column<bool>(type: "bit", nullable: false),
                    HasGasLine = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_Amenities_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "properties",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "interactions",
                columns: table => new
                {
                    CommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isLiked = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "properties",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
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
                name: "CommercialProperties",
                schema: "properties",
                columns: table => new
                {
                    PropertyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    HasStorage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommercialProperties", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_CommercialProperties_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "properties",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyGalleries",
                schema: "properties",
                columns: table => new
                {
                    MediaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyGalleries", x => x.MediaID);
                    table.ForeignKey(
                        name: "FK_PropertyGalleries_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "properties",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidentialProperties",
                schema: "properties",
                columns: table => new
                {
                    PropertyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bedrooms = table.Column<int>(type: "int", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    Floors = table.Column<int>(type: "int", nullable: false),
                    KitchenType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialProperties", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_ResidentialProperties_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "properties",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishLists",
                schema: "interactions",
                columns: table => new
                {
                    ListingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishLists", x => x.ListingID);
                    table.ForeignKey(
                        name: "FK_WishLists_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "properties",
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishLists_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "accounts",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PropertyID",
                schema: "interactions",
                table: "Comments",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                schema: "interactions",
                table: "Comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentID",
                schema: "properties",
                table: "Properties",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CompoundID",
                schema: "properties",
                table: "Properties",
                column: "CompoundID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyGalleries_PropertyID",
                schema: "properties",
                table: "PropertyGalleries",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_PropertyID",
                schema: "interactions",
                table: "WishLists",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_UserID",
                schema: "interactions",
                table: "WishLists",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "Brokers",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "Comments",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "CommercialProperties",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "PropertyGalleries",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "ResidentialProperties",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "WishLists",
                schema: "interactions");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "Agents",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "Compounds",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "accounts");
        }
    }
}
