using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitCalendar.Migrations
{
    /// <inheritdoc />
    public partial class AddIsHabitCompletedToHabitDaysCompletedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "HabitDaysCompleted",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isHabitCompleted",
                table: "HabitDaysCompleted",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isHabitCompleted",
                table: "HabitDaysCompleted");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "HabitDaysCompleted",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400);
        }
    }
}
