using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Confab.Modules.Agendas.Infrastructure.EF.Migrations
{
    public partial class Agendas_Module_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "agendas");

            migrationBuilder.CreateTable(
                name: "Speakers",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerSubmission",
                schema: "agendas",
                columns: table => new
                {
                    SpeakersId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubmissionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerSubmission", x => new { x.SpeakersId, x.SubmissionsId });
                    table.ForeignKey(
                        name: "FK_SpeakerSubmission_Speakers_SpeakersId",
                        column: x => x.SpeakersId,
                        principalSchema: "agendas",
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpeakerSubmission_Submissions_SubmissionsId",
                        column: x => x.SubmissionsId,
                        principalSchema: "agendas",
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerSubmission_SubmissionsId",
                schema: "agendas",
                table: "SpeakerSubmission",
                column: "SubmissionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeakerSubmission",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "Speakers",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "Submissions",
                schema: "agendas");
        }
    }
}
