using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octopus.EF.Migrations
{
    /// <inheritdoc />
    public partial class TempRemoveVenues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Venues_VenueId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_VenueId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Teams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VenueId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_VenueId",
                table: "Teams",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Venues_VenueId",
                table: "Teams",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
