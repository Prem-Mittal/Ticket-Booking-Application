using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Booking_Application.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingeventtableAddeduserIdfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Events");
        }
    }
}
