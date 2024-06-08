using System;
using System.Threading.Tasks;

namespace Octopus.ApiClient.Services.Interfaces
{
    public interface IApiClientService
    {
        Task<string> GetAsync(string endpoint);
        void SetRateLimitInfo(int remainingCalls, DateTime rateLimitReset);
        int GetRemainingCalls();
        DateTime GetRateLimitReset();
    }
}