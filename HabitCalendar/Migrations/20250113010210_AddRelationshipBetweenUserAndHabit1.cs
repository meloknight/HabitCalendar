using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HabitCalendar.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipBetweenUserAndHabit1 : Migration
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "StartDate",
                value: new DateOnly(2025, 1, 13));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "StartDate",
                value: new DateOnly(2025, 1, 13));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Habits",
                columns: new[] { "HabitId", "HabitDescription", "HabitDisplayMethod", "HabitName", "UserId" },
                values: new object[,]
                {
                    { 1, "Brush my teeth for 2 minutes", "icon", "Brush Teeth", 1 },
                    { 2, "Workout for at least half an hour", "string", "Workout", 2 },
                    { 3, "Take the dog out for a half hour walk", "icon", "Walk the dog", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "StartDate",
                value: new DateOnly(2025, 1, 12));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "StartDate",
                value: new DateOnly(2025, 1, 12));
        }
    }
}
