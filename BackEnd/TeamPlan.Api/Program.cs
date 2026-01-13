using Microsoft.EntityFrameworkCore;
using TeamPlan.Application.Ioc;
using TeamPlan.Infra.Data.Context;
using TeamPlan.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfra();
builder.Services.AddDbContext<AppDbContext>(x
    => x.UseSqlServer("Server=localhost,1430;Database=TeamPlan;User ID=sa;Password=Kaiky@2048;TrustServerCertificate=true;",
                          x=>x.MigrationsAssembly(typeof(Program).Assembly)));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization(); 

app.MapControllers();

app.Run();