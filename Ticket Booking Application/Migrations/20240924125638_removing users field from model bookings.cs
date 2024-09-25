using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket_Booking_Application.Migrations
{
    /// <inheritdoc />
    public partial class removingusersfieldfrommodelbookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UsersId",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "UsersId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UsersId",
                table: "Bookings",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UsersId",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "UsersId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UsersId",
                table: "Bookings",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
