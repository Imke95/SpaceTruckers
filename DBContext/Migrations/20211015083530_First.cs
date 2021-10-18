using Microsoft.EntityFrameworkCore.Migrations;

namespace DBContext.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    PlanetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.PlanetId);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    ConnectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectedToPlanetId = table.Column<int>(nullable: true),
                    OwnerPlanetId = table.Column<int>(nullable: true),
                    ConnectedWeight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_Connections_Planets_ConnectedToPlanetId",
                        column: x => x.ConnectedToPlanetId,
                        principalTable: "Planets",
                        principalColumn: "PlanetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connections_Planets_OwnerPlanetId",
                        column: x => x.OwnerPlanetId,
                        principalTable: "Planets",
                        principalColumn: "PlanetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_ConnectedToPlanetId",
                table: "Connections",
                column: "ConnectedToPlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_OwnerPlanetId",
                table: "Connections",
                column: "OwnerPlanetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "Planets");
        }
    }
}
