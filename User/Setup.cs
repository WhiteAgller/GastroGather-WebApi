﻿using System.Reflection;
using Common.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace User;

public static class Setup
{
    public static IServiceCollection InstallUserDependencies(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(Setup).Assembly);
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(Setup).Assembly);

            options.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            options.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            options.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            options.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}