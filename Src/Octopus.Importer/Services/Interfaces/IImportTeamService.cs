namespace Octopus.Importer.Services.Interfaces
{
    public interface IImportTeamService
    {
        Task<bool> ImportTeamsByCountryAsync(string countryName);
        Task<bool> ImportTeamsByLeagueAsync(int leagueId, string season);
    }
}