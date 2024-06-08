using Octopus.ApiClient.Models;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Mappers.Interfaces
{
    public interface ILeagueMapper
    {
        League Map(ApiLeague apiLeague);
    }
}