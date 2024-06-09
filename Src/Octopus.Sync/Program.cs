using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Extensions.Logging;
using Octopus.ApiClient;
using Octopus.ApiClient.Configuration;
using Octopus.ApiClient.Mappers.Impl;
using Octopus.ApiClient.Mappers.Interfaces;
using Octopus.ApiClient.Services.Impl;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Data;
using Octopus.EF.Repositories.Impl;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Importer.Services.Impl;
using Octopus.Importer.Services.Interfaces;
using Octopus.Scheduler.Services.Impl;
using Octopus.Scheduler.Services.Interfaces;
using Octopus.Sync.Services.Impl;
using Octopus.Sync.Services.Interfaces;
using System.Net.Http.Headers;

namespace Octopus.Sync
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
            PrintBanner(logger);

            var builder = Host.CreateApplicationBuilder(args);
            ConfigureServices(builder);

            var host = builder.Build();
            host.Run();
        }

        private static void ConfigureServices(HostApplicationBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            ConfigureLogging(builder);
            ConfigureDatabase(builder, configuration);
            ConfigureApiClient(builder, configuration);
            ConfigureHangfire(builder, configuration);

            RegisterApiServices(builder);
            RegisterRepositoryServices(builder);
            RegisterImporterServices(builder);
            RegisterSyncServices(builder);
            RegisterSchedulerServices(builder);

            builder.Services.AddHostedService<Worker>();    
        }

        private static void ConfigureApiClient(HostApplicationBuilder builder, IConfigurationRoot configuration)
        {
            builder.Services.Configure<ApiConfiguration>(configuration.GetSection("ApiConfiguration"));

            builder.Services.AddHttpClient<IApiClientService, ApiClientService>((serviceProvider, client) =>
            {
                var apiConfig = serviceProvider.GetRequiredService<IOptions<ApiConfiguration>>().Value;
                if (apiConfig == null)
                {
                    throw new System.Exception("ApiConfiguration is not set");
                }
                else
                {
                    client.BaseAddress = new Uri(apiConfig.BaseUrl!);
                    client.DefaultRequestHeaders.Add(ApiGlobal.Headers.NAME_API_KEY, apiConfig.ApiKey);
                    client.DefaultRequestHeaders.Add(ApiGlobal.Headers.NAME_HOST, apiConfig.ApiHost);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
            });
        }

        private static void ConfigureDatabase(HostApplicationBuilder builder, IConfigurationRoot configuration)
        {
            builder.Services.AddDbContext<OctopusDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        private static void ConfigureLogging(HostApplicationBuilder builder)
        {
            builder.Services.AddLogging(configure =>
            {
                configure.ClearProviders();
                configure.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                configure.AddNLog();
            });
        }

        private static void RegisterRepositoryServices(HostApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISystemSettingsRepository, SystemSettingsRepository>();
            builder.Services.AddScoped<IInstallInfoRepository, InstallInfoRepository>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<ILeagueRepository, LeagueRepository>();
            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        private static void RegisterApiServices(HostApplicationBuilder builder)
        {
            builder.Services.AddTransient<IApiCountryService, ApiCountryService>();
            builder.Services.AddTransient<ICountryMapper, CountryMapper>();
            builder.Services.AddTransient<IApiLeagueService, ApiLeagueService>();
            builder.Services.AddTransient<ILeagueMapper, LeagueMapper>();
            builder.Services.AddSingleton<ApiState>();
        }

        private static void RegisterImporterServices(HostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IImportCountryService, ImportCountryService>();
            builder.Services.AddScoped<IImportLeagueService, ImportLeagueService>();
        }

        private static void RegisterSyncServices(HostApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISyncService, SyncService>();
            builder.Services.AddScoped<IInitializerService, InitializerService>();
            builder.Services.AddScoped<IInstallerService, InstallerService>();
            builder.Services.AddScoped<IImportCountryService, ImportCountryService>();
            builder.Services.AddScoped<IImportLeagueService, ImportLeagueService>();
        }

        private static void RegisterSchedulerServices(HostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IScheduleCountryService, ScheduleCountryService>();
        }

        private static void ConfigureHangfire(HostApplicationBuilder builder, IConfigurationRoot configuration)
        {
            builder.Services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            builder.Services.AddHangfireServer();            
        }


        private static void PrintBanner(Logger logger)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.ProductVersion?.Substring(0, fvi.ProductVersion.IndexOf('+')) ?? string.Empty;
            logger.Info(GetBanner(version));
        }

        private static string GetBanner(string version)
        {

            var banner = string.Format("\r\n///////////////////////////////////////\r\n///////////////////////////////////////\r\n   ____  " +
                "     _                        \r\n  / __ \\     | |                       \r\n | |  | | ___| |_ ___  _ __  _   _ ___ \r\n | |  " +
                "| |/ __| __/ _ \\| '_ \\| | | / __|\r\n | |__| | (__| || (_) | |_) | |_| \\__ \\\r\n  \\____/ \\___|\\__\\___/| .__/ \\__,_|___/\r\n " +
                "                     | |              \r\n                      |_|  v{0}       \r\n\r\n///////////////////////////////////////\r\n///////////////////////////////////////", version);
            return banner;
        }
    }
}