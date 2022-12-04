using Bot.persistence.MongoDb.LoggingSink;
using Serilog;

namespace Bot.Configurations;

public static class SerilogConfig
{
    /// <summary>
    ///     Configures misc settings for serilog.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration" /> containing the application secrets and configurations.</param>
    public static void Configure(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
                     #if DEBUG
                     .MinimumLevel.Debug()
                     #else
                         .MinimumLevel.Information()
                     #endif
                     .Enrich.WithThreadId()
                     .Enrich.WithProcessId()
                     .Enrich.FromLogContext()
                     .Enrich.WithMachineName()
                     .Enrich.WithAssemblyName()
                     .Enrich.WithAssemblyVersion()
                     .Enrich.WithAssemblyInformationalVersion()
                     .WriteTo.Async(writeTo =>
                     {
                         writeTo.Console();
                         writeTo.MongoDb(configuration);
                     }).CreateLogger();
    }

    /// <summary>
    ///     Enriches the HTTP request log with additional data via the Diagnostic Context
    /// </summary>
    /// <param name="diagnosticContext">The Serilog diagnostic context</param>
    /// <param name="httpContext">The current HTTP Context</param>
    public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
    }

    /// <summary>
    ///     Sets up serilog to be used with <see cref="Microsoft.Extensions.Logging.ILogger" />.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors</param>
    public static void AddSerilog(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
    }
}