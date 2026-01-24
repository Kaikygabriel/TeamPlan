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
    => x.UseSqlServer(builder.Configuration["ConnectionString:DefaultConnection"],
                          x=>x.MigrationsAssembly(typeof(Program).Assembly)));
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization(); 

app.MapControllers();

app.Run();