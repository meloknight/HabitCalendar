using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HabitCalendar.Migrations
{
    /// <inheritdoc />
    public partial class AddHabitTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    HabitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HabitName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HabitDescription = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    HabitDisplayMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.HabitId);
                });

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
            migrationBuilder.DropTable(
                name: "Habits");
        }
    }
}
