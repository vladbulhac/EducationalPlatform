using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace Identity.API
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();
            Log.Logger = CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Starting application...");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Program terminated unexpectedly({ ApplicationContext})!", AppName);
                return 1;
            }
            finally { Log.CloseAndFlush(); }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                            .MinimumLevel.Verbose()
                            .Enrich.WithProperty("ApplicationContext", AppName)
                            .WriteTo.Console()
                            .WriteTo.File(path: @"Logs/logs.json",
                                          formatter: new Serilog.Formatting.Json.JsonFormatter(),
                                          fileSizeLimitBytes: 1_000_000,
                                          rollOnFileSizeLimit: true,
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
}