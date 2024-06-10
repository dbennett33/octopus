using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Octopus.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Height = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Injured = table.Column<bool>(type: "bit", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentVersion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Installed = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surface = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leagues_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstallInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemSettingsId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    InstallStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    EnabledEntitiesApplied = table.Column<bool>(type: "bit", nullable: false),
                    EnabledEntitiesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountriesInstalled = table.Column<bool>(type: "bit", nullable: false),
                    LeaguesInstalled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallInfo_SystemSettings_SystemSettingsId",
                        column: x => x.SystemSettingsId,
                        principalTable: "SystemSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Founded = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    IsNationalTeam = table.Column<bool>(type: "bit", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Current = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FixtureLineups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Formation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixtureLineups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixtureLineups_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FixtureLineups_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    Games_Appearances = table.Column<int>(type: "int", nullable: false),
                    Games_Lineups = table.Column<int>(type: "int", nullable: false),
                    Games_Minutes = table.Column<int>(type: "int", nullable: false),
                    Games_Number = table.Column<int>(type: "int", nullable: true),
                    Games_Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Games_Rating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Games_Captain = table.Column<bool>(type: "bit", nullable: false),
                    Substitutes_In = table.Column<int>(type: "int", nullable: false),
                    Substitutes_Out = table.Column<int>(type: "int", nullable: false),
                    Substitutes_Bench = table.Column<int>(type: "int", nullable: false),
                    Shots_Total = table.Column<int>(type: "int", nullable: false),
                    Shots_On = table.Column<int>(type: "int", nullable: false),
                    Goals_Total = table.Column<int>(type: "int", nullable: false),
                    Goals_Conceded = table.Column<int>(type: "int", nullable: true),
                    Goals_Assists = table.Column<int>(type: "int", nullable: false),
                    Goals_Saves = table.Column<int>(type: "int", nullable: false),
                    Passes_Total = table.Column<int>(type: "int", nullable: false),
                    Passes_Key = table.Column<int>(type: "int", nullable: false),
                    Passes_Accuracy = table.Column<int>(type: "int", nullable: false),
                    Tackles_Total = table.Column<int>(type: "int", nullable: false),
                    Tackles_Blocks = table.Column<int>(type: "int", nullable: false),
                    Tackles_Interceptions = table.Column<int>(type: "int", nullable: false),
                    Duels_Total = table.Column<int>(type: "int", nullable: true),
                    Duels_Won = table.Column<int>(type: "int", nullable: true),
                    Dribbles_Attempts = table.Column<int>(type: "int", nullable: false),
                    Dribbles_Success = table.Column<int>(type: "int", nullable: false),
                    Dribbles_Past = table.Column<int>(type: "int", nullable: true),
                    Fouls_Drawn = table.Column<int>(type: "int", nullable: false),
                    Fouls_Committed = table.Column<int>(type: "int", nullable: false),
                    Cards_Yellow = table.Column<int>(type: "int", nullable: false),
                    Cards_YellowRed = table.Column<int>(type: "int", nullable: false),
                    Cards_Red = table.Column<int>(type: "int", nullable: false),
                    Penalty_Won = table.Column<int>(type: "int", nullable: false),
                    Penalty_Commited = table.Column<int>(type: "int", nullable: true),
                    Penalty_Scored = table.Column<int>(type: "int", nullable: false),
                    Penalty_Missed = table.Column<int>(type: "int", nullable: false),
                    Penalty_Saved = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Coverage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    Events = table.Column<bool>(type: "bit", nullable: false),
                    Lineups = table.Column<bool>(type: "bit", nullable: false),
                    FixtureStats = table.Column<bool>(type: "bit", nullable: false),
                    PlayerStats = table.Column<bool>(type: "bit", nullable: false),
                    Standings = table.Column<bool>(type: "bit", nullable: false),
                    Players = table.Column<bool>(type: "bit", nullable: false),
                    TopScorers = table.Column<bool>(type: "bit", nullable: false),
                    TopAssists = table.Column<bool>(type: "bit", nullable: false),
                    TopCards = table.Column<bool>(type: "bit", nullable: false),
                    Injuries = table.Column<bool>(type: "bit", nullable: false),
                    Predictions = table.Column<bool>(type: "bit", nullable: false),
                    Odds = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coverage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coverage_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fixtures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Referee = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Timezone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VenueId = table.Column<int>(type: "int", nullable: false),
                    StatusLong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StatusShort = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TimeElapsed = table.Column<int>(type: "int", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    Round = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    GoalsHomeTeam = table.Column<int>(type: "int", nullable: false),
                    GoalsAwayTeam = table.Column<int>(type: "int", nullable: false),
                    GoalsHomeTeamHalfTime = table.Column<int>(type: "int", nullable: false),
                    GoalsAwayTeamHalfTime = table.Column<int>(type: "int", nullable: false),
                    GoalsHomeTeamFullTime = table.Column<int>(type: "int", nullable: false),
                    GoalsAwayTeamFullTime = table.Column<int>(type: "int", nullable: false),
                    GoalsHomeTeamExtraTime = table.Column<int>(type: "int", nullable: false),
                    GoalsAwayTeamExtraTime = table.Column<int>(type: "int", nullable: false),
                    GoalsHomeTeamPenalty = table.Column<int>(type: "int", nullable: false),
                    GoalsAwayTeamPenalty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fixtures_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fixtures_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fixtures_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fixtures_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fixtures_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    Biggest_Id = table.Column<int>(type: "int", nullable: false),
                    Biggest_BiggestGoalsScoredHome = table.Column<int>(type: "int", nullable: false),
                    Biggest_BiggestGoalsScoredAway = table.Column<int>(type: "int", nullable: false),
                    Biggest_BiggestGoalsConcededHome = table.Column<int>(type: "int", nullable: false),
                    Biggest_BiggestGoalsConcededAway = table.Column<int>(type: "int", nullable: false),
                    Biggest_BiggestHomeWin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Biggest_BiggestAwayWin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Biggest_BiggestHomeLoss = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Biggest_BiggestAwayLoss = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Biggest_StreakWins = table.Column<int>(type: "int", nullable: false),
                    Biggest_StreakDraws = table.Column<int>(type: "int", nullable: false),
                    Biggest_StreakLosses = table.Column<int>(type: "int", nullable: false),
                    Goals_HomeAverage = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    Goals_AwayAverage = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    Goals_TotalAverage = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    Goals_HomeTotal = table.Column<int>(type: "int", nullable: false),
                    Goals_AwayTotal = table.Column<int>(type: "int", nullable: false),
                    Goals_Total = table.Column<int>(type: "int", nullable: false),
                    CleanSheets_Id = table.Column<int>(type: "int", nullable: false),
                    CleanSheets_Home = table.Column<int>(type: "int", nullable: false),
                    CleanSheets_Away = table.Column<int>(type: "int", nullable: false),
                    CleanSheets_Total = table.Column<int>(type: "int", nullable: false),
                    FailedToScore_Id = table.Column<int>(type: "int", nullable: false),
                    FailedToScore_Home = table.Column<int>(type: "int", nullable: false),
                    FailedToScore_Away = table.Column<int>(type: "int", nullable: false),
                    FailedToScore_Total = table.Column<int>(type: "int", nullable: false),
                    Wins_Id = table.Column<int>(type: "int", nullable: false),
                    Wins_Home = table.Column<int>(type: "int", nullable: false),
                    Wins_Away = table.Column<int>(type: "int", nullable: false),
                    Wins_Total = table.Column<int>(type: "int", nullable: false),
                    Draws_Id = table.Column<int>(type: "int", nullable: false),
                    Draws_Home = table.Column<int>(type: "int", nullable: false),
                    Draws_Away = table.Column<int>(type: "int", nullable: false),
                    Draws_Total = table.Column<int>(type: "int", nullable: false),
                    Losses_Id = table.Column<int>(type: "int", nullable: false),
                    Losses_Home = table.Column<int>(type: "int", nullable: false),
                    Losses_Away = table.Column<int>(type: "int", nullable: false),
                    Losses_Total = table.Column<int>(type: "int", nullable: false),
                    Played_Id = table.Column<int>(type: "int", nullable: false),
                    Played_Home = table.Column<int>(type: "int", nullable: false),
                    Played_Away = table.Column<int>(type: "int", nullable: false),
                    Played_Total = table.Column<int>(type: "int", nullable: false),
                    Form = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Penalties_Id = table.Column<int>(type: "int", nullable: false),
                    Penalties_Scored_Id = table.Column<int>(type: "int", nullable: false),
                    Penalties_Scored_Percentage = table.Column<int>(type: "int", nullable: false),
                    Penalties_Scored_Total = table.Column<int>(type: "int", nullable: false),
                    Penalties_Missed_Id = table.Column<int>(type: "int", nullable: false),
                    Penalties_Missed_Percentage = table.Column<int>(type: "int", nullable: false),
                    Penalties_Missed_Total = table.Column<int>(type: "int", nullable: false),
                    Penalties_Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStats_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamStats_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamStats_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StartXIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FixtureLineupId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartXIs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StartXIs_FixtureLineups_FixtureLineupId",
                        column: x => x.FixtureLineupId,
                        principalTable: "FixtureLineups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StartXIs_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Substitutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FixtureLineupId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substitutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Substitutes_FixtureLineups_FixtureLineupId",
                        column: x => x.FixtureLineupId,
                        principalTable: "FixtureLineups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Substitutes_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FixtureStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FixtureId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    ShotsOnGoal = table.Column<int>(type: "int", nullable: false),
                    ShotsOffGoal = table.Column<int>(type: "int", nullable: false),
                    TotalShots = table.Column<int>(type: "int", nullable: false),
                    BlockedShots = table.Column<int>(type: "int", nullable: false),
                    ShotsInsideBox = table.Column<int>(type: "int", nullable: false),
                    ShotsOutsideBox = table.Column<int>(type: "int", nullable: false),
                    Fouls = table.Column<int>(type: "int", nullable: false),
                    CornerKicks = table.Column<int>(type: "int", nullable: false),
                    Offsides = table.Column<int>(type: "int", nullable: false),
                    BallPossession = table.Column<int>(type: "int", nullable: false),
                    YellowCards = table.Column<int>(type: "int", nullable: false),
                    RedCards = table.Column<int>(type: "int", nullable: false),
                    GoalkeeperSaves = table.Column<int>(type: "int", nullable: false),
                    TotalPasses = table.Column<int>(type: "int", nullable: false),
                    PassesAccurate = table.Column<int>(type: "int", nullable: false),
                    PassesPercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixtureStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FixtureStats_Fixtures_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixtures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FixtureStats_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatsFormations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamStatsId = table.Column<int>(type: "int", nullable: false),
                    Formation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Played = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsFormations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatsFormations_TeamStats_TeamStatsId",
                        column: x => x.TeamStatsId,
                        principalTable: "TeamStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coverage_SeasonId",
                table: "Coverage",
                column: "SeasonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FixtureLineups_CoachId",
                table: "FixtureLineups",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_FixtureLineups_TeamId",
                table: "FixtureLineups",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_AwayTeamId",
                table: "Fixtures",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_HomeTeamId",
                table: "Fixtures",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_LeagueId",
                table: "Fixtures",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_SeasonId",
                table: "Fixtures",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_VenueId",
                table: "Fixtures",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_FixtureStats_FixtureId",
                table: "FixtureStats",
                column: "FixtureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FixtureStats_TeamId",
                table: "FixtureStats",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallInfo_SystemSettingsId",
                table: "InstallInfo",
                column: "SystemSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_CountryId",
                table: "Leagues",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_LeagueId",
                table: "PlayerStatistics",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_TeamId",
                table: "PlayerStatistics",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_LeagueId",
                table: "Seasons",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_StartXIs_FixtureLineupId",
                table: "StartXIs",
                column: "FixtureLineupId");

            migrationBuilder.CreateIndex(
                name: "IX_StartXIs_PlayerId",
                table: "StartXIs",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_StatsFormations_TeamStatsId",
                table: "StatsFormations",
                column: "TeamStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Substitutes_FixtureLineupId",
                table: "Substitutes",
                column: "FixtureLineupId");

            migrationBuilder.CreateIndex(
                name: "IX_Substitutes_PlayerId",
                table: "Substitutes",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_VenueId",
                table: "Teams",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStats_LeagueId",
                table: "TeamStats",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStats_SeasonId",
                table: "TeamStats",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamStats_TeamId",
                table: "TeamStats",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coverage");

            migrationBuilder.DropTable(
                name: "FixtureStats");

            migrationBuilder.DropTable(
                name: "InstallInfo");

            migrationBuilder.DropTable(
                name: "PlayerStatistics");

            migrationBuilder.DropTable(
                name: "StartXIs");

            migrationBuilder.DropTable(
                name: "StatsFormations");

            migrationBuilder.DropTable(
                name: "Substitutes");

            migrationBuilder.DropTable(
                name: "Fixtures");

            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.DropTable(
                name: "TeamStats");

            migrationBuilder.DropTable(
                name: "FixtureLineups");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
