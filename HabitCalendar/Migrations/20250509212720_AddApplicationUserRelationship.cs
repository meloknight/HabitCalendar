using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HabitCalendar.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Habits",
                keyColumn: "HabitId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Habits",
                keyColumn: "HabitId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Habits",
                keyColumn: "HabitId",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Habits",
                columns: new[] { "HabitId", "ApplicationUserId", "HabitDescription", "HabitDisplayMethod", "HabitName" },
                values: new object[,]
                {
                    { 1, null, "Brush my teeth for 2 minutes", "icon", "Brush Teeth" },
                    { 2, null, "Workout for at least half an hour", "string", "Workout" },
                    { 3, null, "Take the dog out for a half hour walk", "icon", "Walk the dog" }
                });
        }
    }
}
