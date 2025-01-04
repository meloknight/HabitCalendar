using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HabitCalendar.Migrations
{
    /// <inheritdoc />
    public partial class SeedHabitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Habits",
                columns: new[] { "HabitId", "HabitDescription", "HabitDisplayMethod", "HabitName" },
                values: new object[,]
                {
                    { 1, "Brush my teeth for 2 minutes", "icon", "Brush Teeth" },
                    { 2, "Workout for at least half an hour", "string", "Workout" },
                    { 3, "Take the dog out for a half hour walk", "icon", "Walk the dog" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
