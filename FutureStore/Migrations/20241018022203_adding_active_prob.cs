using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureStore.Migrations
{
    /// <inheritdoc />
    public partial class adding_active_prob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTimeStamp",
                table: "Categories",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedTimeStamp",
                table: "Categories");
        }
    }
}
