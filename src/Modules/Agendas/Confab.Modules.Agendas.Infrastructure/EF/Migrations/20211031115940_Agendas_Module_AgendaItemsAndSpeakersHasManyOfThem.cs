using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Confab.Modules.Agendas.Infrastructure.EF.Migrations
{
    public partial class Agendas_Module_AgendaItemsAndSpeakersHasManyOfThem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_AgendaItems_AgendaItemId",
                schema: "agendas",
                table: "Speakers");

            migrationBuilder.DropIndex(
                name: "IX_Speakers_AgendaItemId",
                schema: "agendas",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "AgendaItemId",
                schema: "agendas",
                table: "Speakers");

            migrationBuilder.CreateTable(
                name: "AgendaItemSpeaker",
                schema: "agendas",
                columns: table => new
                {
                    AgendaItemsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpeakersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaItemSpeaker", x => new { x.AgendaItemsId, x.SpeakersId });
                    table.ForeignKey(
                        name: "FK_AgendaItemSpeaker_AgendaItems_AgendaItemsId",
                        column: x => x.AgendaItemsId,
                        principalSchema: "agendas",
                        principalTable: "AgendaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendaItemSpeaker_Speakers_SpeakersId",
                        column: x => x.SpeakersId,
                        principalSchema: "agendas",
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendaItemSpeaker_SpeakersId",
                schema: "agendas",
                table: "AgendaItemSpeaker",
                column: "SpeakersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaItemSpeaker",
                schema: "agendas");

            migrationBuilder.AddColumn<Guid>(
                name: "AgendaItemId",
                schema: "agendas",
                table: "Speakers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_AgendaItemId",
                schema: "agendas",
                table: "Speakers",
                column: "AgendaItemId");

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
    }
}
