using Microsoft.Extensions.DependencyInjection;
using TeamPlan.Application.Interfaces.Queries;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Infra.Queries;
using TeamPlan.Infra.Repositories;

namespace TeamPlan.Infra.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITeamQueryService, TeamQueryService>();
        
        return services;
    }
}