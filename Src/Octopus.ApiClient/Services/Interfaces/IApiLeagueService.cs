using System.Collections.Generic;
using System.Threading.Tasks;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Services.Interfaces
{
    public interface IApiLeagueService
    {
        Task<IEnumerable<League>> GetLeaguesAsync();
        Task<League> GetLeagueByIdAsync(int leagueId);
    }
}