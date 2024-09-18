using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoopTracker.Migrations
{
    /// <inheritdoc />
    public partial class add_gropukey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupKeyMasterId",
                table: "Trackers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GroupKeyMaster",
                table: "TrackerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GroupKeyMasterId",
                table: "Trackees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupKeyMasterId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupKeyMasterId",
                table: "ProffApplys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GroupKeyMaster",
                columns: table => new
                {
                    GroupKeyMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashGroup = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupKeyMaster", x => x.GroupKeyMasterId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trackers_GroupKeyMasterId",
                table: "Trackers",
                column: "GroupKeyMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Trackees_GroupKeyMasterId",
                table: "Trackees",
                column: "GroupKeyMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupKeyMasterId",
                table: "Students",
                column: "GroupKeyMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProffApplys_GroupKeyMasterId",
                table: "ProffApplys",
                column: "GroupKeyMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProffApplys_GroupKeyMaster_GroupKeyMasterId",
                table: "ProffApplys",
                column: "GroupKeyMasterId",
                principalTable: "GroupKeyMaster",
                principalColumn: "GroupKeyMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_GroupKeyMaster_GroupKeyMasterId",
                table: "Students",
                column: "GroupKeyMasterId",
                principalTable: "GroupKeyMaster",
                principalColumn: "GroupKeyMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trackees_GroupKeyMaster_GroupKeyMasterId",
                table: "Trackees",
                column: "GroupKeyMasterId",
                principalTable: "GroupKeyMaster",
                principalColumn: "GroupKeyMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trackers_GroupKeyMaster_GroupKeyMasterId",
                table: "Trackers",
                column: "GroupKeyMasterId",
                principalTable: "GroupKeyMaster",
                principalColumn: "GroupKeyMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProffApplys_GroupKeyMaster_GroupKeyMasterId",
                table: "ProffApplys");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_GroupKeyMaster_GroupKeyMasterId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Trackees_GroupKeyMaster_GroupKeyMasterId",
                table: "Trackees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trackers_GroupKeyMaster_GroupKeyMasterId",
                table: "Trackers");

            migrationBuilder.DropTable(
                name: "GroupKeyMaster");

            migrationBuilder.DropIndex(
                name: "IX_Trackers_GroupKeyMasterId",
                table: "Trackers");

            migrationBuilder.DropIndex(
                name: "IX_Trackees_GroupKeyMasterId",
                table: "Trackees");

            migrationBuilder.DropIndex(
                name: "IX_Students_GroupKeyMasterId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_ProffApplys_GroupKeyMasterId",
                table: "ProffApplys");

            migrationBuilder.DropColumn(
                name: "GroupKeyMasterId",
                table: "Trackers");

            migrationBuilder.DropColumn(
                name: "GroupKeyMaster",
                table: "TrackerDetails");

            migrationBuilder.DropColumn(
                name: "GroupKeyMasterId",
                table: "Trackees");

            migrationBuilder.DropColumn(
                name: "GroupKeyMasterId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GroupKeyMasterId",
                table: "ProffApplys");
        }
    }
}
