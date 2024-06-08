namespace Octopus.Sync
{
    public class WorkerTest : BackgroundService
    {
        private readonly ILogger<WorkerTest> _logger;

        public WorkerTest(ILogger<WorkerTest> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("FAKE Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}