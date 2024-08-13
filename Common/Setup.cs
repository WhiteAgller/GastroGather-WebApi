using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Common;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}