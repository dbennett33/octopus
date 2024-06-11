using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octopus.ApiClient.Mappers.Impl;
using Octopus.ApiClient.Models;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Tests
{
    [TestClass]
    public class LeagueMapperTests
    {
        private LeagueMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapper = new LeagueMapper();
        }

        [TestMethod]
        public void Map_ShouldReturnLeague_WhenApiLeagueIsValid()
        {
            // Arrange
            var apiLeague = new ApiLeague
            {
                League = new LeagueData { Id = 1, Name = "League1", Type = "Type1", Logo = "Logo1" },
                Country = new ApiCountry { Name = "Country1", Code = "C1", Flag = "Flag1" },
                Seasons = new List<ApiSeason>
                {
                    new ApiSeason
                    {
                        Year = 2020, Start = "2020-01-01", End = "2020-12-31", Current = true,
                        Coverage = new Models.Coverage
                        {
                            Fixtures = new CoverageFixtures { Events = true, Lineups = true, StatisticsFixtures = true, StatisticsPlayers = true },
                            Standings = true, Players = true, TopScorers = true, TopAssists = true, TopCards = true,
                            Injuries = true, Predictions = true, Odds = true
                        }
                    }
                }
            };

            // Act
            var result = _mapper.Map(apiLeague);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("League1", result.Name);
            Assert.AreEqual("Type1", result.Type);
            Assert.AreEqual("Logo1", result.Logo);
            Assert.IsNotNull(result.Country);
            Assert.AreEqual("Country1", result.Country.Name);
            Assert.AreEqual("C1", result.Country.Code);
            Assert.AreEqual("Flag1", result.Country.Flag);
            Assert.IsNotNull(result.Seasons);
            Assert.AreEqual(1, result.Seasons.Count);
            Assert.AreEqual("2020", result.Seasons.ElementAt(0).Year);
            Assert.AreEqual("2020-01-01", result.Seasons.ElementAt(0).StartDate);
            Assert.AreEqual("2020-12-31", result.Seasons.ElementAt(0).EndDate);
            Assert.IsTrue(result.Seasons.ElementAt(0).Current);
            Assert.IsNotNull(result.Seasons.ElementAt(0).SeasonCoverage);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.Events);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.Lineups);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.FixtureStats);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.PlayerStats);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.Standings);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.Players);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.TopScorers);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.TopAssists);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.TopCards);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.Injuries);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.Predictions);
            Assert.IsTrue(result.Seasons.ElementAt(0).SeasonCoverage.Odds);
        }

        [TestMethod]
        public void Map_ShouldHandleNullPropertiesInApiLeague()
        {
            // Arrange
            var apiLeague = new ApiLeague
            {
                League = new LeagueData { Id = 1, Name = null, Type = null, Logo = null },
                Country = new ApiCountry { Name = null, Code = null, Flag = null },
                Seasons = new List<ApiSeason>
                {
                    new ApiSeason
                    {
                        Year = 2020, Start = null, End = null, Current = false,
                        Coverage = new Models.Coverage
                        {
                            Fixtures = new CoverageFixtures { Events = false, Lineups = false, StatisticsFixtures = false, StatisticsPlayers = false },
                            Standings = false, Players = false, TopScorers = false, TopAssists = false, TopCards = false,
                            Injuries = false, Predictions = false, Odds = false
                        }
                    }
                }
            };

            // Act
            var result = _mapper.Map(apiLeague);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(string.Empty, result.Name);
            Assert.AreEqual(string.Empty, result.Type);
            Assert.AreEqual(string.Empty, result.Logo);
            Assert.IsNotNull(result.Country);
            Assert.AreEqual(string.Empty, result.Country.Name);
            Assert.AreEqual(string.Empty, result.Country.Code);
            Assert.AreEqual(string.Empty, result.Country.Flag);
            Assert.IsNotNull(result.Seasons);
            Assert.AreEqual(1, result.Seasons.Count);
            Assert.AreEqual("2020", result.Seasons.ElementAt(0).Year);
            Assert.AreEqual(null, result.Seasons.ElementAt(0).StartDate);
            Assert.AreEqual(null, result.Seasons.ElementAt(0).EndDate);
            Assert.IsFalse(result.Seasons.ElementAt(0).Current);
            Assert.IsNotNull(result.Seasons.ElementAt(0).SeasonCoverage);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.Events);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.Lineups);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.FixtureStats);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.PlayerStats);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.Standings);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.Players);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.TopScorers);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.TopAssists);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.TopCards);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.Injuries);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.Predictions);
            Assert.IsFalse(result.Seasons.ElementAt(0).SeasonCoverage.Odds);
        }

        [TestMethod]
        public void Map_ShouldHandleNullSeasonsInApiLeague()
        {
            // Arrange
            var apiLeague = new ApiLeague
            {
                League = new LeagueData { Id = 1, Name = "League1", Type = "Type1", Logo = "Logo1" },
                Country = new ApiCountry { Name = "Country1", Code = "C1", Flag = "Flag1" },
                Seasons = new List<ApiSeason>()
            };

            // Act
            var result = _mapper.Map(apiLeague);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("League1", result.Name);
            Assert.AreEqual("Type1", result.Type);
            Assert.AreEqual("Logo1", result.Logo);
            Assert.IsNotNull(result.Country);
            Assert.AreEqual("Country1", result.Country.Name);
            Assert.AreEqual("C1", result.Country.Code);
            Assert.AreEqual("Flag1", result.Country.Flag);
            Assert.IsNotNull(result.Seasons);
            Assert.AreEqual(0, result.Seasons.Count);
        }
    }
}
