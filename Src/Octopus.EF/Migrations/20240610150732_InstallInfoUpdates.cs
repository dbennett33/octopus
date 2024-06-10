using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octopus.EF.Migrations
{
    /// <inheritdoc />
    public partial class InstallInfoUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnabledEntitiesJson",
                table: "InstallInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnabledEntitiesJson",
                table: "InstallInfo");
        }
    }
}
