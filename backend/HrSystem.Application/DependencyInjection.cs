using HrSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HrSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();

        return services;
    }
}
