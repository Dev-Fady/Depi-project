using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_PROJECT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCommercialRelationFromProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_CommercialProperties_CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties",
                column: "CommercialPropertyPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_CommercialProperties_CommercialPropertyPropertyId",
                schema: "pros",
                table: "Properties",
                column: "CommercialPropertyPropertyId",
                principalSchema: "pros",
                principalTable: "CommercialProperties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
