﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Booking_Application.Migrations
{
    /// <inheritdoc />
    public partial class Addedavailableticketscolumnineventsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketsAvailable",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketsAvailable",
                table: "Events");
        }
    }
}
