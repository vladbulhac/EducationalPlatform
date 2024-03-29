using Microsoft.AspNetCore;
using Serilog;

namespace Notification.API;

public class Program
{
    public static readonly string AppName = typeof(Program).Namespace;

    public static int Main(string[] args)
    {
        var configuration = GetConfiguration();
        Log.Logger = CreateSerilogLogger(configuration);

        try
        {
            Log.Information("Starting application...");
            BuildWebHost(args).Run();
            return 0;
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Program terminated unexpectedly({ApplicationContext})!", AppName);
            return 1;
        }
        finally { Log.CloseAndFlush(); }
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseSerilog()
            .Build();

    private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
    {
        return new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .Enrich.WithProperty("ApplicationContext", AppName)
                        .WriteTo.Console()
                        .WriteTo.File(path: "Logs/logs.json",
                                      formatter: new Serilog.Formatting.Json.JsonFormatter(),
                                      fileSizeLimitBytes: 1_000_000,
                                      rollOnFileSizeLimit: true,
                                      rollingInterval: RollingInterval.Day,
                                      shared: true)
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
    }

    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

        return builder.Build();
    }
}