namespace web.Infrastructure;

using Data;
using Microsoft.EntityFrameworkCore;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder builder)
    {
        using var services = builder.ApplicationServices.CreateScope();

        var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();
        
        dbContext.Database.Migrate();
    }

    public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        => app.UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Castragram API V1");
                options.RoutePrefix = string.Empty;
            });
}