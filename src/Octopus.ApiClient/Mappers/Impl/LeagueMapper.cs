using Octopus.ApiClient.Mappers.Interfaces;
using Octopus.ApiClient.Models;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Mappers.Impl
{
    public class LeagueMapper : ILeagueMapper
    {
        public League Map(ApiLeague apiLeague)
        {
            var league = new League
            {
                Id = apiLeague.League?.Id ?? 0,
                Name = apiLeague.League?.Name ?? string.Empty,
                Type = apiLeague.League?.Type ?? string.Empty,
                Logo = apiLeague.League?.Logo ?? string.Empty,
                Country = new Country
                {
                    Name = apiLeague.Country?.Name ?? string.Empty,
                    Code = apiLeague.Country?.Code ?? string.Empty,
                    Flag = apiLeague.Country?.Flag ?? string.Empty
                },
                Seasons = new List<Season>()
            };

            foreach (var apiSeason in apiLeague.Seasons!)
            {
                var coverage = new EF.Data.Entities.Coverage
                {
                    Events = apiSeason.Coverage?.Fixtures?.Events ?? false,
                    Lineups = apiSeason.Coverage?.Fixtures?.Lineups ?? false,
                    FixtureStats = apiSeason.Coverage?.Fixtures?.StatisticsFixtures ?? false,
                    PlayerStats = apiSeason.Coverage?.Fixtures?.StatisticsPlayers ?? false,
                    Standings = apiSeason.Coverage?.Standings ?? false,
                    Players = apiSeason.Coverage?.Players ?? false,
                    TopScorers = apiSeason.Coverage?.TopScorers ?? false,
                    TopAssists = apiSeason.Coverage?.TopAssists ?? false,
                    TopCards = apiSeason.Coverage?.TopCards ?? false,
                    Injuries = apiSeason.Coverage?.Injuries ?? false,
                    Predictions = apiSeason.Coverage?.Predictions ?? false,
                    Odds = apiSeason.Coverage?.Odds ?? false
                };

                var season = new Season
                {
                    SeasonCoverage = coverage,
                    Year = apiSeason.Year.ToString(),
                    StartDate = apiSeason.Start,
                    EndDate = apiSeason.End,
                    Current = apiSeason.Current,
                    League = league,
                    LeagueId = league.Id
                };

                season.SeasonCoverage.Season = season;

                league.Seasons.Add(season);
            }    

            return league;
        }
    }
}