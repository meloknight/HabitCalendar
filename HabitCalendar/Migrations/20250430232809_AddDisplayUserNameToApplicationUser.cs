using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitCalendar.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayUserNameToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayUserName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayUserName",
                table: "AspNetUsers");
        }
    }
}
