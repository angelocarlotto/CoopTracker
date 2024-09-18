using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoopTracker.Migrations
{
    /// <inheritdoc />
    public partial class add_description_tracker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Trackers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Trackers");
        }
    }
}
