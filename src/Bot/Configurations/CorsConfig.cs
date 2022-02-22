namespace Bot.Configurations;

public static class CorsConfig
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                              builder => builder.AllowAnyOrigin()
                                                .AllowAnyMethod()
                                                .AllowAnyHeader());
        });
    }
}