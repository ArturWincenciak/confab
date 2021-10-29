using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Confab.Modules.Agendas.Infrastructure.EF.Migrations
{
    public partial class Agendas_Module_FK_v_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgendaSlotId",
                schema: "agendas",
                table: "AgendaItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgendaItems_AgendaSlotId",
                schema: "agendas",
                table: "AgendaItems",
                column: "AgendaSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaItems_AgendaSlots_AgendaSlotId",
                schema: "agendas",
                table: "AgendaItems",
                column: "AgendaSlotId",
                principalSchema: "agendas",
                principalTable: "AgendaSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendaItems_AgendaSlots_AgendaSlotId",
                schema: "agendas",
                table: "AgendaItems");

            migrationBuilder.DropIndex(
                name: "IX_AgendaItems_AgendaSlotId",
                schema: "agendas",
                table: "AgendaItems");

            migrationBuilder.DropColumn(
                name: "AgendaSlotId",
                schema: "agendas",
                table: "AgendaItems");
        }
    }
}
