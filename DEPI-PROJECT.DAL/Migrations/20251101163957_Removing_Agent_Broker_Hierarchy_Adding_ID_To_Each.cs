using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_PROJECT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Removing_Agent_Broker_Hierarchy_Adding_ID_To_Each : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agents_AgentId",
                schema: "pros",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brokers",
                schema: "accounts",
                table: "Brokers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                schema: "accounts",
                table: "Agents");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "accounts",
                table: "Brokers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "accounts",
                table: "Agents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brokers",
                schema: "accounts",
                table: "Brokers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                schema: "accounts",
                table: "Agents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Brokers_UserId",
                schema: "accounts",
                table: "Brokers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agents_UserId",
                schema: "accounts",
                table: "Agents",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agents_AgentId",
                schema: "pros",
                table: "Properties",
                column: "AgentId",
                principalSchema: "accounts",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agents_AgentId",
                schema: "pros",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brokers",
                schema: "accounts",
                table: "Brokers");

            migrationBuilder.DropIndex(
                name: "IX_Brokers_UserId",
                schema: "accounts",
                table: "Brokers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                schema: "accounts",
                table: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Agents_UserId",
                schema: "accounts",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "accounts",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "accounts",
                table: "Agents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brokers",
                schema: "accounts",
                table: "Brokers",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                schema: "accounts",
                table: "Agents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agents_AgentId",
                schema: "pros",
                table: "Properties",
                column: "AgentId",
                principalSchema: "accounts",
                principalTable: "Agents",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
