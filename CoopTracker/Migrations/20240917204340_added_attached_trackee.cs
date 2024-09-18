using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoopTracker.Migrations
{
    /// <inheritdoc />
    public partial class added_attached_trackee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProffApplys",
                columns: table => new
                {
                    ProffApplyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackeeId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProffApplys", x => x.ProffApplyId);
                    table.ForeignKey(
                        name: "FK_ProffApplys_Trackees_TrackeeId",
                        column: x => x.TrackeeId,
                        principalTable: "Trackees",
                        principalColumn: "TrackeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProffApplys_TrackeeId",
                table: "ProffApplys",
                column: "TrackeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProffApplys");
        }
    }
}
