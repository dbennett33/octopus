using Hangfire;
using Octopus.ApiClient;
using Octopus.ApiClient.Configurations;
using Octopus.ApiClient.Mappers.Impl;
using Octopus.ApiClient.Mappers.Interfaces;
using Octopus.ApiClient.Services.Impl;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Repositories.Impl;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Importer.Services.Impl;
using Octopus.Importer.Services.Interfaces;
using Octopus.Scheduler.Services.Impl;
using Octopus.Scheduler.Services.Interfaces;
using Octopus.Sync.Services.Impl;
using Octopus.Sync.Services.Interfaces;
using System.Net.Http.Headers;
using Hangfire.SqlServer;
using Microsoft.Extensions.Options;
using Octopus.Scheduler.Tasks.Interfaces;
using Octopus.Scheduler.Tasks.Impl;

namespace Octopus.Sync.Configurations
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiConfiguration>(configuration.GetSection("ApiConfiguration"));

            services.AddHttpClient<IApiClientService, ApiClientService>((serviceProvider, client) =>
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

            services.AddTransient<IApiCountryService, ApiCountryService>();
            services.AddTransient<IApiLeagueService, ApiLeagueService>();
            services.AddTransient<IApiTeamService, ApiTeamService>();

            services.AddTransient<ICountryMapper, CountryMapper>();
            services.AddTransient<ILeagueMapper, LeagueMapper>();
            services.AddTransient<ITeamMapper, TeamMapper>();
            services.AddSingleton<ApiState>();

            return services;
        }

        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<ISystemSettingsRepository, SystemSettingsRepository>();
            services.AddScoped<IInstallInfoRepository, InstallInfoRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }

        public static IServiceCollection AddImporterServices(this IServiceCollection services)
        {
            services.AddScoped<IImportCountryService, ImportCountryService>();
            services.AddScoped<IImportLeagueService, ImportLeagueService>();
            services.AddScoped<IImportTeamService, ImportTeamService>();

            return services;
        }

        public static IServiceCollection AddSyncServices(this IServiceCollection services)
        {
            services.AddScoped<IInitializerService, InitializerService>();
            services.AddScoped<IImportCountryService, ImportCountryService>();
            services.AddScoped<IImportLeagueService, ImportLeagueService>();

            return services;
        }

        public static IServiceCollection AddSchedulerServices(this IServiceCollection services)
        {
            services.AddScoped<IScheduleManagerService, ScheduleManagerService>();

            services.AddScoped<IScheduleSystemInstall, ScheduleSystemInstall>();
            services.AddScoped<IScheduleCountryService, ScheduleCountryService>();
            services.AddScoped<IScheduleLeagueService, ScheduleLeagueService>();

            services.AddTransient<ITasksSystemInstall, TasksSystemInstall>();
            services.AddTransient<ITasksCountry, TasksCountry>();
            services.AddTransient<ITasksLeague, TasksLeague>();


            return services;
        }

        public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {  
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                    PrepareSchemaIfNecessary = true
                }));

            services.AddHangfireServer();


            return services;
        }
    }
}
