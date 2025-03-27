using BannerModule.Context;
using BannerModule.Repositories;
using BannerModule.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BannerModule;

public static class BannerBootstrapper
{
    public static IServiceCollection InitBannerModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<BannerContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("DefultConnection"));
        });
        services.AddScoped<IBannerRepository, BannerRepository>();
        services.AddScoped<IBannerService, BannerService>();
        services.AddAutoMapper(typeof(MapperProfile).Assembly);

        return services;
    }
}