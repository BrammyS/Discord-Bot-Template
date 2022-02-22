using Microsoft.AspNetCore.HttpOverrides;

namespace Bot.Configurations;

public static class ProxyConfig
{
    public static void ConfigureForProxyServers(this IServiceCollection services)
    {
        services.Configure<ForwardedHeadersOptions>(options => { options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto; });
    }
}