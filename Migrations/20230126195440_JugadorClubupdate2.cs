using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationLabo4.Migrations
{
    public partial class JugadorClubupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JugadorClub",
                table: "JugadorClub");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JugadorClub");

            migrationBuilder.RenameColumn(
                name: "IdJugador",
                table: "JugadorClub",
                newName: "idJugador");

            migrationBuilder.RenameColumn(
                name: "IdClub",
                table: "JugadorClub",
                newName: "idClub");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JugadorClub",
                table: "JugadorClub",
                columns: new[] { "idClub", "idJugador" });

            migrationBuilder.CreateIndex(
                name: "IX_JugadorClub_idJugador",
                table: "JugadorClub",
                column: "idJugador");

            migrationBuilder.AddForeignKey(
                name: "FK_JugadorClub_Clubes_idClub",
                table: "JugadorClub",
                column: "idClub",
                principalTable: "Clubes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JugadorClub_Jugadores_idJugador",
                table: "JugadorClub",
                column: "idJugador",
                principalTable: "Jugadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JugadorClub_Clubes_idClub",
                table: "JugadorClub");

            migrationBuilder.DropForeignKey(
                name: "FK_JugadorClub_Jugadores_idJugador",
                table: "JugadorClub");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JugadorClub",
                table: "JugadorClub");

            migrationBuilder.DropIndex(
                name: "IX_JugadorClub_idJugador",
                table: "JugadorClub");

            migrationBuilder.RenameColumn(
                name: "idJugador",
                table: "JugadorClub",
                newName: "IdJugador");

            migrationBuilder.RenameColumn(
                name: "idClub",
                table: "JugadorClub",
                newName: "IdClub");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JugadorClub",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JugadorClub",
                table: "JugadorClub",
                column: "Id");
        }
    }
}
