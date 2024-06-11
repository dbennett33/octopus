using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octopus.EF.Migrations
{
    /// <inheritdoc />
    public partial class MoreVenues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Venues_VenueId",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_VenueId",
                table: "Fixtures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Venues",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Fixtures");

            migrationBuilder.RenameTable(
                name: "Venues",
                newName: "Venue");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Venue",
                table: "Venue",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Venue",
                table: "Venue");

            migrationBuilder.RenameTable(
                name: "Venue",
                newName: "Venues");

            migrationBuilder.AddColumn<int>(
                name: "VenueId",
                table: "Fixtures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Venues",
                table: "Venues",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_VenueId",
                table: "Fixtures",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Venues_VenueId",
                table: "Fixtures",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
