﻿using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserModule.Core.EventHandlers;
using UserModule.Core.Services;
namespace UserModule.Core;

public static class UserModuleBootstrapper
{
    public static IServiceCollection InitUserModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("DefultConnection"));
        });

        services.AddMediatR(typeof(UserModuleBootstrapper).Assembly);

        services.AddHostedService<NotificationEventHandler>();

        services.AddScoped<IRoleFacade, RoleFacade>();
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<INotificationFacade, NotificationFacade>();

        services.AddAutoMapper(typeof(UserModuleBootstrapper).Assembly);
        services.AddValidatorsFromAssembly(typeof(UserModuleBootstrapper).Assembly);
        return services;
    }
}

