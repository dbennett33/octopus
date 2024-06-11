using Octopus.Sync.Services.Interfaces;

namespace Octopus.Sync.Services.Impl
{
    public class SyncService : ISyncService
    {
        private readonly IInitializerService _initializerService;
        private readonly ILogger<SyncService> _logger;

        public SyncService(IInitializerService initializerService, ILogger<SyncService> logger)
        {
            _initializerService = initializerService;
            _logger = logger;
        }

        public async Task Run()
        {
            

        }

        public async Task Init()
        {
            await _initializerService.InitializeAsync();            
        }
    }
}
