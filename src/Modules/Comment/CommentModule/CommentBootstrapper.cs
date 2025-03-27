using CommentModule;
using CommentModule.Context;
using CommentModule.Services;
using CoreModule.Infrastructure.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class CommentBootstrapper
{
    public static IServiceCollection InitCommentModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CommentContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("DefultConnection"));
        });
        services.AddScoped<ICommentService, CommentService>();
        services.AddHostedService<UserRegisteredEventHandler>();
        services.AddHostedService<UserEditedEventHandler>();
        services.AddHostedService<UserChangeAvatarEventHandler>();
        services.AddAutoMapper(typeof(MapperProfile).Assembly);

        return services;
    }
}
