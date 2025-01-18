using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitCalendar.Migrations
{
    /// <inheritdoc />
    public partial class LinkHabitsAndApplicationUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Habits",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "HabitId",
                keyValue: 1,
                column: "ApplicationUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "HabitId",
                keyValue: 2,
                column: "ApplicationUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "HabitId",
                keyValue: 3,
                column: "ApplicationUserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Habits_ApplicationUserId",
                table: "Habits",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habits_AspNetUsers_ApplicationUserId",
                table: "Habits",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habits_AspNetUsers_ApplicationUserId",
                table: "Habits");

            migrationBuilder.DropIndex(
                name: "IX_Habits_ApplicationUserId",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Habits");
        }
    }
}
