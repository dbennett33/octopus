namespace Octopus.ApiClient.Services.Interfaces
{
    public interface IApiClientService
    {
        Task<string> GetAsync(string endpoint);
        int GetRemainingCalls();
        DateTime GetResetTime();
    }
}