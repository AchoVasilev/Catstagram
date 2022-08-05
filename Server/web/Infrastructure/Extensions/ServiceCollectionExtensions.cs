namespace web.Infrastructure.Extensions;

using System.Text;
using Controllers.Follows;
using Features.Profiles;
using Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using Data;
using Data.Models;
using Features.Cats;
using Features.Identity;
using Features.Search;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        ApplicationSettings appSettings)
    {
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        => services
            .AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(connectionString));

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddTransient<IIdentityService, IdentityService>()
            .AddTransient<IProfileService, ProfileService>()
            .AddTransient<ICatService, CatService>()
            .AddTransient<ISearchService, SearchService>()
            .AddTransient<IFollowService, FollowService>();

    public static IServiceCollection AddSwagger(this IServiceCollection services)
        => services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo() {Title = "Catstagram API", Version = "v1"});
        });

    public static void AddApiControllers(this IServiceCollection services)
        => services.AddControllers(options => options.Filters.Add<ModelOrNotFoundActionFilter>());
}