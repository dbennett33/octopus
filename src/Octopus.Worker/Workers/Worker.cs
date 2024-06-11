using Octopus.Sync.Services.Interfaces;

namespace Octopus.Sync.Workers
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Worker> _logger;

        public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var initService = scope.ServiceProvider.GetRequiredService<IInitializerService>();
                await initService.InitializeAsync();
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);    
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
