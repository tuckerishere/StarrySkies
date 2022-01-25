using Microsoft.EntityFrameworkCore.Migrations;

namespace StarrySkies.Data.Migrations
{
    public partial class addedpkforvocationSpells : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VocationSpells",
                table: "VocationSpells");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "VocationSpells",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VocationSpells",
                table: "VocationSpells",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VocationSpells_VocationId",
                table: "VocationSpells",
                column: "VocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VocationSpells",
                table: "VocationSpells");

            migrationBuilder.DropIndex(
                name: "IX_VocationSpells_VocationId",
                table: "VocationSpells");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VocationSpells");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VocationSpells",
                table: "VocationSpells",
                columns: new[] { "VocationId", "SpellId" });
        }
    }
}
