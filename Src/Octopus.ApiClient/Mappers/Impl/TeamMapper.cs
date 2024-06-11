using Octopus.ApiClient.Models;
using Octopus.ApiClient.Mappers.Interfaces;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Mappers.Impl
{
    public class TeamMapper : ITeamMapper
    {
        public Team Map(ApiTeam apiTeam)
        {
            if (apiTeam == null || apiTeam.Team == null || apiTeam.Venue == null)
            {
                throw new ArgumentException("Invalid ApiTeam object");
            }

            return new Team
            {
                Id = apiTeam.Team.Id,
                Name = apiTeam.Team.Name ?? string.Empty,
                Code = apiTeam.Team.Code ?? string.Empty,
                Country = apiTeam.Team.Country ?? string.Empty,
                Founded = apiTeam.Team.Founded.ToString() ?? string.Empty,
                IsNationalTeam = apiTeam.Team.National,
                Logo = apiTeam.Team.Logo ?? string.Empty,
                VenueId = apiTeam.Venue.Id,
                Venue = new Venue
                {
                    Id = apiTeam.Venue.Id,
                    Name = apiTeam.Venue.Name ?? string.Empty,
                    Address = apiTeam.Venue.Address ?? string.Empty,
                    City = apiTeam.Venue.City ?? string.Empty,
                    Capacity = Convert.ToInt32(apiTeam.Venue.Capacity),
                    Surface = apiTeam.Venue.Surface ?? string.Empty,
                    Image = apiTeam.Venue.Image ?? string.Empty
                },
                TeamStats = new List<TeamStats>() // Assuming you will populate this separately
            };
        }
    }
}
