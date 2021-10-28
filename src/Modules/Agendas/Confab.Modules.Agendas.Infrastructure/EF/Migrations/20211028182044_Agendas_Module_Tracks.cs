using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Confab.Modules.Agendas.Infrastructure.EF.Migrations
{
    public partial class Agendas_Module_Tracks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgendaItemId",
                schema: "agendas",
                table: "Speakers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgendaItems",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgendaTracks",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaTracks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CallForPapers",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConferenceId = table.Column<Guid>(type: "uuid", nullable: true),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsOpened = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallForPapers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgendaSlots",
                schema: "agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TrackId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Placeholder = table.Column<string>(type: "text", nullable: true),
                    ParticipantLimit = table.Column<int>(type: "integer", nullable: true),
                    AgendaItemId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaSlots_AgendaItems_AgendaItemId",
                        column: x => x.AgendaItemId,
                        principalSchema: "agendas",
                        principalTable: "AgendaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgendaSlots_AgendaTracks_TrackId",
                        column: x => x.TrackId,
                        principalSchema: "agendas",
                        principalTable: "AgendaTracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_AgendaItemId",
                schema: "agendas",
                table: "Speakers",
                column: "AgendaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaSlots_AgendaItemId",
                schema: "agendas",
                table: "AgendaSlots",
                column: "AgendaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaSlots_TrackId",
                schema: "agendas",
                table: "AgendaSlots",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_AgendaItems_AgendaItemId",
                schema: "agendas",
                table: "Speakers",
                column: "AgendaItemId",
                principalSchema: "agendas",
                principalTable: "AgendaItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_AgendaItems_AgendaItemId",
                schema: "agendas",
                table: "Speakers");

            migrationBuilder.DropTable(
                name: "AgendaSlots",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "CallForPapers",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "AgendaItems",
                schema: "agendas");

            migrationBuilder.DropTable(
                name: "AgendaTracks",
                schema: "agendas");

            migrationBuilder.DropIndex(
                name: "IX_Speakers_AgendaItemId",
                schema: "agendas",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "AgendaItemId",
                schema: "agendas",
                table: "Speakers");
        }
    }
}
