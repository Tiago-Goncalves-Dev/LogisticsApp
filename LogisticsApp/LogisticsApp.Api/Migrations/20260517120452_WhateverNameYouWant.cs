using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class WhateverNameYouWant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryAddress",
                table: "Orders",
                newName: "HomeDeliveryAddress");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "HomeDeliveryAddress",
                table: "Orders",
                newName: "DeliveryAddress");
        }
    }
}
