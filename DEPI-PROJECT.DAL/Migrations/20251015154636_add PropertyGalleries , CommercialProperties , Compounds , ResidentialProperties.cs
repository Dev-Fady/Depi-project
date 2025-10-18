using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_PROJECT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addPropertyGalleriesCommercialPropertiesCompoundsResidentialProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompoundId",
                schema: "pros",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CommercialProperties",
                schema: "pros",
                columns: table => new
                {
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    BusinessType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FloorNumber = table.Column<int>(type: "int", nullable: true),
                    HasStorage = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommercialProperties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_CommercialProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "pros",
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Compounds",
                schema: "pros",
                columns: table => new
                {
                    CompoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compounds", x => x.CompoundId);
                });

            migrationBuilder.CreateTable(
                name: "PropertyGalleries",
                schema: "pros",
                columns: table => new
                {
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyGalleries", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_PropertyGalleries_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "pros",
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidentialProperties",
                schema: "pros",
                columns: table => new
                {
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Bedrooms = table.Column<int>(type: "int", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    Floors = table.Column<int>(type: "int", nullable: true),
                    KitchenType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialProperties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_ResidentialProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "pros",
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties",
                column: "CommercialPropertyPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CompoundId",
                schema: "pros",
                table: "Properties",
                column: "CompoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Compounds_City",
                schema: "pros",
                table: "Compounds",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyGalleries_PropertyId",
                schema: "pros",
                table: "PropertyGalleries",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_CommercialProperties_CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties",
                column: "CommercialPropertyPropertyId",
                principalSchema: "pros",
                principalTable: "CommercialProperties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Compounds_CompoundId",
                schema: "pros",
                table: "Properties",
                column: "CompoundId",
                principalSchema: "pros",
                principalTable: "Compounds",
                principalColumn: "CompoundId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_CommercialProperties_CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Compounds_CompoundId",
                schema: "pros",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "CommercialProperties",
                schema: "pros");

            migrationBuilder.DropTable(
                name: "Compounds",
                schema: "pros");

            migrationBuilder.DropTable(
                name: "PropertyGalleries",
                schema: "pros");

            migrationBuilder.DropTable(
                name: "ResidentialProperties",
                schema: "pros");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CompoundId",
                schema: "pros",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CompoundId",
                schema: "pros",
                table: "Properties");
        }
    }
}
