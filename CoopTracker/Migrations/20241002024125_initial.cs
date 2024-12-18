﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoopTracker.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentGeorgianCoolegeId = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    ActualSemester = table.Column<string>(type: "text", nullable: false),
                    CoopSemester = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Program = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Trackers",
                columns: table => new
                {
                    TrackerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Submit = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trackers", x => x.TrackerId);
                });

            migrationBuilder.CreateTable(
                name: "Trackees",
                columns: table => new
                {
                    TrackeeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrackerId = table.Column<int>(type: "integer", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    CompanyCity = table.Column<string>(type: "text", nullable: false),
                    JobTitle = table.Column<string>(type: "text", nullable: false),
                    DateAppliation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DocumentProvided = table.Column<string>(type: "text", nullable: false),
                    LastUpdate = table.Column<string>(type: "text", nullable: false),
                    UrlLink = table.Column<string>(type: "text", nullable: true),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trackees", x => x.TrackeeId);
                    table.ForeignKey(
                        name: "FK_Trackees_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trackees_Trackers_TrackerId",
                        column: x => x.TrackerId,
                        principalTable: "Trackers",
                        principalColumn: "TrackerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProffApplys",
                columns: table => new
                {
                    ProffApplyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<byte[]>(type: "bytea", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    TrackeeId = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Trackees_StudentId",
                table: "Trackees",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Trackees_TrackerId",
                table: "Trackees",
                column: "TrackerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProffApplys");

            migrationBuilder.DropTable(
                name: "Trackees");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Trackers");
        }
    }
}
