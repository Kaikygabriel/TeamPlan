using MediatR;
using TeamPlan.Application.DTOs.RecurringTask;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Application.UseCases.RecurringTask.Command.Request;

public record AddRecurringTaskRequest(Guid TeamId, CreateRecurringTaskDto RecurringTaskDto) : IRequest<Result>
{
    public Result<Domain.BackOffice.Entities.RecurringTask> ToEntity()
        => Domain.BackOffice.Entities.RecurringTask.Factories.Create
        (
            RecurringTaskDto.Title,
            RecurringTaskDto.Description,
            TeamId,
            RecurringTaskDto.DayMonth,
            RecurringTaskDto.DaysActiveTask,
            RecurringTaskDto.Priority
        );
}