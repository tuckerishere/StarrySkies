using Microsoft.EntityFrameworkCore.Migrations;

namespace StarrySkies.Data.Migrations
{
    public partial class vocationspells : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VocationSpells",
                columns: table => new
                {
                    VocationId = table.Column<int>(type: "int", nullable: false),
                    SpellId = table.Column<int>(type: "int", nullable: false),
                    LevelLearned = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocationSpells", x => new { x.VocationId, x.SpellId });
                    table.ForeignKey(
                        name: "FK_VocationSpells_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VocationSpells_Vocations_VocationId",
                        column: x => x.VocationId,
                        principalTable: "Vocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VocationSpells_SpellId",
                table: "VocationSpells",
                column: "SpellId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VocationSpells");
        }
    }
}
