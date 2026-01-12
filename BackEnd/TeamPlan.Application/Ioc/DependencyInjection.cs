using Microsoft.Extensions.DependencyInjection;
using TeamPlan.Application.Services;
using TeamPlan.Application.UseCases;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(HandlerBase).Assembly));
        service.AddScoped<IUserServiceAuth, ServiceUserAuth>();
        service.AddScoped<ITokenService,TokenService>();
        return service;
    }
}