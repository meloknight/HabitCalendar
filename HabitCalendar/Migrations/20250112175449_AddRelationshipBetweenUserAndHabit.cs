using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitCalendar.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipBetweenUserAndHabit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Habits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "HabitId",
                keyValue: 1,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "HabitId",
                keyValue: 2,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Habits",
                keyColumn: "HabitId",
                keyValue: 3,
                column: "UserId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Habits_UserId",
                table: "Habits",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habits_Users_UserId",
                table: "Habits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habits_Users_UserId",
                table: "Habits");

            migrationBuilder.DropIndex(
                name: "IX_Habits_UserId",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Habits");
        }
    }
}
