using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkateEventManager.Migrations
{
    /// <inheritdoc />
    public partial class AddSkateDBToDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_Skate_SkateID",
                table: "Rent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skate",
                table: "Skate");

            migrationBuilder.RenameTable(
                name: "Skate",
                newName: "Skates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skates",
                table: "Skates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_Skates_SkateID",
                table: "Rent",
                column: "SkateID",
                principalTable: "Skates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_Skates_SkateID",
                table: "Rent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skates",
                table: "Skates");

            migrationBuilder.RenameTable(
                name: "Skates",
                newName: "Skate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skate",
                table: "Skate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_Skate_SkateID",
                table: "Rent",
                column: "SkateID",
                principalTable: "Skate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
