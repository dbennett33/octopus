using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Extensions.Logging;
using Octopus.EF.Data;
using Octopus.Sync.Configurations;
using Octopus.Sync.Utils;
using Octopus.Sync.Workers;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
        logger.Info(LogUtils.GetBanner());
       

        var builder = Host.CreateApplicationBuilder(args);
        ConfigureServices(builder);

        var host = builder.Build();
        host.Run();
    }

    private static void ConfigureServices(HostApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("enabledEntities.json", optional: false, reloadOnChange: true) // Load enabled entities
            .AddEnvironmentVariables()
            .Build();

        builder.Services.Configure<EnabledEntitiesConfig>(configuration.GetSection("EnabledEntities"));


        ConfigureLogging(builder);
        ConfigureDatabase(builder, configuration);

        builder.Services.ConfigureHangfire(configuration);
        builder.Services.AddApiServices(configuration);
        builder.Services.AddRepositoryServices();
        builder.Services.AddImporterServices();
        builder.Services.AddSyncServices();
        builder.Services.AddSchedulerServices();        

        builder.Services.AddHostedService<Worker>();
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




}
