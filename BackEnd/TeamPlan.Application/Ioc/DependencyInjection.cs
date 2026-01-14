using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TeamPlan.Application.Services;
using TeamPlan.Application.UseCases;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddHostedService<RecurringTaskService>();
        service.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(HandlerBase).Assembly));
        service.AddScoped<IUserServiceAuth, ServiceUserAuth>();
        service.AddScoped<ITokenService,TokenService>();

        service.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });
            
        service.AddAuthorization(x =>
        {
            x.AddPolicy(Roles.Member, x => x.RequireRole(Roles.Member));
            x.AddPolicy(Roles.Manager, x => x.RequireRole(Roles.Manager));
            x.AddPolicy(Roles.Owner, x => x.RequireRole(Roles.Owner));
        });
        
        return service;
    }
}