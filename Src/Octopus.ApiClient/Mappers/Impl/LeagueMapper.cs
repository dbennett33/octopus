using System.Collections.Generic;
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
                Id = apiLeague.League.Id,
                Name = apiLeague.League.Name,
                Type = apiLeague.League.Type,
                Logo = apiLeague.League.Logo,
                Country = new Country
                {
                    Name = apiLeague.Country.Name,
                    Code = apiLeague.Country.Code,
                    Flag = apiLeague.Country.Flag
                },
                Seasons = new List<Season>()
            };

            foreach (var apiSeason in apiLeague.Seasons)
            {
                var coverage = new EF.Data.Entities.Coverage
                {                    
                    Events = apiSeason.Coverage.Fixtures.Events,
                    Lineups = apiSeason.Coverage.Fixtures.Lineups,
                    FixtureStats = apiSeason.Coverage.Fixtures.StatisticsFixtures,
                    PlayerStats = apiSeason.Coverage.Fixtures.StatisticsPlayers,
                    Standings = apiSeason.Coverage.Standings,
                    Players = apiSeason.Coverage.Players,
                    TopScorers = apiSeason.Coverage.TopScorers,
                    TopAssists = apiSeason.Coverage.TopAssists,
                    TopCards = apiSeason.Coverage.TopCards,
                    Injuries = apiSeason.Coverage.Injuries,
                    Predictions = apiSeason.Coverage.Predictions,
                    Odds = apiSeason.Coverage.Odds
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