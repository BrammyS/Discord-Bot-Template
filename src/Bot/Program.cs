using System.Reflection;
using Bot.Configurations;
using Color_Chan.Discord.Extensions;
using Serilog;

namespace Bot;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        SerilogConfig.Configure();

        try
        {
            Log.Information("Starting Bot web host");

            var host = CreateHostBuilder(args).Build();
            await host.RegisterSlashCommandsAsync(Assembly.Load("Bot.Discord")).ConfigureAwait(false);
            await host.RunAsync().ConfigureAwait(false);

            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                   .UseSerilog()
                   .ConfigureLogging(logging =>
                   {
                       logging.ClearProviders();
                       logging.AddSerilog();
                   })
                   .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}