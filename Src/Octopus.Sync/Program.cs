using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Extensions.Logging;
using Octopus.EF.Data;
using Octopus.Sync.Configurations;
using Octopus.Sync.Workers;

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
