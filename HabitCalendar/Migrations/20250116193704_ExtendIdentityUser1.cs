using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitCalendar.Migrations
{
    /// <inheritdoc />
    public partial class ExtendIdentityUser1:Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "" );

            migrationBuilder.AddColumn<DateOnly>(
                name: "UserStartDate",
                table: "AspNetUsers",
                type: "date",
                nullable: true );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers" );

            migrationBuilder.DropColumn(
                name: "UserStartDate",
                table: "AspNetUsers" );
        }
    }
}
