using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        Assembly applicationAssembly = typeof(DependencyInjection).Assembly;

        services.AddAutoMapper(cfg => { }, applicationAssembly);

        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));
    }
}
