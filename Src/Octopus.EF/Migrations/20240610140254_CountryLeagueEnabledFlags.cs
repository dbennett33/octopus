using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octopus.EF.Migrations
{
    /// <inheritdoc />
    public partial class CountryLeagueEnabledFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Leagues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Countries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Countries");
        }
    }
}
