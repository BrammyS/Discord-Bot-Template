using Bot.Configurations;
using Bot.Discord.Extensions;
using Color_Chan.Discord.Extensions;

namespace Bot;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDiscord(Configuration);
        services.AddControllers();

        services.ConfigureCors();
        services.ConfigureApiVersioning();
        services.ConfigureForProxyServers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // This is needed to validate incoming interaction requests.
        app.UseColorChanDiscord(); // <---

        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}