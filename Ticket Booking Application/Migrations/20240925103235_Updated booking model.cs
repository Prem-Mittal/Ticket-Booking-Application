using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Booking_Application.Migrations
{
    /// <inheritdoc />
    public partial class Updatedbookingmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Bookings");
        }
    }
}
