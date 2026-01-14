using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.Services;

internal class RecurringTaskService : BackgroundService,IRecurringTaskService
{
    private IServiceScopeFactory  _scopeFactory;

    public RecurringTaskService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            var now = DateTime.Now;

            if (now.Hour == 12 && now.Minute == 0)
            {
                await ActiveRecurringTransaction();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
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
            taskRe.Team.Tasks.Add(task);
            unitOfWork.TeamRepository.Update(taskRe.Team);
        }

        await unitOfWork.CommitAsync();
    }
    
}