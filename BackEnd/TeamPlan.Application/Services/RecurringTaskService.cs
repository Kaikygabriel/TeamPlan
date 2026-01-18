using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.Services;

internal class RecurringTaskService : BackgroundService,IRecurringTaskService
{
    private IServiceScopeFactory  _scopeFactory;
    private int _dayCurrent;
    public RecurringTaskService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

        if (_dayCurrent != DateTime.Now.Day)
        {
            var now = DateTime.Now;

            if (now.Hour == 12 && now.Minute == 0)
            {
                Console.Write("Rodando");
                await ActiveRecurringTransaction();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                _dayCurrent = DateTime.Now.Day;
            }
        }
    }
    
    public async Task ActiveRecurringTransaction()
    {
        using var scope = _scopeFactory.CreateScope();

        var unitOfWork = scope.ServiceProvider
            .GetRequiredService<IUnitOfWork>();
        
        var recurringTasks = await unitOfWork
            .RecurringTaskRepository
            .GetAllByDayCurrentWithTeam();

        foreach (var taskRe in recurringTasks)
        {
            var task = taskRe.CreateTask().Value;
            
            unitOfWork.TaskRepository.Create(task);
            
            await unitOfWork.CommitAsync();
        }

       
    }
    
}